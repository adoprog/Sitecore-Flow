using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Flow.Helpers;
using Sitecore.Links;
using Sitecore.Sites;
using Sitecore.Workflows.Simple;

namespace Sitecore.Flow.Workflow.Actions
{
  public class ItemFieldsToFlow
  {
    private const string HttpPostUrlField = "HTTP POST URL";
    private const string ContextSiteField = "Context Site";

    public void Process(WorkflowPipelineArgs args)
    {
      Item dataItem = args.DataItem;
      if (args.DataItem == null)
      {
        return;
      }

      var url = args.ProcessorItem.InnerItem[HttpPostUrlField];
      var contextSite = args.ProcessorItem.InnerItem[ContextSiteField];
      var requestor = new Requestor();
      if (dataItem != null && dataItem.Parent != null)
      {
        var json = GetFieldsJson(dataItem, contextSite);
        Task.Run(() => requestor.PostRequest((string)url, json));
      }
    }

    private static string GetFieldsJson(Item item, string contextSite)
    {
      var sb = new StringBuilder();
      sb.Append("{");
      var fieldDescriptors = new List<string>();
      var urlOptions = new UrlOptions();
      urlOptions.AlwaysIncludeServerUrl = false;
      if (!string.IsNullOrEmpty(contextSite))
      {
        urlOptions.Site = SiteContext.GetSite(contextSite);
      }

      var url = LinkManager.GetItemUrl(item, urlOptions);
      url = url.Replace(" ", "%20");
      url = Settings.GetSetting("Sitecore.Flow.Actions.ServerUrl") + url;
      fieldDescriptors.Add($" \"ItemUrl\" : \"{url}\" ");

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