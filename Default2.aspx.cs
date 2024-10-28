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

public partial class Default2 : System.Web.UI.Page
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


	string CaseNumber = "";
	string stationId = "";


	string WebApiUrl = WebConfigurationManager.AppSettings["WebApiUrl"].ToString();


	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{	
			try
			{
				CaseNumber = Request.QueryString["CaseNumber"];
				stationId = Request.QueryString["stationId"];
			}
			catch (Exception)
			{

			}		

			GetAPIUrlByStation(stationId);


			/*if (stationId != null)
			{
				GetAPIUrlByStation(stationId);
			}
			else
			{
				GetAPIUrl(index);
			}*/
		}
	}

	#region strUrl 

	string[] strUrl = new string[9];
	string strUrl1 = "";
	string strUrl2 = "";
	string strUrl3 = "";
	string strUrl4 = "";

	public string returnUrl(int index, string Ex)
	{
		if (strUrl[index] == "images/loss.png" || strUrl[index] == "images/NoVideo.png")
		{
			return (strUrl[index]);
		}
		else
		{
			return (strUrl[index] + "." + Ex);
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
		string strSendUrl = "http://" + WebApiUrl + "GetCameraAPI?CaseNumber="+CaseNumber+"&Count=8&index=" + index; //所有監測站
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
		
		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";
	
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
				
			}
		}
	}
	private void GetAPIUrlByStation(string stationID)
	{
		//
		// Loading ccd frame
		//
		string strSendUrl = "http://" + WebApiUrl + "GetCameraAPiByStation?CaseNumber="+ CaseNumber + "&StationName=" + stationID; //所有監測站
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
		strUrl2 = "images/loss.png";
		strUrl3 = "images/loss.png";
		strUrl4 = "images/NoVideo.png";
	
		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";
		

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
				
			}
		}
	}
	private void GetAPIUrlByStationId(string stationID)
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
		
		this.lblTitle1.Text = "";
		this.lblTitle2.Text = "";
		this.lblTitle3.Text = "";
		this.lblTitle4.Text = "";
	

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
			}
		}
	}
	


	protected void s(object sender, ImageClickEventArgs e)
	{
		//lblStation.Text = "1234";
		// 
		// 調整目前page
		//
		try
		{
			stationId = Request.QueryString["stationId"];
		}
		catch (Exception)
		{
		}

		GetAPIUrlByStation(stationId);
		return;
	}

}