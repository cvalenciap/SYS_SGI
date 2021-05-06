function modalToggle(nameModal) {
    $(nameModal).modal('toggle');
}

function LoadLoading() {
    $('body').addClass('loading');
}

function UnloadLoading() {
    $('body').removeClass('loading');
}

function envioNotificacion(mensaje, titulo, tipo, icon, from, aling) {
    if (tipo == "success") {
        $.notify({
            icon: icon,
            title: titulo,
            message: mensaje,
            target: '_blank'
        },
            {
                element: 'body',
                position: null,
                type: tipo,
                allow_dismiss: true,
                newest_on_top: false,
                showProgressbar: false,
                placement: {
                    from: from,
                    align: aling
                },
                offset: 20,
                spacing: 10,
                z_index: 1050,
                delay: 5000,
                timer: 1000,
                url_target: '_blank',
                mouse_over: null,
                animate: {
                    enter: 'animated fadeInDown',
                    exit: 'animated fadeOutUp'
                },
                onShow: null,
                onShown: null,
                onClose: null,
                onClosed: null,
                icon_type: 'class',
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> <br>' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
            });
    } else {
        $.notify({
            icon: icon,
            title: titulo,
            message: mensaje,
            target: '_blank'
        },
            {
                element: 'body',
                position: null,
                type: tipo,
                allow_dismiss: true,
                newest_on_top: false,
                showProgressbar: false,
                placement: {
                    from: from,
                    align: aling
                },
                offset: 20,
                spacing: 10,
                z_index: 1050,
                delay: 0,
                timer: 2000,
                url_target: '_blank',
                mouse_over: null,
                animate: {
                    enter: 'animated fadeInDown',
                    exit: 'animated fadeOutUp'
                },
                onShow: null,
                onShown: null,
                onClose: null,
                onClosed: null,
                icon_type: 'class',
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> <br>' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
            });
    }
}

function instanceModal(title, idmodalBody, idModal, tipo, size) {
    var $divModalBody;
    if (idmodalBody != undefined && idmodalBody !== "") {
        $divModalBody = $("<div id=\'" + idmodalBody + "\'></div>");
    } else {
        $divModalBody = $("<div id='divModalBody'></div>");
    }

    var dialogInstance = new BootstrapDialog({        
        id: idModal,
        closable: true,
        title: $('<span>' + title + '</span>'),
        type: tipo,
        message: $divModalBody,
        draggable: true,
        closeByBackdrop: false,
        //Agregar botones de Guardar y Cancelar
        //buttons: [{
        //    label: 'Guardar',
        //    cssClass: 'btn-primary',
        //    action: function (dialog) {
        //        $('form').submit()
        //    }
        //}, {
        //    label: 'Cancelar',
        //    cssClass: 'btn-danger',
        //    action: function (dialogItself) {
        //        dialogItself.close();
        //    }
        //}]
    });
    dialogInstance.open();
    switch (size) {
        case 'SIZE_WIDE':
            BootstrapDialog.dialogs[idModal].setSize(BootstrapDialog.SIZE_WIDE);
            break;
        case 'SIZE_SMALL':
            BootstrapDialog.dialogs[idModal].setSize(BootstrapDialog.SIZE_SMALL);
            break;
        case 'SIZE_GMD':
            BootstrapDialog.dialogs[idModal].setSize(BootstrapDialog.SIZE_GMD);
            break;
        default:
            BootstrapDialog.dialogs[idModal].setSize(BootstrapDialog.SIZE_NORMAL);
            break;
    }
    return true;
}

function mostrarError(mensaje) {
    var item = $("#errorModal #divModalBody");
    if (item.attr("id") != undefined) {
        item.append(mensaje);
        clearInterval(window.refreshId);
    }
}

function selectCell(that) {
    $(that).select();
}

$(function () {
    $(".selectItem").on("click",
        function () {
            /*Seleccionar item de menu*/
            $(".sidebar-menu").find('li.active').removeClass('active');
            $(this).parent('li').parent('ul').parent('li').addClass('active');
            $(this).parent('li').addClass('active');

            /*Escribir titulo y pan de miga por elemento seleccioando desde menu*/
            $("#titulo-opcion").text($(this).parent('li').data("nombre-opcion"));
            $("#breadcrumb-glyphicon").removeClass();
            $("#breadcrumb-glyphicon").addClass($(this).parent('li').parent('ul').parent('li').data("glyphicon-menu"));
            $("#breadcrumb-menu").text($(this).parent('li').parent('ul').parent('li').data("nombre-menu"));
            $("#breadcrumb-sub-opcion").text($(this).parent('li').data("nombre-opcion"));
        });
});

function setTimeOut(hora, minuto, segundo) {
    var tiempo = {
        hora: hora,
        minuto: minuto,
        segundo: segundo
    };

    var tiempoCorriendo = setInterval(function () {
        if (tiempo.segundo === 0) {
            tiempo.segundo = 60;
            tiempo.minuto--;
            tiempo = EvlauarHora(tiempo);
        }

        tiempo.segundo--;

        $("#hour").text(tiempo.hora < 10 ? "0" + tiempo.hora : tiempo.hora);
        $("#minute").text(tiempo.minuto < 10 ? "0" + tiempo.minuto : tiempo.minuto);
        $("#second").text(tiempo.segundo < 10 ? "0" + tiempo.segundo : tiempo.segundo);

        if (tiempo.hora === 0 && tiempo.minuto === 0 && tiempo.segundo === 0) {
            clearInterval(tiempoCorriendo);
            window.GoToSessionExpired();
        }
    }, 1000);
}

function EvlauarHora(tiempo) {
    if (tiempo.minuto === 00) {
        tiempo.minuto = 59;
        if (tiempo.hora > 0)
            tiempo.hora--;
        return tiempo;
    }
    return tiempo;
}