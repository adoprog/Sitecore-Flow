using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Flow.Helpers;
using Sitecore.Links;
using Sitecore.Workflows.Simple;

namespace Sitecore.Flow.Workflow.Actions
{
  public class ItemFieldsToFlow
  {
    public void Process(WorkflowPipelineArgs args)
    {
      Item dataItem = args.DataItem;
      if (args.DataItem == null)
      {
        return;
      }

      var url = args.ProcessorItem.InnerItem["Parameters"];
      var requestor = new Requestor();
      if (dataItem != null && dataItem.Parent != null)
      {
        var json = GetFieldsJson(dataItem);
        Task.Run(() => requestor.PostRequest((string)url, json));
      }
    }

    private static string GetFieldsJson(Item item)
    {
      var sb = new StringBuilder();
      sb.Append("{");
      var fieldDescriptors = new List<string>();
      var urlOptions = new UrlOptions();
      urlOptions.AlwaysIncludeServerUrl = true;
      fieldDescriptors.Add($" \"ItemUrl\" : \"{LinkManager.GetItemUrl(item, urlOptions)}\" ");

      foreach (Field field in item.Fields)
      {
        if (field.Name.StartsWith("__"))
        {
          continue;
        }

        fieldDescriptors.Add($" \"{field.Name}\" : \"{HttpUtility.JavaScriptStringEncode(field.Value)}\" ");
      }

      sb.Append(fieldDescriptors.Aggregate((i, j) => i + "," + j));
      sb.Append("}");
      return sb.ToString();
    }
  }
}