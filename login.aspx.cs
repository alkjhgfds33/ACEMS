using ACEMS_WebAPi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{

	string WebApiUrl = WebConfigurationManager.AppSettings["WebApiUrl"].ToString();
	
	string CaseNumber = "";
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["id"] != null)
		{
			//可直接登入
			//Response.Redirect("mACE_Realtime_Data_x48.aspx", true);
		}

		CaseNumber = Request.QueryString["CaseNumber"];
		if (CaseNumber == "" || CaseNumber == null)
		{
			CaseNumber = WebConfigurationManager.AppSettings["CaseNumber"].ToString();
		}
	}

	protected void btnLogin_Click(object sender, EventArgs e)
	{
		string strUserID = Request.Form["nID"].ToString();
		string strUserPass = Request.Form["nPass"].ToString();
		Session["id"] = strUserID;
		Session["passwd"] = strUserPass;

		//string strSendUrl = "http://" + WebApiUrl + "/API/token/ApiLogin/" + strUserID + "/" + strUserPass + "/";

		try
		{
			//
			// POST Data
			//
			ACEMS_API_Struct.Login.LoginUserInfo _LoginUserInfo = new ACEMS_API_Struct.Login.LoginUserInfo();
			ACEMS_API_Struct.Login.ApiLogin apiLogin = new ACEMS_API_Struct.Login.ApiLogin();

			apiLogin.Account = strUserID;
			apiLogin.PassWord = strUserPass;
			apiLogin.LoginIP = HttpContext.Current.Request.UserHostAddress; //使用者登入IP

			string url = "http://" + WebApiUrl + "/API/token/ApiLogin2/" + CaseNumber;
			HttpWebRequest reqest = (HttpWebRequest)WebRequest.Create(url);
			reqest.Method = "POST";
			reqest.ContentType = "application/json";
			Stream stream = reqest.GetRequestStream();

			string data = JsonConvert.SerializeObject(apiLogin);
			//this.Label1.Text = data;

			byte[] bs = System.Text.Encoding.UTF8.GetBytes(data);
			stream.Write(bs, 0, bs.Length);
			stream.Flush();
			stream.Close();
			HttpWebResponse response = (HttpWebResponse)reqest.GetResponse();
			StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

			_LoginUserInfo = JsonConvert.DeserializeObject<ACEMS_API_Struct.Login.LoginUserInfo>(sr.ReadToEnd());
			response.Close();

			if (_LoginUserInfo.Status == "OK")
			{
				if (_LoginUserInfo.LoginAuthDes == "管理員" || _LoginUserInfo.LoginAuthDes == "超級管理員")
				{
					Session["caseNum"] = CaseNumber; // _LoginUserInfo.CaseNumber;
					Session["loginAuth"] = _LoginUserInfo.LoginAuthDes; //管理員
					Session["userName"] = _LoginUserInfo.UserName;
					Session["token"] = _LoginUserInfo.token;


					Response.Redirect("Default.aspx", true); //直接進入ACE平台
				}
				else
				{
					Session["id"] = null;
					this.lblWarning.Text = "無權限!!";
				}
			}
			else
			{
				Session["id"] = null;
				this.lblWarning.Text = "帳號或密碼輸入錯誤!!";
			}
		}
		catch (Exception ex)
		{
			this.lblWarning.Text = ex.Message;
			return;
		}
	}
}