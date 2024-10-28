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

public partial class cctv_stno : System.Web.UI.Page
{

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

	string www = "";
	string CaseNumber = "";
	string stationId = "";
	string WebApiUrl = WebConfigurationManager.AppSettings["WebApiUrl"].ToString();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			//myName2Value = Convert.ToInt32(Request.QueryString["Name2"]);
			//myName3Value = Request.QueryString["Name3"];
			try
			{
				www = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
				if (Request.Url.Host == "localhost" || Request.Url.Host =="")
				{
					www = "http://www2.acems.tw/";
				}

				CaseNumber = Request.QueryString["CaseNumber"];
				stationId = Request.QueryString["stationId"];	
			}
			catch (Exception)
			{

			}		
			if (stationId == null)
			{ 
			
			}
            else 
			{
				GetAPIUrlByStation(stationId);
			}
		}
	}

	#region strUrl 

	string[] strUrl = new string[9];
	string strUrl1 = "";
	string strUrl2 = "";
	string strUrl3 = "";
	string strUrl4 = "";
	string strUrl5 = "";
	string strUrl6 = "";

	public string returnUrl(int index, string Ex)
	{
		return (strUrl[index]);

		if (strUrl[index] == "images/loss.png" || strUrl[index] == "images/NoVideo.png")
		{
			return (strUrl[index]);
		}
		else
		{
			return (strUrl[index]);			
		}
	}


	public string ss1()
	{
		return (strUrl1);
	}
	public string ss2()
	{
		return (strUrl2);
	}
	public string ss3()
	{
		return (strUrl3);
	}
	public string ss4()
	{
		return (strUrl4);
	}
	public string ss5()
	{
		return (strUrl5);
	}
	public string ss6()
	{
		return (strUrl6);
	}

	#endregion


	private void GetAPIUrlByStation(string stationID)
	{
		if (stationID == "" || stationID == null)
		{
			return;
		}
		//
		// Loading ccd frame
		//
		string strSendUrl = "http://" + WebApiUrl + "/API/CCTV/GetCameraAPiByStation2?CaseNumber=" + CaseNumber + "&StationName=" + stationID; //所有監測站
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
		//	Response.Write("<script>alert('error:')</script>");
			Response.Write(strSendUrl);

			return;
		}

		for (int i = 1; i < 9; i++)
		{
			strUrl[i] = "images/NoVideo.png";
		}

		strUrl1 = "images/NoVideo.png";
		strUrl2 = "images/NoVideo.png";
		strUrl3 = "images/NoVideo.png";
		strUrl4 = "images/NoVideo.png";
		strUrl5 = "images/NoVideo.png";
		strUrl6 = "images/NoVideo.png";

		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";
		this.lblTitle5.Text = "";
		this.lblTitle6.Text = "";

		for (int i = 0; i < listStation.data.Count; i++)
		{
			switch (i)
			{
				case 0:
					this.lblTitle1.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 1:
					this.lblTitle2.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 2:
					this.lblTitle3.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 3:
					this.lblTitle4.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 4:
					this.lblTitle5.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 5:
					this.lblTitle6.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;

			}
		}
	}





	/// <summary>
	///沒用到
	/// </summary>
	/// <param name="index"></param>
	private void GetAPIUrl2(string index)
	{
		//
		// Loading ccd frame
		//
		string strSendUrl = "http://" + WebApiUrl + "GetCameraAPI?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3&Count=8&index=" + index; //所有監測站
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
		strUrl2 = "images/loss.png";
		strUrl3 = "images/loss.png";
		strUrl4 = "images/NoVideo.png";
		strUrl5 = "images/NoVideo.png";
		strUrl6 = "images/NoVideo.png";

		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";
		this.lblTitle5.Text = "";
		this.lblTitle6.Text = "";

		for (int i = 0; i < listStation.data.Count; i++)
		{
			switch (i)
			{
				case 0:
					this.lblTitle1.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 1:
					this.lblTitle2.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 2:
					this.lblTitle3.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 3:
					this.lblTitle4.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 4:
					this.lblTitle5.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 5:
					this.lblTitle6.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;

			}
		}
	}

	/// <summary>
	///沒用到
	/// </summary>
	/// <param name="index"></param>
	private void GetAPIUrlByStationId_NotUse(string stationID)
	{
		//
		// Loading ccd frame
		//
		string strSendUrl = "http://" + WebApiUrl + "GetCameraAPiByStation?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3&StationName=" + stationID; //所有監測站
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
		strUrl2 = "images/loss.png";
		strUrl3 = "images/loss.png";
		strUrl4 = "images/loss.png";
		strUrl5 = "images/loss.png";
		strUrl6 = "images/loss.png";

		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";
		this.lblTitle5.Text = "";
		this.lblTitle6.Text = "";


		for (int i = 0; i < listStation.data.Count; i++)
		{
			switch (i)
			{
				case 0:
					this.lblTitle1.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 1:
					this.lblTitle2.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 2:
					this.lblTitle3.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 3:
					this.lblTitle4.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 4:
					this.lblTitle5.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
				case 5:
					this.lblTitle6.Text = listStation.data[i].CamName;
					strUrl[i + 1] = _verificationURLisExist(listStation.data[i].CameraAPI);
					break;
			}
		}
	}

	private string _verificationURLisExist(string strURL)
	{
		string strURL_jpg = strURL + ".jpg";

		string ret = www  + "/API/" +  strURL;

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

	protected void s(object sender, ImageClickEventArgs e)
	{
		//lblStation.Text = "1234";
		// 
		// 調整目前page
		//
		try
		{
			www = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
			if (Request.Url.Host == "localhost" || Request.Url.Host == "")
			{
				www = "http://www2.acems.tw/";
			}
			CaseNumber = Request.QueryString["CaseNumber"];
			stationId = Request.QueryString["stationId"];
			//format = Request.QueryString["format"];
		}
		catch (Exception)
		{

		}

		/*-if (format == null)
		{
			format = "jpg";
		}-*/


		if (stationId == null)
		{

		}
		else
		{
			GetAPIUrlByStation(stationId);
		}
		return;
	}





}