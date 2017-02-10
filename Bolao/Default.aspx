<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bolao.Default" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.10.2.js"></script>
     
   
    <div>
                    <h2 class="titulo-quina titulo" style="height: 58px"> &nbsp;<span>Concurso Nº 
                        <asp:Label ID="lblNumeroConcurso" runat="server"></asp:Label>
                        (<asp:Label ID="lblDataConcurso" runat="server"></asp:Label>)</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<img alt="" src="imagens/download.png" style="width: 17px; height: 15px" />&nbsp;<asp:HyperLink ID="HyperLink1" NavigateUrl="~/login.aspx" runat="server" style="font-size: small !important; color: #FFFFFF" Font-Bold="True">Admin</asp:HyperLink>

                      
                
        </div>
    
  
    <div class='popup'>
    <div class='cnt223' runat="server" id="dvAlert">
        <asp:ImageButton ID="ImageButton1" ImageUrl="~/imagens/x.png" runat="server"
            class='x' />
 
              
                    <h3>Para quem ainda não pagou, favor pagar o bolão até quarta-feira dia 10/12/2015</h3>
      <center> <h4> <strong class="label-danger"><span style="color: #FFFFFF">Júlio favor este mês pagar dentro do prazo!</span> </strong></h4></center>

        <center><asp:Image runat="server" ImageAlign="Middle" ImageUrl="~/imagens/tumblr_n9su9yohF11qcjzvuo1_500.gif" Width="150px" Height="100px" /></center>
 
        </div>
</div>
    <table>
        <tr>
        <td style="" >
            <fieldset class="mega-sena"><legend class="titulo-quina"><asp:Label runat="server" Text="Resultado da Quina" Font-Bold="True" Font-Size="Large" ForeColor="White"></asp:Label></legend>
            <div class="resultado-loteria" style="width: 504px">
              
                 

                    <strong class="canais-atendimento">
                        <asp:Label ID="lblAcumulouSimOuNao" runat="server" Font-Size="Small"></asp:Label></strong>
                    <br />

                    <ul class="numbers quina">
                        <li>
                            <asp:Label ID="lblNumberOne" runat="server">1</asp:Label></li>
                        <li>
                            <asp:Label ID="lblNumberTwo" runat="server">2</asp:Label></li>
                        <li>
                            <asp:Label ID="lblNumberTree" runat="server">3</asp:Label></li>
                        <li>
                            <asp:Label ID="lblNumberFor" runat="server">4</asp:Label></li>
                        <li>
                            <asp:Label ID="lblNumberFive" runat="server">5</asp:Label>
                    </ul>



                        <span style="font-weight: bold">Estimativa de prêmio do próximo concurso:</span> <span style="font-size: small">R$<asp:Label ID="lblEstimativaValorProximoSorteio" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label></span>
						

                        <strong><span style="font-size: small">R$</span></strong>
                         (<asp:Label ID="lblDataProximoConcurso" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>)


                        <span style="font-weight: bold">
                    <br />
                    Acumulado próximo concurso: </span><span class=""><strong><span style="font-size: small">R$ </span></strong>
                            <asp:Label ID="lblAcumladoProximoConcurso" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label></span>
               
                      <br />
                        <strong><asp:Label ID="lblSorteiRealizadoEm" runat="server"></asp:Label></strong>
                     <br />

                <div id="dvGanhadores" runat="server">         <h4 class="epsilon">Ganhadores por Região</h4></div>
                        <strong><asp:Label ID="lbGanhador" runat="server" style="font-size: x-small !important"></asp:Label></strong>
                    
            </div>
            </fieldset>
            
        </td>
            <td style="height: 237px" >

                 <div runat="server" id="dvScrool" style="width:725px;height:300px; overflow-y:scroll;">

                 
                    <asp:GridView ID="GridView1" CssClass="table table-hover table-striped" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" style="margin-left: 0px" Font-Size="Small" Width="698px" >
                        <Columns>
                            <asp:BoundField DataField="numeroSorteio" HeaderText="Conferindo  &gt;= 1 acerto(s)">
                                <ControlStyle Font-Bold="True" />
                                <HeaderStyle Font-Bold="True" Font-Size="Medium" />
                                <ItemStyle CssClass=" " Font-Bold="True" ForeColor="#000099" Font-Size="Small"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="numerosAcertados" HeaderText="Nº acertados">
                            <HeaderStyle Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Bold="True" Font-Size="Small" ForeColor="#006600" Height="3px" Width="3px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="quantidadeAcerto" HeaderText="Qtd Acerto(s) ">
                                 <HeaderStyle Font-Bold="True" Font-Size="Medium" />
                            <ItemStyle Font-Bold="True" Font-Size="Small" ForeColor="#006600" Height="3px" Width="3px" />
                                   </asp:BoundField>
                            <asp:BoundField DataField="acertoTipoJogo" HeaderText="Tipo de acerto">
                           <ControlStyle Font-Bold="True" />
                                <HeaderStyle Font-Bold="True" Font-Size="Medium" />
                                <ItemStyle CssClass=" " Font-Bold="True" ForeColor="#000099" Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorGanho" HeaderText="Ganhamos (R$)">
                                <ControlStyle Font-Bold="True" />
                                <HeaderStyle Font-Bold="True" Font-Size="Medium" />
                                <ItemStyle CssClass=" " Font-Bold="True" ForeColor="#000099" Font-Size="Small" />
                              </asp:BoundField>

                        </Columns>
                    </asp:GridView>
                     <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDetalhado" runat="server" Font-Bold="True" Font-Size="Medium" PostBackUrl="~/resultadoDetalhado.aspx"  OnClick="lnkDetalhado_Click">Resultado detalhado (Clique Aqui)</asp:LinkButton>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

            </td>

            </tr>
        <tr>
            <td class="well" rowspan="2" style="text-align:left;vertical-align:top;padding:0">
                <fieldset><legend class="titulo-quina" style="width: 98%"><span style="color: #FFFFFF"><span style="font-size: large"><strong>&nbsp;Premiação: </strong></span>  </legend>
                <table>
            
                      
       

        <tr style="font-size: small">
            <th class="active">Acerto</th>
            <th style="width: 134px" class="active">Ganhadores</th>
            <th class="active">Valor</th>
        </tr>
        <tr>

            <td style="width: 135px; font-size: small; height: 47px;"><strong>QUINA: </strong></td>
            <td style="width: 134px; height: 47px;"><strong>
                <asp:Label ID="lblQuinaGanhadores" runat="server" Style="font-size: small !important"></asp:Label></strong></td>
            <td style="height: 47px"><strong><span style="font-size: small">R$</span>
                <asp:Label ID="lblQuina" runat="server" Style="font-size: small !important"></asp:Label></strong></td>

        </tr>
        <tr>
            <td style="width: 135px; font-size: small !important;"><strong>QUADRA: </strong></td>
            <td style="width: 134px"><strong>
                <asp:Label ID="lblGanhadoresQuadra" runat="server" Style="font-size: small !important"></asp:Label></strong></td>
            <td><strong><span style="font-size: small">R$</span>
                <asp:Label ID="lblQuadra" runat="server" Style="font-size: small !important"></asp:Label></strong></td>
        </tr>
        <tr>
            <td style="width: 135px; font-size: small !important;"><strong>TERNO: </strong></td>
            <td style="width: 134px"><strong>
                <asp:Label ID="lblTernoGanhadores" runat="server" Style="font-size: small !important"></asp:Label></strong></td>
            <td><strong><span style="font-size: small">R$</span>
                <asp:Label ID="lblTerno" runat="server" Style="font-size: small !important"></asp:Label></strong></td>
        </tr>
              
          <tr>
            <td style="width: 135px; font-size: small !important;"><strong>DUQUE: </strong></td>
            <td style="width: 134px"><strong>
                <asp:Label ID="lblGanhadoresDuque" runat="server" Style="font-size: small !important"></asp:Label></strong></td>
            <td><strong><span style="font-size: small">R$</span>
                <asp:Label ID="lblDuqueValor" runat="server" Style="font-size: small !important"></asp:Label></strong></td>
        </tr>
                              
       
                        
