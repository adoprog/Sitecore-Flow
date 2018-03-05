using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using Sitecore.Flow.ExperienceForms.Actions.Models;
using Sitecore.Flow.Helpers;

namespace Sitecore.Flow.ExperienceForms.Actions
{
  public class SendToFlow : SubmitActionBase<SendToFlowActionData>
  {
    public SendToFlow(ISubmitActionData submitActionData) : base(submitActionData)
    {
    }

    protected override bool Execute(SendToFlowActionData data, FormSubmitContext formSubmitContext)
    {
      Assert.ArgumentNotNull(formSubmitContext, "formSubmitContext");
      if (string.IsNullOrEmpty(data.TriggerAddress))
      {
        return false;
      }

      var requestor = new Requestor();
      var fields = GetFieldsJson(formSubmitContext.Fields);
      Task.Run(() => requestor.PostRequest(data.TriggerAddress, fields));

      return true;
    }

    private static string GetFieldsJson(IList<IViewModel> fields)
    {
      var sb = new StringBuilder();
      sb.Append("{");
      var fieldDescriptors = new List<string>();
      foreach (var field in fields)
      {
        if(field is StringInputViewModel)
        {
          var stringField = field as StringInputViewModel;
          fieldDescriptors.Add(" \"" + stringField.Title + "\" : \"" +
                               HttpUtility.JavaScriptStringEncode(stringField.Value) + "\" ");
        }
      }

      sb.Append(fieldDescriptors.Aggregate((i, j) => i + "," + j));
      sb.Append("}");
      return sb.ToString();
    }
  }
}