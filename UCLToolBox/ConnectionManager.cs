using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace UCLToolBox
{
  public class ConnectionManager
  {
    public static string FindUrl()
    {
      return "http://10.56.8.34/webAPI/";
      //Skal helst uptimeres
      //Offentlig base adresse: http://81.27.216.103/webAPI/
      //Intern base adresse: http://10.56.8.34/webAPI/
      //Lokal base adresse til emulator http://10.0.2.2:5000/
#if DEBUG
      if (TestUrl("http://10.0.2.2:53524/User/GetProfile"))
      {
        return "http://10.0.2.2:53524/";
      }
#endif
      //If you are not on the same Net as the server
      if (TestUrl("http://81.27.216.103/webAPI/User/GetProfile"))
      {
        return "http://81.27.216.103/webAPI/";
      }
      //If you are on the same Net as the server
      if (TestUrl("http://10.56.8.34/webAPI/User/GetProfile"))
      {
        return "http://10.56.8.34/webAPI/";
      }
      return null;
    }

    private static bool TestUrl(string url)
    {
      try
      {
        WebClient wc = new WebClient();
        string htmlSource = wc.DownloadString(url);
        return true;
      }
      catch (Exception e)
      {
        return false;
      }
    }
  }
}
