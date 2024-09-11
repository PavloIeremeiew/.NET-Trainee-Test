function filterByName() {
    var searchName = document.getElementById("searchName").value.toLowerCase();

    var rows = document.getElementById("personTable").rows;
    for (var i = 1; i < rows.length; i++) {
        var row = rows[i];
        var nameCell = row.cells[1];
        var name = nameCell.textContent.toLowerCase();

        if (name.indexOf(searchName) > -1) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    }
}
