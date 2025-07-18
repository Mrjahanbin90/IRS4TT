var ContractController = "";


$(document).ready(function (e) {
    window.onscroll = function () { myFunction() };
    function myFunction() {
        var winScroll = document.body.scrollTop || document.documentElement.scrollTop;
        var height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
        var scrolled = (winScroll / height) * 100;

        // Change the top position of the h1 text as you scroll
        document.getElementById("Assesmentstatus").style.top = (50 - scrolled / 2) + "%";

        // Change the font size of the h1 text as you scroll
        document.getElementById("Assesmentstatus").style.fontSize = (300 - scrolled * 2.5) + "px";
    }

    ContractController = $("#ContractController").val();
    $("span,td.row-Dscr p").each(function (e) {
        var myval = $(this).html();
        if (myval == null || myval.length == 0) {    /**/
            return;
        }
        var isRTL = checkRTL(String.fromCharCode($(this).html().replace(/[^\p{Alpha}']/gu, '').charCodeAt(0)));
        if ($(this).parent().hasClass('text-center')) { }
        else {
            if (isRTL == true) {
                $(this).parent().addClass("text-right");
                //$(this).parent().addClass("left-direction");

            }
            else {
                $(this).parent().addClass("text-left");
            }
        }
    });  

    $("#gotoWizard").on("click", function (e) {
        var id = $("#hdnContractid").val();
        var url = '/NAcceptance/Facultative' + '?str=View&sp=1&id=' + id + '&ClassOfBusinessId=' + $("#hdnClassOfBusinessId").val() ;

        window.open(url, '_blank'); 
    });
    $("#gotoLoss").on("click", function (e) {
        var id = $("#hdnLossid").val();
        var ClassOfBusinessId  = $("#hdnClassOfBusinessId").val();
        var url = '/NLoss/Loss' + '?str=View&sp=1&id=' + id + '&ClassOfBusinessId=' + ClassOfBusinessId ;

        window.open(url, '_blank');
    });

    $("#gotoPTWizard").on("click", function (e) {
        var id = $("#hdnContractid").val();
        var url = '/NProportional/Index?Id=' + id + '&str=View&PNPType=Proportional&sp=1';

        window.open(url, '_blank');
    });
 
    $(".form-control").on('keypress', function (e) {
        var obj = $(this);
        if (obj.val().length > 0) {
            return;
        }
        var isRTL = checkRTL(String.fromCharCode(e.charCode));
        if (isRTL == true) {
            $(obj).css('text-align', "right");
        }
        else {
            $(obj).css('text-align', "left");
        }
        $('.form-control.custom-date').css('text-align', "left !important");
        $('.form-control.money').css('text-align', "left !important");
    })
    $("#btndecide").on("click", function () {
        $("#txterror").html('');
        $("#chkNoAccept").prop('checked', false);
        $("#chkAccept").prop('checked', false);
        $("#div-signature input[type='checkbox']").prop('checked', false).attr('disabled', 'disabled');
        //if ($("#hdn_DisplaySignature").val() == "WithTitle") {
        //    $("#div-SignatureImage").remove();
        //}
        //if ($("#hdn_DisplaySignature").val() == "WithSignature") {
        //    $("#div-Title").remove();
        //}
        $('#modal-signconfirm').modal('show');
    });

    $(".form-check-input").on("change", function () {
        makedecision($(this));
    });

    $("#div-signature input[type='checkbox']").on("change", function () {
        if ($(this).attr('id') == "SignatureNo1") {
            $("#SignatureNo2").prop('checked', false);

        }
        else {
            $("#SignatureNo1").prop('checked', false);

        }
    });

    $("#btn-confirm").on("click", function () {
        $("#txterror").html('');
        var img_result = $(this).attr("data-imageresult");
        var SignatureNo = '0';
        var SignatureStatus = '';

        if ($("#chkAccept").prop('checked') == false & $("#chkNoAccept").prop('checked') == false) {
            $("#txterror").html('Make a decision to accept or not.');
            return false;
        }

        //if ($("#hdn_DisplaySignature").val() != "WithTitle") {
            if ($("#chkAccept").prop('checked') == true) {
                if ($("#SignatureNo1").prop('checked') == false & $("#SignatureNo2").prop('checked') == false) {
                    $("#txterror").html('Choose the signature.');
                    return false;
                }
            }
        //}
        $('#modal-signconfirm').modal('hide');
        /*if ($("#hdn_DisplaySignature").val() == "WithTitle") {*/
            if ($("#chkAccept").prop('checked') == true) {
                SignatureStatus = "1";
            }
            if ($("#chkNoAccept").prop('checked') == true) {
                SignatureStatus = "2";
            }
       /* }*/
        /*else {*/
            if ($("#chkAccept").prop('checked') == true && ($("#SignatureNo1").prop('checked') == true || $("#SignatureNo2").prop('checked') == true)) {
                SignatureStatus = "1";
                if ($("#SignatureNo1").prop('checked') == true) {
                    SignatureNo = "1";
                }
                if ($("#SignatureNo2").prop('checked') == true) {
                    SignatureNo = "2";
                }
            }
            if ($("#chkNoAccept").prop('checked') == true) {
                SignatureStatus = "2";
            }
       /* }*/
        var data_user = $(this).attr("data-user");
        savedecision(SignatureNo, SignatureStatus, img_result, data_user);
    });
});

function Get_SignatureContractModel(data) {
    return model = {
        ContractId: data.ContractId,
        SignatureNo: data.SignatureNo,
        SignatureStatus: data.SignatureStatus,
        Description: data.Description,
        ClassOfBusinessId: data.ClassOfBusinessId,
    }
}

function savedecision(SignatureNo, SignatureStatus, img_result, data_user) {
    var actionlink = '/Nwizard/SignatureAssesment';

    var mymodel = {
        ContractId: $("#hdnContractid").val(),
        SignatureNo: SignatureNo,
        SignatureStatus: SignatureStatus,
        Description: $("#txtDescriptionSignature").val(),
        ClassOfBusinessId: $("#hdnClassOfBusinessId").val(),

    }
    $.ajax({
        url: actionlink,
        type: "post",
        dataType: "json",
        data: { request: Get_SignatureContractModel(mymodel) },
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        error: function (xhr, status, error) {
            showtoast("false", getErrorMessage(xhr.status), "");
        },
        success: function (data) {
            //showtoast(data.isSuccess, data.message, '');
            if (data.isSuccess == "true") {
                $("." + data_user).removeClass("cls_Accepted").removeClass("cls_Rejected").removeClass("cls_NoSee");
                $("." + data_user).removeClass("cls_1").removeClass("cls_2").removeClass("cls_0");
                /*if ($("#hdn_DisplaySignature").val() != "WithTitle") {*/
                    if ($("#chkNoAccept").prop('checked') == true) {
                        $("." + img_result).attr('src', '').addClass("no-show").removeClass("show");
                    }
                    else {
                        $("#btndecide").remove();
                        if ($("#SignatureNo1").prop('checked') == true) {
                            var image_id = $("#SignatureNo1").attr("data-image");
                            $("." + img_result).attr('src', $("#" + image_id).attr('src')).removeClass("no-show").addClass("show");
                        }
                        if ($("#SignatureNo2").prop('checked') == true) {
                            var image_id = $("#SignatureNo2").attr("data-image");
                            $("." + img_result).attr('src', $("#" + image_id).attr('src')).removeClass("no-show").addClass("show");
                        }
                    }
                /*}*/
                $("." + data_user).addClass("cls_" + SignatureStatus);
                $("." + data_user).attr("title", $("#txtDescriptionSignature").val());
            }
            else {
            }
        },
    });
}

function makedecision(obj) {
    $("#txterror").html('');
    if ($(obj).attr('id') == "chkAccept") {
        $("#chkNoAccept").prop('checked', false);
        $("#div-signature input[type='checkbox']").removeAttr('disabled', 'disabled');
        if ($(obj).prop('checked') == false) {
            $("#div-signature input[type='checkbox']").prop('checked', false);
        }
    }
    else {
        $("#chkAccept").prop('checked', false);
        $("#div-signature input[type='checkbox']").prop('checked', false).attr('disabled', 'disabled');

    }
}

function checkRTL(s) {
    var ltrChars = 'A-Za-z\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u02B8\u0300-\u0590\u0800-\u1FFF' + '\u2C00-\uFB1C\uFDFE-\uFE6F\uFEFD-\uFFFF',
        rtlChars = '\u0591-\u07FF\uFB1D-\uFDFD\uFE70-\uFEFC',
        rtlDirCheck = new RegExp('^[^' + ltrChars + ']*[' + rtlChars + ']');

    return rtlDirCheck.test(s);
};
