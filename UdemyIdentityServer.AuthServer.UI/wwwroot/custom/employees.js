function GetModalArgument(id) {
    $.get('/EmployeesData/ModalArgument?id=' + id, function (data, status) {
        $('#kt_modal_process').html(data);
        $('#kt_modal_process').modal('show');
        
    });
}

function GetModalWorkHours(id) {
    $.get('/EmployeesData/ModalWorkHours?id=' + id, function (data, status) {
        $('#kt_modal_process').html(data);
        $('#kt_modal_process').modal('show');

    });
}

function GetModalMessage(id) {
    $.get('/EmployeesData/ModalMessage?id=' + id, function (data, status) {
        $('#kt_modal_process').html(data);
        $('#kt_modal_process').modal('show');

    });
}