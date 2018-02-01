using System;
using System.IO;
using System.Net;
using Sitecore.Diagnostics;

namespace Sitecore.Flow.Helpers
{
  public class Requestor
  {
    public void PostRequest(string url, string json)
    {
      try
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
      catch (Exception ex)
      {
        Log.Error("PostToFlow: Action failed to execute", ex);
      }
    }
  }
}