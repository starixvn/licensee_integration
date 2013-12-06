<%@ Page Language="C#" CodeFile="Login.aspx.cs" Inherits="_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=1024">
    <link href="http://www.spondemo.com:8080/template//images/favicon.ico" rel="shortcut icon">    
    
    <title>Simulator ONEbook Internal System 2.0</title>
    <style type="text/css">
<!--
body {
	font-family: Arial, Helvetica, sans-serif;
	margin-top: 0px;
	margin-left: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	background-image: url(template/images/DS_index_bg.gif) ;
}

.body {margin:0 auto; width:100%;height:800px;background-repeat:no-repeat; background-position : center 1%;background-attachment: scroll; }


.footer {
	font-size: 10px;
	font-family: Arial, Helvetica, sans-serif;
	color: #666666;
}
select, form, input,textarea { font-family: Arial, Helvetica, sans-serif;	font-size: 11px; color: #454545; font-weight: normal;}

input{border: 1px solid #8a8a8a; padding: 2px 3px 2px 3px;}
-->
</style>
</head>
<body onload="MM_preloadImages('/images/ds_loginbtn_ov.gif')">
    <div class="body index-body">
        <form id="frmLogin" runat="server">
            <table id="Table_01" align="center" border="0" cellpadding="0" cellspacing="0" height="580"
                width="720">
                <tbody>
                    <tr>
                        <td>
                            <img src="template/images/indexds_01.jpg" alt="" height="79" width="185"></td>
                        <td>
                            <img src="template/images/indexds_02.jpg" alt="" height="79" width="242"></td>
                        <td>
                            <img src="template/images/indexds_03.jpg" alt="" height="79" width="261"></td>
                        <td>
                            <img src="template/images/indexds_04.jpg" alt="" height="79" width="32"></td>
                    </tr>
                    <tr>
                        <td>
                            <img src="template/images/indexds_05.jpg" alt="" height="209" width="185"></td>
                        <td>
                            <img src="template/images/indexds_06.jpg" alt="" height="209" width="242"></td>
                        <td colspan="2">
                            <img src="template/images/indexds_07_2.jpg" alt="" height="209" width="293"></td>
                    </tr>
                    <tr>
                        <td>
                            <img src="template/images/indexds_09.jpg" alt="" height="152" width="185"></td>
                        <td>
                            <img src="template/images/indexds_10.jpg" alt="" height="152" width="242"></td>
                        <td colspan="2" background="template/images/indexds_11_2.jpg" valign="bottom">
                            <table border="0" cellpadding="3" cellspacing="0" width="88%">
                                <tbody>
                                    <tr>
                                        <td width="35%">
                                            &nbsp;</td>
                                        <td width="65%">
                                            <asp:TextBox ID="txtUserName" runat="Server" Text="spon01"></asp:TextBox>                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtPassword" Text runat="Server" TextMode="Password" onkeydown="if(event.keyCode==13)callSubmit();"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td style="padding-right: 32px;" align="right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" ImageUrl="~/template/images/ds_loginbtn.gif" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br>
                            <asp:Label ID="lblMessage" runat="Server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <img src="template/images/indexds_13.jpg" alt="" height="140" width="185"></td>
                        <td colspan="3" style="padding: 6px 8px 0px 0px;" align="right" background="template/images/indexds_14_2.jpg"
                            valign="top">
                           </td>
                    </tr>
                      <tr>
                        <td colspan="2"> <p>Username/pwd : spon01/1234 </p></td>
                    </tr>
                  
                </tbody>
            </table>
           
        </form>
    </div>

    <script type="text/JavaScript">
<!--
function callSubmit(){
    var obj;
    obj=document.getElementById(<%="'"+txtUserName.ClientID+"'"%>);
    if(obj.value==''){
        obj.focus();
        alert('Please enter username');
        return false;
    }
    
    obj=document.getElementById(<%="'"+txtPassword.ClientID+"'"%>);
    if(obj.value==''){
        obj.focus();
        alert('Please enter password');
        return false;
    }
    document.frmLogin.submit();
}
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
//-->
    </script>

</body>
</html>
