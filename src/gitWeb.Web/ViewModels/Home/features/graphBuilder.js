export function build(graphDataService) {
    var data = graphDataService.getTree(null);

    data = data.slice(0, 20);

    var i = 0;
    data.forEach(function (c) {
        c.id = i++;
        c.column = 0;
        c.x = 0;
        c.y = 100 + c.id * 22
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

    var columns = [{ i: 2, free: true }];

    function getFreeColumn() {
        var freeColumn = columns.find(x => x.free === true);

        if (freeColumn !== undefined) {
            return freeColumn;
        } else {
            var maxOfIndex = columns.reduce(function (prev, curr) { return prev.Cost < curr.Cost ? prev : curr; });
            var newColumn = { i: 0, free: true };
            newColumn.i = maxOfIndex.i + 1;
            columns.push(newColumn);

            return newColumn;
        }
    }

    function assignParentColumn(child) {

        var parents = data.filter(function (d) { return child.Parents.indexOf(d.Sha) >= 0 });

        if (parents.length <= 0) return;

        var parentsArray = [];
        for (i = 0; i < child.Parents.length; i++) {
            var parentSha = child.Parents[i];
            var currParent = parents.find(x => x.Sha == parentSha);

            if (currParent === undefined) {
                var fakeParent = { id: -1 }
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

                var allChildrenOfCurrentParrent = data.filter(function (d) { return d.Parents.indexOf(currParent.Sha) >= 0 });

                var otherParent = allChildrenOfCurrentParrent.find(p => p.Sha != child.Sha);

                if (otherParent !== undefined) {
                    continue;
                }
                else {
                    var newColumn = getFreeColumn();
                    currParent.column = newColumn.i;
                    newColumn.free = false;
                }
            }

            currParent.x = currParent.column * 20
        }

        parentsArray.forEach(function (p) { if (p.id > 0) { assignParentColumn(p) } });
    }

    setCommitPosition();



    var links = [];
    var nodes = [];

    for (i = 0; i < data.length; i++) {

        var curr = data[i];

        nodes.push({ Sha: curr.Sha, x: curr.x, y: curr.y, Orig: true });

        if (curr.Parents === undefined) continue;
        for (var j = 0; j < curr.Parents.length; j++) {
            var currPar = curr.Parents[j]

            if (currPar === undefined) continue;

            var exPath = data.find((s) => s.Sha == currPar);


            if (exPath === undefined) continue;

            if (exPath.column === curr.column) {
                links.push({ source: curr.Sha, target: currPar, nodeColumn: exPath.column });
            } else {
                var intermidSha = curr.Sha + exPath.Sha;
                nodes.push({ Sha: intermidSha, x: exPath.x, y: curr.y + 10 })
                links.push({ source: curr.Sha, target: intermidSha, nodeColumn: exPath.column })
                links.push({ source: intermidSha, target: currPar, nodeColumn: exPath.column })
            }
        }
    }

    return { nodes, links, data };
}

export function draw(nodes, links, data) {

    var svg = d3.select(".graphgit")
        .append("svg")
        .classed("graphgit",true);

    console.log(links);

    var links = svg.selectAll("link")
        .data(links)
        .enter()
        .append("line")
        .attr("x1", function (l) {
            var sourceNode = nodes.filter(function (d, i) {
                return d.Sha == l.target;
            })[0];
            d3.select(this).attr("y1", sourceNode.y);
            return sourceNode.x
        })
        .attr("x2", function (l) {
            var targetNode = nodes.filter(function (d, i) {
                return d.Sha == l.source;
            })[0];
            d3.select(this).attr("y2", targetNode.y);
            return targetNode.x
        })
        .attr("stroke", function (d) {
            console.log(d);
            if (d.nodeColumn == 1) {
                return "black";
            } else if (d.nodeColumn == 2) {
                return "yellow";
            }
            else if (d.nodeColumn == 3) {
                return "green";
            }
            else if (d.nodeColumn == 4) {
                return "red";
            }
            else if (d.nodeColumn == 5) {
                return "#343453";
            }
            return "blue";
        })
        .classed("commit-link", true)


    var commit = svg.selectAll(".commit")
        .data(nodes.filter((d) => d.Orig === true));

    commit
        .enter().append("g")
        .append("circle")
        .attr("r", 5)
        .classed("commit-dot", true);

    commit.exit().remove();

    commit
        .transition()
        .select("g>circle")
        .attr("cx", function (d) { return d.x })
        .attr("cy", function (d) { return d.y });


    var labels = svg.selectAll("text")
        .data(data)
        .enter()
        .append("text")
        .text(function (d) { return d.Sha })
        .attr("x", function (d) { return 140 })
        .attr("y", function (d) { return d.y + 5 })
        .classed("commit-label", true);

    labels.exit().remove();
}