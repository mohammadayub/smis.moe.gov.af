var clean = window.clean = window.clean || {};
(function () {
    clean.actionmenu = function (opt) {
        this.opt = opt = opt || {};
        this.el = opt.el;
        this.page = opt.page;
        this.name = opt.name || 'subscreens';
        this.actions = this.el.find('[action]');
        this.menus = [];
        this.attachMainFormRecord = false;
        this.init(opt);
    }
    clean.actionmenu.prototype = {
        init: function (opt) {
            var self = this;
            if (opt['page']['mainform']['attachrecord'])
                self.attachMainFormRecord = true;

            self.construct();
        },
        construct: function () {
            var self = this;
            var sidebar = $('.page-sidebar');
            var sidetemp = '<div class="col-md-2"><div class="sidebar sidebar-secondary sidebar-default sidebar-fixed" ><div class="sidebar-content"><div class="sidebar-category"><div class="category-content no-padding"></div></div></div></div></div>';
            if (sidebar) {
                $('.main-content').removeClass('col-md-offset-1');
                $('.main-content').parent().prepend(sidetemp);
                $('.category-content').html(sidebar);

                this.actions = self.el.find('[action]');
                this.actions.bind('click', function () {
                    //$(this).attr('data') is the subscreen id
                    var act = $(this).attr('action');
                    if (self[act]) self[act]($(this));
                    return false;
                });
            }
        },
        subscreen: function (el) {
            var self = this;
            var action = el;
            var subScreenId = action.attr('data');
			var id = action.attr('data-id');
            var formname = action.attr('page');
            this.page.loadsubscreen(formname, id, self.attachMainFormRecord, subScreenId === '' ? false : subScreenId);
        }
    };
})();