<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

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
    	body {
    		background-image: url(images/bg002.jpg);
    		background-repeat: no-repeat;
    		background-size: cover;
    		height: 100vh;
    	}

    	#wrapper {
   			width: 460px;
			height: 350px;
    		border-radius: 5px;
    		border: 1px solid #CCCCCC;
    		position: absolute;
    		top: 50%;
    		left: 50%;
            margin-top: -200px;
			margin-left: -230px;
    		background-color: rgba(0,127,127,0.3);
    	}

    	header {
    		background-color: #ECECEC;
    		padding: 10px 15px;
    	}

    	#content {
    		padding: 15px;
    	}

    	.form-group {
    		margin-bottom: 15px;
    	}

    	input.form-control {
    		display: block;
    		width: 100%;
    		height: 45px;
    		font-size: 30px;
    		color: #555555;
    		background-color: #FFFFFF;
    		border: 1px solid #CCCCCC;
    		border-radius: 4px;
    		padding: 5px;
    	}

    	.title {
    		font-weight: 300;
    		font: 25px "微軟正黑體";
    		background-color: rgba(15, 185, 216,0.8);
    		color: #efefef;
    		text-shadow: 0px 0px 20px rgba(127,255,255,1);
    	}
    </style>
</head>


<body>
    <form id="form1" runat="server">
        <div class="container">
            <div id="wrapper">
                <div class="title" style="height: 50px; line-height: 50px; padding-left: 15px;">
                    <p style="color: white;">使用者登入</p>
                </div>
                <div id="content" style="margin-top: 10px;">
                    <div>
                        <div class="input-group form-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-user"></i></span>
                            </div>
                            <input name="nID" type="text" class="form-control" placeholder="帳號" />
                        </div>
                        <div class="input-group form-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-key"></i></span>
                            </div>
                            <input name="nPass" type="password" class="form-control" placeholder="密碼" />
                        </div>
                        <br />

                        <asp:Panel ID="Panel1" runat="server" DefaultButton="SubmitButton">
                            <!--  <button type="button" class="btn btn-danger" style="width: 100%" runat="server" onserverclick="btnLogin_Click">登入</button>
						-->
                            <asp:Button ID="SubmitButton" runat="server" Text="登入" OnClick="btnLogin_Click" Style="width: 100%;  font-size: 28px;"  class="btn btn-danger" />

                        </asp:Panel>
                           <asp:Label ID="lblWarning" runat="server" Text="" ForeColor="Red"></asp:Label>

                        <a align="center"  href="default.aspx" class="nav-link"  id="exit_a" runat="server">[回首頁]</a>     
                       
                    </div>
                </div>
            </div>


        </div>
    </form>
</body>
</html>
