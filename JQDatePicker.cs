using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JQControls
{
    [DefaultProperty("Date")]
    [ToolboxData("<{0}:JQDatePicker runat=server></{0}:JQDatePicker>")]
    [ToolboxBitmap(typeof(System.Web.UI.WebControls.Calendar))]
    [ParseChildren(true)]
    public class JQDatePicker : TextBox
    {
        #region Regional Properties
        [Category("Regional")]
        [DefaultValue(Regional.auto)]
        public Regional Regional
        {
            set
            {
                DateTime? oldDate = Date;
                ViewState["Regional"] = value;
                Date = oldDate;
            }
            get
            {
                return (Regional)(ViewState["Regional"] ?? Regional.auto);
            }
        }

        [Category("Regional")]
        [DefaultValue(CalendarType.Default)]
        public CalendarType CalendarType
        {
            set
            {
                DateTime? oldDate = Date;
                ViewState["CalendarType"] = value;
                Date = oldDate;
            }
            get
            {
                return (CalendarType)(ViewState["CalendarType"] ?? CalendarType.Default);
            }
        }

        [Browsable(false)]
        public CalendarType ActualCalendarType
        {
            get
            {
                if (CalendarType != CalendarType.Default) return CalendarType;

                switch (Regional)
                {
                    case Regional.auto:
                        switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower())
                        {
                            case "fa-ir":
                                return CalendarType.Jalali;
                            case "ar-sa":
                                return CalendarType.Hijri;
                            default:
                                return CalendarType.Georgian;
                        }
                    case Regional.fa:
                        return CalendarType.Jalali;
                    case Regional.ar:
                        return CalendarType.Hijri;
                    default:
                        return CalendarType.Georgian;
                }
            }
        }

        [Category("Regional")]
        public string CloseText
        {
            set
            {
                ViewState["CloseText"] = value;
            }
            get
            {
                return (string)ViewState["CloseText"];
            }
        }

        [Category("Regional")]
        public string PrevText
        {
            set
            {
                ViewState["PrevText"] = value;
            }
            get
            {
                return (string)ViewState["PrevText"];
            }
        }

        [Category("Regional")]
        public string NextText
        {
            set
            {
                ViewState["NextText"] = value;
            }
            get
            {
                return (string)ViewState["NextText"];
            }
        }

        [Category("Regional")]
        public string CurrentText
        {
            set
            {
                ViewState["CurrentText"] = value;
            }
            get
            {
                return (string)ViewState["CurrentText"];
            }
        }

        protected SimpleItemCollection monthNames;
        [Category("Regional")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public SimpleItemCollection MonthNames
        {
            get
            {
                if (monthNames == null) monthNames = new SimpleItemCollection();
                return monthNames;
            }
        }

        protected SimpleItemCollection monthNamesShort;
        [Category("Regional")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public SimpleItemCollection MonthNamesShort
        {
            get
            {
                if (monthNamesShort == null) monthNamesShort = new SimpleItemCollection();
                return monthNamesShort;
            }
        }

        protected SimpleItemCollection dayNames;
        [Category("Regional")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public SimpleItemCollection DayNames
        {
            get
            {
                if (dayNames == null) dayNames = new SimpleItemCollection();
                return dayNames;
            }
        }

        protected SimpleItemCollection dayNamesShort;
        [Category("Regional")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public SimpleItemCollection DayNamesShort
        {
            get
            {
                if (dayNamesShort == null) dayNamesShort = new SimpleItemCollection();
                return dayNamesShort;
            }
        }

        protected SimpleItemCollection dayNamesMin;
        [Category("Regional")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public SimpleItemCollection DayNamesMin
        {
            get
            {
                if (dayNamesMin == null) dayNamesMin = new SimpleItemCollection();
                return dayNamesMin;
            }
        }

        [Category("Regional")]
        public DayOfWeek? FirstDay
        {
            set
            {
                ViewState["FirstDay"] = value;
            }
            get
            {
                return (DayOfWeek?)ViewState["FirstDay"];
            }
        }

        [Category("Regional")]
        [DefaultValue(DateFormat.DMY)]
        public DateFormat DateFormat
        {
            set
            {
                DateTime? oldDate = Date;
                ViewState["DateFormat"] = value;
                Date = oldDate;
            }
            get
            {
                if (!base.DesignMode && HttpContext.Current.Request.Browser.Browser.Equals("IE") && ViewState["IEDateFormat"] != null)
                {
                    return (DateFormat)ViewState["IEDateFormat"];
                }
                return (DateFormat)(ViewState["DateFormat"] ?? DateFormat.DMY);
            }
        }

        [Category("Regional")]
        public DateFormat? IEDateFormat
        {
            set
            {
                DateTime? oldDate = Date;
                ViewState["IEDateFormat"] = value;
                Date = oldDate;
            }
            get
            {
                return (DateFormat?)(ViewState["IEDateFormat"]);
            }
        }

        [Category("Regional")]
        public bool? IsRTL
        {
            set
            {
                ViewState["IsRTL"] = value;
            }
            get
            {
                return (bool?)ViewState["IsRTL"];
            }
        }
        #endregion

        #region Appearance Properties
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ShowButtonPanel
        {
            set
            {
                ViewState["ShowButtonPanel"] = value;
            }
            get
            {
                return (bool)(ViewState["ShowButtonPanel"] ?? false);
            }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool HideIfNoPrevNext
        {
            set
            {
                ViewState["HideIfNoPrevNext"] = value;
            }
            get
            {
                return (bool)(ViewState["HideIfNoPrevNext"] ?? false);
            }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ShowSelectButton
        {
            set
            {
                ViewState["ShowButton"] = value;
            }
            get
            {
                return (bool)(ViewState["ShowButton"] ?? false);
            }
        }

        [UrlProperty]
        [Category("Appearance")]
        public string ButtonImage
        {
            set
            {
                ViewState["ButtonImage"] = value;
            }
            get
            {
                return (string)ViewState["ButtonImage"];
            }
        }

        [Category("Appearance")]
        public string ButtonText
        {
            set
            {
                ViewState["ButtonText"] = value;
            }
            get
            {
                return (string)ViewState["ButtonText"];
            }
        }

        [Category("Appearance")]
        public string WeekHeader
        {
            set
            {
                ViewState["WeekHeader"] = value;
            }
            get
            {
                return (string)ViewState["WeekHeader"];
            }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ShowMonthAfterYear
        {
            set
            {
                ViewState["ShowMonthAfterYear"] = value;
            }
            get
            {
                return (bool)(ViewState["ShowMonthAfterYear"] ?? false);
            }
        }

        [Category("Appearance")]
        [DefaultValue(AnimationType.Default)]
        public AnimationType AnimationType
        {
            set
            {
                ViewState["AnimationType"] = value;
            }
            get
            {
                return (AnimationType)(ViewState["AnimationType"] ?? AnimationType.Default);
            }
        }

        [Category("Appearance")]
        [DefaultValue(AnimationSpeed.Default)]
        public AnimationSpeed AnimationSpeed
        {
            set
            {
                ViewState["AnimationSpeed"] = value;
            }
            get
            {
                return (AnimationSpeed)(ViewState["AnimationSpeed"] ?? AnimationSpeed.Default);
            }
        }
        #endregion

        #region Behavior Properties
        [DefaultValue(AutoCompleteType.Disabled)]
        public override AutoCompleteType AutoCompleteType
        {
            get
            {
                return (AutoCompleteType)(ViewState["AutoCompleteType"] ?? AutoCompleteType.Disabled);
            }
            set
            {
                base.AutoCompleteType = value;
            }
        }

        [Category("Behavior")]
        public DateTime? Date
        {
            get
            {
                int y, m, d;
                readValues(out y, out m, out d);
                return getDate(y, m, d);
            }
            set
            {
                Text = getDateString(value);
            }
        }

        [Category("Behavior")]
        public DateTime? SqlDateTime
        {
            get
            {
                DateTime? date = Date;
                return date.HasValue && date >= new DateTime(1753, 1, 1) ? date : null;
            }
            set
            {
                Date = value;
            }
        }

        [Category("Behavior")]
        public DateTime? SqlSmallDateTime
        {
            get
            {
                DateTime? date = Date;
                return date.HasValue && date >= new DateTime(1900, 1, 1) && date <= new DateTime(2079, 6, 6) ? date : null;
            }
            set
            {
                Date = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ChangeMonth
        {
            set
            {
                ViewState["ChangeMonth"] = value;
            }
            get
            {
                return (bool)(ViewState["ChangeMonth"] ?? false);
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ChangeYear
        {
            set
            {
                ViewState["ChangeYear"] = value;
            }
            get
            {
                return (bool)(ViewState["ChangeYear"] ?? false);
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ShowWeek
        {
            set
            {
                ViewState["ShowWeek"] = value;
            }
            get
            {
                return (bool)(ViewState["ShowWeek"] ?? false);
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool AutoSize
        {
            set
            {
                ViewState["AutoSize"] = value;
            }
            get
            {
                return (bool)(ViewState["AutoSize"] ?? false);
            }
        }

        [Category("Behavior")]
        [DefaultValue(1)]
        public int NumberOfMonths
        {
            set
            {
                if (value < 1) throw new IndexOutOfRangeException();
                ViewState["NumberOfMonths"] = value;
            }
            get
            {
                return (int)(ViewState["NumberOfMonths"] ?? 1);
            }
        }

        [Category("Behavior")]
        [DefaultValue(0)]
        public int ShowCurrentAtPos
        {
            set
            {
                if (value < 0) throw new IndexOutOfRangeException();
                ViewState["ShowCurrentAtPos"] = value;
            }
            get
            {
                return (int)(ViewState["ShowCurrentAtPos"] ?? 0);
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ShowInline
        {
            set
            {
                ViewState["ShowInline"] = value;
            }
            get
            {
                return (bool)(ViewState["ShowInline"] ?? false);
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ShowOtherMonths
        {
            set
            {
                ViewState["ShowOtherMonths"] = value;
            }
            get
            {
                return (bool)(ViewState["ShowOtherMonths"] ?? false);
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool GotoCurrent
        {
            set
            {
                ViewState["GotoCurrent"] = value;
            }
            get
            {
                return (bool)(ViewState["GotoCurrent"] ?? false);
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool AutoCorrectDate
        {
            set
            {
                ViewState["AutoCorrectDate"] = value;
            }
            get
            {
                return (bool)(ViewState["AutoCorrectDate"] ?? true);
            }
        }

        [Category("Behavior")]
        [DefaultValue(null)]
        public int? ShortYearCutoff
        {
            set
            {
                ViewState["ShortYearCutoff"] = value;
            }
            get
            {
                return (int?)ViewState["ShortYearCutoff"];
            }
        }

        [Category("Behavior")]
        public DateTime? MinDate
        {
            set
            {
                ViewState["MinDate"] = value;
            }
            get
            {
                return (DateTime?)ViewState["MinDate"];
            }
        }

        [Category("Behavior")]
        public DateTime? MaxDate
        {
            set
            {
                ViewState["MaxDate"] = value;
            }
            get
            {
                return (DateTime?)ViewState["MaxDate"];
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool RegisterCssWithJavaScript
        {
            set
            {
                ViewState["RegisterCssWithJavaScript"] = value;
            }
            get
            {
                return (bool)(ViewState["RegisterCssWithJavaScript"] ?? false);
            }
        }
        #endregion

        #region Client Callback Properties
        [Category("Client Callback")]
        public string OnClientSelect
        {
            set
            {
                ViewState["OnClientSelect"] = value;
            }
            get
            {
                return (string)ViewState["OnClientSelect"];
            }
        }

        [Category("Client Callback")]
        public string OnClientClose
        {
            set
            {
                ViewState["OnClientClose"] = value;
            }
            get
            {
                return (string)ViewState["OnClientClose"];
            }
        }

        [Category("Client Callback")]
        public string OnClientChangeMonthYear
        {
            set
            {
                ViewState["OnClientChangeMonthYear"] = value;
            }
            get
            {
                return (string)ViewState["OnClientChangeMonthYear"];
            }
        }
        #endregion

        #region public Methods

        public string DateString(DateFormat format)
        {
            return getDateString(Date, format, ActualCalendarType);
        }

        public string DateString(DateFormat format, CalendarType calendar)
        {
            return getDateString(Date, format, calendar);
        }

        #endregion

        #region protected Methods

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            validateDate();
        }

        protected override void OnPreRender(EventArgs e)
        {
            Utility.RegisterJS(this, "jquery.ui.datepicker-cc.js");

            if (ActualCalendarType != CalendarType.Default)
                Utility.RegisterJS(this, "calendar.js");
            if (ActualCalendarType == CalendarType.Jalali)
                Utility.RegisterJS(this, "jquery.ui.datepicker-cc-fa.js");
            if (ActualCalendarType == CalendarType.Hijri)
                Utility.RegisterJS(this, "jquery.ui.datepicker-cc-ar.js");

            Utility.RegisterJS(this, "jquery.ui.datepicker-validate.js");
            Utility.RegisterCSS(this, "ui.datepicker.css", RegisterCssWithJavaScript);

            attachDatepicker();

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (ShowInline)
            {
                Style[HtmlTextWriterStyle.Display] = "none";
                base.Render(writer);
                Style.Remove(HtmlTextWriterStyle.Display);

                string _id = ID;
                ID += "_dp";
                AddAttributesToRender(writer);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
                ID = _id;
            }
            else
            {
                base.Render(writer);
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            // workaround for the bug with autocomplete=disabled
            if (TextMode == TextBoxMode.SingleLine &&
                Context != null && Context.Request.Browser["supportsVCard"] != "true" &&
                AutoCompleteType == AutoCompleteType.Disabled)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "off");
            }
        }

        protected void validateDate()
        {
            //any suggestions?
            Date = Date;
        }

        protected void attachDatepicker()
        {
            StringBuilder script = new StringBuilder(";jQuery(function($){");

            script.Append("$('#");
            script.Append(ClientID);
            if (ShowInline) script.Append("_dp");
            script.Append("')");

            script.Append(".datepicker(");
            using (JsonBuilder parameters = new JsonBuilder())
            {
                if (!Enabled || ReadOnly) parameters.Add("beforeShowDay", "function(){return [false];}", false);

                parameters.Add("dateFormat", getDateFormatString(DateFormat));
                switch (Regional)
                {
                    case Regional.auto:
                        switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower())
                        {
                            case "fa-ir":
                                parameters.Add("regional", "fa");
                                break;
                            case "ar-sa":
                                parameters.Add("regional", "ar");
                                break;
                            default:
                                parameters.Add("regional", "");
                                break;
                        }
                        break;
                    case Regional.fa:
                        parameters.Add("regional", "fa");
                        break;
                    case Regional.ar:
                        parameters.Add("regional", "ar");
                        break;
                    default:
                        parameters.Add("regional", "");
                        break;
                }
                if (CalendarType != CalendarType.Default)
                {
                    switch (CalendarType)
                    {
                        case CalendarType.Jalali:
                            parameters.Add("calendar", "JalaliDate");
                            break;
                        case CalendarType.Hijri:
                            parameters.Add("calendar", "HijriDate");
                            break;
                        default:
                            parameters.Add("calendar", "Date");
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(CloseText)) parameters.Add("closeText", CloseText);
                if (!string.IsNullOrEmpty(PrevText)) parameters.Add("prevText", PrevText);
                if (!string.IsNullOrEmpty(NextText)) parameters.Add("nextText", NextText);
                if (!string.IsNullOrEmpty(CurrentText)) parameters.Add("currentText", CurrentText);
                if (MonthNames.Count == 12) parameters.Add("monthNames", MonthNames);
                if (MonthNamesShort.Count == 12) parameters.Add("monthNamesShort", MonthNamesShort);
                if (DayNames.Count == 7) parameters.Add("dayNames", DayNames);
                if (DayNamesShort.Count == 7) parameters.Add("dayNamesShort", DayNamesShort);
                if (DayNamesMin.Count == 7) parameters.Add("dayNamesMin", DayNamesMin);
                if (FirstDay.HasValue) parameters.Add("firstDay", (int)FirstDay.Value);
                if (ShortYearCutoff.HasValue) parameters.Add("shortYearCutoff", ShortYearCutoff.Value);
                if (IsRTL.HasValue) parameters.Add("isRTL", IsRTL.Value);

                if (AnimationType != AnimationType.Default && AnimationSpeed != AnimationSpeed.Disabled) parameters.Add("showAnim", getAnimationString(AnimationType));
                if (AnimationSpeed != AnimationSpeed.Default) parameters.Add("duration", getDurationString(AnimationSpeed));
                if (HideIfNoPrevNext) parameters.Add("hideIfNoPrevNext", true);
                if (ShowButtonPanel) parameters.Add("showButtonPanel", true);
                if (ChangeMonth) parameters.Add("changeMonth", true);
                if (ChangeYear) parameters.Add("changeYear", true);
                if (ShowWeek) parameters.Add("showWeek", true);
                if (AutoSize) parameters.Add("autoSize", true);
                if (ShowMonthAfterYear) parameters.Add("showMonthAfterYear", true);
                if (NumberOfMonths > 1) parameters.Add("numberOfMonths", NumberOfMonths);
                if (0 < ShowCurrentAtPos && ShowCurrentAtPos < NumberOfMonths) parameters.Add("showCurrentAtPos", ShowCurrentAtPos);
                if (ShowOtherMonths) parameters.Add("showOtherMonths", true);
                if (GotoCurrent) parameters.Add("gotoCurrent", true);
                if (!string.IsNullOrEmpty(ButtonText)) parameters.Add("buttonText", ButtonText);
                if (!string.IsNullOrEmpty(WeekHeader)) parameters.Add("weekHeader", WeekHeader);
                if (ShowSelectButton || !string.IsNullOrEmpty(ButtonImage))
                {
                    string buttonImage = ButtonImage;
                    if (string.IsNullOrEmpty(buttonImage)) buttonImage = (this.Enabled && !this.ReadOnly) ?
                         Page.ClientScript.GetWebResourceUrl(GetType(), "JQControls.images.dp.png") :
                         Page.ClientScript.GetWebResourceUrl(GetType(), "JQControls.images.dp-gray.png");
                    parameters.Add("buttonImage", buttonImage);
                    parameters.Add("showOn", "button");
                    parameters.Add("buttonImageOnly", true);
                }

                if (MinDate.HasValue) parameters.Add("minDate", getJSDate(MinDate.Value), false);
                if (MaxDate.HasValue) parameters.Add("maxDate", getJSDate(MaxDate.Value), false);

                if (!string.IsNullOrEmpty(OnClientSelect))
                    parameters.Add("onSelect", getClientFunctionString(OnClientSelect, "dateStr", "inst"), false);
                if (!string.IsNullOrEmpty(OnClientClose))
                    parameters.Add("onClose", getClientFunctionString(OnClientClose, "dateStr", "inst"), false);
                if (!string.IsNullOrEmpty(OnClientChangeMonthYear))
                    parameters.Add("onChangeMonthYear", getClientFunctionString(OnClientChangeMonthYear, "year", "month", "inst"), false);

                if (ShowInline)
                {
                    parameters.Add("altField", "#" + ClientID);
                    int y, m, d;
                    readValues(out y, out m, out d);
                    if (y != 0) parameters.Add("defaultDate", getJSDate(y, m, d), false);
                }

                script.Append(parameters.ToString());
            }
            script.Append(")");

            if (!ShowInline)
                script.AppendFormat(".change(function(){{validateDatepicker(this,{0});}})", AutoCorrectDate ? 1 : 0);

            script.Append(";");
            script.Append("});\n");

            Utility.RegisterScript(this, ClientID, script.ToString());
            script.Length = 0;
        }

        protected string getJSDate(DateTime date)
        {
            switch (ActualCalendarType)
            {
                case CalendarType.Jalali:
                    PersianCalendar pc = new PersianCalendar();
                    return "new JalaliDate(" + pc.GetYear(date) + "," + (pc.GetMonth(date) - 1) + "," + pc.GetDayOfMonth(date) + ")";
                case CalendarType.Hijri:
                    HijriCalendar hc = new HijriCalendar();
                    return "new HijriDate(" + hc.GetYear(date) + "," + (hc.GetMonth(date) - 1) + "," + hc.GetDayOfMonth(date) + ")";
                default:
                    return "new Date(" + date.Year + "," + (date.Month - 1) + "," + date.Day + ")";
            }
        }

        protected string getJSDate(int y, int m, int d)
        {
            switch (ActualCalendarType)
            {
                case CalendarType.Jalali:
                    return "new JalaliDate(" + y + "," + (m - 1) + "," + d + ")";
                case CalendarType.Hijri:
                    return "new HijriDate(" + y + "," + (m - 1) + "," + d + ")";
                default:
                    return "new Date(" + y + "," + (m - 1) + "," + d + ")";
            }
        }

        protected string getAnimationString(AnimationType animation)
        {
            switch (animation)
            {
                case AnimationType.Default: return "show";
                case AnimationType.Fade: return "fadeIn";
                case AnimationType.Slide: return "slideDown";
            }
            throw new ArgumentOutOfRangeException();
        }

        protected string getDurationString(AnimationSpeed speed)
        {
            switch (speed)
            {
                case AnimationSpeed.Default: return "default";
                case AnimationSpeed.Slow: return "slow";
                case AnimationSpeed.Fast: return "fast";
                case AnimationSpeed.Disabled: return "";
            }
            throw new ArgumentOutOfRangeException();
        }

        protected string getDateFormatString(DateFormat dateformat)
        {
            switch (dateformat)
            {
                case DateFormat.DMY: return "dd/mm/yy";
                case DateFormat.YMD: return "yy/mm/dd";
                case DateFormat.MDY: return "mm/dd/yy";
            }
            return "dd/mm/yy";
        }

        protected string getClientFunctionString(string clientString, params string[] parameters)
        {
            StringBuilder functionBuilder = new StringBuilder("function(");
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i != 0) functionBuilder.Append(",");
                functionBuilder.Append(parameters[i]);
            }
            functionBuilder.Append("){try{eval('");
            functionBuilder.Append(clientString.Replace("'", @"\'"));
            functionBuilder.Append("');}catch(ex){alert(ex);}}");
            return functionBuilder.ToString();
        }

        protected int getDatePartIndex(char datepart)
        {
            return DateFormat.ToString().IndexOf(datepart);
        }

        protected void readValues(out int y, out int m, out int d)
        {
            y = m = d = 0;
            string[] splitted = Text.Split('/');
            if (splitted.Length == 3)
            {
                if (!int.TryParse(splitted[getDatePartIndex('D')], out d) ||
                    !int.TryParse(splitted[getDatePartIndex('M')], out m) ||
                    !int.TryParse(splitted[getDatePartIndex('Y')], out y))
                {
                    y = m = d = 0;
                }
            }
        }

        protected string formatValues(int y, int m, int d, DateFormat dateFormat)
        {
            string format = null;
            switch (dateFormat)
            {
                case DateFormat.DMY:
                    format = "{2:00}/{1:00}/{0:0000}";
                    break;
                case DateFormat.YMD:
                    format = "{0:0000}/{1:00}/{2:00}";
                    break;
                case DateFormat.MDY:
                    format = "{1:00}/{2:00}/{0:0000}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return string.Format(format, y, m, d);
        }

        protected DateTime? getDate(int y, int m, int d)
        {
            return getDate(y, m, d, ActualCalendarType, true);
        }

        protected DateTime? getDate(int y, int m, int d, CalendarType calendar, bool checkRange)
        {
            if (y < 100)
            {
                int thisYear = DateTime.Now.Year;
                y += thisYear - thisYear % 100 +
                    (y <= (ShortYearCutoff ?? 99) ? 0 : -100);
            }

            DateTime date;
            switch (calendar)
            {
                case CalendarType.Jalali:
                    PersianCalendar pc = new PersianCalendar();
                    try
                    {
                        date = pc.ToDateTime(y, m, d, 0, 0, 0, 0);
                    }
                    catch
                    {
                        return null;
                    }
                    break;
                case CalendarType.Hijri:
                    HijriCalendar hc = new HijriCalendar();
                    try
                    {
                        date = hc.ToDateTime(y, m, d, 0, 0, 0, 0);
                    }
                    catch
                    {
                        return null;
                    }
                    break;
                default:
                    try
                    {
                        date = new DateTime(y, m, d);
                    }
                    catch
                    {
                        return null;
                    }
                    break;
            }

            if (checkRange)
            {
                if (MinDate.HasValue && date.Date < MinDate.Value.Date) return null;
                if (MaxDate.HasValue && date.Date > MaxDate.Value.Date) return null;
            }

            return date;
        }

        protected string getDateString(DateTime? date)
        {
            return getDateString(date, DateFormat, ActualCalendarType);
        }

        protected string getDateString(DateTime? date, DateFormat dateFormat, CalendarType calendar)
        {
            if (date.HasValue)
            {
                try
                {
                    switch (calendar)
                    {
                        case CalendarType.Jalali:
                            PersianCalendar pc = new PersianCalendar();
                            return formatValues(pc.GetYear(date.Value), pc.GetMonth(date.Value), pc.GetDayOfMonth(date.Value), dateFormat);
                        case CalendarType.Hijri:
                            HijriCalendar hc = new HijriCalendar();
                            return formatValues(hc.GetYear(date.Value), hc.GetMonth(date.Value), hc.GetDayOfMonth(date.Value), dateFormat);
                        default:
                            return formatValues(date.Value.Year, date.Value.Month, date.Value.Day, dateFormat);
                    }
                }
                catch
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion
    }
}