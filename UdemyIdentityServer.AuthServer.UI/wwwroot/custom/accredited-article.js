$(document).ready(function () {
    datatable_init("#kendotable", "&ust=1&ust_=0");
});

function detailInit(e) {
    //console.log(e)
    //console.log(e.data.id);

    var detailRow = e.detailRow;
    if (e.data.id > 0) {
        console.log(e.data.id);
        datatable_init("#tblSubPages" + e.data.id, "&ustx=" + e.data.id);
    };
    //datatable_init("#tblZiyaretler" + e.data.id, "&SehirId=0&IlceId=" + e.data.ilceKod + "&BasTarih=01-01-2000&BitTarih=01-01-2021");
}

function Get(id) {
    $.get('/ManageData/EditAccreditedArticle?id=' + id, function (data, status) { 
        $('#kt_modal_process').html(data);
        $('#kt_modal_process').modal('show');

        

        $.get('/ManageData/GetInputSelect?Name=ParentId&SelectedIndex=&Items=&constantType=5&SubParameterId=0', function (dataSelect, status) {
            $("#dvAccreditedArticle").html(dataSelect);
            $('#ParentId').select2({ dropdownParent: $('#kt_modal_process') });
            $('select[name="ParentId"]').val($("#dvAccreditedArticle").data("selected")).change();
        });

        $.get('/ManageData/GetInputSelect?Name=VersionId&SelectedIndex=&Items=&constantType=3&SubParameterId=0', function (dataSelect, status) {
            $("#dvVersions").html(dataSelect);
            $('#VersionId').select2({ dropdownParent: $('#kt_modal_process') });
            $('select[name="VersionId"]').val($("#dvVersions").data("selected")).change();
        });

        $.get('/ManageData/GetInputSelect?Name=Type&SelectedIndex=&Items=&constantType=7&SubParameterId=0', function (dataSelect, status) {
            $("#dvAccrediteArticlePlansType").html(dataSelect);
            $('#Type').select2({ dropdownParent: $('#kt_modal_process') });
            $('select[name="Type"]').val($("#dvAccrediteArticlePlansType").data("selected")).change();
        });

        /*$('#dvAccrediteArticlePlansType').select2({ dropdownParent: $('#kt_modal_process') });*/


 
    });
}

function Edit() {
    $.post("/ManageData/UpdateAccreditedArticle", $('#kt_modal_process_form').serialize(),
        function (data, status) {
            if (data.isConfirmed == true) {
                Swal.fire(data.message, '', 'isConfirmed')
            }
            else {
                Swal.fire(data.isConfirmed, '', 'warning')
            }

            setTimeout(refresh, 2000);
        });
}

function Add() {
    $.post("/ManageData/AddAccreditedArticle", $('#kt_modal_process_form').serialize(),
        function (data, status) {
            if (data.isConfirmed == true) {
                Swal.fire(data.message, '', 'isConfirmed')
            }
            else {
                Swal.fire(data.message, '', 'warning')
            }

            setTimeout(refresh, 2000);
        });
}

function Delete(id) {
    Swal.fire({
        title: 'İşlemi gerçekleştirmek istediğinize emin misiniz?',
        showDenyButton: true,
        confirmButtonText: 'Evet',
        denyButtonText: `Hayır`,
    }).then((result) => {
        if (result.isConfirmed) {
            $.get('/ManageData/DeleteAccreditedArticle?id=' + id, function (data, status) {
                Swal.fire(data.message, '', 'isConfirmed')
                setTimeout(refresh, 1000);
            });
        } else if (result.isDenied) {
            Swal.fire(data.message, '', 'info')
            setTimeout(refresh, 1000);
        }
    })
}

function refresh() {
    location.reload();
}