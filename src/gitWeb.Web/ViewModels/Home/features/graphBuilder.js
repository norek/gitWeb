import * as graphHelper from './graphHelpers.js';

function Node(x,y){
  return new {x:x,y:y};
}

export function build(graphDataService) {
    var data = graphDataService.getTree(null);

    data = data.slice(0, 20);

    var i = 0;
    data.forEach(function(c) {
        c.id = i++;
        c.column = 0;
        c.x = 0;
        c.y = 40 + c.id * 22
    });

    function setCommitPosition() {
        console.log('start');
        var first = data[0];
        first.column = 1;
        first.x = first.column * 20
        createMainPath(first);
        assignParentColumn(first);
    }

    function createMainPath(element) {
        if (element.Parents === undefined || element.Parents.Length < 0) return;

        var firstParentSha = element.Parents[0];

        var firstParentArray = data.find(x => x.Sha === firstParentSha);

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
            return child.Parents.indexOf(d.Sha) >= 0
        });

        if (parents.length <= 0) return;

        var parentsArray = [];
        for (i = 0; i < child.Parents.length; i++) {
            var parentSha = child.Parents[i];
            var currParent = parents.find(x => x.Sha == parentSha);

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
                    return d.Parents.indexOf(currParent.Sha) >= 0
                });

                var otherParent = allChildrenOfCurrentParrent.find(p => p.Sha != child.Sha);

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
            Sha: curr.Sha,
            x: curr.x,
            y: curr.y,
            Orig: true
        });

        if (curr.Parents === undefined) continue;
        for (var j = 0; j < curr.Parents.length; j++) {
            var currPar = curr.Parents[j]

            if (currPar === undefined) continue;

            var exPath = data.find((s) => s.Sha == currPar);


            if (exPath === undefined) continue;

            if (exPath.column === curr.column) {
                links.push({
                    source: curr.Sha,
                    target: currPar,
                    nodeColumn: exPath.column
                });
            } else {
                var intermidSha = curr.Sha + exPath.Sha;
                nodes.push({
                    Sha: intermidSha,
                    x: exPath.x,
                    y: curr.y + 10
                })
                links.push({
                    source: curr.Sha,
                    target: intermidSha,
                    nodeColumn: exPath.column
                })
                links.push({
                    source: intermidSha,
                    target: currPar,
                    nodeColumn: exPath.column
                })
            }
        }
    }

    nodes.unshift({  Sha: 'uncommited changes',
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

    var groups = svg.selectAll("g")
        .data(nodes.filter((d) => d.Orig === true))
        .enter()
        .append("g")
        .attr("id",(d) => {return d.Sha})
        .attr("cursor", "pointer");

    var rect = groups
        .append("rect")
        .attr("x", 0)
        .attr("y", (d) => {return d.y - 10})
        .classed("commit-row", true);


    //
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
        .text((d) =>{return d.Sha})
        .attr("x", (d) => {return 140})
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
                return d.Sha == l.target;
            })[0];
            d3.select(this).attr("y1", sourceNode.y);
            return sourceNode.x
        })
        .attr("x2", function(l) {
            var targetNode = nodes.filter(function(d, i) {
                return d.Sha == l.source;
            })[0];
            d3.select(this).attr("y2", targetNode.y);
            return targetNode.x
        })
        .attr("stroke", function(d) {
            return graphHelper.pickupColor(d.nodeColumn);
        })
        .classed("commit-link", true)

    groups.on("mouseover", function(d) {
        var el = d3.select(this);
        el.select("text").classed("selected", true);
        el.select("circle").classed("selected", true);
    }).on("mouseout", function(d) {
        var el = d3.select(this);
        el.select("text").classed("selected", false);
        el.select("circle").classed("selected", false);
    });
}

export function onCommitClick(cb) {

    var groups = d3.select("svg").selectAll("g")
    groups.on("click", (g) => cb(g));
}
