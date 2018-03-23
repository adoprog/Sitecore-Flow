using System.IO;
using System.Net;
using System.Threading.Tasks;
using Sitecore.XConnect.Collection.Model;
using Sitecore.Xdb.MarketingAutomation.Core.Activity;
using Sitecore.Xdb.MarketingAutomation.Core.Processing.Plan;

namespace Sitecore.Flow.MarketingAutomation.Activities
{
  public class SendToFlowActivity : IActivity
  {
    public IActivityServices Services { get; set; }

    public string triggerAddress { get; set; }

    public ActivityResult Invoke(IContactProcessingContext context)
    {
      //get email facet from context contact
      EmailAddressList facet = context.Contact.GetFacet<EmailAddressList>();
      var fields = "{}";
      Task.Run(() => PostRequest(triggerAddress, fields));

      return (ActivityResult) new SuccessMove();
    }

    public void PostRequest(string url, string json)
    {
      var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
      httpWebRequest.ContentType = "application/json";
      httpWebRequest.Method = "POST";

      using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
      {
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
      }

      var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
      using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
      {
        var result = streamReader.ReadToEnd();
      }
    }
  }
}