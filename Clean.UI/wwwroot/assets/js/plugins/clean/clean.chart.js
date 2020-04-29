var clean = window.clean = window.clean || {};
(function () {
    clean.chart = function (opt) {
        this.opt = opt = opt || {};
        this.OnInit = window[opt.el.attr('onInit')];
        this.el = opt.el;
        this.path = "";
        this.page = opt.page;
        this.prefix = this.el.attr('prefix') || "ux";
        this.actions = this.el.find('.div-form-control [action]');
        this.fields = [];
        this.multiple = this.el.attr('multiple') === 'true';
        this.hasGrid = this.el.hasClass('grid-chart');
        this.grid = {};
        this.grid.template = this.el.find('.form-grid');
        this.grid.table = this.el.attr('id').replace('dv', 'gv');
        this.grid.cols = [];
        this.grid.actions = this.el.find('.div-grid-control').html();
        this.validationrule = {};
        this.charts = [];
        this.EncryptedID = this.el.find('#' + this.el.attr('prefix') + 'pageid').val();
        if (this.el.attr('OnBind')) {
            this.OnBind = this.el.attr('OnBind');
        }
        this.init(this.el);
    };
    clean.chart.prototype = {
        init: function (opt) {
            var self = this;
            self.construct();

            //Registering the clean form actions -- Save -- Search -- New
            this.actions.bind('click', function () {
                var act = $(this).attr('action');
                if (self[act]) self[act]();
                return false;
            });

            if (!clean.isEmpty(self.OnInit))
                clean.invoke(self.OnInit, self);
        },
        getactions: function () {
            var self = this;
            self.actions = this.el.find('.div-form-control [action]');
        },
        getfields: function () {
            var self = this;
            self.fields = self.el.find(':text, :radio, :checkbox, input:hidden, select, textarea').not(":button, :submit, input[static=true]");
            var filtered = self.fields.filter(function (item, index, arry) {
                if ($(self.fields[item]).attr('id').startsWith(self.prefix)) return self.fields[item];
            });
            self.fields = filtered;
        },
        loaddatepicker: function () {
            var self = this;
            self.el.find('.Shamsi').each(function () {
                var el = $(this);
                var sib = el.attr('sibling');
                var shamsi = el.MdPersianDateTimePicker({
                    targetTextSelector: '#' + el.attr('id'),
                    targetDateSelector: '#' + sib,
                    dateFormat: 'yyyy-MM-dd',
                    isGregorian: false,
                    enableTimePicker: false
                });
                self.datepickers.push(shamsi);
            });

            self.el.find('.Miladi').each(function () {
                var el = $(this);
                var sib = el.attr('sibling');
                var miladi = el.MdPersianDateTimePicker({
                    targetTextSelector: '#' + sib,
                    targetDateSelector: '#' + el.attr('id'),
                    dateFormat: 'yyyy-MM-dd',
                    isGregorian: true,
                    toPersian: true,
                    enableTimePicker: false
                });
                self.datepickers.push(miladi);
            });
        },
        construct: function () {
            var self = this;
            var path = self.el.attr('id');
            self.path = '/' + path.substring(path.indexOf("_") + 1).replace(/_/g, '/');

            self.getfields();
            self.el.find('select').select2({
                placeholder: "--",
                allowClear: true
            });


            self.el.find('select').on('change', function (evt) {
                $thisVal = $(this).val();
                if ($thisVal != '') {
                    setTimeout(function () {
                        self.el.valid();
                    }, 100);
                }

                if ($(this).attr('child')) {
                    var c = $(this).attr('child');
                    self.loadDynamicLists($thisVal, c);
                }
            });


            self.loaddatepicker();

            self.el.find('.national-id').each(function () {
                var opt = {};
                opt.el = $(this).find('.national-id-input');
                opt.parent = $(this).parent();
                opt.form = self;
                self.tazkira = new clean.Tazkira(opt);
            });

            if (!self.multiple && self.hasGrid) {
                $(self.grid.template.find('thead').find('th')).each(function (index) {
                    self.grid.cols.push($(this).attr('colname'));
                });
                $(self.grid.template).find('table').addClass('table table-bordered table-hover').attr('id', self.grid.table);
                self.el.parents('.panel').append(self.grid.template);
            }

            self.validationrule = self.validation();
            self.configure();
            self.el.find('.chart-container').each(function (i, item) {
                require([
                    'echarts',
                    'echarts/theme/limitless',
                    'echarts/chart/bar',
                    'echarts/chart/line',
                    'echarts/chart/pie',
                    'echarts/chart/funnel'],
                    function (ec, config) {
                        var chart = self.buildChart(item, ec, config);
                        chart.OrginElement = item;
                        self.charts.push(chart);
                    });
            });

        },
        buildChart: function (el, ec, config) {
            var elm = $(el);
            var type = elm.attr('chart-type');
            var options = {};
            if (type == 'bar') {
                if (elm.attr('chart-title')) {
                    options.title = {
                        text: elm.attr('chart-title'),
                        x: 'right'
                    };
                }
                options = this.getBarChartOptions();
                options.legend.data = ['Year 2013', 'Year 2014'];
                options.yAxis[0].data = ['Germany', 'France', 'Spain', 'Netherlands', 'Belgium'];
                options.series.push(
                    {
                        name: 'Year 2013',
                        type: 'bar',
                        itemStyle: {
                            normal: {
                                color: '#EF5350'
                            }
                        },
                        data: [38203, 73489, 129034, 204970, 331744]
                    });
                options.series.push(
                    {
                        name: 'Year 2014',
                        type: 'bar',
                        itemStyle: {
                            normal: {
                                color: '#66BB6A'
                            }
                        },
                        data: [39325, 83438, 131000, 221594, 334141]
                    });

            }
            else if (type == "rose") {
                options = this.getRoseChartOptions();
                if (elm.attr('chart-title')) {
                    options.title = {
                        text: elm.attr('chart-title'),
                        x: 'center'
                    };
                }
                options.legend.data = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
                options.series.push({
                    name: 'Increase (brutto)',
                    type: 'pie',
                    radius: ['15%', '73%'],
                    center: ['50%', '57%'],
                    roseType: 'area',

                    // Funnel
                    width: '40%',
                    height: '78%',
                    x: '30%',
                    y: '17.5%',
                    max: 450,
                    sort: 'ascending',

                    data: [
                        { value: 440, name: 'January' },
                        { value: 260, name: 'February' },
                        { value: 350, name: 'March' },
                        { value: 250, name: 'April' },
                        { value: 210, name: 'May' },
                        { value: 350, name: 'June' },
                        { value: 300, name: 'July' },
                        { value: 430, name: 'August' },
                        { value: 400, name: 'September' },
                        { value: 450, name: 'October' },
                        { value: 330, name: 'November' },
                        { value: 200, name: 'December' }
                    ]
                });

            }
            else if (type == 'column') {
                options = this.getColumnChartOptions();
                options.legend.data = ['Evaporation', 'Precipitation'];
                options.xAxis[0].data = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                options.series.push({
                    name: 'Evaporation',
                    type: 'bar',
                    data: [2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20.0, 6.4, 3.3],
                    itemStyle: {
                        normal: {
                            label: {
                                show: true,
                                textStyle: {
                                    fontWeight: 500
                                }
                            }
                        }
                    }
                });
                options.series.push({
                    name: 'Precipitation',
                    type: 'bar',
                    data: [2.6, 5.9, 9.0, 26.4, 58.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3],
                    itemStyle: {
                        normal: {
                            label: {
                                show: true,
                                textStyle: {
                                    fontWeight: 500
                                }
                            }
                        }
                    }
                });
            }

            var chart = ec.init(elm.find('.element')[0], config);
            chart.setOption(options);
            chart.ChartType = type;
            return chart;
        },

        getBarChartOptions: function () {
            var options = {};
            options.grid = {
                x: 75,
                x2: 35,
                y: 35,
                y2: 25
            };
            options.tooltip = {
                trigger: 'axis',
                axisPointer: {
                    type: 'shadow'
                }
            };
            options.legend = {
                type: 'scroll',
                orient: 'horizontal',
                right: 10,
                top: 20,
                bottom: 20,
                data: []
            };
            options.xAxis = [{
                type: 'value',
                boundaryGap: [0, 0.01]
            }];
            // Vertical axis
            options.yAxis = [{
                type: 'category',
                data: []
            }];

            options.series = [];
            return options;
        },
        getColumnChartOptions: function () {
            var options = {};
            options.grid = {
                x: 40,
                x2: 40,
                y: 35,
                y2: 25
            };
            options.tooltip = {
                trigger: 'axis'
            };
            options.legend = {
                data: []
            };
            options.xAxis = [{
                type: 'category',
                data: []
            }];
            options.yAxis = [{
                type: 'value'
            }];
            options.series = [];

            return options;
        },
        getRoseChartOptions: function () {
            var options = {};
            options.tooltip = {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            };
            options.legend = {
                x: 'left',
                y: 'top',
                orient: 'vertical',
                data: []
            };
            options.toolbox = {
                show: true,
                orient: 'vertical',
                feature: {
                    saveAsImage: {
                        show: true,
                        title: 'Same as image',
                        lang: ['Save']
                    }
                }
            };
            options.series = [];
            return options;
        },
        search: function (r) {
            var self = r || this;
            var path = self.path + '/search';


            var data = {};
            if (r) {
                data = self.record;
            }
            else {
                self.el.find('input.search, textarea.search, select.search').each(function () {
                    var fld = $(this), col = fld.attr('id').substring(self.prefix.length);
                    Object.defineProperty(data, col.toString(), { value: fld.val().toString(), enumerable: true, configurable: true });
                });
            }
            $.each(self.charts, function (i, item) {
                var cPath = path + $(item.OrginElement).attr('chart-method');
                clean.data.post({
                    async: false, url: cPath, data: clean.data.json.write(data), dataType: 'json',
                    success: function (msg) {
                        if (msg.status > 0) {
                            var data = msg.data;
                            if (data) {
                                var options = {};
                                var colors = ['#EF5350', '#66BB6A'];
                                item.clear();
                                if (item.ChartType == 'bar') {
                                    options = self.getBarChartOptions();
                                    options.legend.data = data.legends;

                                    options.yAxis[0].data = data.yAxis;
                                    var i = 1;
                                    for (var key in data.series) {
                                        options.series.push({
                                            name: key,
                                            type: 'bar',
                                            itemStyle: {
                                                normal: {
                                                    color: colors[i % 2]
                                                }
                                            },
                                            data: data.series[key]
                                        });
                                        i++;
                                    }
                                }
                                else if (item.ChartType == 'rose') {
                                    options = self.getRoseChartOptions();
                                    options.legend.data = data.legends;

                                    var i = 1;
                                    for (var key in data.series) {
                                        options.series.push({
                                            name: key,
                                            type: 'pie',
                                            radius: ['15%', '73%'],
                                            center: ['50%', '57%'],
                                            roseType: 'area',

                                            // Funnel
                                            width: '40%',
                                            height: '78%',
                                            x: '30%',
                                            y: '17.5%',
                                            max: 450,
                                            sort: 'ascending',

                                            data: data.series[key]
                                        });
                                        i++;
                                    }
                                }
                                else if (item.ChartType == 'column') {
                                    options = self.getColumnChartOptions();
                                    options.legend.data = data.legends;

                                    options.xAxis[0].data = data.xAxis;
                                    var i = 1;
                                    for (var key in data.series) {
                                        options.series.push({
                                            name: key,
                                            type: 'bar',
                                            itemStyle: {
                                                normal: {
                                                    label: {
                                                        show: true,
                                                        textStyle: {
                                                            fontWeight: 500
                                                        }
                                                    },
                                                    color: colors[i % 2]
                                                }
                                            },
                                            data: data.series[key]
                                        });
                                        i++;
                                    }
                                }
                                item.setOption(options,true);
                            }
                            else {
                                if (self.el.hasClass('main-form')) {
                                    clean.widget.warn("تشریحات", "ریکارد پیدا نشد!");
                                }
                            }
                        }
                        else {
                            clean.widget.warn(msg.text, msg.description);
                        }
                    }
                });
            });
            
        },
        new: function (opt) {
            var self = this;
            self.fields.each(function () {
                var fld = $(this);
                if (fld.attr('placeholder', 'درج نگردیده')) fld.attr('placeholder', '');

                if (fld.is(':checkbox'))
                    fld.prop('checked', false);
                else if (fld.attr('default') && fld.attr('default').toString().length) {
                    fld.val(fld.attr('default')).change();
                }
                else if (!fld.attr('default')) {
                    try {
                        fld.val(null).change();
                    } catch (e) {
                        console.error(e);
                    }
                }
            });
            
            if (self.OnNew && window[self.OnNew]) {
                window[self.OnNew](self);
            }
            self.getfields();
        },
        configure: function (opt) {
            var self = this;
            require.config({
                paths: {
                    echarts: '/assets/js/plugins/visualization/echarts'
                }
            });
            // Resize charts
            // ------------------------------

            window.onresize = function () {
                setTimeout(function () {
                    $.each(self.charts, function (i, item) {
                        item.resize();
                    });
                }, 200);
            };
        },
        fetch: function (r) {
            var self = this;
            self.search(r);
        },
        clearGrid: function () {
            let self = this;
            $('#' + self.grid.table).DataTable().clear().draw().destroy();
            $('#' + self.grid.table).find('tbody').empty();

            var tableheight = !$('#' + self.grid.table).attr('fullheight') ? 150 : null;
            var filter = !$('#' + self.grid.table).attr('filter') ? false : true;

            $('#' + self.grid.table).DataTable({
                "paging": false,
                "showExpander": true,
                "ordering": false,
                "info": false,
                "filter": filter,
                autoWidth: true,
                scrollY: tableheight,
                "oLanguage": {
                    "sSearch": "جستجو",
                    "sLengthMenu": "تعداد در هر صفحه _MENU_",
                    "sEmptyTable": "جدول خالی است",
                    "sInfo": "نمایش صفحه _PAGE_ از _PAGES_ صفحه که مجموعاً شامل _MAX_ ریکارد است"
                },
            });
        },
        bindtogrid: function (d) {
            var self = this;
            var row = "";
            var rowclick = $('#' + self.grid.table).attr('bindonclick') ? 'fetch-record' : '';

            $('#' + self.grid.table).DataTable().clear().draw().destroy();
            $.each(d, function (ind, ob) {
                var column = "";
                for (var i = 0; i < self.grid.cols.length; i++) {
                    var colname = self.grid.cols[i].toLowerCase();
                    for (var key in ob) {
                        if (key.toLowerCase() == colname) {

                            if (colname != 'path' && colname != 'remarks') {

                                var va = ob[key];
                                if (clean.isEmpty(ob[key]))
                                    va = '--';
                                column = column + "<td col='" + key.toLowerCase() + "' data='" + va + "'>" + va + "</td>";
                            }

                            else if (colname == 'path') {

                                var temp = '<button type="button" downloadpath="$path" class="btn-link download-on-click"><i class="icon-download position-left"></i>دریافت فایل</button>'
                                column = column + "<td col='" + key.toLowerCase() + "'>" + temp.replace('$path', ob[key]) + "</td>";
                            }
                            else if (colname == 'remarks') {
                                if (ob[key] != null) {
                                    var temp = '<a class="link" data-popup="tooltip" data-trigger="hover" data-placement="bottom" title="$Text">نمایش</a>'
                                    column = column + "<td col='" + key.toLowerCase() + "'>" + temp.replace('$Text', ob[key]) + "</td>";
                                }
                                else {
                                    column = column + "<td col='" + key.toLowerCase() + "'></td>";
                                }
                            }
                        }
                    }
                    if (colname == 'action') {
                        var temp = '<td class="text-center"><ul class="icons-list"><li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="icon-menu9"></i></a>$content</li></ul></td>';
                        var actions = "";
                        if (ob.parentId === undefined || ob.parentId === null) {
                            var t = $(self.grid.actions).clone();
                            t.find('[action=neighbour]').parent().remove().end();
                            actions = $(t).get(0).outerHTML;
                        }
                        else
                            actions = self.grid.actions;
                        column = column + temp.replace('$content', actions);
                    }
                }
                if (ob.parentNodeId === undefined || ob.parentNodeId === null) {
                    row = row + "<tr role='row' data-tt-id='" + ob.nodeId + "' class='" + rowclick + "' data='" + ob.id + "'>" + column + "</tr>";
                }
                else
                    row = row + "<tr role='row' data-tt-id='" + ob.nodeId + "' data-tt-parent-id='" + ob.parentNodeId + "' class='" + rowclick + "' data='" + ob.id + "'>" + column + "</tr>";
            });
            $('#' + self.grid.table).find('tbody').empty().html(row);

            var tableheight = !$('#' + self.grid.table).attr('fullheight') ? 150 : null;
            var filter = !$('#' + self.grid.table).attr('filter') ? false : true;

            $('#' + self.grid.table).DataTable({
                "paging": false,
                "showExpander": true,
                "ordering": false,
                "info": false,
                "filter": filter,
                autoWidth: true,
                scrollY: tableheight,
                "oLanguage": {
                    "sSearch": "جستجو",
                    "sLengthMenu": "تعداد در هر صفحه _MENU_",
                    "sEmptyTable": "جدول خالی است",
                    "sInfo": "نمایش صفحه _PAGE_ از _PAGES_ صفحه که مجموعاً شامل _MAX_ ریکارد است"
                },
            });

            $('[data-popup="tooltip"]').tooltip({ template: '<div class="tooltip"><div class="bg-primary"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div></div>', trigger: 'click' });
            $('#' + self.grid.table).find('tr').click(function () {
                $(this).siblings().removeClass('row-selected');
                $(this).addClass('row-selected');
            });

            $('#' + self.grid.table).find('.grid-action').click(function () {
                var act = $(this).attr('action');
                if (self[act]) self[act](this);
                return false;
            });



            $.each(self.actions, function (i, v) {
                var el = $(v);
                if (el.attr('showongrid'))
                    $('#' + self.grid.table + '_wrapper').find('.dataTables_filter').append(el.css({ 'float': 'left', 'margin-right': '5px' }));
            });


        },
        validation: function () {
            var self = this;
            var validator = $(self.el).validate({
                ignore: 'input[type=hidden], input[type=file] .select2-input',
                errorClass: 'validation-error-label',
                successClass: 'validation-valid-label',
                highlight: function (element, errorClass) {
                    $(element).addClass("hrmis-error");
                    if ($(element).hasClass('file-attachment')) {
                        $(element).parents('.input-group').find('.form-control').addClass('hrmis-error');
                    }
                },
                unhighlight: function (element, errorClass) {
                    setTimeout(function () {
                        $(element).removeClass("hrmis-error");
                        if ($(element).hasClass('file-attachment')) {
                            $(element).parents('.input-group').find('.form-control').removeClass('hrmis-error');
                        }
                    }, 50);
                },
                errorPlacement: function (error, element) {
                    if (element.parents('div').hasClass('input-group') && element.siblings('span').hasClass('input-group-addon')) {
                        error.insertBefore(element.parent());
                    }
                    else if (element.siblings('div').hasClass('select2-container')) {
                        error.insertAfter(element.siblings('label'));
                    }
                    else if (element.hasClass('file-attachment')) {
                        error.insertAfter(element.parents('.file-parent').find('label'));

                    }
                    else {
                        error.insertBefore(element);
                    }
                },
                rules: {
                    email: {
                        email: true
                    },
                    SerialNumber: {  
                        
                        digits: true,
                        maxlength: 15
                    },
                    Juld: {
                        maxlength: 12
                    },
                    Page: {
                        digits: true,
                        maxlength: 3
                    },
                    No: {
                        digits: true,
                        maxlength: 5
                    },
                    Year: {
                        digits: true,
                        maxlength: 4,
                        minlength: 4
                    },
                    Percentage: {
                        digits: true,
                        number: true,
                        min: 10,
                        max: 100
                    }

                }
            });
            return validator;
        },
        update: function (v) {
            var self = this;
            self.record = {};

            var b = $(v);
            var row = b.parents('tr');

            var temp = {};
            temp.id = row.attr('data');
            temp.parentId = 0;
            self.new();

            if (row.attr('data-tt-parent-id')) {
                temp.parentId = row.attr('data-tt-parent-id');
            }
            self.loadDynamicLists(temp.parentId);

            if (temp.id > 0) {
                self.record.id = temp.id;
            }

            if (b.attr('data-attribute')) {
                var attribute = b.attr('data-attribute');
                var value = 0;
                if (attribute == 'parentid') {
                    value = row.attr('data-tt-parent-id');
                }
                else {
                    value = row.attr('data-tt-id');
                }

                if (!self.el.find($('#' + self.prefix + attribute)).length) {
                    self.el.append("<input type='hidden' id='" + self.prefix + attribute + "' value='" + value + "' />");
                    self.getfields();
                }
                else {
                    $('#' + self.prefix + attribute).val(value);
                }
                self.record[attribute] = value;
            }
            //self.modal.modal();
            if (self.datepickers.length == 0)
                self.loaddatepicker();

            if (self.record.id > 0) {
                self.fetch(self);
            }
        },
        neighbour: function (v) {
            var self = this;
            var b = $(v);
            var row = b.parents('tr');
            var parentid;
            if (row.attr('data-tt-parent-id')) {
                self.new();
                parentid = row.attr('data-tt-parent-id');
                if (!self.el.find($('#' + self.prefix + 'parentid')).length) {
                    self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + "parentid' value='" + parentid + "' />");
                    self.getfields();
                }
                else {
                    $('#' + self.prefix + 'parentid').val(parentid);
                }
                self.loadDynamicLists(parentid);
                self.modal.modal();
            }
        },
        chart: function () {
            
        },
        
        loadDynamicLists: function (v, c) {
            var self = this;
            var controls = [];
            var controlnames = [];
            var fld = {};

            if (!$.isEmptyObject(c)) {
                controlnames = c.split('|');

                $.each(controlnames, function (i, e) {
                    controls.push($('#' + e));
                });
            }
            else {

                self.fields.each(function () {
                    if ($(this).attr('data-type')) {
                        controls.push($(this));
                    }
                });
            }

            $.each(controls, function (index, e) {
                if (!$.isEmptyObject(v)) {
                    var elname = $(this).attr('id');
                    var datatype = $(this).attr('data-type');
                    var path = self.path + '/' + datatype;
                    var data = {};
                    data.id = v;
                    clean.data.post({
                        async: false, url: path, data: clean.data.json.write(data), dataType: 'json',
                        success: function (msg) {
                            var list = msg.data.list;
                            if (list.length > 0) {
                                $('#' + elname + ' option[value]').remove();
                                for (var i = 0; i < list.length; i++) {
                                    $('#' + elname).append("<option value='" + list[i].id + "'>" + list[i].text + "</option>");
                                }
                            }
                            else {
                                $('#' + elname + ' option[value]').remove();
                            }
                            $('#' + elname).val('').change();
                        }
                    });
                }
            });

        }
    };
}
)();