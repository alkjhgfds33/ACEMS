<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default6.aspx.cs" Inherits="Default6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>


    <script>
        var x = document.getElementById("demo");

        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition, showError);
            }
            else {
                x.innerHTML = "Geolocation is not supported by this browser.";
            }
        }

        function showPosition(position) {
            var latlon = position.coords.latitude + "," + position.coords.longitude;
            var img_url = "https://maps.googleapis.com/maps/api/staticmap?center="
                + latlon + "&zoom=16&size=400x300&key=AIzaSyBu-916DdpKAjTmJNIgngS6HL_kDIKU0aU";
            document.getElementById("mapholder1").innerHTML = "<img src='" + img_url + "'>";
            document.getElementById("mapholder2").innerText = latlon;
            document.getElementById("mapholder3").innerText = img_url;

            document.getElementById("TextBox2").innerText = img_url;

            OpenWin(img_url);
        }

        function showError(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    x.innerHTML = "User denied the request for Geolocation."
                    break;
                case error.POSITION_UNAVAILABLE:
                    x.innerHTML = "Location information is unavailable."
                    break;
                case error.TIMEOUT:
                    x.innerHTML = "The request to get user location timed out."
                    break;
                case error.UNKNOWN_ERROR:
                    x.innerHTML = "An unknown error occurred."
                    break;
            }
        }

        function OpenWin(url) {
            window.open(url, 'mywin', 'height=400, width=400, toolbar=no,location=no, status=no')
        }

    </script>
</head>
<body>
        <p id="demo">Click the button to get your position.</p>
    <button onclick="getLocation()">Try It</button>
    <div id="mapholder1"></div>
    <div id="mapholder2"></div>
    <div id="mapholder3"></div>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="uploadFile" runat="server" />
            <br />    
            <br />
            <asp:Button ID="Button1" runat="server" Text="顯示圖片" onclick="Button1_Click" />     
            <asp:TextBox ID="TextBox1" runat="server" Text="顯示圖片"  />  
            <br />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="GPS" OnClientClick="getLocation()" />     
            <asp:TextBox ID="TextBox2" runat="server" Text="GPS"  /> 
        </div>        
    </form>



</body>
</html>
