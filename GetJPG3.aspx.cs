using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetJPG3 : System.Web.UI.Page
{
    int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void tmrRefresh_Tick(object sender, EventArgs e)
    {

        string srcImageUrl = "http://www.acems.tw:44459/image/8002-01.jpg";

        System.Drawing.Image.GetThumbnailImageAbort callBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

        //取得原始圖片
        System.Drawing.Image img = this.getImageFromURL(srcImageUrl);

        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        Response.ClearContent();
        Response.BinaryWrite(ms.ToArray());
        Response.ContentType = "image/jpeg";//指定輸出格式為圖形 


    }





    #region 取得網路上的圖片
    /// <summary>
    /// 取得網路上的圖片
    /// </summary>
    /// <param name="strUrl">圖片的Url路徑</param>
    /// <returns>回傳 System.Drawing.Image物件</returns>
    public System.Drawing.Image getImageFromURL(string strUrl)
    {
        System.Drawing.Image MyImage = null;

        try
        {
            //建立一個 Web Request
            WebRequest MyWebRequest = WebRequest.Create(strUrl);
            //由 Web Request 取得 Web Response
            WebResponse MyWebResponse = MyWebRequest.GetResponse();
            //由 Web Response 取得 Stream
            Stream MyStream = MyWebResponse.GetResponseStream();
            //由 Stream 取得 Image
            MyImage = System.Drawing.Image.FromStream(MyStream);

            //該關的關一關, 該放的放一放
            MyStream.Close();
            MyStream.Dispose();
            MyWebResponse.Close();
            MyWebResponse = null;
            MyWebRequest = null;

        }
        catch (Exception ex)
        {
            throw new Exception("getImageFromURL(string strUrl)發生例外，可能抓不到網路上的圖片" + strUrl);
        }

        //回傳 Image
        return MyImage;
    }

    #endregion

    private bool ThumbnailCallback()
    {
        return false;
    }

}