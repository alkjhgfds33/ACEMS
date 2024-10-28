using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          //  ss1();
        }
    }

    public Bitmap String2Bmp(string font, int font_size, bool font_bold, Color bgcolor, Color color)
    {
        Font f = new System.Drawing.Font("微軟正黑體", font_size, font_bold ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular); //文字字型
        Brush b = new System.Drawing.SolidBrush(color); //文字顏色

        //計算文字長寬
        int img_width = 0, img_height = 0;
        using (Graphics gr = Graphics.FromImage(new Bitmap(1, 1)))
        {
            SizeF size = gr.MeasureString(font, f);
            img_width = Convert.ToInt32(size.Width);
            img_height = Convert.ToInt32(size.Height);
            gr.Dispose();
        }

        //圖片產生
        Bitmap image = new Bitmap(img_width, img_height);

        //填滿顏色並透明
        using (Graphics g = Graphics.FromImage(image))
        {
            g.Clear(bgcolor);
            // image = Image_ChangeOpacity(image, 0.5f);
            g.Dispose();
        }

        //文字寫入
        using (Graphics g = Graphics.FromImage(image))
        {
            g.DrawString(font, f, b, 0, 0);
            g.Dispose();
        }

        return image;
    }

    public void ss1()
    {
        this.lblTitle1.Text = "測試";

        //Bitmap bmp = String2Bmp("外水位", 180, true, Color.Gray, Color.DarkRed);
        Bitmap bmp = String2Bmp(DateTime.Now.ToString("HH:mm:ss"), 180, true, Color.Gray, Color.DarkRed);

        //bmp.Save("/images/WL1.png");
        bmp.Save(Server.MapPath("/images/WL1.png"), ImageFormat.Jpeg);
        //   this.ccdImage1.ImageUrl 

    }

    protected void s(object sender, ImageClickEventArgs e)
    {
        //lblStation.Text = "1234";
        // 
        // 調整目前page
        //
        try
        {
        //    CaseNumber = Request.QueryString["CaseNumber"];

         //   stationId = Request.QueryString["stationId"];
        }
        catch (Exception)
        {
        }

        //GetAPIUrlByStation(stationId);
        return;
    }



}