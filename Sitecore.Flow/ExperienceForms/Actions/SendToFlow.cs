using System.Threading.Tasks;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
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
      var fields = "{}";
      Task.Run(() => requestor.PostRequest(data.TriggerAddress, fields));

      return true;
    }
  }
}