<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aguarde.aspx.cs" Inherits="Bolao.Aguarde" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
  
    <title></title>
  

    <style type="text/css">
        .auto-style1 {
            width: 613px;
            height: 617px;
            margin-right: 0px;
        }
    </style>
  

</head>

    

<body>
    <form id="form1" runat="server">
   <center> <div id="divCarregando" class="progresso">
    <h1> SORTEIO AINDA NÃO SAIU!!!</h1>
     </div>  <asp:Button ID="btnTentarAgain" runat="server" Text="Aguarde ou clique aqui para tentar novamente!!" Font-Bold="True" Font-Size="Large" OnClick="btnTentarAgain_Click" /> </center>
       





    </form>
  <center>  <p>
        <img alt="" class="auto-style1" src="imagens/tartarugas.jpg" /></p></center>
</body>
</html>
