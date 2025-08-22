
        // DataTable başlat
        var table = $("#table").DataTable({
            processing: false,
            paging: true,
            searching: true,
            ordering: true,
            info: true,
            language: {
                lengthMenu: "_MENU_ kayıt göster",
                zeroRecords: "Kayıt bulunamadı",
                info: "_TOTAL_ kayıttan _START_ - _END_ arası gösteriliyor",
                infoEmpty: "Kayıt yok",
                infoFiltered: "(_MAX_ kayıt içerisinden filtrelendi)",
                paginate: {
                    first: "İlk",
                    last: "Son",
                    next: "İleri",
                    previous: "Geri"
                }
            },
            dom:
                "<'row'" +
                    "<'col-sm-6 d-flex align-items-center justify-content-start'l>" +
                    "<'col-sm-6 d-flex align-items-center justify-content-end'>" +
                ">" +
                "<'table-responsive'tr>" +
                "<'row'" +
                    "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
                    "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
                ">"
        });

        
        $('#kt_filter_search').on('keyup', function () {
            table.search(this.value).draw();
        });
