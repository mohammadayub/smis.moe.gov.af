

var clean = window.clean = window.clean || {};
(function () {
    clean.form = function (opt) {
        this.opt = opt = opt || {};
        this.OnInit = window[opt.el.attr('onInit')];
        this.el = opt.el;
        this.path = "";
        this.page = opt.page;
        this.parent = opt.parent || {};
        this.prefix = this.el.attr('prefix') || "ux";
        this.plogicid = this.el.attr('plogicid'); // process logic id
        this.attachrecord = this.el.attr('attachrecord');
        this.record = {};
        this.uploaders = {};
        // NEW CHANGES FOR FILE UPLOAD
        this.uploaders.types = [];
        this.uploaders.keys = [];
        //this.uploaders.photo = {};
        this.uploaders.document = {};
        this.master = {};
        this.tazkira = {};
        this.actions = this.el.find('.div-form-control [action]');
        this.fields = [];
        this.datepickers = [];
        this.modal = {};
        this.attachmentpath = {};
        this.processmodal = {};
        this.grid = {};
        this.grid.template = this.el.find('.form-grid');
        this.grid.table = this.el.attr('id').replace('dv', 'gv');
        this.grid.cols = [];
        this.grid.actions = this.el.find('.div-grid-control').html();
        this.validationrule = {};
        this.orgchart = {};
        this.subforms = [];
        this.children = [];
        this.EncryptedID = this.el.find('#' + this.el.attr('prefix') + 'pageid').val();
        if (this.el.attr('OnBind')) {
            this.OnBind = this.el.attr('OnBind');
        }
        if (this.el.attr('OnNew')) {
            this.OnNew = this.el.attr('OnNew');
        }
        this.init(this.el);
    }
    clean.form.prototype = {
        init: function (opt) {
            var self = this;
            window.CleanForms = window.CleanForms || {};
            window.CleanForms[self.el.attr('id')] = self;

            self.construct();
            //Registering the clean form actions -- Save -- Search -- New
            this.actions.bind('click', function () {
                var act = $(this).attr('action');
                if (self[act]) self[act]();
                return false;
            });

            
            $(window).on('resize', function () {
                if (!$.fn.DataTable.isDataTable('#' + self.grid.table)) {
                    var tableheight = !$('#' + self.grid.table).attr('fullheight') ? 150 : null;
                    var filter = !$('#' + self.grid.table).attr('filter') ? false : true;
                    var opts = {
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
                    };
                    $('#' + self.grid.table).DataTable(opts).columns.adjust();
                }
                else {
                    $('#' + self.grid.table).DataTable().columns.adjust();
                }
            });

            if (!clean.isEmpty(self.OnInit))
                clean.invoke(self.OnInit, self);

            if (self.el.hasClass("read-only")) {
                self.el.find(':text, :radio, :checkbox, input:hidden, select, textarea').not('.no-disabled').attr('disabled', 'disabled');
                self.el.find('.cropControls').remove();
            }
        },
        getactions: function () {
            var self = this;
            self.actions = this.el.find('.div-form-control [action]');
        },
        getfields: function () {
            var self = this;
            self.fields = self.el.find(':text, :radio, :password, :checkbox,input[type="number"], input:hidden , select, textarea').not(":button, :submit, input[static=true]");
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

            if (self.el.attr('category') == 'modal') {
                var modalid = self.prefix + self.el.attr('id') + '_Modal';
                self.el.find('.modal').attr('id', modalid);
                self.modal = $('#' + modalid);

            }

            if (self.el.attr('attachment')) {
                self.attachmentpath = self.el.attr('attachment-path');
                self.el.find('.actions').append('<button type="button" class="btn btn-primary" action="attach" style="float:left;"><i class="icon-attachment position-left"></i>اسناد و ضمایم </button>');
                self.getactions();

            }

            if (self.el.attr('hasprocess')) {
                self.el.find('.actions').append('<button type="button" class="btn btn-primary" action="process" style="float:left; margin-left: 5px;"><i class="icon-loop position-left"></i>طی مراحل </button>');
                self.getactions();
            }

            // When we want to the process function to use a different record id than the page record id itself. ADDED
            if (self.el.attr('hasprocessrecordid')) {
                self.el.append("<input type='hidden' id='toprocessrecordid' value='' />");
            }

            if (self.el.hasClass('sub-form')) {
                if (!$.isEmptyObject(self.el.find('#' + self.prefix + self.el.attr('parentcol')))) {
                    var v = (self.parent.record && self.parent.record.id) ? self.parent.record.id : -1;
                    self.el.append("<input type='hidden' class='auto-gen-hidden search' id='" + self.prefix + self.el.attr('parentcol') + "' value = '" + v + "' /> ");

                }
            }
            self.getfields();
            self.el.find('input.search:text, select.search').after('<div class="form-control-feedback"><i class="icon-search4 text-size-base"></i></div>');
            self.el.find('select').select2({
                placeholder: "--",
                minimumResultsForSearch: 10,
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
            self.el.find('input[type=text][lang]').each(function (i, item) {
                new clean.LanguageInput({
                    el: $(item),
                    lang: $(item).attr('lang')
                });
            });
            

            $('.bar-reader').click(function () {
                $(this).val('');
            });

            if ($.isEmptyObject(self.modal)) {
                self.loaddatepicker();
            }
            self.el.find('.national-id').each(function () {
                var opt = {};
                opt.el = $(this).find('.national-id-input');
                opt.parent = $(this).parent();
                opt.form = self;
                self.tazkira = new clean.Tazkira(opt);
            });
            $(self.grid.template.find('thead').find('th')).each(function (index) {
                self.grid.cols.push($(this).attr('colname'));
            });
            $(self.grid.template).find('table').addClass('table table-bordered table-hover').attr('id', self.grid.table);
            self.el.parents('.panel').append(self.grid.template);

            if (self.el.find('.img-uploader').length) {
                self.el.find('.img-uploader').each(function (i, item) {
                    let cur = $(item);
                    let type = cur.attr('data-type');
                    let ref = cur.attr('data-ref');
                    let curUploader = {};
                    curUploader.el = cur;
                    curUploader.type = type;
                    curUploader.ref = ref;
                    var croppicContainerModalOptions = {
                        cropData: {
                            "UploadType": type
                        },
                        uploadData: {
                            "UploadType": type
                        },
                        uploadUrl: self.path + '/Upload',
                        cropUrl: self.path + '/Crop',
                        downloadUrl: self.path + '/Download',
                        outputUrlId: curUploader.ref,
                        modal: true,
                        imgEyecandy: true,
                        doubleZoomControls: false,
                        rotateControls: false,
                        imgEyecandyOpacity: 0.4,
                        loaderHtml: '<div class="loader bubblingG"><span id="bubblingG_1"></span><span id="bubblingG_2"></span><span id="bubblingG_3"></span></div> ',
                        onBeforeImgUpload: function () {  },
                        onAfterImgUpload: function () {  },
                        onImgDrag: function () {  },
                        onImgZoom: function () {  },
                        onBeforeImgCrop: function () {  },
                        onAfterImgCrop: function () {  },
                        onReset: function () {  },
                        onError: function (errormessage) { console.error('Croppic Uploader Error :' + errormessage); }
                    };
                    curUploader.uploader = new Croppic(cur.attr('id'), croppicContainerModalOptions);
                    curUploader.required = !cur.hasClass('optional');
                    self.uploaders.types.push(curUploader);
                    self.uploaders.keys.push(ref);
                });
            }
            if (self.el.find('.file-attachment').length) {
                var el = self.el.find('.file-attachment');
                self.initiateFileUpload(el);
            }


            if ($('#' + self.grid.table).attr('organogram') == 'true') {
                var bttntext = $('#' + self.grid.table).attr('action-button');
                self.el.find('.div-form-control').find('.form-group').append('<button type="button" class="btn btn-primary" action="chart" showongrid="true"><i class="icon-tree6 position-left"></i>' + bttntext + '</button>');
                self.getactions();
            }
            self.validationrule = self.validation();

            if (self.el.hasClass("sub-form") && !$.isEmptyObject(self.parent.record)) {
                self.search();
            }

        },
        initiateFileUpload: function (e) {
            var self = this;
            self.uploaders.document = e.fileinput({
                uploadUrl: 'adsfasdfadfaf',
                showCancel: false,
                browseLabel: '',
                browseClass: 'btn btn-primary btn-icon',
                removeLabel: '',
                uploadLabel: 'aaa',
                browseIcon: '<i class="icon-plus22"></i> ',
                removeClass: 'btn btn-remove btn-icon',
                removeIcon: '<i class="icon-cancel-square"></i> ',
                layoutTemplates: {
                    caption: '<div tabindex="-1" class="form-control file-caption {class}">\n' + '<span class="icon-file-plus kv-caption-icon"></span><div class="file-caption-name"></div>\n' + '</div>'
                },
                initialCaption: "فایل انتخاب نگردیده"
            }).on('fileselect', function (event, numFiles, label) {
                setTimeout(function () {
                    self.el.valid();
                }, 100);
                var formData = new FormData(self.el[0]);
                $.ajax({
                    url: self.path + '/Upload',
                    data: formData,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },
                    context: document.body,
                    cache: false,
                    contentType: false,
                    processData: false,
                    type: 'POST'
                }).always(function (data) {
                    if (data.status > 0) {
                        if (!self.el.find($('#' + self.prefix + 'path')).length)
                            self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + "path' value='" + data.url + "' />");
                        else
                            self.el.find($('#' + self.prefix + 'path')).val(data.url);
                        self.getfields();
                        clean.widget.success(data.text, data.description);
                        self.el.find('.file-caption-name').removeClass('error-upload');
                    }
                    else {
                        self.el.find('.file-caption-name').addClass('error-upload');
                        clean.widget.error(data.text, data.description);
                    }
                    self.el.find('.file-caption-name').html(data.text + "، " + data.description);
                });
            });
        },
        save: function () {
            var self = this;
            if (self.el.hasClass('sub-form')) {
                if (!self.el.find($('#' + self.prefix + self.el.attr('parentcol'))).length) {
                    self.el.append("<input type='hidden' class='auto-gen-hidden search' id='" + self.prefix + self.el.attr('parentcol') + "' value = '" + self.parent.record.id + "' /> ");
                }
            }

            self.getfields();
            var _tazkira = false;
            if (!$.isEmptyObject(self.tazkira)) {
                _tazkira = self.tazkira.validate();
                if (_tazkira) {
                    self.tazkira.el.val(self.tazkira.val());
                }
            }
            else {
                _tazkira = true;
            }

            var _photovalid = true;
            if (self.uploaders.types.length > 0) {
                for (var i = 0; i < self.uploaders.types.length; i++) {
                    let cur = self.uploaders.types[i];
                    if (cur.required) {
                        _photovalid = cur.uploader.validate();
                    }
                }
            }
            if (!_photovalid) {
                clean.widget.error('عکس اپلود نگردیده', 'کاربر محترم، لطفاً عکس‌های مورد ضرورت را انتخاب نماید. دیتا بدون عکس ثبت نمیگردد');
            }
            if (self.el.valid() && _photovalid && _tazkira) {
                var path = self.path + '/save';
                var data = {};
                self.fields.each(function () {
                    var fld = $(this);
                    var col = fld.attr('id').substring(self.prefix.length);
                    Object.defineProperty(data, col.toString(), { value: fld.is(':checkbox') ? fld.is(':checked') : fld.val(), enumerable: true });
                });
                clean.data.post({
                    async: false, url: path, data: clean.data.json.write(data), dataType: 'json',
                    success: function (msg) {
                        if (msg.status > 0) {
                            if (typeof(msg.data.list) != "object") {
                                clean.widget.success(msg.text, msg.description);
                            }
                            else {
                                var list = msg.data.list;
                                clean.widget.success(msg.text, msg.description);
                                self.bindtogrid(list);
                            }
                        }
                        else {
                            clean.widget.warn(msg.text, msg.description);
                        }
                    }
                });
            }
        },
        search: function (r) {
            var self = r || this;
            var path = self.path + '/search';


            if (self.el.hasClass('sub-form') && !$.isEmptyObject(self.parent.record)) {
                if (!self.el.find($('#' + self.prefix + self.el.attr('parentcol'))).length) {
                    self.el.append("<input type='hidden' class='auto-gen-hidden search' id='" + self.prefix + self.el.attr('parentcol') + "' value = '" + self.parent.record.id + "' /> ");
                }
                self.getfields();
            }
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
            clean.data.post({
                async: false, url: path, data: clean.data.json.write(data), dataType: 'json',
                success: function (msg) {
                    if (msg.status > 0) {
                        var list = msg.data.list;
                        if (list && list.length) {
                            if (r) {
                                self.bindtoform(list[0]);
                            }
                            else {
                                self.bindtogrid(list);
                            }
                        }
                        else {
                            if (self.el.hasClass('main-form')) {
                                clean.widget.warn("تشریحات", "ریکارد پیدا نشد!");
                                for (let i in self.page.subforms) {
                                    let cur = self.page.subforms[i];
                                    cur.cleanFields();
                                    cur.clearGrid();
                                }
                            } else {
                                self.clearGrid();
                            }
                        }
                    }
                    else {
                        clean.widget.warn(msg.text, msg.description);
                    }
                }
            });
        },
        new: function (opt) {
            let self = this;
            self.cleanFields(opt);
            self.record = {};
            if (self.OnNew && window[self.OnNew]) {
                window[self.OnNew](self);
            }
        },
        cleanFields: function (opt) {
            var self = this;
            self.fields.each(function () {
                var fld = $(this);
                if (fld.attr('placeholder') =='درج نگردیده') fld.attr('placeholder', '');

                if (fld.is(':checkbox'))
                    fld.prop('checked', false);
                else if (fld.attr('default') && fld.attr('default').toString().length) {
                    fld.val(fld.attr('default')).change();
                }
                else if (!fld.attr('default')) {
                    try {
                        fld.val(null).change();
                    } catch (e) {
                        console.log(e);
                    }
                }
            });
            self.el.find('.auto-gen-hidden').remove();
            
            for (var i = 0; i < self.uploaders.types.length; i++) {
                let cur = self.uploaders.types[i];
                cur.uploader.destroy();
                cur.uploader.init();
            }
            if (!$.isEmptyObject(self.uploaders.document)) {
                if (self.el.find('.file-attachment').length) {
                    self.el.find('.file-attachment').attr('required', 'required');
                    self.el[0].reset();
                }
            }
            self.getfields();
        },
        test: function () {
            alert('test');
        },
        configure: function (opt) {
        },
        fetch: function (r) {
            var self = this;
            if (self.el.hasClass('sub-form') && Number(self.el.attr('attach-parent')) == 1) {
                r.record[self.el.attr('parentcol')] = self.parent.record.id;
            }
            self.search(r);
        },
        bindtoform: function (d) {
            var self = this;
            self.record = d;
            self.cleanFields();
            for (var key in d) {
                var control = $('#' + (self.prefix + key).toLowerCase());
                if (control.attr('nobinding') == "true")
                    d[key] = "";
                if (clean.isEmpty(d[key])) {
                    control.attr('placeholder', 'درج نگردیده')
                }
                else {
                    if (control.is(':checkbox')) {
                        control.prop('checked', d[key]);
                    }
                    else if (!control.attr('default')) {
                        control.val(d[key]);
                        try {
                            control.trigger('change');
                        }
                        catch (e) {
                            console.error(e);
                        }
                    }
                }
                if (control.hasClass("Miladi") && control.val() != "") {
                    control.MdPersianDateTimePicker('setDate', new Date(d[key]));
                }
                if (key == 'id' || key == 'parentid') {
                    if (!self.el.find($('#' + self.prefix + key)).length) {
                        self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + key + "' value='" + d[key] + "' />");
                        self.getfields();
                    }
                }


                if (key == 'nid') {
                    // In cases where nid is present in recieved data but the form doesnt have the nid portion and self.Tazkira is not initialized self.tazkira.val(d[key]); will throw error. ADDED
                    if (!jQuery.isEmptyObject(self.tazkira))
                        self.tazkira.val(d[key]);
                }

                if (self.uploaders.keys.includes(self.prefix + key.toLowerCase())) {
                    if (d[key] != null) {
                        for (var i = 0; i < self.uploaders.types.length; i++) {
                            let cur = self.uploaders.types[i];
                            if (cur.ref == self.prefix + key.toLowerCase()) {
                                cur.uploader.bind(d[key]);
                            }
                        }
                    }
                }


                if (key == 'path') {
                    if (!self.el.find($('#' + self.prefix + key)).length)
                        self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + "path' value='" + d[key] + "' />");
                    self.el.find('.file-attachment').removeAttr('required');
                }
            }

            if (self.el.attr('subform')) {
                if (!self.subform) {
                    self.subform = [];
                    let path = self.path + "/GetSubForms?pageid="+self.page.parameter('p');
                    clean.data.post({
                        async: false, url: path, data: clean.data.json.write({}), dataType: 'json',
                        success: function (msg) {
                            if (msg.status > 0) {
                                var list = msg.data.list;
                                if (list && list.length) {
                                    $.each(list, function (i, item) {
                                        self.subform.push({
                                            id: item.id,
                                            path: item.path
                                        });
                                    }); 
                                }
                            }
                            else {
                                clean.widget.warn(msg.text, msg.description);
                            }
                        }
                    });
                }
                $('.dependent-screens').empty();
                
                self.children = self.el.attr('subform').split('|');
                $.each(self.subform, function (i, cur) {
                    self.page.loadsubscreens(cur.path,cur.id);
                });

                var container = $('body');
                $('html,body').animate({
                    scrollTop: self.el.offset().top - container.offset().top + container.scrollTop() - 140
                });
            }

            // Bind the toprocessrecordid. ADDED
            if (self.el.attr('hasprocessrecordid') && self.record) {
                $("#toprocessrecordid").val(self.record.toprocessrecordid);
            }


            if (self.OnBind && window[self.OnBind]) {
                window[self.OnBind](self);
            }
            $('.sub-form').each(function () {
                var form = $(this);
                if (form.attr('parent') == self.el.attr('id')) {
                    $.each(self.page.subforms, function (index, sub) {
                        if (sub.el.attr('id') == form.attr('id')) {
                            sub.cleanFields();
                            sub.clearGrid();
                            sub.search();
                        }
                    });
                }
            });

            if ($('.viewonly').length) {
                self.el.find(':text, :radio, :checkbox, input:hidden, select, textarea').not('.no-disabled').attr('disabled', 'disabled');
                $('.cropControls').remove();
            }
            if (self.el.hasClass('read-only')) {
                window.setTimeout(function () {
                    self.el.find('.cropControls').remove();
                }, 300);
            }
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
        download: function (e) {
            var self = this;
            var file = {};
            file.Name = e;
            var xhr = new XMLHttpRequest();
            xhr.open('POST', self.path + '/Download', true);
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
            xhr.setRequestHeader("Content-Type", 'application/json; charset=utf-8');
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    var blob = new Blob([xhr.response], {
                        type: xhr.getResponseHeader("Content-Type")
                    });
                    var url = window.URL.createObjectURL(blob);
                    window.open(url, url, "directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0,scrollbars=no,resizable=no,width=400,height=400");
                }
            }
            xhr.responseType = "arraybuffer";
            xhr.send(JSON.stringify(file));
        },
        bindtogrid: function (d) {
            var self = this;
            var row = "";
            var rowclick = $('#' + self.grid.table).attr('bindonclick') ? 'fetch-record' : '';

            $('#' + self.grid.table).DataTable().clear().draw().destroy();
            // colum to show in popu as remarks
            var RemarksColumn = $('#' + self.grid.table).attr('RemarksColumn') ? $('#' + self.grid.table).attr('RemarksColumn') : '';

            // TotalColumnName use to sum result of this column with each others
            var TotalColumnName = $('#' + self.grid.table).attr('TotalColumnName') ? $('#' + self.grid.table).attr('TotalColumnName') : '';
            var Total = 0;
            var TotalIndex = 0;

            $.each(d, function (ind, ob) {
                var column = "";
                for (var i = 0; i < self.grid.cols.length; i++) {
                    var colname = self.grid.cols[i].toLowerCase();

                    for (var key in ob) {
                        if (key.toLowerCase() == colname) {

                            if (colname != 'path') {

                                var va = ob[key];
                                if (clean.isEmpty(ob[key]))
                                    va = '--';
                                if (colname == RemarksColumn.toLowerCase()) {
                                    if (ob[key] != null) {
                                        var temp = '<a class="link" data-toggle="tooltip"  data-trigger="hover" data-placement="bottom" title="$Text">نمایش</a>'
                                        column = column + "<td style='background - color: #123054' col='" + key.toLowerCase() + "'>" + temp.replace('$Text', ob[key]) + "</td>";
                                    }
                                    else {
                                        column = column + "<td col='" + key.toLowerCase() + "'></td>";
                                    }
                                } else {
                                    column = column + "<td col='" + key.toLowerCase() + "' data='" + va + "'>" + va + "</td>";
                                }

                                if (colname == TotalColumnName.toLowerCase()) {
                                    Total += va;
                                    TotalIndex = i;
                                }

                                
                            }
                            else if (colname == 'path') {

                                var temp = '<button type="button" downloadpath="$path" class="btn-link download-on-click"><i class="icon-download position-left"></i>دریافت فایل</button>'
                                column = column + "<td col='" + key.toLowerCase() + "'>" + temp.replace('$path', ob[key]) + "</td>";
                            }
                            //else if (colname == 'remarks') {
                            //    if (ob[key] != null) {
                            //        var temp = '<a class="link" data-popup="tooltip" data-trigger="hover" data-placement="bottom" title="$Text">نمایش</a>'
                            //        column = column + "<td col='" + key.toLowerCase() + "'>" + temp.replace('$Text', ob[key]) + "</td>";
                            //    }
                            //    else {
                            //        column = column + "<td col='" + key.toLowerCase() + "'></td>";
                            //    }
                            //}

                            

                        }
                    }
                    if (colname == 'singleaction') {
                        var temp = '<td class="text-center">$content</td>';

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
                    if (colname == 'multiaction') {
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
            // use to create a stracture to add total in grid
            if (TotalColumnName != '') {
                var TotalColumns = "";
                if (TotalIndex > 0) {
                    TotalColumns += "<td style='border:none !important;font-weight: bold !important;'>مجموع&nbsp;:</td>";
                }
                for (var i = 1; i < TotalIndex; i++) {
                    TotalColumns += "<td style='border:none !important'></td>";
                }
                TotalColumns += "<td style='border:none !important;font-weight: bold !important;' col='" + TotalColumnName + "' data='" + Total + "'>" + Total + "</td>";
                if (self.grid.cols.length > TotalIndex) {
                    for (var i = TotalIndex + 1;i <self.grid.cols.length; i++ ) {
                        TotalColumns += "<td style='border:none !important'></td>";
                    }
                }
                row = row + "<tr role='row'>" + TotalColumns + "</tr>";
            }
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

            if ($('#' + self.grid.table).attr('type') == 'treetable') {
                $('#' + self.grid.table).treetable('destroy');
                $('#' + self.grid.table).treetable({ expandable: true });
                $('#' + self.grid.table).treetable('expandAll');
                $('#' + self.grid.table).parents('.dataTables_scrollBody').css({ overflow: 'visible' });
            }
            $('[data-popup="tooltip"]').tooltip({ template: '<div class="tooltip"><div class="bg-primary"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div></div>', trigger: 'click' });
            $('#' + self.grid.table).find('tr').click(function () {
                $(this).siblings().removeClass('row-selected');
                $(this).addClass('row-selected');
            });
            $('#' + self.grid.table).find('.fetch-record').click(function () {
                self.record = {};
                self.record.id = $(this).attr('data');
                self.fetch(self);
            });

            $('#' + self.grid.table).find('.grid-action').click(function () {
                var act = $(this).attr('action');
                if (self[act]) self[act](this);
                return false;
            });


            $('#' + self.grid.table).find('.download-on-click').click(function () {
                self.download($(this).attr('downloadpath'));
            });

            $.each(self.actions, function (i, v) {
                var el = $(v);
                if (el.attr('showongrid'))
                    $('#' + self.grid.table + '_wrapper').find('.dataTables_filter').append(el.css({ 'float': 'left', 'margin-right': '5px' }));
            });

            if (!$('#' + self.grid.table).attr('ignoreinitialformbind'))
                self.bindtoform(d[0]);

        },
        attach: function () {
            var self = this;
            if ($.isEmptyObject(self.EncryptedID))
                self.EncryptedID = $('#' + self.prefix + 'pageid').val();

            if (!$.isEmptyObject(self.record)) {
                var patharray = self.attachmentpath.split('_');
                path = "/" + patharray.join('/') + "/Get";// "/Document/Document/Get";

                var modalid = self.prefix + self.el.attr('id') + '_Modal';
                if ($.isEmptyObject(self.modal)) {
                    var modal = '<div id="' + modalid + '" class="modal fade"><div class="modal-dialog modal-lg"><div class="modal-content"></div></div></div>';
                    var close = '<button type="button" class="btn btn-link close-bttn" data-dismiss="modal"><i class="icon-close2 position-left"></i>صرف نظر</button>'
                    $('.dependent-screens').append(modal);
                    self.modal = $('#' + modalid);
                    var data = { ScreenID: self.EncryptedID };
                    clean.data.get({
                        async: false, url: path, data: data, dataType: 'html',
                        success: function (msg) {
                            var html = msg;
                            self.modal.find('.modal-content').html(html);
                            var subform = {};
                            subform.el = $('#' + $(html).find('form').attr('id'));
                            subform.el.attr('parent', self.el.attr('id'));
                            var items = self.path.split('/');
                            subform.el.append("<input type='hidden'  id='" + subform.el.attr('prefix') + "module' value='" + items[1] + "' default='" + items[1] + "' />");
                            subform.el.append("<input type='hidden'  id='" + subform.el.attr('prefix') + "item' value='" + items[2] + "' default='" + items[2] + "' />");
                            subform.el.find('.actions').append(close);
                            subform.parent = self;
                            subform.page = self.page;
                            if (subform.el.hasClass('page-component')) {
                                if (subform.el.attr('type') == 'form' && subform.el.hasClass('sub-form')) {
                                    var sub = new clean[subform.el.attr('type')](subform);
                                    self.page.subforms.push(sub);
                                }
                            }
                        }
                    });
                }
                self.modal.modal();
                self.modal.on('shown.bs.modal', function () {
                    self.modal.find('table').DataTable().columns.adjust();
                });
            }
            else {
                var title = 'فورم $formname خالی میباشد';
                var des = 'برای اینکه اسناد و ضمایم مربوط به فورم $formname را مشاهده نمائید، لطفاً ریکارد این فورم را مشخص سازید';
                var heading = self.el.parents('.panel-body').siblings('.panel-heading').find('h1');
                title = title.replace('$formname', $(heading).text());
                des = des.replace('$formname', $(heading).text());
                clean.widget.error(title, des);
            }
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
            self.cleanFields();

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
            self.modal.modal();
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
                self.cleanFields();
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
            var self = this;
            var table = $('#' + self.grid.table);
            var modal_title = table.attr('modal-title');
            var modal_description = table.attr('modal-description');
            if (table.attr('node-title') && table.attr('node-sub-title') && table.attr('node-description')) {
                var modalid = self.el.attr('id') + '_chart_Modal';
                var modal = '<div id="' + modalid + '" class="modal fade"><div class="modal-dialog modal-full"><div class="modal-content"><div class="panel panel-flat" ><div class="panel-heading"><h1 class="panel-title">' + modal_title + '</h1></div><div class="panel-body" style="padding-bottom: 5px; "><legend class="text-bold">' + modal_description + '</legend><fieldset class="content-group"><div id="chart-container"></div><hr /></fieldset><div class="row"><div class="col-md-12 action-bttns" style="padding:5px;"><button type="button" class="btn btn-link close-bttn" data-dismiss="modal"><i class="icon-close2 position-left"></i>صرف نظر</button></div></div></div></div></div></div></div>';
                $('.dependent-screens').append(modal);
                $('#' + modalid).modal();
                var nodeTemplate = function (data) {
                    return `
                        <div class="title">${data.title}</div>
                        <div class="content">
                            <div class="subtitle">${data.subtitle}</div>
                            <div class="description">${data.description}</div>
                        </div>
                      `;
                };
                var rows = [];
                var node = {};
                node.title = $('#' + self.grid.table).attr('node-title').split(',');
                node.subtitle = $('#' + self.grid.table).attr('node-sub-title').split(',');
                node.description = $('#' + self.grid.table).attr('node-description').split(',');
                var tbl_html = table.find('tbody');
                $(tbl_html).find('tr').each(function (i, v) {
                    var tr = $(v);
                    var row = {};
                    row.id = tr.attr('data-tt-id');
                    row.parent = tr.attr('data-tt-parent-id');
                    row.title = "";
                    row.subtitle = "";
                    row.description = "";
                    $.each(node.title, function (i, k) {
                        var key = k.toLowerCase();
                        var td = tr.find('[col="' + key + '"]');
                        row.title = row.title + ' ' + $(td).attr('data');
                    });
                    $.each(node.subtitle, function (i, k) {
                        var key = k.toLowerCase();
                        var td = tr.find('[col="' + key + '"]');
                        row.subtitle = row.subtitle + ' ' + $(td).attr('data');
                    });
                    $.each(node.description, function (i, k) {
                        var key = k.toLowerCase();
                        var td = tr.find('[col="' + key + '"]');
                        row.description = row.description + ' ' + $(td).attr('data');
                    });
                    rows.push(row);
                });
                var datascource = {};
                if (rows.length > 0) {
                    $.each(rows, function (i, v) {

                        if (v.parent == 0) {
                            datascource.title = v.title;
                            datascource.subtitle = v.subtitle;
                            datascource.description = v.description;
                            datascource.children = self.getchildren(v, rows);
                        }
                    });
                }

                if ($.isEmptyObject(self.orgchart)) {
                    self.orgchart = $('#chart-container').orgchart({
                        'data': datascource,
                        'nodeContent': 'title',
                        'nodeTemplate': nodeTemplate,
                        'exportButton': true,
                        'exportFilename': 'OrgChart',
                        'pan': true,
                        'zoom': true
                    });
                    $('#chart-container').css('overflow-y', 'auto');
                    $('#chart-container').css('max-height', $(window).height() * 0.7);
                    $('#chart-container').css('height', $(window).height() * 0.7);
                    if ($('#chart-container').find('.oc-export-btn').length && !$('.action-bttns').find('.oc-export-btn').length)
                        $('.action-bttns').append($('.oc-export-btn').addClass('btn btn-primary').prepend('<i class="icon-tree6 position-left"></i>'));


                }
                else {
                    self.orgchart.init({ 'data': datascource });
                    $('#chart-container .oc-export-btn').remove();
                }
            }
            else {
                clean.widget.error('اشتباه', 'لطفاً تمام مشخصات مورد ضرورت چارت را واضح سازید');
            }


        },
        getchildren: function (o, rows) {
            var self = this;
            var result = [];
            $.each(rows, function (i, v) {
                var obj = {};
                if (v.parent == o.id) {
                    obj.title = v.title;
                    obj.subtitle = v.subtitle;
                    obj.description = v.description;
                    obj.children = self.getchildren(v, rows);
                    result.push(obj)
                }
            });
            return result;
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
                })
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



        },
        child: function (v) {
            var self = this;
            var b = $(v);
            var row = b.parents('tr');
            var parentid = row.attr('data');
            self.cleanFields();
            if (!self.el.find($('#' + self.prefix + 'parentid')).length) {
                self.el.append("<input type='hidden' class='auto-gen-hidden' id='" + self.prefix + "parentid' value='" + parentid + "' />");
                self.getfields();
            }
            else {
                $('#' + self.prefix + 'parentid').val(parentid);
            }
            self.loadDynamicLists(parentid);
            self.modal.modal();
        },
        remove: function (v) {
            var self = this;
            var b = $(v);
            var row = b.parents('tr');
            var recordid = row.attr('data');
            var path = self.path + '/remove';
            var data = {};
            data.id = recordid;
            clean.data.post({
                async: false, url: path, data: clean.data.json.write(data), dataType: 'json',
                success: function (msg) {
                    //var list = msg.data.list;
                    if (msg.status > 0) {
                        self.bindtogrid(msg.data.list);
                        clean.widget.success(msg.text, msg.description);
                    }
                    else {

                        clean.widget.error(msg.text, msg.description);
                    }
                }
            });
        },
        process: function () {
            var self = this;
            var sub = {};
            var ScreenID;
            if (!self.el.hasClass('sub-form'))
                ScreenID = self.page.parameter('p');
            else
                ScreenID = self.el.find('#' + self.prefix + 'pageid').val();
            // Use the record to be processed instead of the record id of the form. ADDED
            if (self.el.attr('hasprocessrecordid'))
                self.record.id = $('#toprocessrecordid').val();

            if (self.record.id === undefined || self.record.id === null || $('#'+ self.prefix +'haslogic').val() == "true") {
                clean.widget.error("کوشش خلاف اصول", "ریکارد انتخاب شده واجد شرایط به طی مراحل نیست");
            }
            else {
                if (!$.isEmptyObject(self.record)) {

                    if (self.el.attr('plogicid'))
                        path = "/Document/Process?p=" + ScreenID + "&id=" + self.el.attr('plogicid') + "&recordid=" + self.record.id;
                    else
                    //path = "/Document/Process?p=" + ScreenID;
                    path = "/Document/Process?p=" + ScreenID + "&recordid=" + self.record.id
                    var modalid = self.prefix + self.el.attr('id') + '_Modal_Process';
                    var showModal = true;
                    if ($.isEmptyObject(self.processmodal)) {
                        var modal = '<div id="' + modalid + '" class="modal fade"><div class="modal-dialog modal-lg"><div class="modal-content"></div></div></div>';
                        var close = '<button type="button" class="btn btn-link close-bttn" data-dismiss="modal"><i class="icon-close2 position-left"></i>صرف نظر</button>'
                        $('.dependent-screens').append(modal);
                        self.processmodal = $('#' + modalid);

                        clean.data.get({
                            async: false, url: path, data: clean.data.json.write(), dataType: 'html',
                            success: function (msg) {

                                if (!clean.isJsonString(msg)) {
                                    var html = msg;
                                    self.processmodal.find('.modal-content').html(html);
                                    var subform = {};
                                    subform.el = $('#' + $(html).find('form').attr('id'));
                                    subform.el.attr('parent', self.el.attr('id'));
                                    var items = self.path.split('/');
                                    subform.el.find('.actions').append(close);
                                    subform.parent = self;
                                    subform.page = self.page;
                                    if (subform.el.hasClass('page-component')) {
                                        if (subform.el.attr('type') == 'form' && subform.el.hasClass('sub-form')) {
                                            sub = new clean[subform.el.attr('type')](subform);
                                            self.page.subforms.push(sub);
                                            sub.loadDynamicLists(ScreenID);
                                            sub.search();
                                        }
                                    }
                                }
                                else {
                                    var x = JSON.parse(msg);
                                    showModal = false;
                                    clean.widget.warn(x.text, x.description);
                                }

                            }
                        });
                    }


                    if (showModal) {
                        self.processmodal.modal();
                        self.processmodal.on('shown.bs.modal', function () {
                            $(this).find('table').DataTable().columns.adjust();
                        });
                    }

                    self.processmodal = {};
                }
                else {
                    var title = 'فورم $formname خالی میباشد';
                    var des = 'برای اینکه طی مراحل اسناد مربوط به فورم $formname را مشاهده نمائید، لطفاً ریکارد این فورم را مشخص سازید';
                    var heading = self.el.parents('.panel-body').siblings('.panel-heading').find('h1');
                    title = title.replace('$formname', $(heading).text());
                    des = des.replace('$formname', $(heading).text());
                    clean.widget.error(title, des);
                }
            }
        },

        export: function () {

            var self = this;
            if (!(self.record.id === undefined)) {

                self.getfields();
                var path = self.path + '/export';
                var data = {};

                self.fields.each(
                    function () {
                        var fld = $(this);
                        var col = fld.attr('id').substring(self.prefix.length);
                        Object.defineProperty(data, col.toString(), { value: fld.val().toString(), enumerable: true });
                    }
                );

                clean.data.getFile({ url: path, data: data });
            }

            else {
                clean.widget.error('کوشش خلاف اصول', 'جهت دریافت فورم لطفا ریکارد موردنظر را مشخص نمائید');
                
            }

        },


        print: function () {
            var self = this;
            var sub = {};
            var ScreenID;
            if (!self.el.hasClass('sub-form'))
                ScreenID = self.page.parameter('p');
            else
                ScreenID = self.el.find('#pageid').val();
            if (self.record.id === undefined || self.record.id === null) {
                clean.widget.error("کوشش خلاف اصول", "ریکارد انتخاب شده واجد شرایط به پرنت نمی باشد");
            }
            else {
                if (!$.isEmptyObject(self.record)) {

                    var patharray = self.path.split('/');
                    var last = patharray.pop();
                    patharray.push('Prints');
                    var printpath = patharray.join('/');
                    window.open(printpath + "?recordid=" + self.record.id + '&&ts=' + (new Date().getTime()), 'Slips', 'toolbar=no,scrollbars=yes,resizable=no,top:200,left=200,width:1200px,height=800px');
                }
                else {
                    var title = 'فورم $formname خالی میباشد';
                    var des = 'برای پرنت اسناد مربوط به فورم $formname ، لطفاً ریکارد این فورم را مشخص سازید';
                    var heading = self.el.parents('.panel-body').siblings('.panel-heading').find('h1');
                    title = title.replace('$formname', $(heading).text());
                    des = des.replace('$formname', $(heading).text());
                    clean.widget.error(title, des);
                }
            }
        }
    };
}
)();

