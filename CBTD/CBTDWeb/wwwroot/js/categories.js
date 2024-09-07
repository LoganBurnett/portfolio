var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_Categories').DataTable({
        "ajax": {
            "url": "/api/category",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "name", "width": "55%" },
            { "data": "displayOrder", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Categories/Upsert?id=${data}" class="btn btn-primary test-white" style="cursor:pointer; width: 100px;">
                                    <i class="bi bi-pencil-square"></i> Edit
                                </a>
                                <a href="/Categories/Delete?id=${data}" class="btn btn-danger test-white" style="cursor:pointer; width: 100px;">
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
        "order": [[1, "asc"]]
    });
}