</table>
              
                    </span>
              
         </fieldset>   


            </td>
                  
               
             <td class="well" rowspan="2" style="text-align:left;vertical-align:top;padding:0" >

             
                 <fieldset><legend class="titulo-quina" style="width: 98%"><span style="color: #FFFFFF"><strong><span style="font-size: large">Jogos válidos de:</span> <span style="font-size: large">Nº </span></strong></span><strong>
                     <asp:Label runat="server" ID="lblEndNumber" Text="4079" ForeColor="White" style="font-size: large"></asp:Label>   </strong>   <span style="color: #FFFFFF">
                         <strong><span style="font-size: large">&nbsp;até</span></strong></span><span style="font-size: large"> </span><strong> <span style="color: #FFFFFF"><span style="font-size: large">Nº </span></span> 
                          <asp:Label runat="server" ID="lblStartNumber" Text="4056" ForeColor="White" style="font-size: large"></asp:Label>      &nbsp; <asp:Label runat="server" ID="Label6" Text="- Referência (Abril/2016)" ForeColor="White" style="font-size: large"></asp:Label>   </strong>   </legend>
            
                 <asp:GridView ID="GridView2" CssClass="table table-striped table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" Font-Bold="True" Font-Size="Small" OnRowDataBound="GridView2_RowDataBound">
                     <Columns>
                         <asp:BoundField DataField="Id" HeaderText="Qtd" >
                         <ItemStyle Height="3px" Width="3px" />
                         </asp:BoundField>
                         <asp:BoundField DataField="Nome" HeaderText="Nome" >
                           
                        
                        
                         <ItemStyle Height="15px" Width="15px" />
                         </asp:BoundField>
                           
                        
                        
                         <asp:BoundField DataField="MesReferencia" HeaderText="Mês" >
                      
                         <ItemStyle Height="6px" Width="6px" />
                      
                         </asp:BoundField>
                         <asp:TemplateField HeaderText="Pagou ?">
                             <EditItemTemplate>
                                 <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StatusPG") %>'></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label4" runat="server" Text='<%# Bind("StatusPG") %>'></asp:Label>
                             </ItemTemplate>
                             <ItemStyle Height="1px" Width="1px" ForeColor="White" />
                         </asp:TemplateField>
                     </Columns>
                 </asp:GridView>
                    
                     </span></span>
                    
                 </fieldset>
            </td>
        </tr>
    </@table>

    


    
    </table>
</asp:Content>
