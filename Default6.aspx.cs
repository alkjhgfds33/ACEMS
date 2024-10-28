using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //上傳圖檔並顯示在畫面上
        if (uploadFile.PostedFile != null)
        {
            // File was sent
            HttpPostedFile myFile = uploadFile.PostedFile;

            // Get size of uploaded file
            int nFileLen = myFile.ContentLength;

            // Allocate a buffer for reading of the file
            byte[] myData = new byte[nFileLen];
            myFile.InputStream.Read(myData, 0, nFileLen);
            Response.Clear();
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(myData);
        }
        else
        {
            // No file
        }
    }
}