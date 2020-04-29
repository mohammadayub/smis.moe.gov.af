function getModal(dirctory,screenname) {
    $('#SubScreens a').each(function (index, value) {
        $('#' + value.id).on('click', function () {

            let page = value.id.substring(value.id.indexOf('_') + 1);
            //let path = '/' + dirctory + '/' + screenname + '/SubScreensForProcess';
            let path = '../Controllers/ControlSearch/search';
            let data = {};
            data.id = $('#' + $('#dv_' + dirctory + '_' + screenname).attr('prefix') + 'id').val();
            if (!$.isEmptyObject(data.id)) {
                switch (page) {
                    case 'notify': // data on notify modal
                        data.page = page;
                        getNotifyData(path,data, page);
                        break;
                    case 'servicerecord': // data on service record modal
                        data.page = page;
                        getServiceRecordData(path, data, page);
                        
                    case 'servicedetail':
                        data.page = 'servicedetail';// page;
                        getServiceDetailData(path, data, page);
                        break;
                    case 'application': // data on application modal
                        data.page = page;
                        getApplicationData(path, data, page, dirctory, screenname);

                    case 'profile':
                        data.page = 'profile'//page;
                        getProfileData(path, data, page);
                       

                    case 'heir':
                        data.page = 'heir';//page;
                        getHeirData(path, data, page, dirctory, screenname);

                    case 'address':
                        data.page = 'address'//page;
                        getAddressData(path, data, page);

                    case 'survivor':
                        data.page = 'survivor'//page;
                        getSurvivorData(path, data, page);
                        break;

                    case 'calculation': // data on calculation modal
                        data.page = page;
                        getCalculationData(path, data, page);
                        break;

                        case 'disability': // data on distability modal
                        data.page = page;
                        getDisabilityData(path, data, page);
                        break;
                    case 'document': // data on document modal
                        data.page = page;
                        getDocumentData(path, data, page);
                        break;
                    
                    case 'calculationresult': // data for calculation result
                        data.page = 'application'//page;
                        getApplicationData(path, data, page, dirctory, screenname);

                    case 'profile':
                        data.page = 'profile'//page;
                        getProfileData(path, data, page);
                    case 'heir':
                        data.page = 'heir';//page;
                        getHeirData(path, data, page, dirctory, screenname);

                    case 'servicerecord':
                        data.page = 'servicerecord'//page;
                        getServiceRecordData(path, data, page);

                    case 'servicedetail':
                        data.page = 'servicedetail';// page;
                        getServiceDetailData(path, data, page);

                    case 'survivor':
                        data.page = 'survivor'//page;
                        getSurvivorData(path, data, page);

                        break;
                       
                    //end 
                }
            }
            else {
                clean.widget.warn('کوشش خلاف اصول', 'لطف نموده ابتدا متقاعد را انتخاب نموده سپس اطلاعات آن مشاهده و  بررسی نمائید');
            }

        });
    }
    );
}

function download(path) {
        var file = {};
        file.name = path;
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Document/Document' + '/Download', true);
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
}

function getNotifyData(path,data,page) {
    clean.data.post({
        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                let list = result.data.list;
                $('#no_firstname').val(list[0].firstName);
                $('#no_lastname').val(list[0].lastName);
                $('#no_fathername').val(list[0].fatherName);
                $('#no_grandfathername').val(list[0].grandFatherName);
                $('#no_formcode').val(list[0].formCode);
                $('#no_organizationtext').val(list[0].organizationText);
                $('#no_section').val(list[0].section);
                $('#no_pensiontypetext').val(list[0].pensionTypeText);
                $('#no_ranktext').val(list[0].rankText);
                $('#no_referraldate').val(list[0].referralDateShamsi);
                $('#no_lastdayofemployment').val(list[0].lastDayOfEmploymentShamsi);
                $('#no_unusedleavedue').val(list[0].unusedLeaveDue);
                $('#no_wagesoverdue').val(list[0].wagesOverDue);
                $('#no_otherliabilities').val(list[0].otherLiabilities);
                $('#no_debtamount').val(list[0].debtAmount);
                $('#no_advancefunds').val(list[0].advanceFunds);
                $('#no_description').val(list[0].description);
                $('#no_departmenttext').val(list[0].departmentText);

                $('#' + page + '_Modal').modal();
            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });

}

