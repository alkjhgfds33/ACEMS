<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default_GPS.aspx.cs" Inherits="Default_GPS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" href="Images/cctv__2__LCU_icon.ico" />
    <title><% =pageTitle %></title>
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



        /* 大屏幕上的高度 */
        @media (min-width: 992px) {
            .ImgH1 {
                padding: 0px;
                height: 40vh;
            }
        }

        /* 小屏幕上的高度 */
        @media (max-width: 991.98px) {
            .ImgH2 {
                padding: 0px;
                height: 20vh;
            }
        }
    </style>
    <script>
        var myVar = setInterval(changeImage, 500);

        function OpenWin(url) {
            window.open(url, 'mywin', 'height=400, width=400, toolbar=no,location=no, status=no')
        }

        function Navigate(i) {
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
                case 5:
                    javascript: window.open("<%=returnUrl(5,"cam")%>");
                    break;
                case 6:
                    javascript: window.open("<%=returnUrl(6,"cam")%>");
                    break;
                case 7:
                    javascript: window.open("<%=returnUrl(7,"cam")%>");
                    break;
                case 8:
                    javascript: window.open("<%=returnUrl(8,"cam")%>");
                    break;
            }
        }

        function changeImage() {
            var strUrl1 = "<%=ss1()%>";
            var strUrl2 = "<%=ss2()%>";
            var strUrl3 = "<%=ss3()%>";
            var strUrl4 = "<%=ss4()%>";
            var strUrl5 = "<%=ss5()%>";
            var strUrl6 = "<%=ss6()%>";
            var strUrl7 = "<%=ss7()%>";
            var strUrl8 = "<%=ss8()%>";
            var n = Math.random();
            document.getElementById('ccdImage1').src = "<%=returnUrl(1,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage2').src = "<%=returnUrl(2,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage3').src = "<%=returnUrl(3,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage4').src = "<%=returnUrl(4,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage5').src = "<%=returnUrl(5,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage6').src = "<%=returnUrl(6,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage7').src = "<%=returnUrl(7,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage8').src = "<%=returnUrl(8,"jpg")%>" + '?n=' + n;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-lg-12">
            <div class="row bg-primary justify-content-center align-content-center">
                
                <div class="col-sm-6 col-md-6 col-lg-3 bg-info" style="padding: 0.2%; height: 6vh;" align="right"  >
                    <asp:ImageButton ID="ImageGPS" src="images/GPS.png" Style="padding-right: 20px ;height: 70%; width: 15%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom"
                         OnClick="btnGPS_Click" />  

                    <asp:DropDownList ID="DropDownList1" runat="server" Height="100%" Width="80% " 
                        Font-Size="35px" AutoPostBack="True"
                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                    </asp:DropDownList>
                </div>

                <div class="col-sm-6 col-md-6 col-lg-3 bg-info" style="padding: 0.2%; height: 6vh;" align="center">
                    <asp:DropDownList ID="DropDownList2" runat="server" Height="100%" Width="100%"
                        Font-Size="35px" AutoPostBack="True"
                        OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged1">
                    </asp:DropDownList>
                </div>

                <div class="col-sm-6 col-md-6 col-lg-3 bg-info" style="padding: 0.2%; height: 6vh;" align="center">
                    <input type="button" value="上一頁" class="w3-btn_green" style="height: 100%; width: 100%; font-size: 38px;"
                        runat="server" onserverclick="btnUpPage_Click" />
                </div>

                <div class="col-sm-6 col-md-6 col-lg-3 bg-info" style="padding: 0.2%; height: 6vh;" align="center">
                    <input type="button" value="下一頁" class="w3-btn_green" style="height: 100%; width: 100%; font-size: 38px;"
                        runat="server" onserverclick="btnDownPage_Click" />
                </div>

            </div>

            <div class="row">
                <div class="col-sm-6 col-md-6 col-lg-3 bg-info ImgH1 ImgH2" >
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 30px;">
                        <asp:Label ID="lblTitle1" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle1_stno" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage1" src="images/loading2.png" Style="height: 100%; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(10)" OnClick="s" />
                </div>

                <div class="col-sm-6 col-md-6 col-lg-3 bg-info ImgH1 ImgH2"">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 30px;">
                        <asp:Label ID="lblTitle2" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle2_stno" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage2" src="images/loading2.png" Style="height: 100%; width: 100%;" runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"  OnClientClick="Navigate(10)" OnClick="s" />
                </div>

                <div class="col-sm-6 col-md-6 col-lg-3 bg-info ImgH1 ImgH2">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 30px;">
                        <asp:Label ID="lblTitle3" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle3_stno" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage3" src="images/loading2.png" Style="height: 100%; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(10)" OnClick="s" />
                </div>

                <div class="col-sm-6 col-md-6 col-lg-3 bg-info ImgH1 ImgH2">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 30px;">
                        <asp:Label ID="lblTitle4" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle4_stno" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage4" src="images/loading2.png" Style="height: 100%; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(10)" OnClick="s" />
                </div>
            </div>

            <div class="row">
                <div class="col-sm-6 col-md-6 col-lg-3 bg-info ImgH1 ImgH2">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 30px;">
                        <asp:Label ID="lblTitle5" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle5_stno" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage5" src="images/loading2.png" Style="height: 100%; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(10)" OnClick="s" />
                </div>
                <div class="col-sm-6 col-md-6 col-lg-3 bg-info ImgH1 ImgH2">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 30px;">
                        <asp:Label ID="lblTitle6" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle6_stno" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage6" src="images/loading2.png" Style="height: 100%; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(10)" OnClick="s" />
                </div>
                <div class="col-sm-6 col-md-6 col-lg-3 bg-info ImgH1 ImgH2">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 30px;">
                        <asp:Label ID="lblTitle7" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle7_stno" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage7" src="images/loading2.png" Style="height: 100%; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(10)" OnClick="s" />
                </div>
                <div class="col-sm-6 col-md-6 col-lg-3 bg-info ImgH1 ImgH2">
                    <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 30px;">
                        <asp:Label ID="lblTitle8" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblTitle8_stno" runat="server" Text="" Visible="false"></asp:Label>
                    </p>
                    <asp:ImageButton ID="ccdImage8" src="images/loading2.png" Style="height: 100%; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(10)" OnClick="s" />
                </div>
            </div>

            &nbsp;&nbsp; 
           第<asp:Label ID="lblPage" runat="server" Text="?" />
            頁, 
           共<asp:Label ID="lblPageTotle" runat="server" Text="?" />
            頁
            <asp:Label ID="lblStation" runat="server" Text="?" />
            &nbsp;<asp:Label ID="lblIdx" runat="server" Text="?" />
            &nbsp;<asp:Label ID="lblTotalCount" runat="server" Text="?" />
            &nbsp;<asp:Label ID="lblSelectType" runat="server" Text="?" />           
        </div>
        
        <footer class="text-center bg-dark text-white py-2" style="z-index: 5; margin-right: 0px; margin-left: 0px; margin-top: 0%; height: 15vh" align="center">
            Copyrights © 2023 All Rights Reserved by 台灣監測系統科技有限公司
            <a href="login.aspx" class="nav-link" id="login_a" runat="server">[登入]</a>     
            <a class="nav-link" id="GetGPS" runat="server" onserverclick="btnGPS_Click">[GPS] </a>  
                  

            <asp:Label ID="userName_a" runat="server" Text="?" ></asp:Label>
            <a href="logout.aspx" class="nav-link" onclick="return confirm('你確定要登出嗎?')" id="logout_a" runat="server">[登出]</a>     
            
            <input type="text" style="width: 80px ; height : 30px ; display: none " id="txtbLat" name="nLat" runat="server" />
            <input type="text" style="width: 80px ; height : 30px ; display: none " id="txtbLng" name="nLng" runat="server" />   
            

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>            
            <asp:Timer ID="Timer1" runat="server" Interval="10000" ontick="tmrRefresh_Tick"></asp:Timer>            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1">
                    </asp:AsyncPostBackTrigger>
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="GPS" runat="server" Text="Label"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <label type="text" id="mapholder1"></label>  
        </footer>

        <script>
            var x = document.getElementById("demo");

            function getLocation() {

                console.log("getLocation ");

                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(showPosition, showError);
                }
                else {
                    x.innerHTML = "Geolocation is not supported by this browser.";
                }
            }

            function showPosition(position) {
                var latlon = position.coords.latitude + "," + position.coords.longitude;

                //set value in textbox
                var inputLat = document.getElementById("txtbLat");
                inputLat.value = position.coords.latitude;
                var inputLng = document.getElementById("txtbLng");
                inputLng.value = position.coords.longitude;

                //document.getElementById("mapholder1").innerText = latlon;         
                console.log("showPosition " + position.coords.latitude + " , " + position.coords.longitude);

                document.getElementById("GPS").innerText = latlon;

            }

            function showError(error) {
                switch (error.code) {
                    case error.PERMISSION_DENIED:
                        x.innerHTML = "User denied the request for Geolocation.";
                        break;
                    case error.POSITION_UNAVAILABLE:
                        x.innerHTML = "Location information is unavailable.";
                        break;
                    case error.TIMEOUT:
                        x.innerHTML = "The request to get user location timed out.";
                        break;
                    case error.UNKNOWN_ERROR:
                        x.innerHTML = "An unknown error occurred.";
                        break;
                }
            }

            window.onload = function () {
                getLocation();
                //setInterval(getLocation, 1000);
            };
        </script>
    </form>
</body>
</html>