(function () {
    clean.LanguageInput = function (opt) {
        this.el = opt.el;
        this.lang = this.el.attr('lang');
        this.specialCharsEnabled = this.el.get(0).attributes.hasOwnProperty('special-chars');
        this.UpperCase = this.el.get(0).attributes.hasOwnProperty('uppercase');
        this.NumberShift = this.el.get(0).attributes.hasOwnProperty('numbershift');
        this.AllowedChars = [];
        if (this.el.get(0).attributes.hasOwnProperty('allowedchars')) {
            var acs = this.el.attr('allowedchars');
            this.AllowedChars = acs.split(',').map(e => e.trim()).filter(e => e.length > 0);
        }
        this.init();
        this.el.data('LangInput', this);
    }
    clean.LanguageInput.prototype = {
        NumbersKeyCodes : {
            48: '0',49: '1',50: '2', 51: '3', 52: '4',
            53: '5', 54: '6', 55: '7', 56: '8', 57: '9',
            96: '0',97: '1',98: '2',99: '3',100: '4',
            101: '5',102: '6',103: '7',104: '8',105: '9'
        },
        NumbersShiftKeyCodes: {
            48: { ps: '(', dr: '(', en: ')' },
            49: { ps: '!', dr: '!', en: '!' },
            50: { ps: '٬', dr: '٬', en: '@' },
            51: { ps: '٫', dr: '٫', en: '#' },
            52: { ps: '؋', dr: 'ریال', en: '$' },
            53: { ps: '٪', dr: '٪', en: '%' },
            54: { ps: '×', dr: '×', en: '^' },
            55: { ps: '»', dr: '،', en: '&' },
            56: { ps: '«', dr: '*', en: '*' },
            57: { ps: ')', dr: ')', en: '(' },
        },
        IgnoreKeys: {
            8: 'BACK-SPACE',
            9: 'TAB',
            32: 'SPACE',
            37: 'LEFT-ARROW',
            38: 'UP-ARROW',
            39: 'RIGHT-ARROW',
            40: 'DOWN-ARROW'
        },
        InvalidCharacters: [':'],
        KeyCodes: {
            65: {ps : 'ش',dr : 'ش',en : 'a'},
            66: {ps : 'ذ',dr : 'ذ',en : 'b'},
            67: {ps : 'ز',dr : 'ز',en : 'c'},
            68: {ps : 'ی',dr : 'ی',en : 'd'},
            69: {ps : 'ث',dr : 'ث',en : 'e'},
            70: {ps : 'ب',dr : 'ب',en : 'f'},
            71: {ps : 'ل',dr : 'ل',en : 'g'},
            72: {ps : 'ا',dr : 'ا',en : 'h'},
            73: {ps : 'ه',dr : 'ه',en : 'i'},
            74: {ps : 'ت',dr : 'ت',en : 'j'},
            75: {ps : 'ن',dr : 'ن',en : 'k'},
            76: {ps : 'م',dr : 'م',en : 'l'},
            77: {ps : 'ړ',dr : 'پ',en : 'm'},
            78: {ps : 'د',dr : 'د',en : 'n'},
            79: {ps : 'خ',dr : 'خ',en : 'o'},
            80: {ps : 'ح',dr : 'ح',en : 'p'},
            81: {ps : 'ض',dr : 'ض',en : 'q'},
            82: {ps : 'ق',dr : 'ق',en : 'r'},
            83: {ps : 'س',dr : 'س',en : 's'},
            84: {ps : 'ف',dr : 'ف',en : 't'},
            85: {ps : 'ع',dr : 'ع',en : 'u'},
            86: {ps : 'ر',dr : 'ر',en : 'v'},
            87: {ps : 'ص',dr : 'ص',en : 'w'},
            88: {ps : 'ط',dr : 'ط',en : 'x'},
            89: {ps : 'غ',dr : 'غ',en : 'y'},
            90: { ps: 'ظ', dr: 'ظ', en: 'z' },

            174: { ps: 'چ', dr: 'چ', en: ']' },
            175: { ps: 'ج', dr: 'ج', en: '[' },
            219: { ps: 'چ', dr: 'چ',en : '['},
            221: { ps: 'ج', dr: 'ج',en : ']'},

            58: { ps: 'ک', dr: 'ک', en: ';' },
            59: { ps: 'ک', dr: 'ک', en: ';' },
            222: { ps: 'ګ', dr: 'گ', en: '\'' },

            188: {ps : 'و',dr : 'و',en : ','},
            62: { ps: 'و', dr: 'و', en: ',' },
            190: { ps: 'ږ', dr: '.', en: '.' },

            220: { ps: 'پ', dr: 'پ', en: '\\' },
            
        },
        ShiftKeyCodes: {
            65: { ps: 'ښ', dr: 'ؤ', en: 'A' },
            66: { ps: '‌', dr: '‌', en: 'B' },
            67: { ps: 'ژ', dr: 'ژ', en: 'C' },
            68: { ps: 'ي', dr: 'ي', en: 'D' },
            69: { ps: 'ٍ', dr: '', en: 'E' },
            70: { ps: 'پ', dr: 'إ', en: 'F' },
            71: { ps: 'أ', dr: 'أ', en: 'G' },
            72: { ps: 'آ', dr: 'آ', en: 'H' },
            73: { ps: 'ّ', dr: '', en: 'I' },
            74: { ps: 'ټ', dr: 'ة', en: 'J' },
            75: { ps: 'ڼ', dr: '»', en: 'K' },
            76: { ps: 'ة', dr: '«', en: 'L' },
            77: { ps: 'ؤ', dr: 'ء', en: 'M' },
            78: { ps: 'ډ', dr: 'ٔ', en: 'N' },
            79: { ps: 'ځ', dr: ']', en: 'O' },
            80: { ps: 'څ', dr: '[', en: 'P' },
            81: { ps: 'ْ', dr: 'ْ', en: 'Q' },
            82: { ps: 'ً', dr: 'ً', en: 'R' },
            83: { ps: 'ۍ', dr: 'ئ', en: 'S' },
            84: { ps: 'ُ', dr: 'ُ', en: 'T' },
            85: { ps: 'َ', dr: 'َ', en: 'U' },
            86: { ps: 'ء', dr: 'ٰ', en: 'V' },
            87: { ps: 'ٌ', dr: 'ٌ', en: 'W' },
            88: { ps: 'ئ', dr: 'ط', en: 'X' },
            89: { ps: 'ِ', dr: 'ِ', en: 'Y' },
            90: { ps: 'ئ', dr: 'ك', en: 'Z' },

            174: { ps: '[', dr: '{', en: '}' },
            175: { ps: ']', dr: '}', en: '{' },
            219: { ps: ']', dr: '}', en: '{' },
            221: { ps: '[', dr: '{', en: '}' },

            58: { ps: ':', dr: ':', en: ':' },
            59: { ps: ':', dr: ':', en: ':' },
            222: { ps: 'گ', dr: '؛', en: '"' },

            188: { ps: '،', dr: '>', en: '<' },
            62: { ps: '،', dr: '>', en: '<' },
            190: { ps: '.', dr: '<', en: '>' },

            220: { ps: '*', dr: '|', en: '|' },

        },

        init: function () {
            this.el.on('keydown', {self : this}, this.KeyDownPressed);
        },
        KeyDownPressed: function (e) {
            var self = e.data['self'];
            var charCode = e.keyCode || e.charCode;
            if (self.IgnoreKeys.hasOwnProperty(charCode)) {
                return true;
            }
            else if (e.ctrlKey) {
                return true;
            }
            else if (e.shiftKey && self.ShiftKeyCodes[charCode]) {
                let v = self.ShiftKeyCodes[charCode][self.lang];
                if (self.specialCharsEnabled) {
                    self.insertAtCursor(this, v);
                }
                else {
                    if (self.AllowedChars.includes(v)) {
                        self.insertAtCursor(this, v);
                    }
                    else if (!self.InvalidCharacters.includes(v)) {
                        self.insertAtCursor(this, v);
                    }
                }
                
            }
            else if (self.KeyCodes[charCode]) {
                let v = self.KeyCodes[charCode][self.lang];
                if (self.UpperCase) {
                    v = v.toUpperCase();
                }
                if (self.specialCharsEnabled) {
                    self.insertAtCursor(this, v);
                }
                else {
                    if (self.AllowedChars.includes(v)) {
                        self.insertAtCursor(this, v);
                    }
                    else if (!self.InvalidCharacters.includes(v)) {
                        self.insertAtCursor(this, v);
                    }
                }
            }
            else if (e.shiftKey && self.NumbersShiftKeyCodes[charCode]) {
                if (self.NumberShift) {
                    let v = self.NumbersShiftKeyCodes[charCode][self.lang];
                    if (self.AllowedChars.includes(v)) {
                        self.insertAtCursor(this, v);
                    }
                    else if (!self.InvalidCharacters.includes(v)) {
                        self.insertAtCursor(this, v);
                    }
                }
            }
            else if (self.NumbersKeyCodes[charCode]) {
                let v = self.NumbersKeyCodes[charCode];
                self.insertAtCursor(this, v);
            }
            else {
                self.insertAtCursor(this, '');
            }

            return false;
        },
        insertAtCursor : function(field,text) {
            //IE support
            text = text || '';
            if (document.selection) {
                // IE
                field.focus();
                var sel = document.selection.createRange();
                sel.text = text;
            } else if (field.selectionStart || field.selectionStart === 0) {
                // Others
                var startPos = field.selectionStart;
                var endPos = field.selectionEnd;
                field.value = field.value.substring(0, startPos) +
                    text +
                    field.value.substring(endPos, field.value.length);
                field.selectionStart = startPos + text.length;
                field.selectionEnd = startPos + text.length;
            } else {
                field.value += text;
            }
        }


    }

})();