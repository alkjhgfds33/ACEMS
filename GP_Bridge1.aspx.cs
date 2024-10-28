using ACEMS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GP_Bridge1 : System.Web.UI.Page
{
	ACEMS_SQL aCEMS_SQL = new ACEMS_SQL();

	public string pageTitle;
	public class CameraAPI
	{
		public string Status { get; set; }
		public string Message { get; set; }
		public string CameraCount { get; set; }
		public string CameraIndexTotal { get; set; }
		public List<Camera> Data { get; set; }
	}
	public class Camera
	{
		public string CamID { get; set; }
		public string StationID { get; set; }
		public string StationName { get; set; }
		public string CamName { get; set; }
		public string CameraAPI { get; set; }
	}
	public class CameraStation
	{
		public string StationName { get; set; }
	}
	string www = "";
	string index = "1";

	string CaseNumber = "";
	string stationId = "";
	string WebApiUrl = WebConfigurationManager.AppSettings["WebApiUrl"].ToString();
	string _CaseNumber = WebConfigurationManager.AppSettings["CaseNumber"].ToString();

	int myName2Value = 1;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			pageTitle = "ACEMS Video Monitor";
			bool bVisible = false;
			
		}
	}


	protected void btn_Token_in_Click(object sender, EventArgs e)
	{
		string StrConn1 = "Data Source=122.117.147.249,1433;Initial Catalog=GP_Bridge_System;Persist Security Info=True;User ID=Excellent;Password=89367682";

		string StrConn2 = "Data Source=122.117.147.249,1434;Initial Catalog=GP_Bridge_System;Persist Security Info=True;User ID=Excellent;Password=89367682";

		string sqlStr = "UPdate [GP_Bridge_System].[dbo].[Alert_MSG_LINE] set [Token] = '4BZl8fSsJ8Rfdjp83hO4j5DuOauNI22vkCr8wqNNZ43' ";

		string result = "";
		bool sql1 = aCEMS_SQL.SQL_ENQ_Data(StrConn1 , sqlStr , ref result);
		if (result != "")
		{
			this.lb_result.Text = result;

		}

		bool sql2 = aCEMS_SQL.SQL_ENQ_Data(StrConn2 , sqlStr , ref result);


		if (result != "")
		{
			this.lb_result.Text = result;

		}
		else
		{
			this.lb_status.Text = "OK";
			this.lb_time.Text = DateTime.Now.ToString();
		}
		return;
	}

	protected void btn_Token_in2_Click(object sender, EventArgs e)
	{
		string StrConn1 = "Data Source=122.117.147.249,1433;Initial Catalog=GP_Bridge_System;Persist Security Info=True;User ID=Excellent;Password=89367682";

		string StrConn2 = "Data Source=122.117.147.249,1434;Initial Catalog=GP_Bridge_System;Persist Security Info=True;User ID=Excellent;Password=89367682";

		string sqlStr = "UPdate [GP_Bridge_System].[dbo].[Alert_MSG_LINE] set [Token] = 'qxn2ob50zy1xIy6kxjD9HPA9ZxbfdBSioKWhflgIKV6' ";

		string result = "";
		bool sql1 = aCEMS_SQL.SQL_ENQ_Data(StrConn1, sqlStr, ref result);
		if (result != "")
		{
			this.lb_result.Text = result;

		}

		bool sql2 = aCEMS_SQL.SQL_ENQ_Data(StrConn2, sqlStr, ref result);


		if (result != "")
		{
			this.lb_result.Text = result;

		}
		else
		{
			this.lb_status.Text = "OK";
			this.lb_time.Text = DateTime.Now.ToString();
		}
		return;
	}

	protected void btn_Token_out_Click(object sender, EventArgs e)
	{
		string StrConn1 = "Data Source=122.117.147.249,1433;Initial Catalog=GP_Bridge_System;Persist Security Info=True;User ID=Excellent;Password=89367682";

		string StrConn2 = "Data Source=122.117.147.249,1434;Initial Catalog=GP_Bridge_System;Persist Security Info=True;User ID=Excellent;Password=89367682";

		string sqlStr = "UPdate [GP_Bridge_System].[dbo].[Alert_MSG_LINE] set [Token] = '0ZoF3AHXskl8MWGB6dBA4FHd0QP7ALTQd92H3YurasZ' ";

		string result = "";
		bool sql1 = aCEMS_SQL.SQL_ENQ_Data(StrConn1, sqlStr, ref result);
		if (result != "")
		{
			this.lb_result.Text = result;

		}

		bool sql2 = aCEMS_SQL.SQL_ENQ_Data(StrConn2, sqlStr, ref result);


		if (result != "")
		{
			this.lb_result.Text = result;

		}
		else
		{
			this.lb_status.Text = "OK";
			this.lb_time.Text = DateTime.Now.ToString();
		}
		return;
	}
	protected void btnUpPage_Click(object sender, EventArgs e)
	{


		return;
	}
	protected void btnDownPage_Click(object sender, EventArgs e)
	{
		
		return;
	}
	protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
	{
		
		return;


	}
	protected void DropDownList2_SelectedIndexChanged1(object sender, EventArgs e)
	{
		
		return;


	}


	
}