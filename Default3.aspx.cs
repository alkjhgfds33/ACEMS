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

public partial class Default3 : System.Web.UI.Page
{
	string www = "";
	string CaseNumber = "";
	string stationId = "";
	string index = "";
	string WebApiUrl = WebConfigurationManager.AppSettings["WebApiUrl"].ToString();

	string token = "";


	protected void Page_Load(object sender, EventArgs e)
    {
		if (!Page.IsPostBack)
		{
			string strUserID = "", strUserPass = "", strLoginAuth = "";

			if (Session["id"] != null && Session["passwd"] != null) //已登入
			{
				strUserID = Session["id"].ToString();
				strUserPass = Session["passwd"].ToString();
				strLoginAuth = Session["loginAuth"].ToString();
				token = Session["token"].ToString();
			}
			else
			{
				token = "";		
			}
			
			try
			{
				GetInit();
			}
			catch (Exception)
			{

			}

			if (stationId == null)
			{
				Response.Redirect("Default.aspx", false);
			}
			else
			{
				GetAPIUrlByStation(stationId);
			}
		}
	}
	public class CameraAPI
	{
		public string Status { get; set; }
		public string Message { get; set; }
		public string CameraCount { get; set; }
		public string CameraIndexTotal { get; set; }
		public List<Camera> data { get; set; }
	}
	public class Camera
	{
		public string CamID { get; set; }
		public string StationName { get; set; }
		public string CamName { get; set; }
		public string CameraAPI { get; set; }
	}
	public class CameraStation
	{
		public string StationName { get; set; }
	}

	private void GetInit()
	{
		www = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
		if (Request.Url.Host == "localhost" || Request.Url.Host == "")
		{
			www = "http://www.acems.tw/";
		}
		CaseNumber = Request.QueryString["CaseNumber"];
		if (CaseNumber == "" || CaseNumber == null)
		{
			CaseNumber = WebConfigurationManager.AppSettings["CaseNumber"].ToString();
		}

		WebApiUrl = Request.Url.Scheme + "://" + WebConfigurationManager.AppSettings["WebApiUrl"].ToString(); ;


		stationId = Request.QueryString["stationId"];
		index = Request.QueryString["index"];

		if (index == null)
		{
			index = "0";
		}
	}



	#region strUrl 

	string[] strUrl = new string[9];
	string strUrl1 = "";


	public string returnUrl(int index, string Ex)
	{

		return "http://localhost:5401/CCTV/GetCamera/3";
		return "http://www2.acems.tw/CCTV/GetJPG.aspx";
		if (strUrl[index] == "images/loss.png" || strUrl[index] == "images/NoVideo.png")
		{
			return (strUrl[index]);
		}
		else
		{
			return "http://192.168.1.214:44457/image/8001-01.jpg";
			return (strUrl[index] + "." + Ex);
		}
	}

	public string ss1()
	{
		return "http://192.168.1.214:44457/image/8001-01.jpg";
		return (strUrl1);
	}
	
	#endregion


	private string _verificationURLisExist(string strURL)
	{
		string strURL_jpg = strURL + ".jpg";

		string ret = strURL;

		return ret;

		Uri urlCheck = new Uri(strURL_jpg);
		WebRequest request = WebRequest.Create(urlCheck);
		request.Timeout = 5000;
		WebResponse response;
		try
		{
			response = request.GetResponse();
		}
		catch (Exception)
		{
			//Response.Write("<script>alert('no response:')</script>");
			ret = "images/loss.png";
		}

		return ret;
	}
	private void GetAPIUrl(string index)
	{

		//
		// Loading ccd frame
		//
		string strSendUrl = "http://" + WebApiUrl + "/API2/CCTV/GetCameraAPI?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3&Count=8&index=" + index; //所有監測站
																																					  //Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = new CameraAPI();

		try
		{
			WebClient webClient = new WebClient();
			ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
			webClient.Encoding = Encoding.UTF8;
			listStation = JsonConvert.DeserializeObject<CameraAPI>(webClient.DownloadString(strSendUrl)); //jason string 																									
		}
		catch (Exception ex)
		{
			Response.Write("<script>alert('error:')</script>");
			return;
		}



		for (int i = 1; i < 9; i++)
		{
			strUrl[i] = "images/NoVideo.png";
		}


		strUrl1 = "images/loss.png";
	
		//this.lblTitle1.Text = "";

		for (int i = 0; i < listStation.data.Count; i++)
		{
			switch (i)
			{
				case 0:
				//	this.lblTitle1.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				
			}
		}
	}
	private void GetAPIUrlByStation(string stationID)
	{
		//
		// Loading ccd frame
		//
		string strSendUrl = "http://" + WebApiUrl + "/API2/CCTV/GetCameraAPiByStation?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3&StationName=" + stationID; //所有監測站
																																								 //Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = new CameraAPI();

		try
		{
			WebClient webClient = new WebClient();
			ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
			webClient.Encoding = Encoding.UTF8;
			listStation = JsonConvert.DeserializeObject<CameraAPI>(webClient.DownloadString(strSendUrl)); //jason string 																									
		}
		catch (Exception ex)
		{
			Response.Write("<script>alert('error:')</script>");
			return;
		}



		for (int i = 1; i < 9; i++)
		{
			strUrl[i] = "images/NoVideo.png";
		}


		strUrl1 = "images/loss.png";

		//this.lblTitle1.Text = "";


		for (int i = 0; i < listStation.data.Count; i++)
		{
			switch (i)
			{
				case 0:
				//	this.lblTitle1.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				
			}
		}
	}
	private void GetAPIUrlByStationId(string stationID)
	{
		//
		// Loading ccd frame
		//
		string strSendUrl = "http://" + WebApiUrl + "/API2/CCTV/GetCameraAPiByStation?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3&StationName=" + stationID; //所有監測站
																																								 //Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = new CameraAPI();

		try
		{
			WebClient webClient = new WebClient();
			ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
			webClient.Encoding = Encoding.UTF8;
			listStation = JsonConvert.DeserializeObject<CameraAPI>(webClient.DownloadString(strSendUrl)); //jason string 																									
		}
		catch (Exception ex)
		{
			Response.Write("<script>alert('error:')</script>");
			return;
		}



		for (int i = 1; i < 9; i++)
		{
			strUrl[i] = "images/loss.png";
		}


		strUrl1 = "images/loss.png";
	
		//this.lblTitle1.Text = "";


		for (int i = 0; i < listStation.data.Count; i++)
		{
			switch (i)
			{
				case 0:
				//	this.lblTitle1.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				
			}
		}
	}



	protected void s(object sender, ImageClickEventArgs e)
	{
		ImageButton _Im = (ImageButton)sender;
		if (_Im.ID == "ccdImage1")
		{
			//string station = lblTitle1.Text;

			//string[] sArray = station.Split('_');// 一定是單引 

			//Response.Redirect("Default2.aspx?stationId=" + sArray[1], false);

		}	
		return;
	}

}