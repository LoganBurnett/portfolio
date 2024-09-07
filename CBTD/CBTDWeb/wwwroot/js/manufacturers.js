var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_Manufacturers').DataTable({
        "ajax": {
            "url": "/api/manufacturer",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Manufacturers/Upsert?id=${data}" class="btn btn-primary test-white" style="cursor:pointer; width: 100px;">
                                    <i class="bi bi-pencil-square"></i> Edit
                                </a>
                                <a href="/Manufacturers/Delete?id=${data}" class="btn btn-danger test-white" style="cursor:pointer; width: 100px;">
                                    <i class="bi bi-trash-fill"></i> Delete
                                </a>
                            </div>`;
                },
                "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%",
        "order": [[0, "asc"]]
    });
}