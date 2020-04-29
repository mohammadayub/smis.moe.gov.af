var clean = window.clean = window.clean || {};
(function () {
    clean.Tazkira = function (opt) {
        this.el = opt.el;
        this.sib = $('#' + this.el.attr('sibling'));
        this.sib.val = '';
        this.form = opt.form;
        this.row = {};
        this.template = "<div class='col-md-$width col-sm-12 col-xs-12 tazkira-group $g-class'><div class='form-group'><label class='text-bold'><span class='text-danger pull-left'>&nbsp;*</span>$Label</label>$Control</div></div>";
        this.serialNo = {};
        this.juldNumber = {};
        this.juldType = {};
        this.juldYear = {};
        this.page = {};
        this.No = {};
        this.init();

    };
    clean.Tazkira.prototype = {
        init: function () {
            var self = this;

            self.row = $('<div class="row"></div>').insertAfter(self.el.parents('.nid-row'));
            


            var control = "<select id='$id' name='$name' class='form-control alt-tazkira select' required>$Options</select>";
            var dropdowns = self.template.replace('$Control', control);

            var options = [];
            for (var i = 1; i <= 60; i++) {
                options.push("<option value='" + i + "'>" + i + "</option>");
            }
            var jdn = dropdowns.replace('$width', '3').replace('$Label', 'نمبر جلد').replace('$id', 'jdn').replace('$g-class', 'old').replace('$name', 'JuldN').replace('$Options', options.join(''));
            self.row.append(jdn);
            self.juldNumber = self.row.find('#jdn');

            options = [];
            options.push('<option value="1">قلم انداز</option>');
            options.push("<option value='2'>متفرقه</option>");
            options.push("<option value='3'>اصل اساس</option>");
            options.push("<option value='4'>تولدات</option>");

            var jdt = dropdowns.replace('$width', '3').replace('$Label', 'نوعیت جلد').replace('$id', 'jdt').replace('$g-class', 'old').replace('$name', 'JuldT').replace('$Options', options.join(''));
            self.row.append(jdt);
            self.juldType = self.row.find('#jdt');

            options = [];
            for (var i = 1350; i <= 1422; i++) {
                options.push("<option val='" + i + "'>" + i + "</option>");
            }
            var year = dropdowns.replace('$width', '3').replace('$Label', 'سال کتاب').replace('$id', 'jdy').replace('$g-class', 'old').replace('$Options', options.join(''));
            self.row.append(year);
            self.juldYear = self.row.find('#jdy');


            options = [];
            for (var i = 1; i <= 500; i++) {
                options.push("<option val='" + i + "'>" + i + "</option>");
            }

            var pg = dropdowns.replace('$width', '3').replace('$Label', 'نمبر صفحه').replace('$id', 'pg').replace('$g-class', 'old').replace('$name', 'Page').replace('$Options', options.join(''));;
            self.row.append(pg);
            self.page = self.row.find('#pg');


            options = [];
            for (var i = 1; i <= 2500; i++) {
                options.push("<option val='" + i + "'>" + i + "</option>");
            }

            var no = dropdowns.replace('$width', '3').replace('$Label', 'نمبر ثبت').replace('$id', 'no').replace('$g-class', 'old').replace('$name', 'No').replace('$Options', options.join(''));
            self.row.append(no);
            self.No = self.row.find('#no');

            


            control = "<input id='$id' name='$name' class='form-control alt-tazkira' required/>";
            var input = self.template.replace('$Control', control);

            var sn = input.replace('$width', '3').replace('$Label', 'نمبر مسلسل (صکوک)').replace('$id', 'sn').replace('$g-class', 'always').replace('$name', 'SerialNumber');
            self.row.append(sn);
            self.serialNo = self.row.find('#sn');

            
            self.sib.change(function () {

                var v = $(this).find("option:selected").text().trim();
                self.sib.val = v;
                self.changeselect(v);
            });

            self.row.find('.old').hide();
        },
        changeselect: function (v) {
            var self = this;
            this.serialNo.val('');
            this.juldNumber.val('');
            this.juldType.val('');
            this.juldYear.val('');

            this.page.val('');
            this.No.val('');

            if (v == 'تذکره ورقی') {
                self.row.find('.old').show();
                self.row.find('.old').each(function (i, item) {
                    $(item).find('select').removeAttr('disabled');
                });
                self.row.find('.alt-tazkira').val("");
            }
            else {
                self.row.find('.old').each(function (i, item) {
                    $(item).find('select').attr('disabled', 'true');
                });
                self.row.find('.old').hide();
            }

        },
        getExpr: function (col) {
            var v = "{S:'%" + (this.serialNo.val().trim() != "" ? this.serialNo.val().trim() : "") + "'," +
                "JN:'%" + (this.juldNumber.val().trim() != "" ? this.juldNumber.val().trim() : "") + "'," +
                "JT:'%" + (this.juldType.val().trim() != "" ? this.juldType.val().trim() : "") + "'," +
                "JY:'%" + (this.juldYear.val().trim() != "" ? this.juldYear.val().trim() : "") + "'," +
                "P:'%" + (this.page.val().trim() != "" ? this.page.val().trim() : "") + "'," +
                "N:'%" + (this.No.val().trim() != "" ? this.No.val().trim() : "")
                + "'}";
            var expr = {};
            expr.fn = "endswith";
            expr.expr = {};
            expr.expr[col] = v;
            return expr;
        },
        validate: function () {
            var self = this;
            if (this.sib.val == 'تذکره ورقی')
                return self.row.find('.alt-tazkira').valid();
            else
                return self.row.find('#sn').valid();
        },
        val: function (v) {
            if (v !== undefined) {
                v = v + "";
                // clear
                this.serialNo.val('');
                this.juldNumber.val('');
                this.juldType.val('');
                this.juldYear.val('');
                this.page.val('');
                this.No.val('');
                var v2;
                try {
                    eval('v2 = ' + v);
                    this.serialNo.val(v2.S.replace(',', ''));
                    this.juldNumber.val(v2.JN.replace(',', ''));
                    this.juldType.val(v2.JT.replace(',', ''));
                    this.juldYear.val(v2.JY.replace(',', ''));
                    this.page.val(v2.P.replace(',', ''));
                    this.No.val(v2.N.replace(',', ''));
                }
                catch (e) {
                    v2 = v.split(" ");
                }
                // set
                return v;
            }

            var vals = [this.serialNo.val(), this.juldNumber.val(), this.juldType.val(), this.juldYear.val(), this.page.val(), this.No.val()];
            if (!this.serialNo.val().trim() && !this.juldNumber.val().trim() && !this.juldType.val().trim() && !this.juldYear.val().trim() && !this.page.val().trim() && !this.No.val().trim()) return '';
            // get
            v = clean.format("{S: '{serialNoID}', JN:'{JuldNumberID}', JT:'{JuldTypeID}', JY:'{JuldYearID}', P:'{PageID}', N:'{NoID}'}", vals, { serialNoID: 0, JuldNumberID: 1, JuldTypeID: 2, JuldYearID: 3, PageID: 4, NoID: 5 });
            return v;
        }
    };
})();






