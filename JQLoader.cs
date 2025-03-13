using System.ComponentModel;
using System.Web.UI;

namespace JQControls
{
    [NonVisualControl]
    [DefaultProperty("Theme")]
    [ToolboxData("<{0}:JQLoader runat=server />")]
    public class JQLoader : Control
    {
        #region Properties
        [DefaultValue(true)]
        public bool LoadJQScript
        {
            set { ViewState["LoadJQScript"] = value; }
            get { return (bool)(ViewState["LoadJQScript"] ?? true); }
        }

        [DefaultValue(true)]
        public bool LoadUIScript
        {
            set { ViewState["LoadUIScript"] = value; }
            get { return (bool)(ViewState["LoadUIScript"] ?? true); }
        }

        [DefaultValue(true)]
        public bool LoadUICoreStyles
        {
            set { ViewState["LoadUICoreStyles"] = value; }
            get { return (bool)(ViewState["LoadUICoreStyles"] ?? true); }
        }

        [DefaultValue(JQTheme.UILightness)]
        public JQTheme Theme
        {
            set { ViewState["Theme"] = value; }
            get { return (JQTheme)(ViewState["Theme"] ?? JQTheme.Start); }
        }
        #endregion

        #region Methods
        protected override void OnPreRender(System.EventArgs e)
        {
            if (LoadJQScript) Utility.RegisterJS(this, "jquery.js");
            if (LoadUIScript) Utility.RegisterJS(this, "jquery.ui.core.js");
            if (LoadUICoreStyles) Utility.RegisterCSS(this, "ui.core.css", false);
            if (Theme != JQTheme.None) Utility.RegisterCSS(this, getThemeCSSName(Theme), false);

            base.OnPreRender(e);
        }

        protected string getThemeCSSName(JQTheme theme)
        {
            switch (theme)
            {
                case JQTheme.BlackTie: return "jquery-ui-1.7.2.black-tie.css";
                case JQTheme.Blitzer: return "jquery-ui-1.7.2.blitzer.css";
                case JQTheme.Cupertino: return "jquery-ui-1.7.2.cupertino.css";
                case JQTheme.DarkHive: return "jquery-ui-1.7.2.dark-hive.css";
                case JQTheme.DotLuv: return "jquery-ui-1.7.2.dot-luv.css";
                case JQTheme.Eggplant: return "jquery-ui-1.7.2.eggplant.css";
                case JQTheme.ExciteBike: return "jquery-ui-1.7.2.excite-bike.css";
                case JQTheme.Flick: return "jquery-ui-1.7.2.flick.css";
                case JQTheme.HotSneaks: return "jquery-ui-1.7.2.hot-sneaks.css";
                case JQTheme.Humanity: return "jquery-ui-1.7.2.humanity.css";
                case JQTheme.LeFrog: return "jquery-ui-1.7.2.le-frog.css";
                case JQTheme.MintChoc: return "jquery-ui-1.7.2.mint-choc.css";
                case JQTheme.Overcast: return "jquery-ui-1.7.2.overcast.css";
                case JQTheme.PepperGrinder: return "jquery-ui-1.7.2.pepper-grinder.css";
                case JQTheme.Redmond: return "jquery-ui-1.7.2.redmond.css";
                case JQTheme.Smoothness: return "jquery-ui-1.7.2.smoothness.css";
                case JQTheme.SouthStreet: return "jquery-ui-1.7.2.south-street.css";
                case JQTheme.Start: return "jquery-ui-1.7.2.start.css";
                case JQTheme.Sunny: return "jquery-ui-1.7.2.sunny.css";
                case JQTheme.SwankyPurse: return "jquery-ui-1.7.2.swanky-purse.css";
                case JQTheme.Trontastic: return "jquery-ui-1.7.2.trontastic.css";
                case JQTheme.UIDarkness: return "jquery-ui-1.7.2.ui-darkness.css";
                case JQTheme.UILightness: return "jquery-ui-1.7.2.ui-lightness.css";
                case JQTheme.Vader: return "jquery-ui-1.7.2.vader.css";
            }
            return null;
        }
        #endregion
    }
}