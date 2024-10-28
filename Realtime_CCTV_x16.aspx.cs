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

public partial class Realtime_CCTV_x16 : System.Web.UI.Page
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
			GetAPIUrlByStation();

		}
	}

	string[] strUrl = new string[17];

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

	private void GetAPIUrlByStation()
	{
		//
		// Loading ccd frame
		//
		string strSendUrl = "http://" + WebApiUrl + "/API/CCTV/GetCameraData/" + CaseNumber;  //所有監測站
																							  //Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = GetCamera(strSendUrl);

		for (int i = 1; i < 17; i++)
		{
			strUrl[i] = "images/NoVideo.png";
		}


		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";

		this.lblTitle5.Text = "";
		this.lblTitle6.Text = "";
		this.lblTitle7.Text = "";
		this.lblTitle8.Text = "";

		this.lblTitle9.Text = "";
		this.lblTitle10.Text = "";
		this.lblTitle11.Text = "";
		this.lblTitle12.Text = "";

		this.lblTitle13.Text = "";
		this.lblTitle14.Text = "";
		this.lblTitle15.Text = "";
		this.lblTitle16.Text = "";

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
				case 4:
					this.lblTitle5.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 5:
					this.lblTitle6.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 6:
					this.lblTitle7.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 7:
					this.lblTitle8.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;



				case 8:
					this.lblTitle9.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 9:
					this.lblTitle10.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 10:
					this.lblTitle11.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 11:
					this.lblTitle12.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;


				case 12:
					this.lblTitle13.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 13:
					this.lblTitle14.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 14:
					this.lblTitle15.Text = listStation.Data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 15:
					this.lblTitle16.Text = listStation.Data[i].CamName;
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
		GetAPIUrlByStation();
		return;
	}



	protected void btn_Click(object sender, EventArgs e)
	{
		Response.Write("<script>alert('報修成功')</script>");

		GetInit();

		GetAPIUrlByStation();
		return;
	}





	protected void btnUpPage_Click(object sender, EventArgs e)
	{
		
	}
	protected void btnDownPage_Click(object sender, EventArgs e)
	{
		
	}

}