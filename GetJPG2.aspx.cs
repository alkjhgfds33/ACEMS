using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetJPG2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Drawing.Image img = System.Drawing.Image.FromFile("Z:/1.Synology-DS1819/20.程式開發/02監測程式開發/00_ACEMS/WEB/Prigect/4.CCTV/全營幕RWD_CCTV影像/test.jpg"); 
        System.IO.MemoryStream ms = new System.IO.MemoryStream(); 
        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); 
        Response.ClearContent(); 
        Response.BinaryWrite(ms.ToArray()); 
        Response.ContentType = "image/jpeg";//指定輸出格式為圖形 

    }
}