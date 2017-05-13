using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bolao.Models;
using Bolao.DAO;
using System.Reflection;
using System.Data;


namespace Bolao
{
    public partial class resultadoDetalhado : System.Web.UI.Page
    {
        decimal SomaFesta = 0;
        string final = "";
        string inicial = "";
        
   
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if ((Session["Initial"] != null) || (Session["Final"] !=null))
                {
                    this.LabelInitial.Text = this.Session["Initial"].ToString();
                    this.Label2Final.Text = this.Session["Final"].ToString();
                    this.preencheDropdownlist();
                    this.buscaJogosPorIntervalos();
                    this.geraEstatistica();
                }
                else
                {
                    base.Response.Redirect("Default.aspx");
                }
            }

        }

        protected void grdGeral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var TotalFesta = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "VALOR"));
               
                SomaFesta += TotalFesta;
            }

            lblValorTotal.Text = "R$ " + SomaFesta;
           
        }



        public void geraEstatistica()
        {

            List<string> recebeTodosNumeros = new List<string>();
            int numerosDaQuina = 80;

            for (int i = 1; i <= numerosDaQuina; i++)
            {
                if(i<10)
                {
                    recebeTodosNumeros.Add(i.ToString().PadLeft(2, '0'));
                }
                else
                { 
                recebeTodosNumeros.Add(i.ToString());
                }
            }




            List<string> listInfoNumberDB = new DAO.BolaoUTIL().buscaJogos();
            string[] separaNumero = null;
            List<string> listSemTraco = new List<string>();

            foreach (string item in listInfoNumberDB)
            {
                separaNumero = item.Split('-');

                for (int i = 0; i < separaNumero.Length; i++)
                {
                    listSemTraco.Add(separaNumero[i]);
                }

            }
            ListaEstaticaBolao oListEstatica = new ListaEstaticaBolao();


            List<String> numerosQueNaoSairam = new List<string>();

         
            //foreach (var item in recebeTodosNumeros)
            //{

            //    if(listSemTraco.Contains(item))
            //    {

            //    }
            //    else
            //    {
            //        EstaticaBolao oEstaticaQueNaoSaiu = new EstaticaBolao();

            //        oEstaticaQueNaoSaiu.numero = Convert.ToInt32(item);
            //        oEstaticaQueNaoSaiu.qtd = 0;
            //        oListEstatica.Add(oEstaticaQueNaoSaiu);
            //    }
 
            //}


            foreach (var groupNumber in listSemTraco.GroupBy(i => i).OrderByDescending(i => i.Count()))
            {
                EstaticaBolao oEstatistica = new EstaticaBolao();
                if(Convert.ToInt32(groupNumber.Key) <10)
                { 
                    oEstatistica.numero = Convert.ToInt32(groupNumber.Key.PadLeft(2,'0'));
                }
                else
                {
                    oEstatistica.numero = Convert.ToInt32(groupNumber.Key);

                }
                oEstatistica.qtd = groupNumber.Count();
                oListEstatica.Add(oEstatistica);


            }


            
            lvDetail.DataSource = oListEstatica.OrderByDescending(i =>i.qtd);
            lvDetail.DataBind();


            oListEstatica.Clear();

            foreach (var item in recebeTodosNumeros)
            {

                if (listSemTraco.Contains(item))
                {

                }
                else
                {
                    EstaticaBolao oEstaticaQueNaoSaiu = new EstaticaBolao();

                    if(Convert.ToInt32(item) < 10)
                    { 
                      oEstaticaQueNaoSaiu.numero = Convert.ToInt32(item.PadLeft(2,'0'));
                    }
                    else
                    {
                        oEstaticaQueNaoSaiu.numero = Convert.ToInt32(item);
                    }
                    oEstaticaQueNaoSaiu.qtd = 0;
                    oListEstatica.Add(oEstaticaQueNaoSaiu);
                }

            }


            lstNaoSaiuAinda.DataSource = oListEstatica.OrderByDescending(i => i.qtd);
            lstNaoSaiuAinda.DataBind();


        }

        

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }


        public void buscaJogosPorIntervalos()
        {
            string[] strArray = null;
            char[] separator = new char[] { '-' };
            strArray = this.dpdIntervalos.SelectedItem.Text.Split(separator);
            this.inicial = strArray[0];
            this.final = strArray[1];
            this.LabelInitial.Text = this.inicial;
            this.Label2Final.Text = this.final;
            this.preencheGridView(Convert.ToInt32(this.inicial), Convert.ToInt32(this.final));
        }

        protected void dpdIntervalos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buscaJogosPorIntervalos();
        }
        protected void lvDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        public void preencheDropdownlist()
        {
            BolaoUTIL oBuscaIntervalos = new BolaoUTIL();
            ListaIntervalo intervalo = new ListaIntervalo();
            List<string> list = new List<string>();
            intervalo =  oBuscaIntervalos.buscaIntervalos();
            this.dpdIntervalos.DataSource = intervalo;
            this.dpdIntervalos.DataTextField = "display";
            this.dpdIntervalos.DataValueField = "value";
            this.dpdIntervalos.DataBind();
        }

        public void preencheGridView(int inicial, int final)
        {
             BolaoUTIL oBuscaIntervalos = new BolaoUTIL();
            DataSet set = new DataSet();
            set = oBuscaIntervalos.buscaIntervalosGrid(inicial, final);
            this.grdGeral.DataSource = set.Tables[0];
            this.grdGeral.DataBind();
        }









    }
}