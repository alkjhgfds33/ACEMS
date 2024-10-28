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

public partial class Realtime_CCTV_x6 : System.Web.UI.Page
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

	string CCTV_Click = "1";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{

			this.lblStationID.Visible = false;
			try
			{
				GetInit();
			}
			catch (Exception)
			{

			}
			if (index == null || index == "")
			{
				index = "0";
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
																								  //
			return _CameraAPI;
		}
		catch (Exception ex)
		{
			Response.Write("<script>alert('error:')</script>");
			_CameraAPI.Message = ex.Message;
			return _CameraAPI;
		}
	}
	private void GetAPIUrlByStation(string stationID)
	{
		GetInit();
		if (stationID == "" || stationID == null)
		{
			return;
		}
		//
		// Loading ccd frame
		//
		string strSendUrl = www + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + "/" + stationID + "/" + CCTV_Click;  //所有監測站
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
		this.lblTitle5.Text = "";
		this.lblTitle6.Text = "";


		this.lblTitle1_id.Text = "";
		this.lblTitle2_id.Text = "";
		this.lblTitle3_id.Text = "";
		this.lblTitle4_id.Text = "";
		this.lblTitle5_id.Text = "";
		this.lblTitle6_id.Text = "";


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
				case 4:
					this.lblTitle5.Text = listStation.Data[i].CamName;
					this.lblTitle5_id.Text = listStation.Data[i].CamID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 5:
					this.lblTitle6.Text = listStation.Data[i].CamName;
					this.lblTitle6_id.Text = listStation.Data[i].CamID;
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
		if (_Im.ID == "ccdImage1")
		{
			stationID = lblTitle1_id.Text;
		}
		else if (_Im.ID == "ccdImage2")
		{
			stationID = lblTitle2_id.Text;
		}
		else if (_Im.ID == "ccdImage3")
		{
			stationID = lblTitle3_id.Text;
		}
		else if (_Im.ID == "ccdImage4")
		{
			stationID = lblTitle4_id.Text;
		}
		else if (_Im.ID == "ccdImage5")
		{
			stationID = lblTitle5_id.Text;
		}
		else if (_Im.ID == "ccdImage6")
		{
			stationID = lblTitle6_id.Text;
		}

		string _url = "CCTV_X6.aspx?CaseNumber=" + CaseNumber + "&stationId=" + stationID;

		if (stationID == "")
		{
			CCTV_Click = "1";
		}
		else
		{
			CCTV_Click = stationID;
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
	protected void btn_Return_Click(object sender, EventArgs e)
	{
		GetInit();
		string _url = "Default.aspx?CaseNumber=" + CaseNumber + "&index=" + index;
		Response.Redirect(_url, false);
	}

	protected void btn_Err_Click(object sender, EventArgs e)
	{
		GetInit();
		string add = www + "/API/CCTV/AddCameraEvent/" + this.lblStationID.Text + "/影像中斷";  //所有監測站

		WebClient webClient = new WebClient();
		ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
		webClient.Encoding = Encoding.UTF8;
		status _status = JsonConvert.DeserializeObject<status>(webClient.DownloadString(add)); //jason string 			

		Response.Write("<script>alert('報修成功')</script>");

		GetInit();

		GetAPIUrlByStation(stationId);

		return;

	}


}