function getServiceRecordData(path, data, page) {
    $('#tbl_servicerecord').find('tbody').html('');
    clean.data.post({
        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                if (result.data.list.length > 0) {
                    let list = result.data.list;

                    for (let i = 0; i < list.length; i++) {

                        $('#tbl_servicerecord').find('tbody').append('<tr role="row"><td>' + list[i].organizationText + '</td><td>' + list[i].job + '</td><td>' + list[i].rankText + '</td><td>' + (list[i].positionText == null ? "" : list[i].positionText) + '</td ><td>' + list[i].firstDayOfEmploymentShamsi + '</td><td>' + list[i].lastDayOfEmploymentShamsi + '</td><td>' + list[i].totalService + '</td><td>' + list[i].serviceTypeText + '</td><td>' + list[i].workTypeText + '</td><td>' + list[i].salaryTypeText + '</td><td>' + list[i].actualMonthlySalary + '</td ><td>' + (list[i].educationDegreeText == null ? "" : list[i].educationDegreeText) + '</td><td>' + (list[i].professionalSalary == null ? "" : list[i].professionalSalary) + '</td><td>' + (list[i].educationRankText == null ? "" : list[i].educationRankText) + '</td><td>' + (list[i].kadriSalary == null ? "" : list[i].kadriSalary) + '</td></tr>');
                    }
                    $('#' + page + '_Modal').modal();
                }
                else {
                    clean.widget.warn('عدم وجود اطلاعات', 'متقاعد انتخاب شده بازمانده ندارد');
                }

            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}

function getServiceDetailData(path, data, page) {
    clean.data.post({

        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                let list = result.data.list;
                $('#srlastdayofreceivingsalary').val(list[0].lastDayOfReceivingSalaryShamsi);
                $('#srlastjobleavingdate').val(list[0].lastJobLeavingDateShamsi);
                $('#srregulationtext').val(list[0].regulationText);
                $('#srregulationinrulesdate').val(list[0].regulationInRuledDateShamsi);
                $('#srequalitydate').val(list[0].equalityDateShamsi);
                $('#srtotalexperience').val(list[0].totalExperince);

                $('#' + page + '_Modal').modal();
            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}

function getApplicationData(path, data, page, dirctory,screenname) {
    clean.data.post({
        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                if (result.data.list.length > 0) {
                    let list = result.data.list;
                    $('#apl_formcode').val(list[0].formCode);
                    $('#apl_organizationcode').val(list[0].organizationCode);
                    $('#apl_organizationname').val(list[0].organizationText);
                    $('#apl_departmenttext').val(list[0].departmentText);
                    $('#apl_datacategory').val(list[0].dataCategoryText);
                    $('#apl_datacategoryid').val(list[0].dataCategoryId);// used in address
                    $('#apl_ranktext').val(list[0].rankText);
                    $('#apl_retirmentregulationtext').val(list[0].retirementRegulationText);

                    $('#apl_provincetext').val(list[0].provinceText);
                    $('#apl_districttext').val(list[0].districtText);

                    $('#apl_employmentid').val(list[0].employmentId);
                    $('#apl_applicationdate').val(list[0].applicationDateShamsi);
                    $('#apl_lastdayofemployment').val(list[0].lastDayOfEmploymentShamsi);
                    $('#apl_referraldateshamsi').val(list[0].referralDateShamsi);
                    $('#apl_pensiontypetext').val(list[0].pensionTypeText);
                    $('#apl_retirementstartdateshamsi').val(list[0].retirmentStartDateShamsi);
                    $('#apl_decreeno').val(list[0].decreeNo);
                    $('#apl_decreedateshamsi').val(list[0].decreeDateShamsi);


                    (function () {
                        var file = {
                            name: list[0].photoPath,
                            type: 'jpg'
                        };
                        var xhr = new XMLHttpRequest();
                        xhr.open('POST', '/' + dirctory + '/' + screenname + '/Download', true);
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                        xhr.setRequestHeader("Content-Type", 'application/json; charset=utf-8');
                        xhr.onreadystatechange = function () {
                            if (xhr.readyState == 4 && xhr.status == 200) {
                                var blob = new Blob([xhr.response], {
                                    type: xhr.getResponseHeader("Content-Type")
                                });
                                var img = $('#apl_Photo');
                                img[0].src = window.URL.createObjectURL(blob);
                            }
                        }
                        xhr.responseType = "arraybuffer";
                        xhr.send(JSON.stringify(file));
                    })();

                    $('#' + page + '_Modal').modal();
                }
                else {
                    clean.widget.warn('عدم وجود اطلاعات', 'متقاعد انتخاب شده درخواستی ندارد');
                }
            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}

function getProfileData(path, data, page) {
    clean.data.post({

        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                let list = result.data.list;
                $('#apl_firstname').val(list[0].firstName);
                //$('#apl_code').val(list[0].code);
                $('#apl_lastname').val(list[0].lastName);
                $('#apl_fathername').val(list[0].fatherName);
                $('#apl_grandfathername').val(list[0].grandFatherName);
                $('#apl_gender').val(list[0].genderText);
                $('#apl_nidtext').val(list[0].nidText);
                $('#apl_educationdegreetext').val(list[0].educationDegreeText);
                $('#ap_birthlocationtext').val('ولایت' + ' ' + list[0].birthLocationText + ' ' + 'ولسوالی ' + list[0].districtText);
                $('#apl_dobshamsi').val(list[0].dobShamsi /*+ ' معادل ' + list[0].formatDob*/);
                $('#' + page + '_Modal').modal();
            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}

function getHeirData(path, data, page, dirctory,screenname) {
    clean.data.post({
        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',

        success: function (result) {

            if (result.status > 0) {
                $('#apl_heir').html('');
                if (result.data.list.length > 0) {
                    for (var i = 0; i < result.data.list.length; i++) {
                        let list = result.data.list[i];

                        var heirTypeText = list.heirTypeText;
                        var relativeText = list.relativeText;
                        var firstName = list.firstName;
                        var lastName = list.lastName;
                        var fatherName = list.fatherName;
                        var grandFatherName = list.grandFatherName;
                        var hePhotoPath = list.hephotoPath;
                        var dob = list.dobShamsi;
                        var genderText = list.genderText;
                        var documentNo = list.documentNo;
                        var docNoIssuedPlace = list.docNoIssuePlaceText;
                        var docNoIssueDate = list.docNoIssueDateShamsi;
                        var nid = list.documentTypeText + ' ' + list.nidText;
                        var court = list.court;



                        var html = '<form>\
                            <fieldset class="content-group">\
                                <h3 class="text-bold">معلومات ورثه | وکیل شرعی</h3>\
                                <div class="row">\
                                <div class="col-md-10 ">\
                                    <div class="row">\
                                                <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    نوعیت\
                                                </label>\
                                                <input id="he_heirtypetext" type="text" class="form-control" disabled value="'+ heirTypeText + '" />\
                                            </div>\
                                        </div>\
                                              <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    قرابت\
                                                </label>\
                                                <input type="text" id="he_relativetext" class="form-control" disabled value="'+ relativeText + '" >\
                                            </div>\
                                        </div>\
                                            <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    تاریخ تولد\
                                                </label>\
                                                <input type="text" id="he_dob" class="form-control" disabled value="'+ dob + '" />\
                                            </div>\
                                        </div>\
                                            <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    اسم\
                                                </label>\
                                                <input type="text" id="he_firstname" class="form-control" disabled value="'+ firstName + '" />\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row">\
                                                 <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    تخلص\
                                                </label>\
                                                <input type="text" id="he_lastname" class="form-control" disabled value="'+ lastName + '" />\
                                            </div>\
                                        </div>\
                                             <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    ولد\
                                               </label>\
                                                <input type="text" id="he_fathername" class="form-control" disabled value="'+ fatherName + '" />\
                                            </div>\
                                        </div>\
                                            <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    ولدیت\
                                                </label>\
                                                <input type="text" id="he_grandfathername" class="form-control" disabled value="'+ grandFatherName + '" />\
                                            </div>\
                                        </div>\
                                            <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    جنسیت\
                                                </label>\
                                                <input type="text" id="he_gendertext" class="form-control" disabled value="'+ genderText + '" />\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row">\
                                             <div class="col-md-6 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                   تذکره\
                                                </label>\
                                                <input type="text" class="form-control" disabled value="'+ nid + '" />\
                                            </div>\
                                        </div>\
                                            <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    نمبر وثیقه | وکالت\
                                                </label>\
                                                <input type="text" id="he_documentno" class="form-control" disabled value="'+ documentNo + '" />\
                                            </div>\
                                        </div>\
                                               <div class="col-md-3 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                   محکمه\
                                                </label>\
                                                <input type="text" id="he_court" class="form-control" disabled value="'+ court + '" />\
                                            </div>\
                                        </div>\
                                    </div>\
                                    <div class="row">\
                                            <div class="col-md-6 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    تاریخ صدور\
                                                </label>\
                                                <input type="text" id="he_docnoissuedate" class="form-control" disabled value="'+ docNoIssueDate + '"/>\
                                            </div>\
                                        </div>\
                                            <div class="col-md-6 ">\
                                            <div class="form-group">\
                                                <label class="text-bold">\
                                                    محل صدور\
                                                </label>\
                                                <input type="text" id="he_docnoissueplace" class="form-control" disabled value="'+ docNoIssuedPlace + '"/>\
                                            </div>\
                                        </div>\
                                </div>\
                                </div>\
                                    <div class="col-lg-2 pull-left">\
                                    <div id="hePhoto" class="img-uploader">\
                                    <img src="" class="croppedImg" id="he_photopath'+ i + '" style="width : 100%;height:190px;" />\
                                    </div>\
                                </div>\
                                </div>\
                                             <hr />\
                            </fieldset>\
                        </form>';



                        $('#apl_heir').append(html);


                        (function (w) {
                            var file = {
                                name: list.photoPath,
                                type: 'jpg'
                            };
                            var xhr = new XMLHttpRequest();
                            xhr.open('POST', '/' + dirctory + '/' + screenname + '/Download', true);
                            xhr.setRequestHeader("XSRF-TOKEN",
                                $('input:hidden[name="__RequestVerificationToken"]').val());
                            xhr.setRequestHeader("Content-Type", 'application/json; charset=utf-8');
                            xhr.onreadystatechange = function () {
                                if (xhr.readyState == 4 && xhr.status == 200) {
                                    var blob = new Blob([xhr.response], {
                                        type: xhr.getResponseHeader("Content-Type")
                                    });
                                    var img = $('#he_photopath' + w);
                                    img[0].src = window.URL.createObjectURL(blob);
                                }
                            }
                            xhr.responseType = "arraybuffer";
                            xhr.send(JSON.stringify(file));
                        })(i);
                    }
                    $('#' + page + '_Modal').modal();
                }
                else {
                    clean.widget.warn(/*'عدم وجود اطلاعات', 'متقاعد انتخاب شده ورثه یا وکیل شرعی ندارد'*/);
                }

            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}

function getAddressData(path, data, page) {
    clean.data.post({
        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                if (result.data.list.length > 0) {
                    let list = result.data.list;
                    if ($("#apl_datacategoryid").val() == 1 || $("#apl_datacategoryid").val() == 3) {
                        list = list.find(e => e.holderTypeId == 1)
                    } else {
                        list = list.find(e => e.holderTypeId == 3)
                    }
                    $('#apl_currentprovince').val(list.currentProvinceText);
                    $('#apl_currentdistrict').val(list.currentDistrictText);
                    $('#apl_currentvillage').val(list.currentVillage);
                    $('#apl_telephonenumber').val(list.telephonNo);
                    $('#apl_province').val(list.provinceText);
                    $('#apl_district').val(list.districtText);
                    $('#apl_village').val(list.village);
                    $('#apl_reletivephonenumber').val(list.relativeTelephoneNo);
                    $('#apl_emial').val(list.email);


                    $('#' + page + '_Modal').modal();
                }
                else {
                    clean.widget.warn('');

                }

            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}

function getSurvivorData(path, data, page) {
    $('#apl_survivors').find('tbody').html('');
    clean.data.post({
        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                if (result.data.list.length > 0) {

                    let list = result.data.list;

                    for (let i = 0; i < list.length; i++) {
                        $('#apl_survivors').find('tbody').append('<tr role="row"><td>' + list[i].firstName + '</td><td>' + list[i].lastName + '</td><td>' + list[i].fatherName + '</td><td>' + list[i].grandFatherName + '</td><td>' + list[i].nidText + '</td><td>' + list[i].relativeText + '</td><td>' + list[i].age + '</td><td>' + list[i].maritalStatusText + '</td><td>' + list[i].genderText + '</td><td>' + list[i].survivorStatusText + '</td></tr>');
                    }
                    $('#' + page + '_Modal').modal();
                }
                else {
                    clean.widget.warn('');

                }

            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}

function getCalculationData(path, data, page) {
    $('#tbl_calculation').find('tbody').html('');
    clean.data.post({
        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                if (result.data.list.length > 0) {
                    let list = result.data.list;
                    let total = 0;
                    $('#cal_formula').text(list[0].formula);
                    for (let i = 0; i < list.length; i++) {

                        total = total + list[i].totalPaymentAmount;
                        $('#tbl_calculation').find('tbody').append('<tr role="row"><td>' + list[i].retirementDateText + '</td><td>' + list[i].calculationDateText + '</td><td>' + list[i].mainMonthlySalary + '</td><td>' + list[i].factor + '</td><td>' + list[i].professionalAmount + '</td><td>' + list[i].cadreAmount + '</td><td>' + list[i].calculatedMainMonthlySalary + '</td><td>' + list[i].totalExperience + '</td><td>' + list[i].totalPensionExperience + '</td><td>' + list[i].salaryPercentageText + '</td><td>' + list[i].pensionSalary + '</td><td>' + list[i].paymentTypeText + '</td><td>' + list[i].totalPayableMonth + '</td><td>' + list[i].totalPayableDay + '</td><td>' + list[i].calculationTypeText + '</td><td>' + list[i].totalPaymentAmount + '</td></tr>');

                        if (i === (list.length - 1)) {
                            $('#tbl_calculation').find('tbody').append('<tr role="row"><td colspan=15> مجموعه: </td> <td>' + total + '</td></tr>');

                        }
                    }

                    $('#' + page + '_Modal').modal();
                }
                else {
                    clean.widget.warn('عدم وجود اطلاعات', 'حقوق متقاعد محاسبه نشده لطفا اسناد را رد نمائید ');
                }

            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}

function getDisabilityData(path, data, page) {
    clean.data.post({
        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                if (result.data.list.length > 0) {
                    let list = result.data.list;
                    $('#aptype').val(list[0].typeText);
                    $('#apdisabilitylevel').val(list[0].disabilityleveltext);
                    $('#apdisabilitytype').val(list[0].disabilityTypetext);
                    $('#apdisabilityplace').val(list[0].disabilityPlacetext);
                    $('#apincidentdateshamsi').val(list[0].incidentDateshamsi);
                    $('#apformnumber').val(list[0].formNumber);
                    $('#apdiscription').val(list[0].discription);
                    $('#' + page + '_Modal').modal();
                }
                else {
                    clean.widget.warn('عدم وجود اطلاعات', 'متقاعد انتخاب شده فورم معلولیت, فوت و یا مفقودیت ندارد!');
                }
            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}

function getDocumentData(path, data, page) {
    $('#tbl_documents').find('tbody').html('');
    clean.data.post({

        async: false, url: path, data: clean.data.json.write(data), datatype: 'json',
        success: function (result) {

            if (result.status > 0) {
                if (result.data.list.length > 0) {

                    let list = result.data.list;

                    for (let i = 0; i < list.length; i++) {

                        $('#tbl_documents').find('tbody').append('<tr role="row"><td>' + list[i].documentTypeText + '</td><td>' + list[i].uploadDateText + '</td><td>' + list[i].downloadDateText + '</td><td>' + '<button type="button" downloadpath="$path" onclick="download(\'' + list[i].path + '\');"><i class="icon-download position-left"></i>دریافت فایل</button>' + '</td></tr>');
                    }
                    $('#' + page + '_Modal').modal();
                }
                else {
                    clean.widget.warn('عدم وجود اطلاعات', 'متقاعد انتخاب شده اسناد و ضمایم ندارد !');

                }

            }
            else {
                clean.widget.warn(result.text, result.description);
            }
        }
    });
}