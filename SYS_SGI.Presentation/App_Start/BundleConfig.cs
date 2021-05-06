using System.Web.Optimization;

namespace SYS_SGI.Presentation
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterLayout(bundles);

            RegisterShared(bundles);

            RegisterAccount(bundles);

            RegisterHome(bundles);

            RegisterCharts(bundles);

            RegisterWidgets(bundles);

            RegisterElements(bundles);

            RegisterForms(bundles);

            RegisterTables(bundles);

            RegisterCalendar(bundles);

            RegisterMailbox(bundles);

            RegisterExamples(bundles);

            RegisterDocumentation(bundles);

            RegisterGMD(bundles);

            RegisterValidateUnobtrusive(bundles);

            RegisterUnobtrusiveAjax(bundles);

            bundles.IgnoreList.Clear();
        }

        private static void RegisterGMD(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Contenido/estilos/Scripts").Include(
                                         "~/Contenido/estilos/js/Script.js",
                                         "~/Contenido/estilos/js/Validaciones.js",
                                         "~/Contenido/estilos/js/jquery-confirm.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/Style").Include(
                                        "~/Contenido/estilos/css/Style.css",
                                        "~/Contenido/estilos/css/jquery-confirm.css"));
        }

        private static void RegisterDocumentation(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Documentation/menu").Include(
                "~/Scripts/Plantilla/Documentation/Documentation-menu.js"));
        }

        private static void RegisterExamples(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/Blank/menu").Include(
                "~/Scripts/Plantilla/Examples/Blank-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/Invoice/menu").Include(
                "~/Scripts/Plantilla/Examples/Invoice-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/Lockscreen/menu").Include(
                "~/Scripts/Plantilla/Examples/Lockscreen-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/Login").Include(
                "~/Scripts/Plantilla/Examples/Login.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/Login/menu").Include(
                "~/Scripts/Plantilla/Examples/Login-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/P404/menu").Include(
                "~/Scripts/Plantilla/Examples/P404-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/P500/menu").Include(
                "~/Scripts/Plantilla/Examples/P500-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/Pace").Include(
                "~/Scripts/Plantilla/Examples/Pace.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/Pace/menu").Include(
                "~/Scripts/Plantilla/Examples/Pace-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/ProfileEx/menu").Include(
                "~/Scripts/Plantilla/Examples/ProfileEx-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/Register").Include(
                "~/Scripts/Plantilla/Examples/Register.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Examples/Register/menu").Include(
                "~/Scripts/Plantilla/Examples/Register-menu.js"));
        }

        private static void RegisterMailbox(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Mailbox/Inbox").Include(
                "~/Scripts/Plantilla/Mailbox/Inbox.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Mailbox/Inbox/menu").Include(
                "~/Scripts/Plantilla/Mailbox/Inbox-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Mailbox/Compose").Include(
                "~/Scripts/Plantilla/Mailbox/Compose.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Mailbox/Compose/menu").Include(
               "~/Scripts/Plantilla/Mailbox/Compose-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Mailbox/Read/menu").Include(
                "~/Scripts/Plantilla/Mailbox/Read-menu.js"));
        }

        private static void RegisterCalendar(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Calendar").Include(
                "~/Scripts/Plantilla/Calendar/Calendar.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Calendar/menu").Include(
                "~/Scripts/Plantilla/Calendar/Calendar-menu.js"));
        }

        private static void RegisterTables(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Tables/Simple/menu").Include(
                "~/Scripts/Plantilla/Tables/Simple-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Tables/Data").Include(
                "~/Scripts/Plantilla/Tables/Data.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Tables/Data/menu").Include(
                "~/Scripts/Plantilla/Tables/Data-menu.js"));
        }

        private static void RegisterCharts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Contenido/estilos/Chart").Include(
                "~/Contenido/estilos/js/Chart.js",
                "~/Contenido/estilos/js/jquery.canvasjs.min.js"));
        }

        private static void RegisterForms(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Forms/Advanced").Include(
                "~/Scripts/Plantilla/Forms/Advanced.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Forms/Advanced/menu").Include(
                "~/Scripts/Plantilla/Forms/Advanced-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Forms/Editors").Include(
                "~/Scripts/Plantilla/Forms/Editors.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Forms/Editors/menu").Include(
                "~/Scripts/Plantilla/Forms/Editors-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Forms/General/menu").Include(
                "~/Scripts/Plantilla/Forms/General-menu.js"));
        }

        private static void RegisterElements(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Elements/Buttons/menu").Include(
                "~/Scripts/Plantilla/Elements/Buttons-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Elements/General/menu").Include(
                "~/Scripts/Plantilla/Elements/General-menu.js"));

            bundles.Add(new StyleBundle("~/Styles/Elements/General").Include(
                "~/Styles/Elements/General.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Elements/Icons/menu").Include(
                "~/Scripts/Plantilla/Elements/Icons-menu.js"));

            bundles.Add(new StyleBundle("~/Styles/Elements/Icons").Include(
                "~/Styles/Elements/Icons.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Elements/Modals/menu").Include(
                "~/Scripts/Plantilla/Elements/Modals-menu.js"));

            bundles.Add(new StyleBundle("~/Styles/Elements/Modals").Include(
                "~/Styles/Elements/Modals.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Elements/Sliders").Include(
                "~/Scripts/Plantilla/Elements/Sliders.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Elements/Sliders/menu").Include(
                "~/Scripts/Plantilla/Elements/Sliders-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Elements/Timeline/menu").Include(
                "~/Scripts/Plantilla/Elements/Timeline-menu.js"));
        }

        private static void RegisterWidgets(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Widgets/menu").Include(
                "~/Scripts/Plantilla/Widgets/Widgets-menu.js"));
        }

        //private static void RegisterCharts(BundleCollection bundles)
        //{
        //    bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Charts/ChartsJs").Include(
        //        "~/Scripts/Plantilla/Charts/ChartsJs.js"));
        //    bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Charts/ChartsJs/menu").Include(
        //                    "~/Scripts/Plantilla/Charts/ChartsJs-menu.js"));

        //    bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Charts/Morris").Include(
        //        "~/Scripts/Plantilla/Charts/Morris.js"));

        //    bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Charts/Morris/menu").Include(
        //        "~/Scripts/Plantilla/Charts/Morris-menu.js"));

        //    bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Charts/Flot").Include(
        //        "~/Scripts/Plantilla/Charts/Flot.js"));

        //    bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Charts/Flot/menu").Include(
        //        "~/Scripts/Plantilla/Charts/Flot-menu.js"));

        //    bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Charts/Inline").Include(
        //        "~/Scripts/Plantilla/Charts/Inline.js"));

        //    bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Charts/Inline/menu").Include(
        //        "~/Scripts/Plantilla/Charts/Inline-menu.js"));
        //}

        private static void RegisterHome(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Home/DashboardV1").Include(
                "~/Scripts/Plantilla/Home/DashboardV1.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Home/DashboardV1/menu").Include(
                "~/Scripts/Plantilla/Home/DashboardV1-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Home/DashboardV2").Include(
                "~/Scripts/Plantilla/Home/DashboardV2.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Home/DashboardV2/menu").Include(
                "~/Scripts/Plantilla/Home/DashboardV2-menu.js"));
        }

        private static void RegisterAccount(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Account/Login").Include(
                "~/Scripts/Plantilla/Account/Login.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Account/Register").Include(
                "~/Scripts/Plantilla/Account/Register.js"));
        }

        private static void RegisterShared(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Plantilla/Shared/_Layout").Include(
                "~/Scripts/Plantilla/Shared/_Layout.js"));
        }

        private static void RegisterLayout(BundleCollection bundles)
        {
            // bootstrap
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/bootstrap/js").Include(
                "~/Contenido/estilos/AdminLTE/bootstrap/js/bootstrap.min.js",
                "~/Contenido/estilos/AdminLTE/bootstrap/js/bootstrap-toggle.min.js"
                ));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/bootstrap/css").Include(
                "~/Contenido/estilos/AdminLTE/bootstrap/css/bootstrap.min.css",
                "~/Contenido/estilos/AdminLTE/bootstrap/css/bootstrap-toggle.min.css"
                ));

            // dist
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/dist/js").Include(
                "~/Contenido/estilos/AdminLTE/dist/js/app.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/dist/css").Include(
                "~/Contenido/estilos/AdminLTE/dist/css/admin-lte.min.css"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/dist/css/skins").Include(
                "~/Contenido/estilos/AdminLTE/dist/css/skins/_all-skins.min.css"));

            // documentation
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/documentation/js").Include(
                "~/Contenido/estilos/AdminLTE/documentation/js/docs.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/documentation/css").Include(
                "~/Contenido/estilos/AdminLTE/documentation/css/style.css"));

            // plugins | bootstrap-slider
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/bootstrap-slider/js").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/bootstrap-slider/js/bootstrap-slider.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/bootstrap-slider/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/bootstrap-slider/css/slider.css"));

            // plugins | bootstrap-wysihtml5
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/bootstrap-wysihtml5/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/bootstrap-wysihtml5/js/bootstrap3-wysihtml5.all.min.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/bootstrap-wysihtml5/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/bootstrap-wysihtml5/css/bootstrap3-wysihtml5.min.css"));

            // plugins | chartjs
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/chartjs/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/chartjs/js/chart.min.js"));

            // plugins | ckeditor
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/ckeditor/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/ckeditor/js/ckeditor.js"));

            // plugins | colorpicker
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/colorpicker/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/colorpicker/js/bootstrap-colorpicker.min.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/colorpicker/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/colorpicker/css/bootstrap-colorpicker.css"));

            // plugins | datatables
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/datatables/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/datatables/js/jquery.dataTables.min.js",
                                         "~/Contenido/estilos/AdminLTE/plugins/datatables/js/dataTables.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/datatables/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/datatables/css/dataTables.bootstrap.css"));

            // plugins | datepicker
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/datepicker/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/datepicker/js/bootstrap-datepicker.js",
                                         "~/Contenido/estilos/AdminLTE/plugins/datepicker/js/locales/bootstrap-datepicker*"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/datepicker/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/datepicker/css/datepicker3.css"));

            // plugins | daterangepicker
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/daterangepicker/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/daterangepicker/js/moment.min.js",
                                         "~/Contenido/estilos/AdminLTE/plugins/daterangepicker/js/daterangepicker.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/daterangepicker/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/daterangepicker/css/daterangepicker-bs3.css"));

            // plugins | fastclick
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/fastclick/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/fastclick/js/fastclick.min.js"));

            // plugins | flot
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/flot/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/flot/js/jquery.flot.min.js",
                                         "~/Contenido/estilos/AdminLTE/plugins/flot/js/jquery.flot.resize.min.js",
                                         "~/Contenido/estilos/AdminLTE/plugins/flot/js/jquery.flot.pie.min.js",
                                         "~/Contenido/estilos/AdminLTE/plugins/flot/js/jquery.flot.categories.min.js"));

            // plugins | font-awesome
            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/font-awesome/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/font-awesome/css/font-awesome.min.css"));

            // plugins | fullcalendar
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/fullcalendar/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/fullcalendar/js/fullcalendar.min.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/fullcalendar/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/fullcalendar/css/fullcalendar.min.css"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/fullcalendar/css/print").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/fullcalendar/css/print/fullcalendar.print.css"));

            // plugins | icheck
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/icheck/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/icheck/js/icheck.min.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/icheck/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/icheck/css/all.css"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/icheck/css/flat").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/icheck/css/flat/flat.css"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/icheck/css/sqare/blue").Include(
                                       "~/Contenido/estilos/AdminLTE/plugins/icheck/css/sqare/blue.css"));

            // plugins | input-mask
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/input-mask/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/input-mask/js/jquery.inputmask.js",
                                         "~/Contenido/estilos/AdminLTE/plugins/input-mask/js/jquery.inputmask.date.extensions.js",
                                         "~/Contenido/estilos/AdminLTE/plugins/input-mask/js/jquery.inputmask.extensions.js"));

            // plugins | ionicons
            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/ionicons/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/ionicons/css/ionicons.min.css"));

            // plugins | ionslider
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/ionslider/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/ionslider/js/ion.rangeSlider.min.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/ionslider/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/ionslider/css/ion.rangeSlider.css",
                                        "~/Contenido/estilos/AdminLTE/plugins/ionslider/css/ion.rangeSlider.skinNice.css"));

            // plugins | jquery
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/jquery/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/jquery/js/jQuery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Jquery").Include(
                                         "~/Scripts/Jquery/jquery-3.1.1.min.js"));

            // plugins | jquery-validate
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/jquery-validate/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/jquery-validate/js/jquery.validate*"));

            // plugins | jquery-ui
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/jquery-ui/js"));

            // plugins | jvectormap
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/jvectormap/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/jvectormap/js/jquery-jvectormap-1.2.2.min.js",
                                         "~/Contenido/estilos/AdminLTE/plugins/jvectormap/js/jquery-jvectormap-world-mill-en.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/jvectormap/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/jvectormap/css/jquery-jvectormap-1.2.2.css"));

            // plugins | knob
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/knob/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/knob/js/jquery.knob.js"));

            // plugins | morris
            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/morris/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/morris/css/morris.css"));

            // plugins | momentjs
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/momentjs/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/momentjs/js/moment.min.js"));

            // plugins | pace
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/pace/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/pace/js/pace.min.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/pace/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/pace/css/pace.min.css"));

            // plugins | slimscroll
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/slimscroll/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/slimscroll/js/jquery.slimscroll.min.js"));

            // plugins | sparkline
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/sparkline/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/sparkline/js/jquery.sparkline.min.js"));

            // plugins | timepicker
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/timepicker/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/timepicker/js/bootstrap-timepicker.min.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/timepicker/css").Include(
                                       "~/Contenido/estilos/AdminLTE/plugins/timepicker/css/bootstrap-timepicker.min.css"));

            
            // plugins | raphael
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/raphael/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/raphael/js/raphael-min.js"));

            // plugins | select2 3.5
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/select2/3.5/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/select2/3.5/js/select2.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/select2/3.5/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/select2/3.5/css/select2.css",
                                        "~/Contenido/estilos/AdminLTE/plugins/select2/3.5/css/select2-bootstrap.css"));
            // plugins | select2 4.0
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/select2/4.0/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/select2/4.0/js/select2.full.min.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/select2/4.0/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/select2/4.0/css/select2.min.css"));

            // plugins | morris
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/morris/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/morris/js/morris.min.js"));

            // plugins | TreeView
            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/plugins/tree/css").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/tree/js/themes/default/style.css",
                                        "~/Contenido/estilos/AdminLTE/plugins/tree/css/tree.css"));

            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/plugins/tree/js").Include(
                                         "~/Contenido/estilos/AdminLTE/plugins/tree/js/jquery.jstree.js"));

            // plugins | Notifi
            bundles.Add(new ScriptBundle("~/Notificaciones/Scripts").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/notifi/bootstrap-notify.min.js"));

            // plugins | bootstrap-dialog
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/bootstrap-dialog/Scripts").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/bootstrap-dialog/js/_bootstrap-dialog.js"
                                        //"~/Contenido/estilos/AdminLTE/plugins/bootstrap-dialog/js/bootstrap-dialog.min.js"
                                        ));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/bootstrap-dialog/Style").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/bootstrap-dialog/css/bootstrap-dialog.min.css"));

            // plugins | gmd-grid
            bundles.Add(new ScriptBundle("~/Contenido/estilos/AdminLTE/gmd-grid/Scripts").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/gmd-grid/js/jquery.numeric.js",
                                        "~/Contenido/estilos/AdminLTE/plugins/gmd-grid/js/jsGMDWebGrid.js"));

            bundles.Add(new StyleBundle("~/Contenido/estilos/AdminLTE/gmd-grid/Style").Include(
                                        "~/Contenido/estilos/AdminLTE/plugins/gmd-grid/css/GMDGrid.css"));
        }

        private static void RegisterValidateUnobtrusive(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/ValidateUnobtrusive").Include(
                "~/Scripts/ValidateUnobtrusive/jquery.validate.unobtrusive.min.js"));
        }

        private static void RegisterUnobtrusiveAjax(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/UnobtrusiveAjax").Include(
                "~/Scripts/UnobtrusiveAjax/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/Validate/jquery.validate.min.js",
                "~/Scripts/jquery.validate.bootstrap.js"
                ));
        }
    }
}