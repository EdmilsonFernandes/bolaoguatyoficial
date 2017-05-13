<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resultadoDetalhado.aspx.cs" Inherits="Bolao.resultadoDetalhado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.10.2.js"></script>

    <title></title>

    <style type="text/css">
        .auto-style1 {
            font-size: medium;
        }

        .auto-style2 {
            font-size: x-large;
        }

        tr.myitem td {
            width: auto;
            height: auto;
           
            background:#c332ae;
            color:white;
        }
        td{
            
            color:blue;
            font-size:larger;
            vertical-align:top;
        }
       
        table {
            border: 1px solid black;
            width: auto;
            height: auto;
        }
        </style>

</head>


<body>
    <form id="form1" runat="server">
      
        <center>   <div>
                    <h2 class="titulo-quina alert-success" style="height: 58px"> &nbsp;<span>Resultado detalhado - Bolão da Quina ! </span></h2>
                    <strong><span class="auto-style1">Escolha intervalo de jogos: </span></strong>&nbsp;<span><strong><asp:DropDownList  ID="dpdIntervalos" runat="server" CssClass="dropdown-toggle btn-success" Height="40px" style="font-size: medium; font-weight: bold;" Width="176px" AutoPostBack="True"  DataTextField="display"   DataValueField="value" OnSelectedIndexChanged="dpdIntervalos_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                  
                    <br />
                    </strong> </span><strong><span class="auto-style1">Tabela do intervalo escolhido: - Inicial:  <asp:Label ID="LabelInitial" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Large"></asp:Label></span></strong> &nbsp;<strong><span class="auto-style1">Até </span></strong>
                           
                                 <span class="auto-style1"> - <strong> Final: </strong></span><strong>  <asp:Label ID="Label2Final" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Large"></asp:Label> </strong>
                      
           
            <asp:GridView ID="grdGeral" CssClass="GridView" runat="server" AutoGenerateColumns="False" Font-Bold="True" Height="191px" OnRowDataBound="grdGeral_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="JOGOS" HeaderText="NÚMEROS ACERTADOS" SortExpression="JOGOS" >
                    <FooterStyle />
                    </asp:BoundField>
                    <asp:BoundField DataField="TIPO_ACERTO" HeaderText="TIPO DE ACERTO" SortExpression="TIPO_ACERTO" />
                
                    <asp:BoundField DataField="QUANTIDADES" HeaderText="NÚMERO DO SORTEIO DA QUINA" SortExpression="QUANTIDADES" />
                     <asp:BoundField DataField="VALOR" HeaderText="VALOR" SortExpression="VALOR" />
                    <asp:BoundField DataField="NUMERO_SORTEIO" HeaderText="JOGOS SORTEADOS" SortExpression="NUMERO_SORTEIO" />
                       
                </Columns>
            </asp:GridView>
                    
    
                  <br />
                    <strong><span class="auto-style2">Valor total já ganho neste intervalo -></span></strong><span class="auto-style2">: </span><strong>  <asp:Label ID="lblValorTotal" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Italic="True" ForeColor="#006600" Font-Size="X-Large" ></asp:Label>
         
                    <br />
                    <br />
         
                    </strong>
         
        </div>
   <div style="align-content:center;align-items:center;"><strong><asp:Button ID="btnVoltar" runat="server" Text="Voltar " CssClass="btn-info active" Width="86px" OnClick="btnVoltar_Click" style="font-weight: bold; font-size: medium" /></strong></div> 

        </center>
<center>
        <div>
            
            <h3>Estatistica do Bolão - Números que sairam mais vezes </h3>
            <hr />
            <table>
                <tr>
                    <td>
            <asp:ListView ID="lvDetail" runat="server" OnSelectedIndexChanged="lvDetail_SelectedIndexChanged">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("numero")%></td>
                        <td ><%# Eval("qtd")%></td>

                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table id="tbl1" runat="server">
                        <tr id="tr1" class="myitem" runat="server">
                            <td id="td1" runat="server"><strong>Nº</strong></td>
                            <td id="td2" runat="server"><strong>Qtds X que saiu</strong></td>

                        </tr>
                        <tr id="ItemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>

                    </td>
                    <td> <asp:ListView ID="lstNaoSaiuAinda" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("numero")%></td>
                        <td><%# Eval("qtd")%></td>

                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table id="tbl1" runat="server">
                        <tr id="tr1" class="myitem" runat="server">
                            <td id="td1" runat="server"><strong>Nº (Nunca saiu)</strong></td>
                            <td id="td2" runat="server"><strong>Qtds</strong></td>

                        </tr>
                        <tr id="ItemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView></td>
                </tr>
            </table>

            <br />

        </div>
    </center>
    </form>
</body>
</html>
