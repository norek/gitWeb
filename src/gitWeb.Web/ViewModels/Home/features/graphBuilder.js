import * as graphHelper from './graphHelpers.js';
import * as d3 from 'd3';

export function draw(nodes, links) {

    console.log(nodes);
    console.log(links);

    var svg = d3.select("svg");
    svg.selectAll("*").remove();

    var groups = svg.selectAll("g")
        .data(nodes)
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
            d3.select(this).attr("y1", l.y1);
            return l.x1
        })
        .attr("x2", function(l) {
          d3.select(this).attr("y2", l.y2);
          return l.x2
        })
        .attr("stroke", function(d) {
            return graphHelper.pickupColor(d.targetHIndex);
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
