<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlterandoQuina.aspx.cs" Inherits="Bolao.AlterandoQuina" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />

    <style>
    fieldset {
    font-family: sans-serif;
    border: 5px solid #1F497D;
    background: #ddd;
    border-radius: 5px;
    padding: 15px;
}

fieldset legend {
    background: #1F497D;
    color: #fff;
    padding: 5px 10px ;
    font-size: 18px;
    border-radius: 5px;
    box-shadow: 0 0 0 5px #ddd;
    margin-left: 20px;
}
        .auto-style1 {
            padding: 9px;
            border-radius: 3px;
            width: 97%;
        }
    .auto-style2 {
        color: #fff;
        background-color: #428bca;
        border-color: #357ebd;
        font-size: small;
    }
    .auto-style3 {
        background-color: #5bc0de;
        font-size: small;
    }
        .auto-style4 {
            width: 703px;
        }
        .auto-style5 {
            color: #FF0000;
        }
    </style>

    <fieldset>
        <legend class="titulo-quina">
            <asp:Label runat="server" Text="Lista e status de Jogadores" ForeColor="white"></asp:Label>
        </legend>

        <asp:GridView ID="GridView2" CssClass="table table-striped table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" Font-Bold="True" Font-Size="Small" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound1">
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
                            <asp:ListItem>Novembro</asp:ListItem>
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


    <fieldset>
        <legend class="auto-style1">

           Administração de pagamentos - Bolão!          
        </legend>
         <Table style="height:77px; background-color: #C0C0C0;" >
             <tr>
                 <td class="auto-style4">
                     <strong><span style="font-size: small">Faça download o xml para atualizar&nbsp; </span></strong>
                     <asp:ImageButton ID="ImageButton1" runat="server" Height="47px" ImageUrl="~/imagens/download.jpg" OnClick="ImageButton1_Click" Width="47px" />
        
                 
                   
                 </td>
                
             </tr>
             <tr>
                  <td class="auto-style4">
                           <strong>
                           <asp:Label runat="server" Text="Atualizar XML - Pagamentos" ForeColor="black" style="font-size: small !important"></asp:Label>
                            </strong>
                            <asp:FileUpload ID="fluploadXml" runat="server" Width="664px" />
                           
                               <td> <asp:Button ID="btnUpload" runat="server" CssClass="auto-style2" Height="36px" OnClick="btnUpload_Click" Text="Enviar" Width="122px" /></td>
                          
                           <br />
                           <br />
                         
                           <asp:Label ID="lblMsg" runat="server" CssClass="auto-style3"></asp:Label>
                 </td>
             </tr>

         </Table>
    </fieldset>

    <fieldset>
        <legend>

            Atualizar referência de validade dos jogos
        </legend>
    <table id="tbConfg" runat="server" class="table table-striped table-bordered table-condensed table-hover" border="1">
        <tr>
            <td colspan="2"><strong>
                <asp:Label ID="lblSetupQuina" runat="server" Text="Configuração de Mensagem Quina!" CssClass="label-danger" Style="font-size: medium !important"></asp:Label>
            </strong></td>
        </tr>
        <tr>
            <td style="width: 195px">
                <strong>
                    <asp:Label ID="lblMesReference" runat="server" Text="Mês de referência:" Style="font-size: medium !important"></asp:Label>
                </strong>
            </td>
            <td>
                <strong>
                    <asp:TextBox ID="TextBox6" runat="server" Style="font-weight: bold; font-size: medium"></asp:TextBox>
                </strong>
            </td>
        </tr>
        <tr>
            <td style="width: 195px">
                <strong>
                    <asp:Label ID="lblInicio" runat="server" Text="Inicio do Jogo (Número):" Style="font-size: medium !important"></asp:Label>
                </strong>
            </td>
            <td>
                <strong>
                    <asp:TextBox ID="TextBox4" runat="server" Style="font-weight: bold; font-size: medium"></asp:TextBox>
                </strong>
            </td>
        </tr>

        <tr>
            <td style="width: 195px">
                <strong>
                    <asp:Label ID="lblFimJogo" runat="server" Text="Fim do Jogo (Número):" Style="font-size: medium !important"></asp:Label>
                </strong>
            </td>
            <td>
                <strong>
                    <asp:TextBox ID="TextBox5" runat="server" Style="font-weight: bold; font-size: medium"></asp:TextBox>
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

        <fieldset>
            <legend class="titulo-quina">
                <asp:Label runat="server" Text="Lista e status de Jogadores" ForeColor="white"></asp:Label>
            </legend>

            <table class="dijitTabContainerNoLayout">
                
                <tr>
                    <th>Jogos 1 - Guaty</th>
                    <th>Jogos 2 - Edmilson</th>
                </tr>
                <tr>
                    
                    <td>
                        
                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-striped table-bordered table-condensed table-hover">
                        </asp:GridView>
                        <br />
                        <asp:ImageButton ID="imgFileGuaty" runat="server" Height="27px" ImageUrl="~/imagens/txtFile.png" OnClick="imgFileGuaty_Click" Width="29px" />
                    &nbsp;
                    </td>
                   
                    <td>

                        <asp:GridView ID="GridView4" runat="server" CssClass="table table-striped table-bordered table-condensed table-hover">
                        </asp:GridView>
                         <br />
                        <asp:ImageButton ID="imgFileEdmilson" runat="server" Height="27px" ImageUrl="~/imagens/txtFile.png" Width="29px" OnClick="imgFileEdmilson_Click" />
                    </td>
                </tr>
            </table>
            <span class="auto-style5"><strong>*Este jogos pode ser modificados, fazendo download clicando no icone TXT e para jogar no servidor você usar upload (Atualizar xml...)</strong></span>

        </fieldset>

    
     <center> <asp:Button ID="btnClosed" CssClass="btn btn-lg btn-primary btn-block" runat="server" Text="Sair" OnClick="btnClosed_Click"></asp:Button>
         
               </center>
        </fieldset>
</asp:Content>
