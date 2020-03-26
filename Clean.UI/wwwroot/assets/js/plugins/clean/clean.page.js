var clean = window.clean = window.clean || {};
(function () {
    clean.page = function (opt) {
        this.opt = opt = opt || {};
        this.el = opt.el;
        this.mainform = {};
        this.subforms = [];
        this.charts = [];
        this.mainpanel = {};
        this.rout = {};
        this.mainpanel.subnames = [];
        this.subroutes = [];
        this.name = opt.name || 'default';
        this.components = this.el.find('.page-component');
        this.init(this.el);
    }
    clean.page.prototype = {
        init: function (opt) {
            var self = this;
            self.construct();
        },
        construct: function (opt) {
            var self = this;
            self.components.each(function (index) {
                this.el = $(this);
                this.page = self;
                if (this.el.attr('type') == 'form' && this.el.hasClass('main-form')) {
                    self.mainform = new clean[this.el.attr('type')](this);
                    self.mainform.EncryptedID = window.location.href.split('?').reverse()[0].substr(2);
                }
                else if (this.el.attr('type') == 'form' && this.el.hasClass('sub-form')) {
                    this.parent = self.mainform;
                    var prefix = this.el.attr('prefix');
                    var sub = new clean[this.el.attr('type')](this);
                    sub.EncryptedID = sub.el.find("#" + prefix + 'pageid').val();
                    self.subforms.push(sub);
                }
                else if (this.el.attr('type') === 'actionmenu') {
                    var menu = {};
                    menu.el = this;
                    menu.page = self;
                    new clean[this.el.attr('type')](this);
                }
                else if (this.el.attr('type') === 'chart') {
                    var chart = new clean[this.el.attr('type')](this);
                    chart.EncryptedID = window.location.href.split('?').reverse()[0].substr(2);
                    self.charts.push(chart);
                }
                else if (this.el.attr('type') == 'form-tail') {
                    $('.main-content').append(this.el);
                }
            });
           
        },
        loadsubscreens: function (formname, id) {
            var self = this;
            var path = '/' + formname.substring(formname.indexOf("_") + 1).replace(/_/g, '/') + '/Get/' + id;
            var data = {};
            clean.data.get({
                async: false, url: path, data: clean.data.json.write(data), dataType: 'html',
                success: function (msg) {
                    var html = msg;
                    $('.dependent-screens').append(html);
                    var subform = {};
                    subform.el = $('#' + formname);
                    subform.parent = self.mainform;
                    subform.page = self;
                    if (subform.el.hasClass('page-component')) {
                        if (subform.el.attr('type') == 'form' && subform.el.hasClass('sub-form')) {
                            var sub = new clean[subform.el.attr('type')](subform);
                            sub.EncryptedID = id;
                            self.subforms.push(sub);
                        }
                    }
                }
            });
        },
        loadsubscreen: function (formname, id, attachMainFormRecord, subScreenId) {
            var self = this;
            var path = '/' + formname.substring(formname.indexOf("_") + 1).replace(/_/g, '/') + '/Get';
            var data = {};

            if (!$.isEmptyObject(self.mainform.record)) {

                if (attachMainFormRecord)
                    path = path + '/' + self.mainform.record.id;
                
                if (subScreenId)
                    path = path + '/' + subScreenId;

                clean.data.get({
                    async: false, url: path, data: clean.data.json.write(data), dataType: 'html',
                    success: function (msg) {
                        var html = msg;
                        $('.dependent-screens').html(html);
                        var subform = {};
                        subform.el = $('#' + formname);
                        subform.parent = self.mainform;
                        subform.page = self;
                        
                        if (subform.el.hasClass('page-component')) {
                            if (subform.el.attr('type') == 'form' && subform.el.hasClass('sub-form')) {
                                var sub = new clean[subform.el.attr('type')](subform);
                                sub.EncryptedID = id;
                                self.subforms.push(sub);
                            }
                        }

                        if ($('.panel.viewonly').length == 0) {
                            //scrolling to subform
                            var scrollTo = $('#' + formname).attr('scrollto') == undefined ? $('#' + formname) : $('#' + ( $('#' + formname).attr('scrollto')));

                            var container = $('body');
                            $('html,body').animate({
                                scrollTop: scrollTo.offset().top - container.offset().top + container.scrollTop() -140
                            });
                        }

                    }
                });
            }
            else {
                clean.widget.error('فورم اصلی خالی میباشد', 'لطفاً برای اینکه صفحه های فرعی را مشاهده نمائید، ریکارد فورم اصلی را مشخص سازید');
            }
        },
        parameter: function (name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(location.search);

            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        }
    };
})();