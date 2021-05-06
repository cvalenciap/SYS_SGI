(function ($) {
    function LoadDiv(table) {
        var position = $("#" + table).position();
        var height = $("#" + table).height();
        var width = $("#" + table).width();
        var nameDiv = table + "_Loading_Panel";
        if ($("#" + nameDiv).length == 0) {
            $("<div id='" + nameDiv + "'></div>").addClass("grid-gmd-Loadinng").css({
                top: position.top + "px",
                left: position.left + "px",
                height: height,
                width: width
            }).appendTo($("#" + table).parent());
        }
    }
    /*
    Función para extender un objeto
    target [Object]: Objeto donde se adicionarán propiedades
    source [Object]: Objeto de donde se obtendrán las funcionalidades nuevas
    */
    function extend(target, source) {
        var prop;
        if (!target)
            target = {};
        for (prop in source) {
            target[prop] = source[prop];
        }
        return target;
    }



    function merge() {
        var i,
            len = arguments.length,
            ret = {},
            doCopy = function (copy, original) {
                var value, key;

                for (key in original) {
                    if (original.hasOwnProperty(key)) {
                        value = original[key];
                        if (typeof copy !== 'object') {
                            copy = {};
                        }
                        if (value && typeof value === "object" && Object.prototype.toString.call(value) !== "[object Array]"
                                && typeof value.nodeType !== "number") {
                            copy[key] = doCopy(copy[key] || {}, value);
                        } else {
                            copy[key] = original[key];
                        }
                    }
                }
                return copy;
            };
        for (i = 0; i < len; i++) {
            ret = doCopy(ret, arguments[i]);
        }
        return ret;
    }

    var argsMerge = {};

    $.fn.extend({
        GMDWebGrid: function (args) {
            var defaultOptions = {
                DirectionOrder: "",
                OrderBy: "",
                Filters: [],
                CurrentPageIndex: 1,
                RowsPerPage: 0,
                TotalPage: 1,
                isFirstLoad: false
            };

            this.each(function () {
                var table = this;
                argsMerge = merge(defaultOptions, args);
                $(table).data("config", argsMerge);
                GMDSetEvents(table, argsMerge);
                inputGrid();
            });

            function inputGrid() {
                $("table[class='grid-gmd'] thead").find("input").bind({
                    keypress: function (event) {
                        return validarSoloNumerosLetrasEspaciosEnBlancoYGuionBajoPuntoGuion(event);
                    }
                });
            }

            function GMDSetEvents(table, config) {
                $("thead input[type='text']", table).each(function () {
                    $(this).attr("maxlength", "50");
                });

                GMDSetEventLoad(table, config);
                GMDSetEvetClickRow(table);
            }

            function GetFilter(table) {
                var filters = [];
                $("thead input[type='text']", table).each(function () {
                    var val = $.trim($(this).val());
                    if (val != '') {
                        filters.push({ KeyFilter: $(this).closest("th").attr("data-key"), ValueFilter: val });
                    }
                });
                return filters;
            }

            function GMDSetEventLoad(table, config) {

                var methodLoad = config.callBackMethod;

                var rowOrder = $(table).find("thead tr")[0];
                var arr = [];

                $(rowOrder).find("th").each(function () {
                    arr.push($(this).data("key") + "|" + $(this).find("a").html());
                });

                var string = arr.join();
                //Agregar boton exportar-pdf
                //$("tfoot tr td", table).append('<button class="btn-export-pdf pull-right" data-toggle="tooltip" data-placement="bottom" title="Exportar a PDF" onclick="javascript:exportarAlgo(\'' + string + '\')"><i class="fa fa-file-pdf-o"></i></button>');
                //Agregar boton exportar-excel
                //$("tfoot tr td", table).append('<button class="btn-export-excel pull-right"  data-toggle="tooltip" data-placement="bottom" title="Eportar a Excel" onclick="javascript:exportarAlgo(\'' +string + '\')"><i class="fa fa-file-excel-o"></i></button>');
                
                $("thead th input[type='checkbox']", table).unbind("click");
                $("thead th input[type='checkbox']", table).bind("click", function (event) {
                    $("tbody td input[type='checkbox']", table).attr("checked", $(this).is(":checked"));
                });

                $("thead th input[type='text']", table).unbind("keypress");
                $("thead th input[type='text']", table).bind("keypress", function (event) {
                    // Refrescar grilla con la tecla ENTER
                    var key = (window.event ? window.event.keyCode : event.which); //firefox
                    if (key == 13) {
                        event.preventDefault();
                        var data = $(table).data("config");
                        data.Filters = GetFilter(table);
                        data.CurrentPageIndex = 1;
                        $(table).data("config", data);
                        LoadDiv($(this).closest("table")[0].id);
                        eval(methodLoad + "();");
                    }
                });

                $("thead th", table).unbind("click");
                $("thead th", table).bind("click", function (event) {
                    event.stopPropagation();
                    if ($(this).children().attr("data-sordirection") != undefined) {
                        if ($(this).closest("th").attr("data-key") != "") {
                            if ($(this).children().attr("data-sordirection") != 'Ascending') {
                                $("a", $(this).closest("th")).attr("data-sordirection", "");
                                $(this).children().attr("data-sordirection", "Ascending");
                            }
                            else {
                                $("a", $(this).closest("th")).attr("data-sordirection", "");
                                $(this).children().attr("data-sordirection", "Descending");
                            }

                            var data = $(table).data("config");
                            data.OrderBy = $(this).closest("th").attr("data-key");
                            data.DirectionOrder = $(this).children().attr("data-sordirection");
                            $(table).data("config", data);
                            LoadDiv($(this).closest("table")[0].id);
                            eval(methodLoad + "();");
                        }
                    }
                });

                $("tfoot input[type='text']", table).numeric(false, false);
                $("tfoot input[type='text']", table).unbind("keyup");
                $("tfoot input[type='text']", table).bind("keyup", function (event) {
                    var key = (window.event ? window.event.keyCode : event.which); //firefox
                    if (key == 13) {
                        var data = $(table).data("config");
                        data.CurrentPageIndex = $(this).val() == "" ? "1" : $(this).val();
                        if (data.CurrentPageIndex > data.TotalPage)
                            data.CurrentPageIndex = data.TotalPage;
                        $(table).data("config", data);
                        LoadDiv($(this).closest("table")[0].id);
                        eval(methodLoad + "();");
                    }
                });

                $("tfoot input[id='FirstPage']", table).unbind("click");
                $("tfoot input[id='FirstPage']", table).bind("click", function () {
                    var data = $(table).data("config");
                    data.CurrentPageIndex = 1;
                    $(table).data("config", data);
                    LoadDiv($(this).closest("table")[0].id);
                    eval(methodLoad + "();");
                });

                $("tfoot input[id='PreviusPage']", table).unbind("click");
                $("tfoot input[id='PreviusPage']", table).bind("click", function () {
                    var data = $(table).data("config");
                    if (data.CurrentPageIndex > 1) {
                        data.CurrentPageIndex = data.CurrentPageIndex - 1;
                        $(table).data("config", data);
                        LoadDiv($(this).closest("table")[0].id);
                        eval(methodLoad + "();");
                    }
                });

                $("tfoot input[id='NextPage']", table).unbind("click");
                $("tfoot input[id='NextPage']", table).bind("click", function () {
                    var data = $(table).data("config");
                    if (data.CurrentPageIndex < data.TotalPage) {
                        data.CurrentPageIndex = data.CurrentPageIndex + 1;
                        $(table).data("config", data);
                        LoadDiv($(this).closest("table")[0].id);
                        eval(methodLoad + "();");
                    }
                });

                $("tfoot input[id='LastPage']", table).unbind("click");
                $("tfoot input[id='LastPage']", table).bind("click", function () {
                    var data = $(table).data("config");
                    data.CurrentPageIndex = data.TotalPage;
                    $(table).data("config", data);
                    LoadDiv($(this).closest("table")[0].id);
                    eval(methodLoad + "();");
                });
            }

            function GMDSetEvetClickRow(table) {
                $("tbody tr", table).unbind("click");

                $("tbody tr", table).bind("click", function () {
                    if ($(this).hasClass("selected_row")) {
                        $(this).removeClass("selected_row");
                    }
                    else {
                        $("tr", $(this).closest("tbody")).removeClass("selected_row");
                        $(this).addClass("selected_row");
                    }
                });
            }
        },

        GMDDeserialice: function () {
            var table = this;
            return $(table).data("config");
        },

        RemoveFilter: function (keyFilter) {
            var table = this;
            var bolFindFilter = false;
            var obj;
            if ($(table).data("config").Filters != null) {
                for (var j = 0; j < $(table).data("config").Filters.length; j++) {
                    if ($(table).data("config").Filters[j].KeyFilter == keyFilter) {
                        obj = $(table).data("config").Filters[j];
                        bolFindFilter = true;
                        break;
                    }
                }
            }
            if (bolFindFilter) {
                $(table).data("config").Filters.slice();
                var indexItem = $(table).data("config").Filters.indexOf(obj);
                if (indexItem > -1) {
                    $(table).data("config").Filters.splice(indexItem, 1);
                }
            }
        },

        AddFilter: function (keyFilter, valueFilter) {
            var table = this;
            var bolFindFilter = false;
            if ($(table).data("config").Filters != null) {
                for (var j = 0; j < $(table).data("config").Filters.length; j++) {
                    if ($(table).data("config").Filters[j].KeyFilter == keyFilter) {
                        $(table).data("config").Filters[j].ValueFilter = valueFilter;
                        bolFindFilter = true;
                        break;
                    }
                }
            }
            if (!bolFindFilter) {
                var newFilter = {
                    KeyFilter: keyFilter,
                    ValueFilter: valueFilter
                }
                if ($(table).data("config").Filters == null) {
                    $(table).data("config").Filters = [];
                }
                $(table).data("config").Filters.push(newFilter);
            }
        }
    });
})(jQuery);