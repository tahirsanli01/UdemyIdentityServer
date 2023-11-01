function datatable_init(id, filterDatatable) {
    return KendoTable();

    function KendoTable() {
        dataSource = new kendo.data.DataSource({
            type: "json",
            transport: {
                read: {
                    url: getParam("readurl"),
                    dataType: "json",
                    type: "get"
                },
                update: {
                    url: getParam("updateurl"),
                    dataType: "json",
                    type: "post"
                },
                destroy: {
                    url: getParam("deleteurl"),
                    dataType: "json",
                    type: "post"
                },
                create: {
                    url: getParam("createurl"),
                    dataType: "json",
                    type: "post"
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                    else {
                        var urlColumnFilter = "";
                        console.log(options);
                        if (options.filter != undefined)
                            for (var i = 0; i < options.filter.filters.length; i++) {
                                urlColumnFilter += "&filter[0]._field=" + options.filter.filters[i].field;
                                urlColumnFilter += "&filter[0]._operator=" + options.filter.filters[i].operator;
                                urlColumnFilter += "&filter[0]._value=" + options.filter.filters[i].value;
                                urlColumnFilter += "&filter[0]._ignoreCase=" + options.filter.filters[i].ignoreCase;
                                urlColumnFilter += "&filter[0]._logic=" + options.filter.filters[i].logic;
                            }

                        if (options.sort != undefined)
                            for (var i = 0; i < options.sort.length; i++) {

                                urlColumnFilter += "&sort[0]._field=" + options.sort[i].field;
                                urlColumnFilter += "&sort[0]._dir=" + options.sort[i].dir;
                            }
                        return "page=" + options.page + "&pageSize=" + options.pageSize + "&skip=" + options.skip + "&take=" + options.take + filterDatatable + urlColumnFilter;
                    }
                }
            },
            dataBound: function () {
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            //batch: true,
            pageSize: getParam("pagesize"),
            serverPaging: getParam("serverpaging"),
            serverSorting: getParam("serverordering"),
            serverFiltering: getParam("serversearching"),
            schema: {
                model: {
                    id: "id",
                    fields: getParam("modelList")
                },
                data: "data",
                total: "total_length"
            }
        });

        var config;
        if (getParam("template") != '') {
            config = {
                excel: {
                    allPages: true,
                    fileName: getParam("excelname") + ".xlsx",
                    proxyURL: getParam("readurl"),
                    filterable: true
                },
                pdf: {
                    author: "Lohith G N",
                    creator: "Telerik India",
                    date: new Date(),
                    fileName: getParam("excelname") + ".pdf",
                    keywords: "northwind products",
                    landscape: false,
                    margin: {
                        left: 10,
                        right: "10pt",
                        top: "10mm",
                        bottom: "1in"
                    },
                    paperSize: "A4",
                    subject: "Northwind Products",
                    title: "Northwind Products"
                },
                dataSource: dataSource,
                detailTemplate: kendo.template($(getParam("template")).html()),
                    detailInit: function (e) {
                    if (getParam("detailfunction") != '') {
                        var wrap = s => "{ return " + getParam("detailfunction") + " };";
                        var func = new Function(wrap(getParam("detailfunction")));
                        return func.call(null).call(null, e);
                    }
                },
                toolbar: getParam("toolbar"),//["create", "excel"],
                sortable: getParam("ordering"),
                pageable: getParam("paging"),
                groupable: getParam("groupable"),
                filterable: getParam("searching"),
                columnMenu: false,
                reorderable: true,
                resizable: true,
                serverGrouping: getParam("server-grouping"),
                //filterable: { mode: "row" },
                columns: getParam("columnList"),
                editable: getParam("command-button-view"),
                edited: function (e) {
                    var container = e.container;
                    container.css("background-color", "#90EE90");
                }
            };
        }
        else {
            config = {
                excel: {
                    allPages: true,
                    fileName: getParam("excelname") + ".xlsx",
                    proxyURL: getParam("readurl"),
                    filterable: true
                },
                pdf: {
                    author: "Lohith G N",
                    creator: "Telerik India",
                    date: new Date(),
                    fileName: getParam("excelname") + ".pdf",
                    keywords: "northwind products",
                    landscape: false,
                    margin: {
                        left: 10,
                        right: "10pt",
                        top: "10mm",
                        bottom: "1in"
                    },
                    paperSize: "A4",
                    subject: "Northwind Products",
                    title: "Northwind Products"
                },
                dataSource: dataSource,
                toolbar: getParam("toolbar"),//["create", "excel"],
                sortable: getParam("ordering"),
                pageable: getParam("paging"),
                groupable: getParam("groupable"),
                filterable: getParam("searching"),
                columnMenu: true,
                reorderable: true,
                resizable: true,
                navigatable: true,
                serverGrouping: getParam("server-grouping"),
                filterable: { mode: "row" },
                columns: getParam("columnList"),
                editable: getParam("command-button-view"),
                edited: function (e) {
                    var container = e.container;
                    container.css("background-color", "#90EE90");
                }
            }
        }

 
        return $(id).kendoGrid(config);
    }

    var dataList = null;
    function getParam(param) {

        function getInitParam(id) {
            var dataList = [];
            var columnList = [];
            var modelListString = '{{0}}';
            var modelListSubString = "";
            var commandButtonListString = '{ "command":[{0}]}';
            var commandButtonListSubString = '';

            $(id + ' > thead  > tr > th').each(function (index, tr) {
                if ($(tr).data("column-type") === "button") {
                    columnList.push({
                        template: $(tr).html(),
                        field: $(tr).data("column"),
                        title: $(tr).data("header"),
                        width: defaultValueGet($(tr).data("width"), ""),
                        format: defaultValueGet($(tr).data("format"), ""),
                        class: defaultValueGet($(tr).data("class"), ""),
                        filterable: defaultValueGet($(tr).data("filterable"), false)
                    });
                }
                else if ($(tr).data("img") === "img") {
                    columnList.push({
                        template: $(tr).html(),
                        field: $(tr).data("column"),
                        title: $(tr).data("header"),
                        width: defaultValueGet($(tr).data("width"), ""),
                        format: defaultValueGet($(tr).data("format"), ""),
                        template: "<img src=" + $(tr).data("template").replace("../","") + " width='100%'>",
                        class: defaultValueGet($(tr).data("class"), ""),
                        filterable: defaultValueGet($(tr).data("filterable"), false)
                    });
                }
                else if ($(tr).data("link") === "link") {
                    columnList.push({
                        template: $(tr).html(),
                        field: $(tr).data("column"),
                        title: $(tr).data("header"),
                        width: defaultValueGet($(tr).data("width"), ""),
                        format: defaultValueGet($(tr).data("format"), ""),
                        template: "<a href=" + $(tr).data("template").replace("../", "") + " width='100%'>Dosya Url</a>",
                        class: defaultValueGet($(tr).data("class"), ""),
                        filterable: defaultValueGet($(tr).data("filterable"), false)
                    });
                }
                else if ($(tr).data("column-type") === "inline-button-command") {
                    if ($(tr).data("is-remove") == true && $(tr).data("is-edit") == true) {
                        columnList.push({ command: [{ className: defaultValueGet($(tr).data("remove-class"), ""), width: defaultValueGet($(tr).data("width"), ""), format: defaultValueGet($(tr).data("format"), ""), name: "destroy", text: defaultValueGet($(tr).data("remove-text"), "") }, { className: defaultValueGet($(tr).data("edit-class"), ""), name: "edit", text: { edit: defaultValueGet($(tr).data("edit-text"), ""), cancel: defaultValueGet($(tr).data("cancel-text"), ""), update: defaultValueGet($(tr).data("update-text"), "") } }] });
                    }
                    else if ($(tr).data("is-remove") == true && $(tr).data("is-edit") != true) {
                        columnList.push({ command: [{ className: defaultValueGet($(tr).data("remove-class"), ""), width: defaultValueGet($(tr).data("width"), ""), format: defaultValueGet($(tr).data("format"), ""), name: "destroy", text: defaultValueGet($(tr).data("remove-text"), "") }] });
                    }
                    else if ($(tr).data("is-remove") != true && $(tr).data("is-edit") == true) {
                        columnList.push({ command: [{ className: defaultValueGet($(tr).data("edit-class"), ""), width: defaultValueGet($(tr).data("width"), ""), format: defaultValueGet($(tr).data("format"), ""), name: "edit", text: { edit: defaultValueGet($(tr).data("edit-text"), ""), cancel: defaultValueGet($(tr).data("cancel-text"), ""), update: defaultValueGet($(tr).data("update-text"), "") } }] });
                    }
                }
                else if ($(tr).data("column-type") == "select") {
                    columnList.push({
                        field: $(tr).data("column"), width: defaultValueGet($(tr).data("width"), ""), format: defaultValueGet($(tr).data("format"), ""), editor: categoryDropDownEditor = function (container, options) {
                            $('<input required name="' + options.field + '"/>')
                                .appendTo(container)
                                .kendoDropDownList({
                                    autoBind: false,
                                    dataTextField: "text",
                                    dataValueField: "id",
                                    dataSource: {
                                        type: "json",
                                        transport: {
                                            read: "/Ortak/Kendo_GetirFiltre?id=0&kod=&grupKod=" + defaultValueGet($(tr).data("grup-name"), "") + "&AnaGrupKod=0&Sorgu="
                                        }
                                    }
                                });
                        }, template: "#=parametre.text#"
                    });
                    modelListSubString += '"' + $(tr).data("column") + '": { "defaultValue": { "id": 6, "text": "Ankara" } },'
                }
                else {
                    columnList.push({ field: $(tr).data("column"), width: defaultValueGet($(tr).data("width"), ""), format: defaultValueGet($(tr).data("format"), ""), filterable: defaultValueGet($(tr).data("filterable"), false) });
                    modelListSubString += '"' + $(tr).data("column") + '": { "editable": ' + defaultValueGet($(tr).data("editable"), false) + ', "nullable": ' + defaultValueGet($(tr).data("nullable"), false) + ', "type": "' + defaultValueGet($(tr).data("type"), "string") + '" },';
                }

                $(tr).html($(tr).data("header"));
            });

            modelListString = modelListString.replace("{0}", modelListSubString.substring(0, modelListSubString.length - 1));
            commandButtonListString = commandButtonListString.replace("{0}", commandButtonListSubString.substring(0, commandButtonListSubString.length - 1));
 

            dataList.push({ "data": "columnList", "value": columnList });
 
            dataList.push({ "data": "modelList", "value": JSON.parse(modelListString) });

            dataList.push({ "data": "readurl", "value": $(id).data("url") });
            dataList.push({ "data": "createurl", "value": $(id).data("createurl") });
            dataList.push({ "data": "updateurl", "value": $(id).data("updateurl") });
            dataList.push({ "data": "deleteurl", "value": $(id).data("deleteurl") });
            /*dataList.push({ "data": "toolbar", "value": defaultValueGet($(id).data("toolbar"), ["excel"]) });*/
            dataList.push({ "data": "groupable", "value": $(id).data("groupable") });
            dataList.push({ "data": "template", "value": defaultValueGet($(id).data("template"), '') });
            dataList.push({ "data": "detailfunction", "value": defaultValueGet($(id).data("detailfunction"), '') });

            dataList.push({ "data": "server-grouping", "value": $(id).data("server-grouping") });

            dataList.push({ "data": "filter", "value": $(id).data("filter") });
            dataList.push({ "data": "excelname", "value": $(id).data("excelname") });

            dataList.push({ "data": "pagesize", "value": defaultValueGet($(id).data("pagesize"), 10) });
            dataList.push({ "data": "paging", "value": defaultValueGet($(id).data("paging"), true) });
            dataList.push({ "data": "ordering", "value": defaultValueGet($(id).data("ordering"), true) });
            dataList.push({ "data": "searching", "value": defaultValueGet($(id).data("searching"), true) });
            dataList.push({ "data": "serverpaging", "value": defaultValueGet($(id).data("serverpaging"), true) });
            dataList.push({ "data": "serverordering", "value": defaultValueGet($(id).data("serverordering"), true) });
            dataList.push({ "data": "serversearching", "value": defaultValueGet($(id).data("serversearching"), true) });

            dataList.push({ "data": "command-button-view", "value": defaultValueGet($(id).data("command-button-view"), "inline") });
             
            return dataList;
        }

        if (dataList == null)
            dataList = getInitParam(id);

        for (var i = 0; i < dataList.length; i++) {
            if (dataList[i].data == param) {
                return dataList[i].value;
            }
        }

        return "";
    }

    function defaultValueGet(val, defVal) {
        if (val == null || val == undefined || val == "") {            
            return defVal;
        }
        else {
 
            return val;
        }
    }
}

function kendo_reload(id, filterDatatable) {
    $(id).data("kendoGrid").dataSource.transport.parameterMap = function (options, operation) {
        if (operation !== "read" && options.models) {
            return { models: kendo.stringify(options.models) };
        }
        else {
            var urlColumnFilter = "";
            console.log(options);
            if (options.filter != undefined)
                for (var i = 0; i < options.filter.filters.length; i++) {
                    urlColumnFilter += "&filter[0]._field=" + options.filter.filters[i].field;
                    urlColumnFilter += "&filter[0]._operator=" + options.filter.filters[i].operator;
                    urlColumnFilter += "&filter[0]._value=" + options.filter.filters[i].value;
                    urlColumnFilter += "&filter[0]._ignoreCase=" + options.filter.filters[i].ignoreCase;
                    urlColumnFilter += "&filter[0]._logic=" + options.filter.filters[i].logic;
                }

            if (options.sort != undefined)
                for (var i = 0; i < options.sort.length; i++) {

                    urlColumnFilter += "&sort[0]._field=" + options.sort[i].field;
                    urlColumnFilter += "&sort[0]._dir=" + options.sort[i].dir;
                }
            return "page=" + options.page + "&pageSize=" + options.pageSize + "&skip=" + options.skip + "&take=" + options.take + filterDatatable + urlColumnFilter;
        }
    };
    $(id).data("kendoGrid").dataSource.page(1)
    $(id).data("kendoGrid").dataSource.read()
}

 