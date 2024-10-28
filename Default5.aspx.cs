using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default5 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	protected void tmrRefresh_Tick(object sender, EventArgs e)
	{	
		this.lblDateTime.InnerText = " ____羌園排水分洪道____  資料時間:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		Random r = new Random();
		double d = r.NextDouble();
		this.lbl1.InnerText = "內水位  ：" + (123 + d).ToString("F2") + " M";
		d = r.NextDouble();
		this.lbl2.InnerText = "外水位  ：" + (123 + d).ToString("F2") + " M";
		d = r.NextDouble();
		this.lbl3.InnerText = "前池水位";

		//string teststr = "<font color=#6f6f6f> 測試 </font>";
		this.lbl4.InnerText = (123 + d).ToString("F2") + " M";

		//this.lbl4.Style["Style"] = "color:blue";
		//this.lbl4.InnerText = teststr;
		//this.lbl4.ForeColor = Color.FromArgb(0, 0, 0);//(R, G, B) (0, 0, 0 = black
	}
}