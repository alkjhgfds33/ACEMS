<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

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
            var myVar = setInterval(changeImage, 2000);
            function changeImage()
            {
                var strUrl1 = "<%=ss_dt()%>";
                var n = Math.random();
                document.getElementById('Image_Dt').src = strUrl1 + '?n=' + n;
                document.getElementById('Image_WL1').src = "<%=ss_WL1()%>" + '?n=' + n;
                document.getElementById('Image_WL2').src = "<%=ss_WL2()%>" + '?n=' + n;
                document.getElementById('Image_WL3').src = "<%=ss_WL3()%>" + '?n=' + n;
            }
        </script>
        
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick"></asp:Timer>         
                <div>
                 <p class="text-light text-center" style="background-color:black; height:30px; line-height:30px; margin:0px;">
                    
                    <asp:Label ID="lblTitle1" style="height:100px; width:100%;" runat="server" Text=""></asp:Label></p>

                    <asp:Image  id="Image_Dt" src="/images/stno_Dt.png"  style="height:100px; width:100%;"  
                        runat="server" AutoPostBack="True"  ImageAlign="Bottom" ImageUrl="~/img/plus.png"  />
                    <asp:Image  id="Image_WL1" src="/images/stno_WL1.png"  style="height:300px; width:100%;"  
                        runat="server" AutoPostBack="True"  ImageAlign="Bottom" ImageUrl="~/img/plus.png"  />

                    <asp:Image  id="Image_WL2" src="/images/stno_WL2.png"  style="height:300px; width:100%;"  
                        runat="server" AutoPostBack="True"  ImageAlign="Bottom" ImageUrl="~/img/plus.png"  />
                    <asp:Image  id="Image_WL3" src="/images/stno_WL3.png"  style="height:300px; width:100%;"  
                        runat="server" AutoPostBack="True"  ImageAlign="Bottom" ImageUrl="~/img/plus.png"  />                                                    
                </div>               
    </form>
   
</body>
</html>



