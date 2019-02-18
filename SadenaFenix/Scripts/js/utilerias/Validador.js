//// JavaScript Validación
//$('document').ready(function () {
//    // Validación para campos de texto exclusivo, sin caracteres especiales ni números
//    var nameregex = /^[a-zA-Z ]+$/;

//    $.validator.addMethod("validname", function (value, element) {
//        return this.optional(element) || nameregex.test(value);
//    });

//    // Máscara para validación de Email
//    var eregex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

//    $.validator.addMethod("validemail", function (value, element) {
//        return this.optional(element) || eregex.test(value);
//    });

//    $("#formulario-contacto").validate({

//        rules:
//        {
//            nombre: {
//                required: true,
//                minlength: 8
//            },
//            email: {
//                required: true,
//                validemail: true
//            },
//            mensaje: {
//                required: true,
//                minlength: 20,
//                maxlength: 300
//            },
//        },
//        messages:
//        {
//            nombre: {
//                required: "Tu Nombre y Apellidos son Importantes",
//                minlength: "Tu Nombre es demasiado corto"
//            },
//            email: {
//                required: "Por Favor, introduzca una dirección de correo",
//                validemail: "Introduzca correctamente su correo"
//            },
//            mensaje: {
//                required: "Tu Nombre y Apellidos son Importantes",
//                minlength: "Tu Mensaje es demasiado corto",
//                maxlength: "Tu Mensaje supera los 300 caracteres"
//            },
//        },
//        errorPlacement: function (error, element) {
//            $(element).closest('.form-group').find('.help-block').html(error.html());
//        },
//        highlight: function (element) {
//            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
//        },
//        unhighlight: function (element, errorClass, validClass) {
//            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
//            $(element).closest('.form-group').find('.help-block').html('');
//        },

//        submitHandler: function (form) {
//            form.action = "pagina que envia el correo.php";
//            form.submit();

//            alert('ok');
//        }
//    });
//})


$(document).ready(function () {
    $('#registerFormId').validate({
        errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page  
        errorElement: 'div',
        errorPlacement: function (error, e) {
            e.parents('.form-group > div').append(error);
        },
        highlight: function (e) {

            $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
            $(e).closest('.help-block').remove();
        },
        success: function (e) {
            e.closest('.form-group').removeClass('has-success has-error');
            e.closest('.help-block').remove();
        },
        rules: {
            'CorreoE': {
                required: true,
                email: true
            },

            'Calle': {
                required: true,
                minlength: 10
            },

            'Numero': {
                required: true,
                minlength: 1
            }
        },
        messages: {
            'CorreoE': 'Please enter valid email address',

            'Calle': {
                required: 'Please provide a calle',
                minlength: 'Your password must be at least 10 characters long'
            },

            'Numero': {
                required: 'Please provide a numero',
                minlength: 'Your password must be at least 1 characters long',               
            }
        }
    });

}); 

function solonumeros(e) {
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57)
        e.preventDefault();
}