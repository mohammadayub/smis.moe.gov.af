﻿$(document)
    .ajaxStart(function () {
        $("#overlay").show();
    })
    .ajaxStop(function () {
        $("#overlay").hide();
    });

var clean = window.clean = {};
(function () {
    clean = {
        init: function (opt) {
            var self = this;
            var page = {};
            page.el = $('body');
            window.CleanPage = new clean.page(page);
        },
        invoke: function (fn, o) {
            // Resolve the function
            if (!$.isFunction(fn)) {
                // Try to take get the function using fn as a key for o property
                if (o != null) {
                    fn = o[fn];
                }
                if (!$.isFunction(fn)) return;
            }
            // Create array of arguments to pass to function fn
            var a = [];
            for (var i = 1; i < arguments.length; i++) {
                a[i - 1] = arguments[i];
            }
            // Invoke
        
            return fn.apply(o, a);
        },
        isEmpty: function (value) {
            return typeof value == 'string' && !value.trim() || typeof value == 'undefined' || value === null;
        },
        isJsonString: function (str) {
            try {
                JSON.parse(str);
            }
            catch (e) {
                return false;
            }
            return true;
        },
        format: function (s, row, indexes) {
            if (!indexes) return s;
            // get each key of indexes and replace with coresponding row value
            if (row instanceof Array) // row: [1, "Ahmad", "CEO", "21", ...]
                for (key in indexes) {
                    var index = indexes[key];
                    //if (typeof index == "number") index = {index:index};
                    if (!isNaN(index)) index = { index: index };
                    var val = row[index.index]; val = val == null ? "" : this.accounting(val, key);
                    if (index.fn) val = val[0];
                    s = s.replace(new RegExp("{" + key + "}", 'g'), val) // replace all
                }
            else // row: {ID:1, Name:"Ahmad", "Position":"CEO", "Age": 21, ...}
                for (key in indexes) {
                    var val = row[key]; val = val == null ? "" : this.accounting(val, key) /*val*/;
                    if (typeof indexes[key] !== "number" && key.fn == "first") { val = val[0]; key = key.index };
                    s = s.replace(new RegExp("{" + key + "}", 'g'), row[key]) // replace all
                }
            return s;
        },
        accounting: function (n, key) {
            if (key && key.length >= 2 && (key.substr(key.length - 2, 2).toLowerCase() == "id" || key.substr(key.length - 4, 4).toLowerCase() == "code" || key.substr("mobile") > -1 || key.substr("phone") > -1)) return n;
            if (!$.isNumeric(n)) return n;
            if ((n + "").length > 3 && (n + "").substr(0, 1) == "0")
                return n;

            return n.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        },
        data: {
            get: function (opt) {
                $.extend(opt, { type: 'get' });
                return clean.data.ajax(opt);
            },
            post: function (opt) {
                $.extend(opt, { type: 'post' });
                return clean.data.ajax(opt);
            },
            json: {
                /**
                 * Initialize the JSON support.
                 */
                init: function () {
                    if (!window.JSON2) return;
                    // In case JSON is supported natively by the browser
                    if (!window.JSON) {
                        window.JSON = JSON2;
                        window.JSON.parse = function (data) { return (new Function("return " + data))(); };
                    }
                    // Native JSON in firefox converts dates using timezones, so we use a custom formatter
                    window.JSON.stringify = JSON2.stringify;
                },


                write: function (v) { return JSON.stringify(v); }
            },
            ajax: function (opt) {
                var url = opt.url || opt.service;

                if (!opt.url) $.extend(opt, { url: url });
                var complete1 = opt.complete, success1 = opt.success, ui = clean.widget;
                opt.contentType = opt.contentType === undefined ? 'application/json; charset=utf-8' : opt.contentType;
                opt.dataType = opt.dataType === undefined ? 'json' : opt.dataType; // we may require to get list
                return $.ajax($.extend(opt, {
                    timeout: 300000,

                    type: opt.type,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },
                    complete: function (xhr, status) {
                        if (status === 'timeout')
                            return;
                        if (xhr.status === 400 || xhr.status === 404)
                            // ui.error("Invalid request: " + opt.url);
                            if (complete1)
                                complete1(xhr);
                    },
                    success: function (msg) {
                        if (msg == null) {
                            ui.error("درخواست اشتباه میباشد", "سیستم درخواست شما را پذیرفته نتوانست لطفاً با مسؤلین تخنیکی صحبت نمائید");
                        }
                        if (success1)
                            success1(msg);
                    },
                    error: function (xhr, status, e) {
                        if (status == null) {
                            ui.error("Web API not accessible");
                        } else if (status === 'timeout') {
                            ui.error('timeout');
                        } else if (status == "parseerror") {
                            ui.error("The url {0} did not respond.".replace('{0}', opt.url));
                        } else if (status.toLowerCase() == "abort") {
                            ui.error('', '');
                        } else if (xhr.getResponseHeader("jsonerror")) {
                            var msg = xhr.responseText;
                            ui.error(msg.Message);
                        } else {
                            ui.error((status ? status + " " : "") + (e || "") || xhr.responseText);
                        }
                    }
                }));
            },

            getFile: function (opt) {

                var oReq = new XMLHttpRequest();

                //oReq.open("GET", opt.url + "?" + "id=" + opt.data.id.toString(), true);
                oReq.open("POST", opt.url, true);

                oReq.onreadystatechange = function () {

                    if (oReq.readyState == 2) {
                        if (oReq.status == 200) {
                            oReq.responseType = "arraybuffer";
                        }
                        else {
                            oReq.responseType = "text";
                        }
                    }
                };
              
                oReq.setRequestHeader('content-type', 'application/json');
                oReq.setRequestHeader('XSRF-TOKEN', $('input:hidden[name="__RequestVerificationToken"]').val());
                oReq.onload = function (msg) {

                    switch (this.status) {
                        case 200:
                            var blob = new Blob([oReq.response], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                            var objectUrl = URL.createObjectURL(blob);
                            window.open(objectUrl);
                            break;
                        default:
                            var message = JSON.parse(oReq.responseText);
                            clean.widget.error(message.title, message.msg);
                            break;
                    }

                };

                oReq.send(JSON.stringify(opt.data));
            }

        },
       
        widget: {
            message: function (type, msg, body, messagelife) {
                if (!msg) return;
                $.jGrowl(body, {
                    header: msg,
                    theme: type,
                    life: (messagelife) ? messagelife : 15000
                });
            },
            warn: function (msg, body, messagelife) {

                clean.widget.message('alert-styled-left  bg-warning', msg, body, messagelife ? messagelife : null);
            },
            error: function (msg, body, messagelife) { clean.widget.message('bg-danger-400 alert-styled-left alert-styled-custom', msg, body, messagelife ? messagelife : null); },
            success: function (msg, body, messagelife) { clean.widget.message('bg-success-700 alert-styled-left alert-styled-custom', msg, body, messagelife ? messagelife : null); }
        }
    }
})();




function download_csv(csv, filename) {
    var csvFile;
    var downloadLink;
    csvFile = new Blob([csv], { type: "text/csv" });
    downloadLink = document.createElement("a");
    downloadLink.download = filename;
    downloadLink.href = window.URL.createObjectURL(csvFile);
    downloadLink.style.display = "none";
    document.body.appendChild(downloadLink);
    downloadLink.click();
}

function export_table_to_csv(html, filename) {
    var csv = [];
    var rows = document.querySelectorAll("table tr");

    for (var i = 0; i < rows.length; i++) {
        var row = [], cols = rows[i].querySelectorAll("td, th");

        for (var j = 0; j < cols.length; j++)
            row.push(cols[j].innerText);

        csv.push(row.join(","));
    }
    download_csv(csv.join("\n"), filename);
}

function exportTable() {
    var html = document.getElementById('gv_Transfer_Profile').outerHTML;
    export_table_to_csv(html, "table.csv");
}

