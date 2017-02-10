using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bolao.Models;
using System.Reflection;

namespace Bolao
{
    public partial class resultadoDetalhado : System.Web.UI.Page
    {
        decimal SomaFesta = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["Initial"] != null || Session["Final"]  != null)
                {
                    LabelInitial.Text = Session["Initial"].ToString();
                    Label2Final.Text = Session["Final"].ToString();
                    geraEstatistica();
                }
                else
                {
                    Response.Redirect("Default.aspx");
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
    }
}