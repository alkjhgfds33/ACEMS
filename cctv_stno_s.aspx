<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cctv_stno_s.aspx.cs" Inherits="cctv_stno_s" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
            padding: 5px 7px 5px 7px;
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
        var myVar = setInterval(changeImage, 250);

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
            }
        }

        function changeImage() {
            var strUrl1 = "<%=ss1()%>";
            var strUrl2 = "<%=ss2()%>";
            var strUrl3 = "<%=ss3()%>";
            var strUrl4 = "<%=ss4()%>";
            var strUrl5 = "<%=ss5()%>";
            var strUrl6 = "<%=ss6()%>";

          
            var n = Math.random();
            var ss = Date.now.toString("HHmmssfff");
          //  document.getElementById('ccdImage1').src = "<%=returnUrl(1,"mjpg")%>";
          //  document.getElementById('ccdImage2').src = "<%=returnUrl(2,"mjpg")%>";
          //  document.getElementById('ccdImage3').src = "<%=returnUrl(3,"mjpg")%>" ;
          //  document.getElementById('ccdImage4').src = "<%=returnUrl(4,"mjpg")%>" ;
          //  document.getElementById('ccdImage5').src = "<%=returnUrl(5,"mjpg")%>" ;
          //  document.getElementById('ccdImage6').src = "<%=returnUrl(6,"mjpg")%>"



            document.getElementById('ccdImage_1').src = "<%=returnUrl(1,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage_2').src = "<%=returnUrl(2,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage_3').src = "<%=returnUrl(3,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage_4').src = "<%=returnUrl(4,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage_5').src = "<%=returnUrl(5,"jpg")%>" + '?n=' + n;
            document.getElementById('ccdImage_6').src = "<%=returnUrl(6,"jpg")%>" + '?n=' + n;


        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div class="col-lg-12">


           <div class="row" style="height:600px;">
               <div class="col-sm-8 col-md-8 col-lg-8 bg-info" style="padding: 0px;">
                   <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 35px; font-weight: bold;">
                       <asp:Label ID="lblTitle1" runat="server" Text=""></asp:Label>
                       <asp:Label ID="lblTitle1_id" runat="server" Text=""></asp:Label>
                   </p>

                   <p class="text-light text-center" style="background-color: black; height: 600px; margin: 0px;">
                       <asp:ImageButton ID="ccdImage_1" src="images/loading2.png" Style="height: 600px; width: 100%;"
                           runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                           OnClientClick="Navigate(10)" OnClick="s" />
                   </p>
               </div>

               <div class="col-sm-4 col-md-4 col-lg-4 bg-info" style="padding: 0px;">
                   <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 35px; font-weight: bold;">
                       <asp:Label ID="lblTitle2" runat="server" Text=""></asp:Label>
                       <asp:Label ID="lblTitle2_id" runat="server" Text=""></asp:Label>
                   </p>

                   <p class="text-light text-center" style="background-color: black; height: 285px; margin: 0px;">
                       <asp:ImageButton ID="ccdImage_2" src="images/loading2.png" Style="height: 285px; width: 100%;"
                           runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                           OnClientClick="Navigate(20)" OnClick="s" />
                   </p>

                   <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 35px; font-weight: bold;">
                       <asp:Label ID="lblTitle3" runat="server" Text=""></asp:Label>
                       <asp:Label ID="lblTitle3_id" runat="server" Text=""></asp:Label>
                   </p>

                   <p class="text-light text-center" style="background-color: black; height: 285px; margin: 0px;">
                       <asp:ImageButton ID="ccdImage_3" src="images/loading2.png" Style="height: 285px; width: 100%;"
                           runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                           OnClientClick="Navigate(30)" OnClick="s" />
                   </p>
               </div>



               <div class="col-sm-12 col-md-12 col-lg-4 bg-info" style="padding: 0px;">
                   <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 35px">
                       <asp:Label ID="lblTitle4" runat="server" Text=""></asp:Label>
                       <asp:Label ID="lblTitle4_id" runat="server" Text=""></asp:Label>
                   </p>
                   <asp:ImageButton ID="ccdImage_4" src="images/loading2.png" Style="height: 390px; width: 100%;"
                       runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                       OnClientClick="Navigate(40)" OnClick="s" />
               </div>


               <div class="col-sm-12 col-md-12 col-lg-4 bg-info" style="padding: 0px;">
                   <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px; font-size: 35px">
                       <asp:Label ID="lblTitle5" runat="server" Text=""></asp:Label>
                       <asp:Label ID="lblTitle5_id" runat="server" Text=""></asp:Label>
                   </p>
                   <asp:ImageButton ID="ccdImage_5" src="images/loading2.png" Style="height: 390px; width: 100%;"
                       runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="~/img/plus.png"
                       OnClientClick="Navigate(50)" OnClick="s" />
               </div>

               <div class="col-sm-12 col-md-12 col-lg-4 bg-info" style="padding: 0px;">
                   <p class="text-light text-center" style="background-color: black; height: 30px; line-height: 30px; margin: 0px;">
                       <asp:Label ID="lblTitle6" runat="server" Text=""></asp:Label>
                       <asp:Label ID="lblTitle6_id" runat="server" Text=""></asp:Label>
                   </p>
                   <asp:ImageButton ID="ccdImage_6" src="images/loading2.png" Style="height: 390px; width: 100%;"
                       runat="server" AutoPostBack="True" ImageAlign="Bottom" ImageUrl="images/loss.png"
                       OnClientClick="Navigate(60)" OnClick="s" />
               </div>
           </div>


        </div>
    </form>
</body>
</html>

