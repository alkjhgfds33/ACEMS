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


public partial class Default_GPS : System.Web.UI.Page
{
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
	string strLat = "";
	string strLon = "";

	string CaseNumber = "";
	string stationId = "";	
	
	string WebApiUrl = WebConfigurationManager.AppSettings["WebApiUrl"].ToString();
	string _CaseNumber = WebConfigurationManager.AppSettings["CaseNumber"].ToString();

	int myName2Value = 1;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			//tmrRefresh_Tick(sender, e);  //更新資料

			//ScriptManager.RegisterStartupScript(Page, GetType(), "getLocation", "<script>getLocation()</script>", false);
			//string strLat = Convert.ToDouble(Request.Form["nLat"].ToString()).ToString();
			//string strLng = Convert.ToDouble(Request.Form["nLng"].ToString()).ToString();
			//this.GPS.Text = DateTime.Now.ToString("HH:mm:ss") +  "  lat = " + strLat + "; lng = " + strLng ;


			string strUserID = "", strUserPass = "", strLoginAuth = "";
			if (Session["id"] != null && Session["passwd"] != null) //已登入
			{
				strUserID = Session["id"].ToString();
				strUserPass = Session["passwd"].ToString();
				strLoginAuth = Session["loginAuth"].ToString();

				this.userName_a.Text = strUserID + "(" + strLoginAuth + ")";

				Session["id"] = strUserID;
				this.login_a.Visible = false;
				this.userName_a.Visible = true;
				this.logout_a.Visible = true;
			}
			else
			{
				this.login_a.Visible = true;
				this.userName_a.Visible = false;
				this.logout_a.Visible = false;
			}

			pageTitle = "ACEMS Video Monitor";
			bool bVisible = false;
			this.lblIdx.Visible = bVisible;
			this.lblStation.Visible = bVisible;
			this.lblTotalCount.Visible = bVisible;
			this.lblSelectType.Visible = bVisible;


			//this.GPS.Visible = false;

			GetInit();

			try
			{
				index = Request.QueryString["index"];
			}
			catch (Exception)
			{
				index = "1";
			}


			try
			{
				myName2Value = Convert.ToInt32(index);
			}
			catch (Exception)
			{
				index = "1";
			}
			if (index == null)
			{ index = "1"; }



			try
			{
				strLat = Request.QueryString["Lat"];
				strLon = Request.QueryString["Lon"];

			}
			catch (Exception)
			{

			}

			try
			{
				stationId = Request.QueryString["stationId"];
			}
			catch (Exception)
			{
			}

			string strGetCameraAPi = WebApiUrl + "/API/CCTV/GetCameraData/" + CaseNumber + "/8/1"; //所有監測站
																								   //Response.Write("<script>alert('" + strSendUrl + "')</script>");
			CameraAPI CameraAPi = new CameraAPI();
			try
			{


				WebClient webClient = new WebClient();
				ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
				ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);

				webClient.Encoding = Encoding.UTF8;
				CameraAPi = JsonConvert.DeserializeObject<CameraAPI>(webClient.DownloadString(strGetCameraAPi)); //jason string 																									
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('error 1:" + ex.Message + "') </script>");
				Response.Write("<script>alert('error 2 :" + strGetCameraAPi + "') </script>");

