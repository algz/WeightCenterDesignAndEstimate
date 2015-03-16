using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{

    /// <summary>
    /// 基础库配置
    /// </summary>
   public class BasicDBSetting
    {
       private string str_Server;
       public string strServer
       {
           get
           {
               return str_Server;
           }
           set
           {
               str_Server = value;
           }
       }

       private string str_UserName;
       public string strUserName
       {
           get
           {
               return str_UserName;
           }
           set
           {
               str_UserName = value;
           }
       }

       private string str_Pwd;
       public string strPwd
       {
           get
           {
               return str_Pwd;
           }
           set
           {
               str_Pwd = value;
           }
       }

       private string str_Url;
       public string strUrl
       {
           get
           {
               return str_Url;
           }
           set
           {
               str_Url = value;
           }
       }

       private string str_Folder;
       public string strFolder
       {
           get
           {
               return str_Folder;
           }
           set
           {
               str_Folder = value;
           }
       }
    }
}
