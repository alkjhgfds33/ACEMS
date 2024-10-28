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
public partial class Realtime_CCTV_x9 : System.Web.UI.Page
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
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			try
			{
				GetInit();

			}
			catch (Exception)
			{

			}
			if (index == null)
			{
				index = "-1";
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

	string[] strUrl = new string[10];

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
		stationId = Request.QueryString["stationId"];
		index = Request.QueryString["index"];
	}
	private CameraAPI GetCamera(string Url)
	{
		GetInit();
		CameraAPI _CameraAPI = new CameraAPI();
		try
		{
			WebClient webClient = new WebClient();
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
		string strSendUrl = "http://" + WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + "/" + stationID + "/1";  //所有監測站
																																//Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = GetCamera(strSendUrl);


		for (int i = 1; i < 10; i++)
		{
			strUrl[i] = "images/NoVideo.png";
		}



		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";

		//strUrl[i] = "images/loading2.png";
		for (int i = 0; i < listStation.Data.Count; i++)
		{

			switch (i)
			{
				case 0:
					this.lblTitle1.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 1:
					this.lblTitle2.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 2:
					this.lblTitle3.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 3:
					this.lblTitle4.Text = listStation.Data[i].CamName;
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

		string stationID = "";
		int stationIndex = 0;

		if (_Im.ID == "ccdImage1")
		{
			string station = lblTitle1.Text;

			string[] sArray = station.Split('_');// 一定是單引 
			stationID = sArray[0];
			stationIndex = 0;
		}
		else if (_Im.ID == "ccdImage2")
		{
			string station = lblTitle2.Text;

			string[] sArray = station.Split('_');// 一定是單引 
			stationID = sArray[0];
			stationIndex = 1;
		}
		else if (_Im.ID == "ccdImage3")
		{
			string station = lblTitle3.Text;

			string[] sArray = station.Split('_');// 一定是單引 
			stationID = sArray[0];
			stationIndex = 2;
		}
		else if (_Im.ID == "ccdImage4")
		{
			string station = lblTitle4.Text;

			string[] sArray = station.Split('_');// 一定是單引 
			stationID = sArray[0];
			stationIndex = 3;
		}




		string _url = "cctv_stno_s.aspx?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3&stationId=";

		string strSendUrl = "http://" + WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + "/" + stationID + "/1";  //所有監測站
																																//Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = GetCamera(strSendUrl);


		_url = listStation.Data[stationIndex].CameraAPI;

		//Response.Redirect(_url, false);
		GetAPIUrlByStation(stationId);
		return;
	}
}