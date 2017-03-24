var treeGridControl = {
    gridSelector: "#treegrid",

   
    // Initialize jqGrid
    initializeGrid: function () {
        param = treeGridControl.getParams("idDomain"),
        $(treeGridControl.gridSelector).jqGrid({
            url: "/MeasurementResult/GetDataForGrid/",
            datatype: 'json',
            postData: { idDomain: param },
            colNames: ['Url', 'MinTime', 'MaxTime'],
            mtype: 'GET',
            colModel: [
                { name: 'DomainUrl', width: 897, alight: 'center', formatter: "link" },
                { name: 'MinRespTime', width: 120, align: 'center' },
                { name: 'MaxRespTime', width: 120, align: 'center' }
            ],
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
    },

    // Get parametr id
    getParams: function(name) {
        var s = window.location.search;
        s = s.match(new RegExp(name + '=([^&=]+)'));
        return s ? s[1] : false;
    }
};