using Sitecore.Diagnostics;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Forms.Shell.UI.Dialogs;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Sitecore.Data;
using Sitecore.Flow.Extensions;
using Sitecore.Web.UI.HtmlControls;

namespace Sitecore.Flow.Dialogs
{
  public class PostToFlowPage : EditorBase
  {
    protected ControlledChecklist ConditionList;
    private readonly IResourceManager resourceManager;

    public const string TriggerAddressrKey = "TriggerAddress";
    private const string FormFieldID = "{C9E1BF85-800A-4247-A3A3-C3F5DFBFD6AA}";
    protected Edit TriggerAddress;
    protected TextBox FlowSchema;

    public string TriggerAddressValue
    {
      get { return GetValueByKey(TriggerAddressrKey, string.Empty); }
      set { SetValue(TriggerAddressrKey, value); }
    }

    public PostToFlowPage() : this(DependenciesManager.ResourceManager)
    {
    }

    public PostToFlowPage(IResourceManager resourceManager)
    {
      Assert.IsNotNull(resourceManager, "resourceManager");
      this.resourceManager = resourceManager;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      if (!Sitecore.Context.ClientPage.IsEvent)
      {
        if (!string.IsNullOrEmpty(TriggerAddressValue))
        {
          TriggerAddress.Value = TriggerAddressValue;
        }

        var formId = Sitecore.Context.Request.QueryString["id"];
        var fieldItems = Database.GetDatabase("master").GetItem(formId)?.Axes.GetDescendants()
          .Where(x => x.IsDerived(new ID(FormFieldID))).ToList();

        if (fieldItems != null && fieldItems.Any())
        {
          var sb = new StringBuilder();
          sb.Append($@"{{ {Environment.NewLine}  ""type"": ""object"",{Environment.NewLine}  ""properties"": {{ ");

          var fieldDescriptors = new List<string>();
          foreach (var field in fieldItems)
          {
            fieldDescriptors.Add($"{Environment.NewLine}    \"{HttpUtility.JavaScriptStringEncode(field.Name)}\": {{\"type\": \"string\"}}");
          }

          sb.Append(fieldDescriptors.Aggregate((i, j) => i + "," + j));
          sb.Append(Environment.NewLine + "  } " + Environment.NewLine + "}");
          FlowSchema.Text = sb.ToString();
        }

        Localize();
      }
      else
      {
        TriggerAddress.Value = Sitecore.Web.WebUtil.GetFormValue("TriggerAddress");
      }
    }

    protected override void SaveValues()
    {
      TriggerAddressValue = TriggerAddress.Value;
      base.SaveValues();
    }
  }
}