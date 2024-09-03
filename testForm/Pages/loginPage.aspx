<%@ Page Title="Indesso Login Site" Language="C#" AutoEventWireup="true" CodeBehind="loginPage.aspx.cs" Inherits="testForm.Pages.loginPage" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html>
<head>
<title>Indesso : Material Management Login</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="../Images/HR.png" rel="shortcut icon" type="image/x-icon" />
<link href="../CSS/Login.css" rel='stylesheet' type='text/css' media="all" />
<meta name="viewport" content="width=device-width, initial-scale=1">
<!--OnFocus and OnBlur-->
<script type="text/javascript">
    function onFocus(obj) {
        if (obj.value == "PASSWORD" || obj.value == '%%') {
            obj.value = "";
        }
        else if (obj.value == "USERNAME") {
            obj.value = "";
        }
    }
    /*function onBlur(obj) {
        if (obj.value == "") {
            if (obj.type == 'password') {
                obj.value = "PASSWORD";
            }
            else
            obj.value = "USERNAME";
        }
    }*/
</script>
<!--OnFocus and OnBlur-->
</head>
<body>
    <form runat="server" id="loginForm">
        <div class="content" style="width: 800px">
            <asp:Label runat="server" style="text-align: center; font-family: 'Orbitron', sans-serif; font-size: 34px; font-weight:100; color: #58a360;" Text="MATERIAL MASTER WORKFLOW" />
                <fieldset style="width: 450px; margin: -1% auto 0% auto">                    
                    <legend style="width: 90%; margin-left: auto; margin-right: auto; text-align: center; font-family: 'Orbitron', sans-serif; font-size: 34px; font-weight:100; color: #58a360;">USER LOGIN FORM</legend>
			        <div class="content1">
				        <asp:TextBox runat="server" ID="userid" Text="USERNAME" autocomplete="off" />
			        </div>
			        <div class="content2">
				        <asp:TextBox runat="server" ID="psw" Text="PASSWORD" type="password" autocomplete="off" />
			        </div>
			        <asp:Button runat="server" Text="LOGIN" OnClick="loginValidation_Click" />
                </fieldset>
	    </div>
        <div class="loginFooter">
            <asp:Label ID="lblFooter" runat="server" CssClass="copyright">&nbsp; &nbsp; Indesso &copy; 2018</asp:Label>
        </div>
    </form>
</body>
</html>
