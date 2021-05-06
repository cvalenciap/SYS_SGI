window.alert = function (title, mensaje, callbackok) {
    mensaje = mensaje.split('\n').join('<br/>');
    MostrarAlerta(title, mensaje, 'ALERT', callbackok);
};

window.confirm = function (title, mensaje, callbackok, callbackCancel) {
    mensaje = mensaje.split('\n').join('<br/>');
    MostrarAlerta(title, mensaje, 'CONFIRM', callbackok, callbackCancel);
};

function MostrarAlerta(title, mensaje, tipo, callbackOk, callbackCancel) {
    if (tipo == "ALERT") {
        if (title == "") {
            title = "Mensaje del sistema";
        }

        BootstrapDialog.alert({
            title: title,
            message: mensaje,
            type: BootstrapDialog.TYPE_SUCCESS,
            closable: true,
            buttonLabel: 'Aceptar',
            callback: function (result) {
                if (typeof callbackOk == 'function') { callbackOk(); }
            }
        });
    } else {
        if (title == "") {
            title = "Mensaje de confirmación";
        }

        BootstrapDialog.confirm(title, mensaje, function (result) {
            if (result) {
                if (typeof callbackOk == 'function') { callbackOk(); }
            } else {
                if (typeof callbackCancel == 'function') { callbackCancel(); }
            }
        });
    }
}

function MostrarPopUp(idModal, title, idmodalBody, size) {
    if (idmodalBody != undefined && idmodalBody != "") {
        var $divModalBody = $("<div id=\'" + idmodalBody + "\'></div>");
    } else {
        var $divModalBody = $("<div id='divModalBody'></div>");
    }

    var dialogInstance1 = new BootstrapDialog({
        id: idModal,
        closable: true,
        title: $("<h2>" + title + "</h2>"),
        message: $divModalBody,
        draggable: true,
        closeByBackdrop: false
    });
    dialogInstance1.open();
    if (size == 'SIZE_WIDE') {
        BootstrapDialog.dialogs[idModal].setSize(BootstrapDialog.SIZE_WIDE);
    }
}