				return;
			}

			this.lblPageTotle.Text = Convert.ToInt32(CameraAPi.CameraIndexTotal).ToString(); //總共幾page
			this.lblTotalCount.Text = Convert.ToInt32(CameraAPi.CameraCount).ToString(); //總共幾支畫面
			this.lblPage.Text = index;
			this.DropDownList1.Text = index;


			for (int i = 1; i <= Convert.ToInt32(CameraAPi.CameraIndexTotal); i++)
			{
				this.DropDownList1.Items.Add(i.ToString());
			}

			if (myName2Value < Convert.ToInt32(CameraAPi.CameraIndexTotal) && myName2Value != 1)
			{
				this.DropDownList1.Text = index;
			}

			//string strGetCameraStation = "http://" + WebApiUrl + "GetCameraStation?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3"; //所有監測站
			string strGetCameraStation = WebApiUrl + "/API/CCTV/GetCameraStation/" + CaseNumber; //所有監測站
																								 //Response.Write("<script>alert('" + strSendUrl + "')</script>");
			List<CameraStation> _cameraStation = new List<CameraStation>();
			try
			{
				WebClient webClient = new WebClient();
				ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
				ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
				webClient.Encoding = Encoding.UTF8;
				_cameraStation = JsonConvert.DeserializeObject<List<CameraStation>>(webClient.DownloadString(strGetCameraStation)); //jason string 																									
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('error:')</script>");
				return;
			}
			this.DropDownList2.Items.Add("CCTV監測站");



			for (int i = 0; i < Convert.ToInt32(_cameraStation.Count); i++)
			{
				this.DropDownList2.Items.Add(_cameraStation[i].StationName.ToString());
			}

			if (stationId != null)
			{
				GetAPIUrlByStation(stationId);
			}
			else
			{
				GetAPIUrl(index);
			}
		}
	}
	private void GetInit()
	{
		www = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
		if (Request.Url.Host == "localhost" || Request.Url.Host == "")
		{
			www = Request.Url.Scheme + "://www.acems.tw/";
		}
		CaseNumber = Request.QueryString["CaseNumber"];
		if (CaseNumber == "" || CaseNumber == null)
		{
			CaseNumber = WebConfigurationManager.AppSettings["CaseNumber"].ToString();
		}
		WebApiUrl = Request.Url.Scheme + "://" + WebConfigurationManager.AppSettings["WebApiUrl"].ToString(); ;


		strLat = Request.QueryString["Lat"];
		strLon = Request.QueryString["Lon"];

		
	}

	#region strUrl 

	string[] strUrl = new string[9];
	string strUrl1 = "";
	string strUrl2 = "";
	string strUrl3 = "";
	string strUrl4 = "";
	string strUrl5 = "";
	string strUrl6 = "";
	string strUrl7 = "";
	string strUrl8 = "";
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
	public string ss7()
	{
		return (strUrl7);
	}
	public string ss8()
	{
		return (strUrl8);
	}
	#endregion
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
	private string _verificationURLisExist(string strURL)
	{
		string strURL_jpg = strURL + ".jpg";

		string ret = strURL;

		return strURL;

		//string ret = strURL;

		//return ret;

		Uri urlCheck = new Uri(ret);
		WebRequest request = WebRequest.Create(urlCheck);
		request.Timeout = 2000;
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
		GetInit();
		//
		// Loading ccd frame
		//
		//string strSendUrl = "http://" + WebApiUrl + "/API/CCTV/GetCameraData/" + CaseNumber + "/8/" + index; //所有監測站

		string strSendUrl = WebApiUrl + "/API/CCTV/GetCameraData/" + CaseNumber + "/8/" + index; //所有監測站
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
		this.lblTitle7.Text = "";
		this.lblTitle8.Text = "";

		this.lblTitle1_stno.Text = "";
		this.lblTitle2_stno.Text = "";
		this.lblTitle3_stno.Text = "";
		this.lblTitle4_stno.Text = "";
		this.lblTitle5_stno.Text = "";
		this.lblTitle6_stno.Text = "";
		this.lblTitle7_stno.Text = "";
		this.lblTitle8_stno.Text = "";
		for (int i = 0; i < listStation.Data.Count; i++)
		{
			switch (i)
			{
				case 0:
					this.lblTitle1.Text = listStation.Data[i].CamName;
					this.lblTitle1_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 1:
					this.lblTitle2.Text = listStation.Data[i].CamName;
					this.lblTitle2_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 2:
					this.lblTitle3.Text = listStation.Data[i].CamName;
					this.lblTitle3_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 3:
					this.lblTitle4.Text = listStation.Data[i].CamName;
					this.lblTitle4_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 4:
					this.lblTitle5.Text = listStation.Data[i].CamName;
					this.lblTitle5_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 5:
					this.lblTitle6.Text = listStation.Data[i].CamName;
					this.lblTitle6_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 6:
					this.lblTitle7.Text = listStation.Data[i].CamName;
					this.lblTitle7_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 7:
					this.lblTitle8.Text = listStation.Data[i].CamName;
					this.lblTitle8_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
			}
		}
	}
	private void GetAPIUrlByStation(string stationID)
	{
		GetInit();

		//string strSendUrl = "http://" + WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + @"/"+ stationID + @"/1"; //所有監測站
		string strSendUrl = WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + @"/" + stationID + @"/1"; //所有監測站
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
		this.lblTitle7.Text = "";
		this.lblTitle8.Text = "";

		this.lblTitle1_stno.Text = "";
		this.lblTitle2_stno.Text = "";
		this.lblTitle3_stno.Text = "";
		this.lblTitle4_stno.Text = "";
		this.lblTitle5_stno.Text = "";
		this.lblTitle6_stno.Text = "";
		this.lblTitle7_stno.Text = "";
		this.lblTitle8_stno.Text = "";
		for (int i = 0; i < listStation.Data.Count; i++)
		{
			switch (i)
			{
				case 0:
					this.lblTitle1.Text = listStation.Data[i].CamName;
					this.lblTitle1_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 1:
					this.lblTitle2.Text = listStation.Data[i].CamName;
					this.lblTitle2_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 2:
					this.lblTitle3.Text = listStation.Data[i].CamName;
					this.lblTitle3_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 3:
					this.lblTitle4.Text = listStation.Data[i].CamName;
					this.lblTitle4_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 4:
					this.lblTitle5.Text = listStation.Data[i].CamName;
					this.lblTitle5_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 5:
					this.lblTitle6.Text = listStation.Data[i].CamName;
					this.lblTitle6_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 6:
					this.lblTitle7.Text = listStation.Data[i].CamName;
					this.lblTitle7_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
				case 7:
					this.lblTitle8.Text = listStation.Data[i].CamName;
					this.lblTitle8_stno.Text = listStation.Data[i].StationID;
					strUrl[i + 1] = _verificationURLisExist(listStation.Data[i].CameraAPI);
					break;
			}
		}
	}
	private void GetAPIUrlByStationId(string stationID)
	{
		//
		// Loading ccd frame
		//
		//string strSendUrl = "http://" + WebApiUrl + "GetCameraAPiByStation?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3&StationName=" + stationID; //所有監測站
		string strSendUrl = WebApiUrl + "GetCameraAPiByStation?CaseNumber=33420000-C02D-4DCE-A136-A955481824F3&StationName=" + stationID; //所有監測站
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
		strUrl7 = "images/loss.png";
		strUrl8 = "images/loss.png";
		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";
		this.lblTitle5.Text = "";
		this.lblTitle6.Text = "";
		this.lblTitle7.Text = "";
		this.lblTitle8.Text = "";

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
			}
		}
	}



	protected void btnUpPage_Click(object sender, EventArgs e)
	{
		// 
		// 調整目前page
		//
		int nPage = Convert.ToInt32(this.lblPage.Text);
		nPage = nPage - 1;
		if (nPage < 1)
		{
			nPage = 1;
		}
		this.lblPage.Text = nPage.ToString();

		this.DropDownList1.Text = nPage.ToString();
		this.DropDownList2.Text = "CCTV監測站";

		index = nPage.ToString();
		GetAPIUrl(nPage.ToString());

		return;
	}
	protected void btnDownPage_Click(object sender, EventArgs e)
	{
		// 
		// 調整目前page
		//
		int nPage = Convert.ToInt32(this.lblPage.Text);
		int nPageTotle = Convert.ToInt32(this.lblPageTotle.Text);
		nPage = nPage + 1;
		if (nPage > nPageTotle)
		{
			nPage = nPage - 1;
		}
		this.lblPage.Text = nPage.ToString();

		this.DropDownList1.Text = nPage.ToString();
		this.DropDownList2.Text = "CCTV監測站";

		index = nPage.ToString();
		GetAPIUrl(nPage.ToString());

		return;
	}

	protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
	{
		// 
		// 調整目前page
		//
		int nPage = Convert.ToInt32(this.DropDownList1.Text);


		this.lblPage.Text = nPage.ToString();

		index = nPage.ToString();
		GetAPIUrl(nPage.ToString());

		return;


	}
	protected void DropDownList2_SelectedIndexChanged1(object sender, EventArgs e)
	{
		// 
		// 調整目前page
		//		

		if (DropDownList2.Text.ToString() == "CCTV監測站")
		{
			int nPage = Convert.ToInt32(this.DropDownList1.Text);


			this.lblPage.Text = nPage.ToString();

			GetAPIUrl(nPage.ToString());
		}
		else
		{
			GetAPIUrlByStation(DropDownList2.Text.ToString());
		}
		return;


	}


	protected void s(object sender, ImageClickEventArgs e)
	{
		GetInit();
		ImageButton _Im = (ImageButton)sender;

		string stationID = "";

		if (_Im.ID == "ccdImage1")
		{
			stationID = lblTitle1_stno.Text;
		}
		else if (_Im.ID == "ccdImage2")
		{
			stationID = lblTitle2_stno.Text;

		}
		else if (_Im.ID == "ccdImage3")
		{
			stationID = lblTitle3_stno.Text;
		}
		else if (_Im.ID == "ccdImage4")
		{
			stationID = lblTitle4_stno.Text;
		}
		else if (_Im.ID == "ccdImage5")
		{
			stationID = lblTitle5_stno.Text;
		}
		else if (_Im.ID == "ccdImage6")
		{
			stationID = lblTitle6_stno.Text;
		}
		else if (_Im.ID == "ccdImage7")
		{
			stationID = lblTitle7_stno.Text;
		}
		else if (_Im.ID == "ccdImage8")
		{
			stationID = lblTitle8_stno.Text;
		}

		if (stationID == "示意圖")
		{
			stationID = DropDownList2.Text.ToString();
		}



		//string strSendUrl = "http://" + WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + @"/" + stationID + "/1"; //所有監測站
		string strSendUrl = WebApiUrl + "/API/CCTV/GetCameraDataByStation/" + CaseNumber + @"/" + stationID + "/1"; //所有監測站


		string _url = "";                                                                                     //Response.Write("<script>alert('" + strSendUrl + "')</script>");
		CameraAPI listStation = GetCamera(strSendUrl);

		if (Convert.ToInt32(listStation.CameraCount) < 5)
		{
			_url = "Realtime_CCTV_x4.aspx?CaseNumber=" + CaseNumber + "&stationId=" + stationID + "&index=" + this.lblPage.Text;
		}
		else if (Convert.ToInt32(listStation.CameraCount) > 4 && Convert.ToInt32(listStation.CameraCount) < 7)
		{
			_url = "Realtime_CCTV_x6.aspx?CaseNumber=" + CaseNumber + "&stationId=" + stationID + "&index=" + this.lblPage.Text;
		}

		if (_url != "")
		{
			Response.Redirect(_url, false);
		}

		//lblStation.Text = "1234";
		// 
		// 調整目前page
		//
		int nPage = Convert.ToInt32(this.lblPage.Text);
		//this.lblPage.Text = nPage.ToString();
		if (DropDownList2.Text.ToString() == "CCTV監測站")
		{
			GetAPIUrl(nPage.ToString());
		}
		else
		{
			GetAPIUrlByStation(DropDownList2.Text.ToString());
		}
		return;
	}





	protected void tmrRefresh_Tick(object sender, EventArgs e)
	{
		string strLng, strLat, s;
		try
		{
			strLat = Convert.ToDouble(Request.Form["txtbLat"].ToString()).ToString();
			strLng = Convert.ToDouble(Request.Form["txtbLng"].ToString()).ToString();
			//s = Request.Form["mapholder1"].ToString();

//			上傳至api 或 sql


			this.GPS.Text = DateTime.Now.ToString("HH:mm:ss") + "lat = " + strLat + "; lng = " + strLng;

			//this.txtbLat.Text = Login(strLat, strLng);

		}
		catch (Exception ex)
		{
			//經緯度值有誤
		}
	}

	protected void btnGPS_Click(object sender, EventArgs e)
	{
		string lat = this.txtbLat.Value;
		string lon = this.txtbLng.Value;

		if (lat == "" || lat == null)
		{
			GetAPIUrl(index);
		}
		else
		{
			string _url = "";
			_url = "Default_GPS.aspx?CaseNumber=" + CaseNumber + "&Lat=" + lat + "&Lon=" + lon;

			if (_url != "")
			{
				Response.Redirect(_url, false);
			}
			
		}


	}

}