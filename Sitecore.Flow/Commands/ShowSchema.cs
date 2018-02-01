
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Flow.Commands
{
  public class ShowSchema : Command
  {
    public override void Execute(CommandContext context)
    {
      UrlHandle urlHandle = new UrlHandle();
      UrlString urlString = new UrlString("/sitecore/shell/~/xaml/Sitecore.Flow.Dialogs.Pages.Schema.aspx");
      urlString.Append("id", context.Items[0].ID.ToString());
      urlHandle.Add(urlString);
      SheerResponse.ShowModalDialog(new ModalDialogOptions(urlString.ToString())
      {
        Width = "650"
      });
    }
  }
}