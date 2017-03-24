var arhiveGridControl = {
    gridSelector: "#domainsGrid",

    initializeDomainsGrid: function (f) {
        
        // Initialize jqGrid       
        $(arhiveGridControl.gridSelector).jqGrid({
            url: '/Arhive/LoadDomains',
            datatype: 'json',
            mtype: 'GET',
            colNames: ['DomainUrl', 'MeasurementDate'],
            colModel: [
                {
                    name: 'DomainUrl', width: 590, icon: "ui-icon-link", edittype: 'select', formatter: function (cellvalue, options, rowObjcet) {
                        return '<a href = "/MeasurementResult?idDomain=' + rowObjcet.Id + '">' + rowObjcet.DomainUrl + '</a>';
                    }
                },
                {
                    name: 'MeasurementDate',
                    width: 547,
                    align: "center",
                    formatter: "date",
                    formatoptions: { srcformat: "ISO8601Long", newformat: "m/d/Y h:i A" }
                }],
            height: 'auto',
            gridview: true,
            width: 1140,
            rowNum: 10000,
            shrinkToFit: false,
            sortname: 'name',
            treeGrid: true,
            treeGridModel: 'adjacency',
            ExpandColumn: 'DomainUrl',
            pager: "#jqGridPager",
            jsonReader: {
                repeatitems: false,
                root: function (obj) { return obj; },
                page: function (obj) { return 1; },
                total: function (obj) { return 1; },
                records: function (obj) { return obj.length; }
            }
        });
    }
};