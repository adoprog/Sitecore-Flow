using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Data;
using Sitecore.Diagnostics;
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

      var fields = GetFieldsJson(adaptedFields);
      Task.Run(() => PostRequest(fields));
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

    private void PostRequest(string json)
    {
      try
      {
        var httpWebRequest = (HttpWebRequest) WebRequest.Create(TriggerAddress);
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
      catch (Exception ex)
      {
        Log.Error("PostToFlow: Action failed to execute", ex);
      }
    }
  }
}