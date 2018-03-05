using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Sitecore.Controls;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Flow.Extensions;
using Sitecore.Web;

namespace Sitecore.Flow.Dialogs.Pages
{
  public class SchemaPage : ModalDialogPage
  {
    private readonly TemplateID FormTemplateId = new TemplateID(new ID("{6ABEE1F2-4AB4-47F0-AD8B-BDB36F37F64C}"));
    private readonly ID InputFieldTemplateId = new ID("{0908030B-4564-42EA-A6FA-C7A5A2D921A8}");
    protected TextBox FlowSchema;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      var itemId = WebUtil.GetQueryString("id");
      var item = Sitecore.Context.ContentDatabase.GetItem(itemId);
      if (item.TemplateID == FormTemplateId)
      {
        FlowSchema.Text = GetFromFormFields(item);
      }
      else
      {
        FlowSchema.Text = GetFromItemFields(item);
      }
    }

    private string GetFromFormFields(Item item)
    {
      var sb = new StringBuilder();
      var fieldItems = item.Axes.GetDescendants().Where(x => x.IsDerived(InputFieldTemplateId));
      sb.Append($@"{{ {Environment.NewLine}  ""type"": ""object"",{Environment.NewLine}  ""properties"": {{ ");
      var fieldDescriptors = new List<string>();
      foreach (var fieldItem in fieldItems)
      {
        fieldDescriptors.Add(
          $"{Environment.NewLine}\"{HttpUtility.JavaScriptStringEncode(fieldItem["Title"])}\": {{\"type\": \"string\"}}");
      }

      if (fieldDescriptors.Any())
      {
        sb.Append(fieldDescriptors.Aggregate((i, j) => i + "," + j));
      }

      sb.Append(Environment.NewLine + "  } " + Environment.NewLine + "}");
      return sb.ToString();
    }

    private string GetFromItemFields(Item item)
    {
      var sb = new StringBuilder();
      var fields = item.Fields;
      sb.Append($@"{{ {Environment.NewLine}  ""type"": ""object"",{Environment.NewLine}  ""properties"": {{ ");
      var fieldDescriptors = new List<string>();
      foreach (Field field in fields)
      {
        if (field.Name.StartsWith("__"))
        {
          continue;
        }

        fieldDescriptors.Add(
          $"{Environment.NewLine}\"{HttpUtility.JavaScriptStringEncode(field.Name)}\": {{\"type\": \"string\"}}");
      }

      fieldDescriptors.Add($"{Environment.NewLine}\"ItemUrl\": {{\"type\": \"string\"}}");

      sb.Append(fieldDescriptors.Aggregate((i, j) => i + "," + j));
      sb.Append(Environment.NewLine + "  } " + Environment.NewLine + "}");
      return sb.ToString();
    }
  }
}