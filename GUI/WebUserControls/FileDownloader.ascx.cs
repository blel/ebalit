﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EbalitWebForms.GUI.WebUserControls
{
    public partial class FileDownloader : System.Web.UI.UserControl
    {
        public string Data { get; set; }

        public string FileName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SendFileToClient()
        {
            if (string.IsNullOrWhiteSpace(Data) || string.IsNullOrWhiteSpace(FileName))
            {
                return;
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Cookies.Clear();
            //Add the header & other information      
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.CacheControl = "private";
            Response.Charset = System.Text.Encoding.UTF8.WebName;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("Content-Length", Data.Length.ToString());
            Response.AppendHeader("Pragma", "cache");
            Response.AppendHeader("Expires", "60");
            Response.AppendHeader("Content-Disposition",
            "attachment; " + "filename=\"" + FileName + ".csv\"; ");

            Response.ContentType = "text/plain";
           
            //Write it back to the client    
            Response.Write(Data);
            Response.End();
        }
    }
}