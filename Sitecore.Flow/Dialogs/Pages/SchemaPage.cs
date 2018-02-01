using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Sitecore.Controls;
using Sitecore.Data.Fields;
using Sitecore.Web;

namespace Sitecore.Flow.Dialogs.Pages
{
  public class SchemaPage : ModalDialogPage
  {
    protected TextBox FlowSchema;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      var itemId = WebUtil.GetQueryString("id");
      var fields = Sitecore.Context.ContentDatabase.GetItem(itemId).Fields;

      var sb = new StringBuilder();
      sb.Append($@"{{ {Environment.NewLine}  ""type"": ""object"",{Environment.NewLine}  ""properties"": {{ ");
      var fieldDescriptors = new List<string>();
      foreach (Field field in fields)
      {
        if (field.Name.StartsWith("__"))
        {
          continue;
        }

        fieldDescriptors.Add($"{Environment.NewLine}\"{HttpUtility.JavaScriptStringEncode(field.Name)}\": {{\"type\": \"string\"}}");
      }

      fieldDescriptors.Add($"{Environment.NewLine}\"ItemUrl\": {{\"type\": \"string\"}}");

      sb.Append(fieldDescriptors.Aggregate((i, j) => i + "," + j));
      sb.Append(Environment.NewLine + "  } " + Environment.NewLine + "}");
      FlowSchema.Text = sb.ToString();
    }
  }
}