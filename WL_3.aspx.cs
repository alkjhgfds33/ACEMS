using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACEMS;

public partial class WL_3 : System.Web.UI.Page
{
	ACEMS_SQL aCEMS_SQL = new ACEMS_SQL();
	//string StrConn = "Data Source=192.168.1.101;Initial Catalog=PingTungWaterServer;Persist Security Info=True;User ID=Excellent;Password=aCE+29170257";
	string StrConn = "Data Source=www.acems.tw,1434;Initial Catalog=ACE_Platform_PIF_PingTungWL;Persist Security Info=True;User ID=Excellent;Password=aCE+29170257";

   // string StrConn = "Data Source=www.acems.tw,1434;Initial Catalog=ACE_Platform_PIF_PingTungWL;Persist Security Info=True;User ID=Excellent;Password=aCE+29170257";


    string CaseNumber = "1";
	string stationId = "";
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			CaseNumber = Request.QueryString["CaseNumber"];
			stationId = Request.QueryString["stationId"];
			if (stationId == null)
			{

			}
			else
			{
				//GetAPIUrlByStation(stationId);
			}
		}
		catch (Exception)
		{


		}

		if (!Page.IsPostBack)
		{




		}

	}
	protected void tmrRefresh_Tick(object sender, EventArgs e)
	{
		if (stationId == null)
		{
			this.Label1.InnerText = "無此測站";
		}
		else
		{
			try
			{
				this.Label1.InnerText = stationId;
				this.lbl_station.InnerText = stationId;

				WL _wL = new WL();
				_wL = GetWL(stationId);

				this.lblDateTime.InnerText = Convert.ToDateTime(_wL.datetime[0]).ToString("yyyy-MM-dd HH:mm:ss");

				Random r = new Random();
				double d = r.NextDouble();
				this.WL1_Name.InnerText = _wL.name[0];
				this.WL1_Value.InnerText = _wL.value[0];

				d = r.NextDouble();
				this.WL2_Name.InnerText = _wL.name[1];
				this.WL2_Value.InnerText = _wL.value[1];

				d = r.NextDouble();
				//	this.WL3_Name.InnerText = "前池水位";
				//	this.WL3_Value.InnerText = (123 + d).ToString("F2") + " M";
				this.WL3_Name.InnerText = _wL.name[2];
				this.WL3_Value.InnerText = _wL.value[2];

				//this.lbl4.Style["Style"] = "color:blue";
				//this.lbl4.InnerText = teststr;
				//this.lbl4.ForeColor = Color.FromArgb(0, 0, 0);//(R, G, B) (0, 0, 0 = black
			}
			catch (Exception ex)
			{
				this.Label1.InnerText = ex.Message;
			}
		}
	}
	public struct WL
	{
		//  public string stno;
		public List<string> sub_stno;
        public List<string> pqUUID;

        public List<string> name;
		public List<string> datetime;
		public List<string> value;

	}
	private WL GetWL(string stnoname)
	{
		try
		{
			WL _wL = new WL();
			string result = "";
			string Sql_CMD_Select_Substno = "SELECT  [substno]  as 'DATA' " +
                                    " FROM [ACE_Platform_PIF_PingTungWL].[dbo].[MonitorGroupSub]" +
									" where [stnoname]='" + stnoname + "' and [Enable] =1 order by [Sequence]";

            string Sql_CMD_Select_PqUUID = "SELECT  [PqUUID]  as 'DATA' " +
                                    " FROM [ACE_Platform_PIF_PingTungWL].[dbo].[MonitorGroupSub]" +
                                    " where [stnoname]='" + stnoname + "' and [Enable] =1 order by [Sequence]";



            string Sql_CMD_Select_displayname = "SELECT  [displayname]  as 'DATA' " +
                                    " FROM [ACE_Platform_PIF_PingTungWL].[dbo].[MonitorGroupSub]" +
									" where [stnoname]='" + stnoname + "' and [Enable] =1 order by [Sequence]";
			_wL.sub_stno = new List<string>();
            _wL.pqUUID = new List<string>();


            _wL.name = new List<string>();
			_wL.datetime = new List<string>();
			_wL.value = new List<string>();

			_wL.sub_stno = aCEMS_SQL.Get_SQL_SelectStringList(StrConn, Sql_CMD_Select_Substno, ref result);
            _wL.pqUUID = aCEMS_SQL.Get_SQL_SelectStringList(StrConn, Sql_CMD_Select_PqUUID, ref result);

            _wL.name = aCEMS_SQL.Get_SQL_SelectStringList(StrConn, Sql_CMD_Select_displayname, ref result);
			for (int i = 0; i < _wL.sub_stno.Count; i++)
			{
				string Sql_CMD_Select_lastupdate = "SELECT  [LastUpdate]  as 'DATA' " +
                                    " FROM  [ACE_Platform].[dbo].[PhysicalQuantity]" +
									" where [PqUUID] ='" + _wL.pqUUID[i] + "'";

				string Sql_CMD_Select_lastdata = "SELECT  [LastValue]  as 'DATA' " +
                                    " FROM [ACE_Platform].[dbo].[PhysicalQuantity]" +
                                    " where [PqUUID] ='" + _wL.pqUUID[i] + "'";


				_wL.datetime.Add(aCEMS_SQL.Get_SQL_SelectONE(StrConn, Sql_CMD_Select_lastupdate, ref result));

				double value = Convert.ToDouble(aCEMS_SQL.Get_SQL_SelectONE(StrConn, Sql_CMD_Select_lastdata, ref result));

				if (value < -99990)
				{
					_wL.value.Add("NULL");
				}
				else
				{
					_wL.value.Add(value.ToString("F3") + " M");
				}
			}

			return _wL;
		}
		catch (Exception ex)
		{
			this.Label1.InnerText = ex.Message;

			return new WL(); ;
		}
	}




	/*protected void tmrRefresh_Tick(object sender, EventArgs e)
	{
		if (stationId == null)
		{
			this.Label1.InnerText = "無此測站";
		}
		else
		{


			//stationId = Request.QueryString["stationId"];
			this.Label1.InnerText = stationId;
			this.lbl_station.InnerText = stationId ;

			this.lblDateTime.InnerText ="資料時間:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			Random r = new Random();
			double d = r.NextDouble();
			this.WL1_Name.InnerText = "內水位" ;
			this.WL1_Value.InnerText = (123 + d).ToString("F2") + " M";

			d = r.NextDouble();	
			this.WL2_Name.InnerText = "外水位";
			this.WL2_Value.InnerText = (123 + d).ToString("F2") + " M";


			d = r.NextDouble();
			this.WL3_Name.InnerText = "前池水位";
			this.WL3_Value.InnerText = (123 + d).ToString("F2") + " M";


	

			//this.lbl4.Style["Style"] = "color:blue";
			//this.lbl4.InnerText = teststr;
			//this.lbl4.ForeColor = Color.FromArgb(0, 0, 0);//(R, G, B) (0, 0, 0 = black
		}
	}

	*/




	/*
	 * 
	 * 
	 * *
	 * 
	 
	    <div class="col-sm-1 col-sm-12 col-md-12 col-lg-12 " 
                                    style="height:70px; line-height:70px; background-color:darkgray; color:blue; font-size:70px; text-align:left">
                                    <label id="lblDateTime" runat="server"></label>
                                </div>
	
	
	                                <div class="col-sm-1 col-sm-12 col-md-12 col-lg-12 " 
                                    style="height:30px; line-height:30px; background-color:black; color:white; font-size:16px; text-align:center">
                                     <label id="Label1" runat="server"></label>
                                </div>                           

                                 <div class="col-sm-1 col-sm-6 col-md-6 col-lg-6 " style="border-style:ridge ridge ridge ridge;border-color:#000000;background-color:darkgray; color:blue;height:80px; line-height:70px; background-color:darkgray; color:blue; font-size:70px; text-align:center">
                                    <label id="lbl_station" runat="server"></label></div>
                              
                                <div class="col-sm-1 col-sm-6 col-md-6 col-lg-6 " style="border-style:ridge ridge ridge ridge;border-color:#000000;background-color:darkgray; color:blue; height:80px; line-height:70px; background-color:darkgray; color:blue; font-size:70px; text-align:left">
                                    <label id="lblDateTime" runat="server"></label></div>


                                
                                <div class="col-sm-1 col-sm-12 col-md-12 col-lg-12 " style="background-color:darkgray; color:red; height:30vh; line-height:300px;  text-align:left">
                                    <label id="lbl1" runat="server"></label>
                                </div>

                                <div class="col-sm-1 col-sm-12 col-md-12 col-lg-12 " style="background-color:darkgray; color:red; height:30vh; line-height:300px;  text-align:left">
                                    <label id="lbl2" runat="server"></label>
                                </div>

                                <div class="col-sm-1 col-sm-6 col-md-6 col-lg-6 " style="border-style:dotted dashed ridge double;border-color:#000000;background-color:darkgray; color:blue; height:30vh; line-height:300px;  text-align:left">
                                    <label id="lbl3" runat="server"></label></div>

                                <div class="col-sm-1 col-sm-6 col-md-6 col-lg-6 " style="border-style:dotted dashed ridge double;border-color:#000000;background-color:darkgray; color:blue; height:30vh; line-height:300px;  text-align:right">
                                    <label id="lbl4" runat="server"></label></div>
	
	*/



}