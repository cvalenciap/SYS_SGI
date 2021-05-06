function IsNumberKeyPress(e) {
    var valid = "0123456789";
    var key = String.fromCharCode(event.keyCode);
    if (valid.indexOf("" + key) == "-1") window.event.keyCode = 0;
}

function validarSoloNumeros(evento) {
    evento = (evento) ? evento : event;
    var charCode = (evento.charCode) ? evento.charCode : ((evento.keyCode) ? evento.keyCode : ((evento.which) ? evento.which : 0));

    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function validarSoloNumerosYPunto(evento) {
    evento = (evento) ? evento : event;
    var charCode = (evento.charCode) ? evento.charCode : ((evento.keyCode) ? evento.keyCode : ((evento.which) ? evento.which : 0));

    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
        return false;
    return true;
}

function validarSoloNumerosLetrasEspaciosEnBlancoYGuionBajo(evento) {
    var tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 8 || tecla == 0)
        return true;
    var patron = /^[A-Za-z0-9_\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}

function validarSoloNumerosLetrasEspaciosEnBlancoYGuionBajoPunto(evento) {
    var tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 8 || tecla == 0 || tecla == 46)
        return true;
    var patron = /^[A-Za-z0-9_\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}

function validarSoloNumerosLetrasEspaciosEnBlancoYGuionBajoPuntoGuion(evento) {
    var tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 8 || tecla == 0 || tecla == 46 || tecla == 45)
        return true;
    var patron = /^[A-Za-z0-9_\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}

function validarSoloNumerosLetrasGuionBajo(evento) {
    var tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 32)
        return false;
    if (tecla == 8)
        return true;
    var patron = /^[A-Za-z0-9_-\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}

function validarSoloNumerosLetrasYEspaciosEnBlanco(evento) {
    var tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 8 || tecla == 0)
        return true;
    var patron = /^[A-Za-z0-9\s\u00f1\u00d1\u00e1\u00c1\u00e9\u00c9\u00ed\u00cd\u00f3\u00d3\u00fa\u00da\u0029]$/;
    var codigo = String.fromCharCode(tecla);
    return patron.test(codigo);
}

function validarTeclasPrincipales(tecla) {
    var arr = new Array(8, 9, 35, 36, 37, 38, 39, 40);
    for (ctaArr = 0; ctaArr < arr.length; ctaArr++) {
        if (tecla == arr[ctaArr])
            return true;
    }
    return false;
}

function validarSoloLetras(evento) {
    var key = evento.keyCode ? evento.keyCode : evento.which ? evento.which : evento.charCode; //Aceptado por FF y IE
    if (validarTeclasPrincipales(key))
        return true;
    if (evento && key == 13)/*enter*/
        return true;
    if (evento && key == 45) /*.*/
        return true;
    if (evento && key == 46) /*.*/
        return true;
    if (evento && key >= 65 && key <= 90)
        return true;
    if (evento && key >= 97 && key <= 122)
        return true;
    if (evento && key == 32)/*espace*/
        return true;
    if (evento && key >= 164 && key <= 165)
        return true;
    if (evento && key == 209)/*letra Ñ*/
        return true;
    if (evento && key == 241)/*letra ñ*/
        return true;
    if (evento && key == 193)/*letra Á*/
        return true;
    if (evento && key == 201)/*letra É*/
        return true;
    if (evento && key == 205)/*letra Í*/
        return true;
    if (evento && key == 211)/*letra Ó*/
        return true;
    if (evento && key == 218)/*letra Ú*/
        return true;
    if (evento && key == 225)/*letra á*/
        return true;
    if (evento && key == 233)/*letra é*/
        return true;
    if (evento && key == 237)/*letra í*/
        return true;
    if (evento && key == 243)/*letra ó*/
        return true;
    if (evento && key == 250)/*letra ú*/
        return true;
    return false;
}

function ValidarSiEsNumericoValido(monto) {
    if ($.trim(monto) == '') return false;

    return /^([0-9])*[.]?[0-9]*$/.test(monto);
}

function ValidarSiEsNumericoValidoNulo(monto) {
    return /^([0-9])*[.]?[0-9]*$/.test(monto);
}

function formateafecha(fecha) {
    if (fecha.length >= 1) {
        var resultado = fecha.substr(0, fecha.length - 1);
        var ultimo = fecha.substr(fecha.length - 1);
        if (fecha.length <= 10) {
            if (fecha.length != 3 && fecha.length != 6) {
                if (fecha.length >= 3 && fecha.length < 6 &&
                        fecha.indexOf('/') != 2) {
                    resultado = fecha.substr(0, 2);
                } else {
                    if (fecha.length >= 6 && fecha.lastIndexOf('/') != 5) {
                        resultado = fecha.substr(0, 5);
                    } else {
                        if (ultimo.search(/\d/g) > -1) {
                            if (fecha.length <= 2) {
                                if (fecha <= 31) {
                                    resultado = resultado + ultimo;
                                }
                            } else {
                                if (fecha.length <= 5) {
                                    if (fecha.substr(3, fecha.length - 1) <= 12) {
                                        resultado = resultado + ultimo;
                                    }
                                } else {
                                    resultado = resultado + ultimo;
                                }
                            }
                        }
                    }
                }
            } else {
                if (ultimo == '/') {
                    resultado = resultado + ultimo;
                }
            }
        }
        return resultado;
    } else {
        return fecha;
    }
}

function formatearFechaBase(fecha) {
    var d = fecha.split("/")[0];
    var m = fecha.split("/")[1];
    var y = fecha.split("/")[2];
    if (fecha.indexOf('/') > 1) {
        return d + '/' + m + '/' + y;
    } else {
        var yf = fecha.split("-")[0];
        var mf = fecha.split("-")[1];
        var df = fecha.split("-")[2];
        return df + '/' + mf + '/' + yf;
    }
}

function validarFormatoFecha(campo, evento) {
    var fecha = campo.value;
    if (fecha.length < 10) {
        var ultimo = String.fromCharCode(evento.keyCode);
        if (validarSoloNumeros(evento)) {
            if (fecha.length < 2) {
                var dia = fecha + ultimo;
                if (dia <= 31) {
                    if (fecha.length == 1) dia = dia + '/';
                    fecha = dia;
                }
            } else {
                if (fecha.length < 5) {
                    var mes = fecha.substr(3, fecha.length - 1) + ultimo;
                    if (mes <= 12) {
                        if (fecha.length == 4) mes = mes + '/';
                        fecha = fecha.substr(0, 3) + mes;
                    }
                } else {
                    fecha = fecha + ultimo;
                }
            }
        }
    }
    campo.value = fecha;
    return false;
}

function AgregarComas(nStr) {
    nStr += '';
    nStr = nStr.split(',').join('');
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '.00';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function CrearTreeView(data, handlerAllSeleccionarNodo) {
    $('#demoTree').jstree({
        json_data: {
            data: data
        },
        checkbox: {
            real_checkboxes: true,
            real_checkboxes_names: function (n) {
                return [("check_" + (n[0].id || Math.ceil(Math.random() * 10000))), n[0].id]
            }
        },
        plugins: ["themes", "json_data", "ui", "checkbox"]
    }).bind("loaded.jstree", function (event, data) {
        $('#demoTree').on("change_state.jstree", handlerAllSeleccionarNodo).jstree('check_node', 'li[selected=selected]');
    });
};

function validateEmail(control) {
    var validEmail = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (validEmail.test($(control).val()))
        return true;
    return false;
}