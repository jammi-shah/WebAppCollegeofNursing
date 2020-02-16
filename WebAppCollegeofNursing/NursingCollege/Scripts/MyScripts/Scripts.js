
///preview image
$('#uploadImage').change(function () {
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#previewImage').attr('src', e.target.result);
        }
        reader.readAsDataURL(this.files[0]);
    }
});


//function jQueryAjaxPost(form) {
//    $.validator.unobtrusive.parse(form);
//    if ($(form).valid()) {
//        var ajaxConfig = {
//            url: form.action,
//            type: 'post',
//            data: new FormData(form),
//            contentType: false,
//            processData: false,
//            success: function (res, status, xhr) {
//                $('#viewAllTab').html(res);
//                $('div.alert-success').show();
//                $('ul.nav-tabs a:eq(0)').tab('show');
//                $(form).trigger('reset');
//            },
//            error: function (err) {
//                $('div.alert-danger').html('There is some issue!').show();
//            }
//        }
//        $.ajax(ajaxConfig);
//        return false;
//    }
//}

function searchByDeptAndClass(form) {
        var ajaxConfig = {
            url: form.action + '?deptId=' + $('#DepartmentId').val() + '&currentClass=' + $('#CurrentClass option:selected').text(),
            method: 'get',
            //contentType: false,
            //processData: false,
            success: function (res, status, xhr) {
                if (!res) {
                    $('#studentList').empty();
                    $('div.alert-danger').html('No results found').show();
                }
                else {
                    $('div.collapse').hide();
                    $('#studentList').html(res);
                //$('ul.nav-tabs a:eq(0)').tab('show');
                }
                
            },
            error: function (err) {
                $('#studentList').empty();
                $('div.alert-danger').html('There is some issue! ').show();
            }
        }
        $.ajax(ajaxConfig);
        return false;
}


function Edit(url) {
    $.ajax({
        url: url,
        type: 'get',
        success: function (res) {
            $('#addNewTab').html(res);
            $('ul.nav-tabs a:eq(1)').html('Edit').tab('show');
        }
    });
}

function Details(url) {
    $.ajax({
        url: url,
        type: 'get',
        success: function (res) {
            $('#addNewTab').html(res);
            $('ul.nav-tabs a:eq(1)').html('Details').tab('show');
        }
    });
}


function cancelEdit(url) {
    $.ajax({
        url: url,
        type: 'get',
        success: function (res) {
            $('#addNewTab').html(res);
            $('ul.nav-tabs a:eq(0)').tab('show');
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
        }
    });
}


function Delete(url) {
    if (confirm('Are you sure you want to delete?')) {
        $.ajax({
            url: url,
            type: 'post',
            success: function (res) {
                $('#viewAllTab').html(res);
                $('div.alert-success').removeClass("alert-success").addClass("alert-danger").show();
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
            }
        });
    }
}


function Unblock(url) {
    if (confirm('Are you sure you want to unblock this account?')) {
        alert(url);
        $.ajax({
            url: url,
            type: 'get',
            success: function (res) {
                $('#secondTab').html(res);
                //$('div.alert-success').removeClass("alert-success").addClass("alert-danger").show();
                //$('ul.nav.nav-tabs a:eq(0)').tab('show');
            }
        });
    }
}


function isAvailable(url, value) {
    $.ajax({
        url: url,
        method: 'get',
        data: { value: value },
        success: function (res, ststus, xhr) {
            if (res==='True') {
                $('[type="submit"]').removeAttr('disabled');
                $('#isAvailable').hide();
                return;
            }
            //$('#isAvailable').html(value + ' is already registered').show().css('color', 'red');
            $('[type="submit"]').attr('disabled', 'disabled');
            alert($('[value="' + value + '"]').val(value));
            $('[value="' + value + '"]').appendTo(value + ' is already registered').css('color', 'red');
        }
    })
}



