using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Flow.Helpers;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Actions.Base;

namespace Sitecore.Flow.Actions
{
  public class PostToFlow : WffmSaveAction
  {
    public string TriggerAddress { get; set; }

    public override void Execute(ID formId, AdaptedResultList adaptedFields, ActionCallContext actionCallContext = null,
      params object[] data)
    {
      Assert.ArgumentNotNull(adaptedFields, nameof(adaptedFields));

      var requestor = new Requestor();
      var fields = GetFieldsJson(adaptedFields);
      Task.Run(() => requestor.PostRequest(TriggerAddress, fields));
    }

    private static string GetFieldsJson(AdaptedResultList adaptedFields)
    {
      var sb = new StringBuilder();
      sb.Append("{");
      var fieldDescriptors = new List<string>();
      foreach (AdaptedControlResult adaptedField in adaptedFields)
      {
        fieldDescriptors.Add(" \"" + adaptedField.FieldName + "\" : \"" +
                             HttpUtility.JavaScriptStringEncode(adaptedField.Value) + "\" ");
      }

      sb.Append(fieldDescriptors.Aggregate((i, j) => i + "," + j));
      sb.Append("}");
      return sb.ToString();
    }
  }
}