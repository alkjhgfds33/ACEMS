<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Realtime_CCTV.aspx.cs" Inherits="Realtime_CCTV" %>

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
        var count = 0;
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
            count++;
            if (count > 60 ) {
                document.getElementById('ccdImage').src = "images/NoVideo2.png";

                document.getElementById('Label1').innerText = "";

            }
            else {

                document.getElementById('ccdImage').src = "<%=returnUrl(1,"jpg")%>" + '?n=' + n;
            }
        }

    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="col-lg-12">
            <div class="row" style="height: 10px; background-color : black" >

            </div>
            <div class="row"  style="height: 100vh; background-color : black ;" >
              
                
                <div class="col-sm-12 col-md-12 col-lg-2 bg-info" style="padding: 0px; height: 0px;">                    
                </div>
             

                <div class="col-sm-12 col-md-12 col-lg-8 bg-info" style="padding: 0px ; height: 60vh; background-color : black">                              
                     
                        <p class="text-light text-center" style="padding: 0px ; background-color: black; height: 30px; line-height: 30px; font-size: 33px; margin: 0px;">
                        <asp:Label ID="Label1" runat="server" Text="0" Visible="true"></asp:Label>
                        </p>
          

                    <asp:ImageButton ID="ccdImage" src="images/NoVideo2.png" Style="padding: 0px ; height: 60vh; width: 100%;"
                        runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                        OnClientClick="Navigate(2)" OnClick="s"  />
                    </div>                

               <div class="col-sm-12 col-md-12 col-lg-2 bg-info" style="padding: 0px; height: 0px;">                      
                </div>
            </div>

           

            <asp:Label ID="lblStationID" runat="server" Text="?" />
        </div>
        
    </form>
</body>
</html>
