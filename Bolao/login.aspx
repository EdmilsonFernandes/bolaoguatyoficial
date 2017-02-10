<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Bolao.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
   <table class="form well">
       <tr><td>

           <div class="container" style="width: 291px">
       
           <h2 class="form-signin-heading" contenteditable="false">Por favor, forneça:</h2>

          <asp:TextBox ID="txtUser" CssClass="form-control" placeholder="Usuário" required="" autofocus="" contenteditable="false"   runat="server"></asp:TextBox>
          <asp:TextBox ID="txtSenha"   CssClass="form-control" placeholder="Digite sua senha aqui" required="" autofocus="" contenteditable="false"   runat="server" TextMode="Password"></asp:TextBox>
               <asp:Button ID="btnSubmit"  CssClass="btn btn-lg btn-primary btn-block" runat="server" Text="Entrar" OnClick="btnSubmit_Click" />
     <center> 
         <h5><strong> <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server">Página Principal</asp:HyperLink></strong></h5>
               </center>   
</div>
           </td></tr>
       

   </table>
    
</asp:Content>
