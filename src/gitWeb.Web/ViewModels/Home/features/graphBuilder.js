import * as graphHelper from './graphHelpers.js';
import * as d3 from 'd3';
function Node(x,y){
  return new {x:x,y:y};
}

export function build(data) {
    data = data.slice(0, 120);

    var i = 0;
    data.forEach(function(c) {
        c.id = i++;
        c.column = 0;
        c.x = 0;
        c.y = 40 + c.id * 22
    });

    function setCommitPosition() {
        var first = data[0];
        first.column = 1;
        first.x = first.column * 20
        createMainPath(first);
        assignParentColumn(first);
    }

    function createMainPath(element) {
        if (element.parents === undefined || element.parents.Length < 0) return;

        var firstparentsha = element.parents[0];

        var firstParentArray = data.find(x => x.sha === firstparentsha);

        if (firstParentArray === undefined) return;

        var firstParent = firstParentArray;
        firstParent.column = element.column;
        firstParent.x = element.column * 20
        createMainPath(firstParent);
    }

    var columns = [{
        i: 2,
        free: true
    }];

    function getFreeColumn() {
        var freeColumn = columns.find(x => x.free === true);

        if (freeColumn !== undefined) {
            return freeColumn;
        } else {
            var maxOfIndex = columns.reduce(function(prev, curr) {
                return prev.Cost < curr.Cost ? prev : curr;
            });
            var newColumn = {
                i: 0,
                free: true
            };
            newColumn.i = maxOfIndex.i + 1;
            columns.push(newColumn);

            return newColumn;
        }
    }

    function assignParentColumn(child) {

        var parents = data.filter(function(d) {
            return child.parents.indexOf(d.sha) >= 0
        });

        if (parents.length <= 0) return;

        var parentsArray = [];
        for (i = 0; i < child.parents.length; i++) {
            var parentsha = child.parents[i];
            var currParent = parents.find(x => x.sha == parentsha);

            if (currParent === undefined) {
                var fakeParent = {
                    id: -1
                }
                parentsArray.push(fakeParent);
                continue;
            };

            parentsArray.push(currParent);
        }

        for (i = 0; i < parentsArray.length; i++) {

            var currParent = parentsArray[i];

            if (currParent.id == -1) continue;

            if (currParent.column !== 0) continue;

            if (i == 0) {
                currParent.column = child.column;
            } else {

                var allChildrenOfCurrentParrent = data.filter(function(d) {
                    return d.parents.indexOf(currParent.sha) >= 0
                });

                var otherParent = allChildrenOfCurrentParrent.find(p => p.sha != child.sha);

                if (otherParent !== undefined) {
                    continue;
                } else {
                    var newColumn = getFreeColumn();
                    currParent.column = newColumn.i;
                    newColumn.free = false;
                }
            }

            currParent.x = currParent.column * 20
        }

        parentsArray.forEach(function(p) {
            if (p.id > 0) {
                assignParentColumn(p)
            }
        });
    }

    setCommitPosition();

    var links = [];
    var nodes = [];

    for (i = 0; i < data.length; i++) {

        var curr = data[i];

        nodes.push({
            sha: curr.sha,
            x: curr.x,
            y: curr.y,
            Orig: true,
            message: curr.message,
            reachableBranches: curr.reachableBranches
        });

        if (curr.parents === undefined) continue;
        for (var j = 0; j < curr.parents.length; j++) {
            var currPar = curr.parents[j]

            if (currPar === undefined) continue;

            var exPath = data.find((s) => s.sha == currPar);


            if (exPath === undefined) continue;

            if (exPath.column === curr.column) {
                links.push({
                    source: curr.sha,
                    target: currPar,
                    nodeColumn: exPath.column
                });
            } else {
                var intermidsha = curr.sha + exPath.sha;
                nodes.push({
                    sha: intermidsha,
                    x: exPath.x,
                    y: curr.y + 10
                })
                links.push({
                    source: curr.sha,
                    target: intermidsha,
                    nodeColumn: exPath.column
                })
                links.push({
                    source: intermidsha,
                    target: currPar,
                    nodeColumn: exPath.column
                })
            }
        }
    }

    nodes.unshift({  sha: 'uncommited changes',message: 'uncommited changes',
      x: 20,
      y: 18,Orig:true,openCommit:true});

    return {
        nodes,
        links,
        data
    };
}

export function draw(nodes, links) {

    var svg = d3.select("svg");
    svg.selectAll("*").remove();
    var groups = svg.selectAll("g")
        .data(nodes.filter((d) => d.Orig === true))
        .enter()
        .append("g")
        .attr("id",(d) => {return d.sha})
        .classed("commit-group",true)
        .attr("cursor", "pointer");

    var rect = groups
        .append("rect")
        .attr("x", 0)
        .attr("y", (d) => {return d.y - 10})
        .classed("commit-row", true);

    var commit = groups
        .append("circle")
        .attr("r", 5)
        .attr("cx", (d) => {return d.x})
        .attr("cy", (d) => {return d.y})
        .attr("class", (d) => {
          if(d.openCommit){
            return "commit-dot openCommit";
          }

          return "commit-dot";
        });

    var labels = groups
        .append("text")
        .text((d) =>{return d.message})
        .attr("x", (d) => {return 140})
        .attr("y", (d) => {return d.y + 5})
        .attr("class", (d) => {
          if(d.openCommit){
            return "commit-label openCommit";
          }

          return "commit-label";
        });

        var labels = groups
            .append("text")
            .text((d) =>{
              if(d.reachableBranches != undefined && d.reachableBranches.length > 0){
                return "BRANCH        " + d.reachableBranches
              }
              })
            .attr("x", (d) => {return 400})
            .attr("y", (d) => {return d.y + 5})
            .attr("class", (d) => {
              if(d.openCommit){
                return "commit-label openCommit";
              }
              return "commit-label";
            });

    var links = svg.selectAll("link")
        .data(links)
        .enter()
        .append("line")
        .attr("x1", function(l) {
            var sourceNode = nodes.filter(function(d, i) {
                return d.sha == l.target;
            })[0];
            d3.select(this).attr("y1", sourceNode.y);
            return sourceNode.x
        })
        .attr("x2", function(l) {
            var targetNode = nodes.filter(function(d, i) {
                return d.sha == l.source;
            })[0];
            d3.select(this).attr("y2", targetNode.y);
            return targetNode.x
        })
        .attr("stroke", function(d) {
            return graphHelper.pickupColor(d.nodeColumn);
        })
        .classed("commit-link", true);

    var selected;

    groups.on("mouseover", function(d) {
        var el = d3.select(this);
        el.select("text").classed("hovered", true);
        el.select("circle").classed("hovered", true);
    }).on("mouseout", function(d) {
        var el = d3.select(this);
        el.select("text").classed("hovered", false);
        el.select("circle").classed("hovered", false);
    });
}
var selected;

export function onCommitClick(cb) {
    var groups = d3.select("svg").selectAll("g")
    groups.on("click", function(d)  {

      if(!selected){
       selected = this;
       d3.select(selected).classed("selected",true);
      }
      else{
        d3.select(selected).classed("selected",false);
        selected = this;
        d3.select(selected).classed("selected",true);
      }

      cb(d)
    });
}
