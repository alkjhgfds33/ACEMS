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

public partial class Realtime_CCTV_x4 : System.Web.UI.Page
{
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

				//Session["id"] = strUserID;

				this.btnServiceRestart.Visible = true;
				this.btnGoUrl.Visible = true;
				this.btnGoMap.Visible = true;
				
				this.lb_r1.Visible = true;
				this.lb_r2.Visible = true;
				this.lb_r3.Visible = true;
				this.lb_r4.Visible = true;

				this.btnRefresh.Visible = false;
			}
			else
			{
				token = "";
				this.btnServiceRestart.Visible = false;
				this.btnGoUrl.Visible = false;
				this.btnGoMap.Visible = false;
				//this.btnR.Visible = false;


				this.lb_r1.Visible = false;
				this.lb_r2.Visible = false;
				this.lb_r3.Visible = false;
				this.Label1.Visible = false;
				//this.lb_r4.Visible = false;

			}
			this.lblStationID.Visible = false;
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

	string[] strUrl = new string[9];

	public string returnUrl(int index, string Ex)
	{
		return (strUrl[index]);
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
			this.btnServiceRestart.Visible = false;
			this.btnGoUrl.Visible = false;
			this.btnGoMap.Visible = false;
			this.btnR.Visible = false;
			

			this.lb_r1.Visible = false;
			this.lb_r2.Visible = false;
			this.lb_r3.Visible = false;
			this.lb_r4.Visible = false;



			//this.btnAlert.Style["height"] = "45px";
			//this.btnAlert.Style["width"] =  "150px";
			//this.btnAlert.Style["font-size"] = "25px";
			index = "0";
		}
	}
	private CameraAPI GetCamera(string Url)
	{
		GetInit();
		CameraAPI _CameraAPI = new CameraAPI();
		try
		{
			WebClient webClient = new WebClient();
			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
			ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
			webClient.Encoding = Encoding.UTF8;
			_CameraAPI = JsonConvert.DeserializeObject<CameraAPI>(webClient.DownloadString(Url)); //jason string 			


			return _CameraAPI;
		}
		catch (Exception ex)
		{
			Response.Write("<script>alert('error:')</script>");
			Response.Write(Url);
			_CameraAPI.Message = ex.Message;
			return _CameraAPI;
		}
	}
	private void GetAPIUrlByStation(string stationID)
	{
		if (stationID == "" || stationID == null)
		{
			return;
		}
		//
		// Loading ccd frame
		//
	//	string strSendUrl = "http://" + WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + "/" + stationID + "/" + index;  //所有監測站
		string strSendUrl =  WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + "/" + stationID + "/" + index;  //所有監測站
																																	   //Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = GetCamera(strSendUrl);


		for (int i = 1; i < 9; i++)
		{
			strUrl[i] = "images/NoVideo.png";
		}



		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";

		this.lblTitle1_id.Text = "";
		this.lblTitle2_id.Text = "";
		this.lblTitle3_id.Text = "";
		this.lblTitle4_id.Text = "";
		//strUrl[i] = "images/loading2.png";
		for (int i = 0; i < listStation.Data.Count; i++)
		{
			this.lblStationID.Text = listStation.Data[i].StationID;
			switch (i)
			{
				case 0:
					this.lblTitle1.Text = listStation.Data[i].CamName;
					this.lblTitle1_id.Text = listStation.Data[i].CamID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 1:
					this.lblTitle2.Text = listStation.Data[i].CamName;
					this.lblTitle2_id.Text = listStation.Data[i].CamID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 2:
					this.lblTitle3.Text = listStation.Data[i].CamName;
					this.lblTitle3_id.Text = listStation.Data[i].CamID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 3:
					this.lblTitle4.Text = listStation.Data[i].CamName;
					this.lblTitle4_id.Text = listStation.Data[i].CamID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
			}
		}
	}
	private string _verificationURLisExist(string strURL)
	{
		string strURL_jpg = strURL + ".jpg";

		string ret = www + "/API/" + strURL;

		return strURL;

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
	protected void s(object sender, ImageClickEventArgs e)
	{

		GetInit();

		ImageButton _Im = (ImageButton)sender;

		string CamID = "";
		string stationID = "";
		int stationIndex = 0;

		if (_Im.ID == "ccdImage1")
		{
			string station = lblTitle1.Text;
			CamID =  lblTitle1_id.Text;
			string[] sArray = station.Split('_');// 一定是單引 
			stationID = sArray[0];
			stationIndex = 0;
		}
		else if (_Im.ID == "ccdImage2")
		{
			string station = lblTitle2.Text;

			CamID =   lblTitle2_id.Text;
			string[] sArray = station.Split('_');// 一定是單引 
			stationID = sArray[0];
			stationIndex = 1;
		}
		else if (_Im.ID == "ccdImage3")
		{
			string station = lblTitle3.Text;

			CamID =   lblTitle3_id.Text;
			string[] sArray = station.Split('_');// 一定是單引 
			stationID = sArray[0];
			stationIndex = 2;
		}
		else if (_Im.ID == "ccdImage4")
		{
			string station = lblTitle4.Text;


			CamID =   lblTitle4_id.Text;
			string[] sArray = station.Split('_');// 一定是單引 
			stationID = sArray[0];
			stationIndex = 3;
		}




		string _url = "cctv_stno_s.aspx?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3&stationId=";

	//	string strSendUrl = "http://" + WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + "/" + stationID + "/1";  //所有監測站
		string strSendUrl = WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + "/" + stationID + "/1";  //所有監測站
																																//Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = GetCamera(strSendUrl);


		//	_url = listStation.data[stationIndex].CameraAPI;

		//Response.Redirect(_url, false);


		if (token != "")
        {

        }

		GetAPIUrlByStation(stationId);
		return;
	}



	public struct status
	{
		//public string stno;
		public string Status;
		public string Message;
	}
	protected void btn_ServiceRestart_Click(object sender, EventArgs e)
	{

	}
	protected void btn_GoUrl_Click(object sender, EventArgs e)
	{
		GetInit();





		GetAPIUrlByStation(stationId);

		return;
	}
	protected void btn_GoMap_Click(object sender, EventArgs e)
	{
		GetInit();
				
		string add =  WebApiUrl + "/API/station/GetVideoStationMap/" + CaseNumber + "/" + stationId + "/";  //所有監測站

		WebClient webClient = new WebClient();
		ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
		webClient.Encoding = Encoding.UTF8;
		status _status = JsonConvert.DeserializeObject<status>(webClient.DownloadString(add)); //jason string 			

		Response.Redirect(_status.Message, false);

		return;
	}
	protected void btn_Return_Click(object sender, EventArgs e)
	{
		GetInit();
		string _url = "Default.aspx?CaseNumber=" + CaseNumber + "&index=" + index;
		Response.Redirect(_url, false);
	}
	protected void btn_Err_Click(object sender, EventArgs e)
	{
		GetInit();

		//string add = "http://" + WebApiUrl + "/API/CCTV/AddCameraEvent/" +  this.lblStationID.Text + "/影像中斷";  //所有監測站
		string add = WebApiUrl + "/API/CCTV/AddCameraEvent/" + this.lblStationID.Text + "/影像中斷";  //所有監測站

		WebClient webClient = new WebClient();
		ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
		webClient.Encoding = Encoding.UTF8;
		status _status = JsonConvert.DeserializeObject<status>(webClient.DownloadString(add)); //jason string 			

		Response.Write("<script>alert('報修成功')</script>");

		GetInit();

		GetAPIUrlByStation(stationId);

		return;
	}


	protected void btn_Refresh_Click(object sender, EventArgs e)
	{
		Response.Redirect(Request.Url.ToString());
	}
	protected void tmrRefresh_Tick(object sender, EventArgs e)
	{
		Label1.Text = DateTime.Now.ToString();
	}

}