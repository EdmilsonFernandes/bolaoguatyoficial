    <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlterandoQuina.aspx.cs" Inherits="Bolao.AlterandoQuina" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
   
                 <fieldset><legend class="titulo-quina"><asp:Label runat="server" Text="Jogadores" ForeColor="white"></asp:Label> 
      </legend>
         
                 <asp:GridView ID="GridView2" CssClass="table table-striped table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" Font-Bold="True" Font-Size="Small" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound1" >
                     <Columns>
                         <asp:TemplateField HeaderText="Código">
                             <EditItemTemplate>
                                 <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("Id") %>'></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Nome">
                             <EditItemTemplate>
                                 <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("Nome") %>'></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label2" runat="server" Text='<%# Eval("Nome") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Mês de Referência">
                             <EditItemTemplate>
                                 <asp:DropDownList ID="DropDownList2" runat="server">
                                     <asp:ListItem>Janeiro</asp:ListItem>
                                     <asp:ListItem>Fevereiro</asp:ListItem>
                                     <asp:ListItem>Março</asp:ListItem>
                                     <asp:ListItem>Abril</asp:ListItem>
                                     <asp:ListItem>Maio</asp:ListItem>
                                     <asp:ListItem>Junho</asp:ListItem>
                                     <asp:ListItem>Julho</asp:ListItem>
                                     <asp:ListItem>Agosto</asp:ListItem>
                                     <asp:ListItem>Setembro</asp:ListItem>
                                     <asp:ListItem>Outubro</asp:ListItem>
                                     <asp:ListItem >Novembro</asp:ListItem>
                                     <asp:ListItem Selected="True">Dezembro</asp:ListItem>
                                 </asp:DropDownList>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label3" runat="server" Text='<%# Eval("MesReferencia") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Pagou ?">
                             <EditItemTemplate>
                                 <asp:DropDownList ID="DropDownList1" runat="server">
                                     <asp:ListItem>SIM</asp:ListItem>
                                     <asp:ListItem>NÃO</asp:ListItem>
                                 </asp:DropDownList>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label4" runat="server" Text='<%# Eval("StatusPG") %>'></asp:Label>
                             </ItemTemplate>
                             <ControlStyle ForeColor="Blue" />
                             <ItemStyle Font-Bold="True" />
                         </asp:TemplateField>
                         <asp:CommandField DeleteText="Excluir" EditText="Editar" InsertText="Cadastrar" ShowEditButton="True" UpdateText="Salvar" />
                     </Columns>
                 </asp:GridView>
                  
                 </fieldset>
       
     
   


        <hr />
        <table id="tbConfg" runat="server" class="table table-striped table-bordered table-condensed table-hover" border="1">
            <tr>
                <td colspan="2"><strong>
                    <asp:Label ID="lblSetupQuina" runat="server" Text="Configuração de Mensagem Quina!" CssClass="label-danger" style="font-size: medium !important"></asp:Label>
                    </strong></td>
            </tr>
            <tr>
                <td style="width: 195px">
                    <strong>
                    <asp:Label ID="lblMesReference" runat="server" Text="Mês de referência:" style="font-size: medium !important"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                    <asp:TextBox ID="TextBox6" runat="server" style="font-weight: bold; font-size: medium"></asp:TextBox>
                    </strong>
                </td>
            </tr>
            <tr>
                <td style="width: 195px">
                    <strong>
                    <asp:Label ID="lblInicio" runat="server" Text="Inicio do Jogo (Número):" style="font-size: medium !important"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                    <asp:TextBox ID="TextBox4" runat="server" style="font-weight: bold; font-size: medium"></asp:TextBox>
                    </strong>
                </td>
            </tr>
            <tr>
                <td style="width: 195px">
                    <strong>
                    <asp:Label ID="lblFimJogo" runat="server" Text="Fim do Jogo (Número):" style="font-size: medium !important"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                    <asp:TextBox ID="TextBox5" runat="server" style="font-weight: bold; font-size: medium"></asp:TextBox>
                    </strong>
                </td>
            </tr>
            <tr>
                <td style="width: 195px">&nbsp;</td>
                <td>
                    <asp:Button ID="btnSaveConfg" runat="server" Text="Salvar dados" CssClass="btn active" OnClick="btnSaveConfg_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 195px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
          
            <tr>
                <td style="width: 195px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 195px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>



    </div>
     <center> <asp:Button ID="btnClosed" CssClass="btn btn-lg btn-primary btn-block" runat="server" Text="Sair" OnClick="btnClosed_Click"></asp:Button>
         
               </center> 
</asp:Content>
