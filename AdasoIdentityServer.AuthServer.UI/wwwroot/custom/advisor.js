function GetModalArgument(id) {
    $.get('/AdvisorData/ModalArgument?id=' + id, function (data, status) {
        $('#kt_modal_process').html(data);
        $('#kt_modal_process').modal('show');

    });
}

function GetModalWorkHours(id) {
    $.get('/AdvisorData/ModalWorkHours?id=' + id, function (data, status) {
        $('#kt_modal_process').html(data);
        $('#kt_modal_process').modal('show');

    });
}

function GetModalMessage(id) {
    $.get('/AdvisorData/ModalMessage?id=' + id, function (data, status) {
        $('#kt_modal_process').html(data);
        $('#kt_modal_process').modal('show');

    });
}