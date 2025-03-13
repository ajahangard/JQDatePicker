#define MSAJAX

using System.Web.UI;
using System.Web.UI.HtmlControls;


namespace JQControls
{
    internal static class Utility
    {
        internal static void RegisterCSS(Control control, string cssFileName, bool useJS)
        {
            string cssUrl = control.Page.ClientScript.GetWebResourceUrl(control.GetType(),
                "JQControls.css." + cssFileName);

            if (useJS)
            {
                RegisterScript(control, cssUrl,
                    "$(\"<link href='" + cssUrl + "' type='text/css' rel='stylesheet' />\").appendTo('head');", false);
            }
            else
            {
                HtmlLink cssLink = new HtmlLink();
                cssLink.Href = cssUrl;
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("type", "text/css");
                control.Page.Header.Controls.Add(cssLink);
            }
        }

        internal static void RegisterJS(Control control, string jsFileName)
        {
#if MSAJAX
            ScriptManager.RegisterClientScriptResource(control.Page, control.GetType(), "JQControls.js." + jsFileName); 
#else
                control.Page.ClientScript.RegisterClientScriptResource(control.GetType(),
                    "JQControls.js." + jsFileName);
#endif
        }

        internal static void RegisterScript(Control control, string key, string script)
        {
            RegisterScript(control, key, script, true);
        }

        internal static void RegisterScript(Control control, string key, string script, bool afterContents)
        {
#if MSAJAX
            if (afterContents)
            {
                ScriptManager.RegisterStartupScript(control.Page, control.Page.GetType(), key, script, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(control.Page, control.Page.GetType(), key, script, true);
            }
#else
                if (afterContents)
                {
                    control.Page.ClientScript.RegisterStartupScript(control.GetType(), key, script, true);
                }
                else
                {
                    control.Page.ClientScript.RegisterClientScriptBlock(control.GetType(), key, script, true);
                }
#endif
        }
    }
}