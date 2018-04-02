using System.IO;
using System.Net;
using System.Threading.Tasks;
using Sitecore.XConnect;
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
      var contact = context.Contact;
      if (!contact.IsKnown)
      {
        return (ActivityResult)new SuccessMove();
      }

      if (!contact.ExpandOptions.FacetKeys.Contains(PersonalInformation.DefaultFacetKey))
      {
        var expandOptions = new ContactExpandOptions(PersonalInformation.DefaultFacetKey, EmailAddressList.DefaultFacetKey);
        contact = Services.Collection.GetContactAsync(contact, expandOptions).ConfigureAwait(false).GetAwaiter().GetResult();
      }

      var emailFacets = contact.GetFacet<EmailAddressList>();
      var personalInfo = contact.GetFacet<PersonalInformation>();
      var fields = "{" +
                   "\"Email\": \"" + emailFacets.PreferredEmail?.SmtpAddress + "\", " +
                   "\"FirstName\": \"" + personalInfo.FirstName + "\", " +
                   "\"MiddleName\": \"" + personalInfo.MiddleName + "\", " +
                   "\"LastName\": \"" + personalInfo.LastName + "\", " +
                   "\"PreferredLanguage\": \"" + personalInfo.PreferredLanguage + "\", " +
                   "\"Title\": \"" + personalInfo.Title + "\", " +
                   "\"JobTitle\": \"" + personalInfo.JobTitle + "\" " +
                   "}";
      Task.Run(() => PostRequest(triggerAddress, fields));

      return (ActivityResult) new SuccessMove();
    }

    public void PostRequest(string url, string json)
    {
      try
      {
        var httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
          streamWriter.Write(json);
          streamWriter.Flush();
          streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
          var result = streamReader.ReadToEnd();
        }
      }
      catch
      {
        // TODO: Use MA logging API to log error 
      }
    }
  }
}