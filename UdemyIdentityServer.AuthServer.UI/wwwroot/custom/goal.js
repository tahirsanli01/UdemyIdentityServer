$(document).ready(function () {
 
    datatable_init("#kendotable", "&ust=1&ust_=0");

});

function detailInit(e) { 
    var detailRow = e.detailRow;
    if (e.data.id > 0) {
        console.log(e.data.id);
        datatable_init("#tblSubPages" + e.data.id, "&ustx=" + e.data.id);
    };    
}

function Get(id) {
    $.get('/ManageData/EditGoal?id=' + id, function (data, status) {
        
        $('#kt_modal_process').html(data);
        $('#kt_modal_process').modal('show');

        $.get('/ManageData/GetInputSelect?Name=StrategicPlanId&SelectedIndex=&Items=&constantType=4&SubParameterId=0', function (dataSelect, status) {
            $("#dvStrategicPlans").html(dataSelect);
            $('#StrategicPlanId').select2({ dropdownParent: $('#kt_modal_process') });

            $('select[name="StrategicPlanId"]').val($("#dvStrategicPlans").data("selected")).change();
        });
        $('#dvStrategicPlans').select2({ dropdownParent: $('#kt_modal_process') });

        $('input[name="Ceyrek"]').change(function () {
                updateHiddenInput();
            });
        function updateHiddenInput() {
            var selectedValues = [];
            $('input[name="Ceyrek"]:checked').each(function () {
                selectedValues.push($(this).val());
            });
            $('#Period').val(selectedValues.join('-'));
        }
        var hiddenValue = $('#Period').val();
        var selectedOptions = hiddenValue.split('-');
        selectedOptions.forEach(function (option) {
                $('input[name="Ceyrek"][value="' + option + '"]').prop('checked', true);
            });
        
        
    });
 

}

function Edit() {
    $.post("/ManageData/UpdateGoal", $('#kt_modal_process_form').serialize(),
        function (data, status) {
            if (data.isSuccess == true) {
                Swal.fire(data.message, '', 'isConfirmed')
            }
            else {
                Swal.fire(data.message, '', 'warning')
            }

            setTimeout(refresh, 2000);
        });
}

function Add() {
    $.post("/ManageData/AddGoal", $('#kt_modal_process_form').serialize(),
        function (data, status) {
            if (data.isSuccess == true) {
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
            $.get('/ManageData/DeleteGoal?id=' + id, function (data, status) {
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

function selectOptionsSelected(val, name) {
    $("select[name=" + name + "] option[value='" + val + "']").attr("selected", "selected");
}

 