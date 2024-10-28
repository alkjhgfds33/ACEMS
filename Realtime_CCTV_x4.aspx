<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Realtime_CCTV_x4.aspx.cs" Inherits="Realtime_CCTV_x4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <script src="Scripts/jquery-3.5.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <style type="text/css">
        .w3-btn_green {
            border: 1px solid #00AA00;
            -webkit-box-shadow: #FFFFFF 0px 0px 1px inset;
            -moz-box-shadow: #FFFFFF 0px 0px 1px inset;
            box-shadow: #FFFFFF 0px 0px 1px inset;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            font-size: 12px;
            font-family: 微軟正黑體;
            padding: 1px 1px 1px 1px;
            background-image: linear-gradient(to bottom, #00AA00,darkgreen);
            display: inline-block;
            outline: 0;
            vertical-align: middle;
            overflow: hidden;
            color: #fff;
            text-align: center;
            transition: .2s ease-out;
            cursor: pointer;
            white-space: nowrap;
            width: 100px;
        }

            .w3-btn_green:hover {
                box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
                background-image: linear-gradient(to bottom, White,White);
                color: Green;
            }
    </style>
    <script>
        var myVar = setInterval(changeImage, 500);

        function OpenWin(url) {
            window.open(url, 'mywin', 'height=400, width=400, toolbar=no,location=no, status=no')
        }

        function Navigate(i) {
            return
            switch (i) {
                case 1:
                    javascript: window.open("<%=returnUrl(1,"cam")%>");
                    break;
                case 2:
                    javascript: window.open("<%=returnUrl(2,"cam")%>");
                    break;
                case 3:
                    javascript: window.open("<%=returnUrl(3,"cam")%>");
                    break;
                case 4:
                    javascript: window.open("<%=returnUrl(4,"cam")%>");
                    break;
            }
        }

        function changeImage() {
            var n = Math.random();
            var ss = Date.now.toString("HHmmssfff");

            document.getElementById('ccdImage1').src = "<%=returnUrl(1,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage2').src = "<%=returnUrl(2,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage3').src = "<%=returnUrl(3,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage4').src = "<%=returnUrl(4,"jpg")%>" + '?n=' + n;

        }

    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="col-lg-12">
            <div class="row" >

                <div class="col-sm-12 col-md-12 col-lg-6 bg-info" style="padding: 0px; height: 430px;">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; font-size: 33px; margin: 0px;">
                        <asp:Label ID="lblTitle1" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle1_id" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage1" src="images/loading2.png" Style="height: 400px; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(1)" OnClick="s" />
                </div>

                <div class="col-sm-12 col-md-12 col-lg-6 bg-info" style="padding: 0px; height: 430px;">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; font-size: 33px; margin: 0px;">
                        <asp:Label ID="lblTitle2" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle2_id" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage2" src="images/loading2.png" Style="height: 400px; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(2)" OnClick="s" />
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-6 bg-info" style="padding: 0px; height: 430px;">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; font-size: 33px; margin: 0px;">
                        <asp:Label ID="lblTitle3" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle3_id" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage3" src="images/loading2.png" Style="height: 400px; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(3)" OnClick="s" />
                </div>

                <div class="col-sm-12 col-md-12 col-lg-6 bg-info" style="padding: 0px; height: 430px;">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; font-size: 33px; margin: 0px;">
                        <asp:Label ID="lblTitle4" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle4_id" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage4" src="images/loading2.png" Style="height: 400px; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(4)" OnClick="s" />
                </div>
            </div>

            <asp:Label ID="lblStationID" runat="server" Text="?" />
        </div>
        <footer class="text-center bg-dark text-white py-2" style="z-index: 5; margin-right: 0px; margin-left: 0px; margin-top: 0%; height: 200px" align="center">
            
            <input type="button" id="btnServiceRestart" value="重啟服務" class="w3-btn_green" style="height: 45px; width: 15%; font-size: 25px;"
                runat="server" onserverclick="btn_ServiceRestart_Click" />
            <asp:Label ID="lb_r1" runat="server" Text="">&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>

            <input type="button" id="btnGoUrl" value="開啟攝影機" class="w3-btn_green" style="height: 45px; width: 15%; font-size: 25px;"
                runat="server" onserverclick="btn_GoUrl_Click" />
            <asp:Label ID="lb_r2" runat="server" Text="">&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>

              <input type="button" id="btnGoMap" value="導航" class="w3-btn_green" style="height: 45px; width: 15%; font-size: 25px;"
                runat="server" onserverclick="btn_GoMap_Click" />
            <asp:Label ID="lb_r3" runat="server" Text="">&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>


            <input type="button" id="btnR" value="回首頁" class="w3-btn_green" style="height: 45px; width: 15%; font-size: 25px;"
                runat="server" onserverclick="btn_Return_Click" />
            <asp:Label ID="lb_r4" runat="server" Text="">&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>

            <input type="button" id="btnRefresh" value="重新整理" class="w3-btn_green" style="height: 45px; width: 15%; font-size: 25px;"
                runat="server" onserverclick="btn_Refresh_Click" />
            <asp:Label ID="Label4" runat="server" Text="">&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>

            <input type="button" id="btnAlert" value="故障報修" class="w3-btn_green" style="height: 45px; width: 15%; font-size: 25px;"
                runat="server" onserverclick="btn_Err_Click" />
            <br />
            
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="tmrRefresh_Tick"></asp:Timer>         
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <p class="text-light text-center" style="padding: 0px ;  height: 30px; line-height: 30px; font-size: 33px; margin: 0px;">
                            <asp:Label ID="Label1" runat="server" Text="0" Visible="true"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="0" Visible="true"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text="0" Visible="true"></asp:Label>
                        </p>
                        </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" />
                    </Triggers>
                </asp:UpdatePanel>


            Copyrights © 2024 All Rights Reserved by 台灣整合防災工程技術顧問有限公司
        </footer>
    </form>
</body>
</html>
