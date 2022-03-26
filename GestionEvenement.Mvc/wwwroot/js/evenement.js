var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function getDate(date) {
    var d = new Date(date);
    return d.toLocaleDateString();
}

function getTime(date) {
    var d = new Date(date);
    return d.toLocaleTimeString();
}

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Evenement/GetAll"
        },
        "columns": [
            { "data": "name", "width": "18%" },
            {
                "data": "startDateAndTime",
                "render": function (data) {
                    return getDate(data);
                }, "width": "10%"
            },
            {
                "data": "startDateAndTime",
                "render": function (data) {
                    return getTime(data);
                }, "width": "10%"
            },
            {
                "data": "endDateAndTime",
                "render": function (data) {
                    return getDate(data);
                }, "width": "10%"
            },
            {
                "data": "endDateAndTime",
                "render": function (data) {
                    return getTime(data);
                }, "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Evenement/AddOrEdit/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Evenement/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}