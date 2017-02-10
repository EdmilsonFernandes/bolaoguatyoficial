using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace Bolao
{
    public partial class AlterandoQuina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["ok"].ToString() == "True" && !System.Web.HttpContext.Current.Session["ok"].Equals(null))
                {
                    carregaXMLGridview();

                }
                else
                {
                    Session["ok"] = false;
                    Response.Redirect("~/Default.aspx");
                }

            }

        }

        public void carregaXMLGridview()
        {
            DataSet ds = new DataSet();

            ds.ReadXml(Server.MapPath(@"quinaJogadores.xml"));

            if (ds.Tables.Count > 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }


            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                TextBox6.Text = ds.Tables[1].Rows[i][0].ToString();
                TextBox5.Text = ds.Tables[1].Rows[i][1].ToString();
                TextBox4.Text = ds.Tables[1].Rows[i][2].ToString();
            }

           
         
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {


            GridView2.EditIndex = e.NewEditIndex;
            carregaXMLGridview();

        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            carregaXMLGridview();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = GridView2.Rows[e.RowIndex].DataItemIndex;

            string id = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox1")).Text;
            string nome = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox2")).Text;
            string mesRef = ((DropDownList)GridView2.Rows[e.RowIndex].FindControl("DropDownList2")).Text;
            string status = ((DropDownList)GridView2.Rows[e.RowIndex].FindControl("DropDownList1")).Text;

            GridView2.EditIndex = -1;

            carregaXMLGridview();

            DataSet dsUpdateXMLFile = (DataSet)GridView2.DataSource;

            dsUpdateXMLFile.Tables[0].Rows[index]["Id"] = id;
            dsUpdateXMLFile.Tables[0].Rows[index]["Nome"] = nome;


            dsUpdateXMLFile.Tables[0].Rows[index]["MesReferencia"] = mesRef;
            dsUpdateXMLFile.Tables[0].Rows[index]["StatusPG"] = status;
            dsUpdateXMLFile.WriteXml(Server.MapPath(@"quinaJogadores.xml"));
            carregaXMLGridview();



        }

        protected void btnClosed_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Redirect("~/Default.aspx");

        }


        protected void GridView2_RowDataBound1(object sender, GridViewRowEventArgs e)
        {


        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "")
            {

            }
        }

        protected void btnSaveConfg_Click(object sender, EventArgs e)
        {
            
            XElement configuracao = XElement.Load(Server.MapPath(@"quinaJogadores.xml"));

            IEnumerable<XElement> oConfig = 
                                from b in configuracao.Elements("configuracaoQuina")
                                select b;

            foreach (XElement item in oConfig)
            {

                item.SetAttributeValue("mesReference", TextBox6.Text);
                item.SetAttributeValue("inicioJogo", TextBox4.Text);
                item.SetAttributeValue("fimJogo", TextBox5.Text); 
                    
             }

            configuracao.Save(Server.MapPath(@"quinaJogadores.xml"));
         


        }
    }
}