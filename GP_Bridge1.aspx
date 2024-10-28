<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GP_Bridge1.aspx.cs" Inherits="GP_Bridge1" %>

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
    </style>
    <script>
        var myVar = setInterval(changeImage, 500);

        function OpenWin(url) {
            window.open(url, 'mywin', 'height=400, width=400, toolbar=no,location=no, status=no')
        }

      
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-lg-12">
            <div class="row bg-primary justify-content-center align-content-center">
                <div class="col-sm-6 col-md-6 col-lg-3 bg-info" style="padding: 1%;" align="center">
                    <input type="button" value="修改TOKEN-內部" class="w3-btn_green" style="height: 60px; width: 100%; font-size: 40px;"
                        runat="server" onserverclick="btn_Token_in_Click" />
                </div>

                <div class="col-sm-6 col-md-6 col-lg-3 bg-info" style="padding: 1%;" align="center">
                   <input type="button" value="修改TOKEN-業主" class="w3-btn_green" style="height: 60px; width: 100%; font-size: 40px;"
                        runat="server" onserverclick="btn_Token_out_Click" />
                </div>

                <div class="col-sm-6 col-md-6 col-lg-3 bg-info" style="padding: 1%;" align="center">
                    <input type="button" value="修改TOKEN-監測" class="w3-btn_green" style="height: 60px; width: 100%; font-size: 40px;"
                        runat="server" onserverclick="btn_Token_in2_Click" />
                </div>
                <div class="col-sm-6 col-md-6 col-lg-3 bg-info" style="padding: 1%;" align="center">
                    <input type="button" value="TEST1=2" class="w3-btn_green" style="height: 60px; width: 100%; font-size: 40px;"
                        runat="server" onserverclick="btnDownPage_Click" />
                </div>
            </div>
             &nbsp;&nbsp; 
                  <asp:Label ID="lb_result" runat="server" Text="?" />
            &nbsp;<asp:Label ID="lb_status" runat="server" Text="?" />
            &nbsp;<asp:Label ID="lb_time" runat="server" Text="?" />
            &nbsp;<asp:Label ID="lblSelectType" runat="server" Text="?" />
        </div>
        <footer class="text-center bg-dark text-white py-2" style="z-index: 5; margin-right: 0px; margin-left: 0px; margin-top: 0%; height: 100px" align="center">
            Copyrights © 2022 All Rights Reserved by 台灣監測系統科技有限公司
        </footer>
    </form>
</body>
</html>
