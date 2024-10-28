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

public partial class Realtime_CCTV : System.Web.UI.Page
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
	//string CaseNumber = "";
	string CamID = "";

	string WebApiUrl = WebConfigurationManager.AppSettings["WebApiUrl"].ToString();

	string token = "";

	int count = 0;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			string strUserID = "", strUserPass = "", strLoginAuth = "";

			if (Session["id"] != null && Session["passwd"] != null) //已登入
			{
				//strUserID = Session["id"].ToString();
				//strUserPass = Session["passwd"].ToString();
				//strLoginAuth = Session["loginAuth"].ToString();
				//token = Session["token"].ToString();
			}
			else
			{
				//token = "";
			}

			this.lblStationID.Visible = false;
			try
			{
				GetInit();
			}
			catch (Exception)
			{

			}
			strUrl = "images/NoVideo2.png";
			if (CamID == null)
			{
				strUrl = "images/NoVideo2.png";
				//Response.Redirect("Default.aspx", false);
			}
			else
			{

				GetAPIUrlByCamID(CamID);
			}
		}
	}

	string strUrl = "";

	public string returnUrl(int index, string Ex)
    {	
		return (strUrl);
	}


	public string Zero()
	{
		return (strUrl);
	}


	private void GetInit()
	{
		www = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
		if (Request.Url.Host == "localhost" || Request.Url.Host == "")
		{
			www = "http://www.acems.tw/";
		}
		WebApiUrl = Request.Url.Scheme + "://" + WebConfigurationManager.AppSettings["WebApiUrl"].ToString(); ;

	//	CaseNumber = Request.QueryString["CaseNumber"];

		token = Request.QueryString["token"];

		CamID = Request.QueryString["CamID"];


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
	private void GetAPIUrlByCamID(string _camID)
	{
		if (_camID == "" || _camID == null)
		{
			return;
		}

		string strSendUrl = WebApiUrl + "/API/CCTV/GetCameraData/" + token + "/" + _camID + "/" ;  //所有監測站
																														   //Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = GetCamera(strSendUrl);
				
		this.Label1.Text = listStation.Data[0].CamName;
		strUrl = _verificationURLisExist(listStation.Data[0].CameraAPI);
		
		
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

		GetAPIUrlByCamID(CamID);
		return;
	}


}