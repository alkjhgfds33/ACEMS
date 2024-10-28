using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimersTimer = System.Timers.Timer;

public partial class Default4 : System.Web.UI.Page
{
    TimersTimer _GetRealTimeData = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          // Timer1.Interval = 5000;//設定每秒執行一次

         // Timer1.Enabled = true;

            GetWL();
            this._GetRealTimeData = new TimersTimer();
            this._GetRealTimeData.Interval = 1000;
            this._GetRealTimeData.Elapsed += new System.Timers.ElapsedEventHandler(_GetRealTimeData_Elapsed);
            this._GetRealTimeData.Start();
        }
    }
    private void _GetRealTimeData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        GetWL();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
         GetWL();
    }
    public Bitmap String2Bmp(string font, int font_size, bool font_bold, Color bgcolor, Color color)
    {
      //  try
      //  {
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
      //  }
      //  catch (Exception)
      //  {
     //       return null;
       // }
    }
    public void GetWL()
    {
          this.lblTitle1.Text = "測試";

        //Bitmap bmp = String2Bmp("外水位", 180, true, Color.Gray, Color.DarkRed);
        try
        {
            Bitmap bmp_dt = String2Bmp("　　　　　　　　　　　　　　　資料時間:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 180, true, Color.Gray, Color.Blue);
            Bitmap bmp1 = String2Bmp("　內水位: 123.55 M", 180, true, Color.Gray, Color.DarkRed);
            Bitmap bmp2 = String2Bmp("　外水位: 123.55 M", 180, true, Color.Gray, Color.DarkRed);
            Bitmap bmp3 = String2Bmp("前池水位: 123.55 M", 180, true, Color.Gray, Color.DarkRed);

            //bmp.Save("/images/WL1.png");

            bmp_dt.Save(Server.MapPath("/images/stno_Dt.png"), ImageFormat.Jpeg);
            bmp1.Save(Server.MapPath("/images/stno_WL1.png"), ImageFormat.Jpeg);
            bmp2.Save(Server.MapPath("/images/stno_WL2.png"), ImageFormat.Jpeg);
            bmp3.Save(Server.MapPath("/images/stno_WL3.png"), ImageFormat.Jpeg);

            
        }
        catch (Exception ex)
        {
            this.lblTitle1.Text = ex.Message;
        }
        strUrl_Dt = "/images/stno_Dt.png";
        strUrl_WL1 = "/images/stno_WL1.png";
        strUrl_WL2 = "/images/stno_WL2.png";
        strUrl_WL3 = "/images/stno_WL3.png";
        //   this.ccdImage1.ImageUrl 
    }



    string strUrl_Dt = "";
    string strUrl_WL1 = "";
    string strUrl_WL2 = "";
    string strUrl_WL3 = "";


    public string returnUrl()
    {
        GetWL();
        return (strUrl_Dt);
    }


    public string ss_dt()
    {
        return (strUrl_Dt);
    }
    public string ss_WL1()
    {      
        return (strUrl_WL1);
    }
    public string ss_WL2()
    { 
        return (strUrl_WL2);
    }
    public string ss_WL3()
    {
        return (strUrl_WL3);
    }


}