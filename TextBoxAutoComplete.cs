using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.ComponentModel;
using System.Web.UI;
using System.Security.Permissions;
using System.IO;
using System.Web.UI.HtmlControls;

namespace Psa.Pv3.WebControls
{
    [
    AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
    ToolboxData("<{0}:TextBoxAutoComplete runat=\"server\"> </{0}:TextBoxAutoComplete>")
    ]
    public class TextBoxAutoComplete : TextBox, IScriptControl, ICallbackEventHandler
    {
        private const string jqeuryResource = "Psa.Pv3.WebControls.DropDownCheckBox.EmbeddedResources.jquery-1.6.1.min.js";
        private const string AutoCompleteResource = "Psa.Pv3.WebControls.TextBox.AutoComplete.js";

        public TextBoxAutoComplete()
        {
            this.MaxLength = 40;
            this.Method = "";
            this.Width = 200;
            this.AutoPostBack = false;
        }


        #region Properties
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(200),
        Description("The width of textboxAutocomplete in px.")
        ]
        public virtual string WidthTextBox
        {
            get
            {
                string s = (string)ViewState["widthTB"];
                return (s == null) ? "200" : s;
            }
            set
            {
                ViewState["widthTB"] = value;
            }
        }

        [Bindable(true),
        Category("Default"),
        Description("The WebMethod to call.")]
        public virtual string Method
        {
            get
            {
                string s = (string)ViewState["MethodTextBox"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["MethodTextBox"] = value;
            }
        }
        #endregion

        #region Renders
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, MaxLength.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "false");
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Page.ClientScript.RegisterClientScriptInclude("jquery", Page.ClientScript.GetWebResourceUrl(this.GetType(), jqeuryResource));
            Page.ClientScript.RegisterClientScriptInclude("autocomplete", Page.ClientScript.GetWebResourceUrl(this.GetType(), AutoCompleteResource));

        }

        protected override void Render(HtmlTextWriter writer)
        {
            var url = HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1] + "/" + this.Method;
            writer.Write("<div>");
            writer.Write("<script >" +
                           "var i = -1; $( document ).ready(function() {" +
                           " $(\"#" + this.ClientID + "\").keyup(function( event ) {" +
                             "if(event.keyCode != 40 && event.keyCode != 38 && event.keyCode != 13){ " +
                                "try{ if($(\"#" + this.ClientID + "\").val().length>0)" +
                                    " autoComplete('" + this.ClientID + "','" + url + "',this,event);else document.getElementById(\"" + this.ClientID + "\" + \"_div\").style.visibility = \"hidden\";}catch(e){alert(e);}" +
                             " }else{funcNavigateDiv('" + this.ClientID + "',event.keyCode);}" +
                             " });" +
                           " });" +
                        "</script>");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width:" + this.WidthTextBox + "px");
            base.Render(writer);
            writer.Write("<br/><div id=" + this.ClientID + "_div" + " ></div>");
            writer.Write("<style>" +
                          "#" + this.ClientID + "_div" + "{" +
                           " opacity : 0.9;" +
                           " background-color: #E9EDF4;" +
                           " position: absolute;" +
                           " z-index: 1;" +
                           " height: 150px;" +
                           " visibility: hidden;" +
                           " border-width: 1px;" +
                           " border-style: solid;" +
                           " border-color: #3D489B;" +
                           " overflow:auto;}" +
                        "</style>");
            writer.Write("</div>");
        }
        #endregion

        #region IScriptControl Members

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICallbackEventHandler Members

        public string GetCallbackResult()
        {
            throw new NotImplementedException();
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
