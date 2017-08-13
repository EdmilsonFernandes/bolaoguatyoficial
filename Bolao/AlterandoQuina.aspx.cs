using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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
            if (!base.IsPostBack)
            {
                if ((HttpContext.Current.Session["ok"].ToString() == "True") && !HttpContext.Current.Session["ok"].Equals(null))
                {
                    loadJogosGridview();
                    this.carregaXMLGridview();
                }
                else
                {
                    this.Session["ok"] = false;
                    base.Response.Redirect("~/Default.aspx");
                }
            }


        }

        public void carregaXMLGridview()
        {
            int num2;
            DataSet set = new DataSet();
            set.ReadXml(base.Server.MapPath("quinaJogadores.xml"));
            if (set.Tables.Count > 0)
            {
                this.GridView2.DataSource = set;
                this.GridView2.DataBind();
            }
            for (int i = 0; i < set.Tables[1].Rows.Count; i = num2 + 1)
            {
                this.TextBox6.Text = set.Tables[1].Rows[i][0].ToString();
                this.TextBox4.Text = set.Tables[1].Rows[i][1].ToString();
                this.TextBox5.Text = set.Tables[1].Rows[i][2].ToString();
                num2 = i;
            }


        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void loadJogosGridview()
        {

            string jogosGuaty = "jogosGuaty.txt";
            string jogoAtual = "jogosByConfig.txt";


            DataTable table = new DataTable();
            table.Columns.Add("Jogos");
            table.Columns.Add("A");
            table.Columns.Add("B");
            table.Columns.Add("C");
            table.Columns.Add("D");
            table.Columns.Add("E");
          

            string pathJogosGuaty = Server.MapPath(Path.Combine("~", jogosGuaty));
            string pathJogosAtual = Server.MapPath(Path.Combine("~", jogoAtual));

            using (StreamReader oReader = new StreamReader(pathJogosGuaty))
            {
                string line = "";
                while ((line = oReader.ReadLine()) != null)
                {
                    string[] part = line.Split(',');

                    table.Rows.Add(part[0], part[1], part[2], part[3], part[4], part[5]);

                }

                GridView3.DataSource = table;
                GridView3.DataBind();


            }

            using (StreamReader oReader = new StreamReader(pathJogosAtual))
            {

                table.Clear();
                string line = "";
                while ((line = oReader.ReadLine()) != null)
                {
                    string[] part = line.Split(',');

                    table.Rows.Add(part[0], part[1], part[2], part[3], part[4], part[5]);

                }

                GridView4.DataSource = table;
                GridView4.DataBind();


            }






        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {


            GridView2.EditIndex = e.NewEditIndex;
            carregaXMLGridview();

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            string str = "quinaJogadores.xml";
            string str2 = base.Server.MapPath(System.IO.Path.Combine("~", str));
            base.Response.Clear();
            base.Response.ContentType = "Application/xml";
            base.Response.AddHeader("Content-Disposition", string.Format("Attachment; FileName={0}", str2));
            base.Response.TransmitFile(str);
            base.Response.End();
        }




        protected void btnUpload_Click(object sender, EventArgs e)
        {
            double num = 0.0;
            double num2 = 1000.0;
            string str = "0";
            string str2 = "";
            string path = "";
            if (this.fluploadXml.PostedFile.FileName != "")
            {
                string fileName = this.fluploadXml.PostedFile.FileName;
                num = Convert.ToDouble(this.fluploadXml.PostedFile.ContentLength) / 2048.0;
                str2 = fileName.Substring(fileName.Length - 4).ToLower();
                if (num > num2)
                {
                    this.lblMsg.Text = "Tamanho Máximo permitido \x00e9 de " + num2 + " kb!";
                    str = "1";
                }
                if (str2.Trim() != ".xml")
                {
                    this.lblMsg.Text = "Extensao invalida, é permitida apenas .xml";
                    str = "2";
                }
                path = base.Server.MapPath(Path.Combine("~", fileName));
                if (str == "0")
                {
                    try
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            this.fluploadXml.SaveAs(path);
                            this.lblMsg.Text = "Arquivo publicado com sucesso!!";
                        }
                        else
                        {
                            this.fluploadXml.SaveAs(path);
                            this.lblMsg.Text = "Arquivo publicado com sucesso!!";
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            else
            {
                this.lblMsg.Text = "Nao existe arquivo selecionado!!";
            }
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

        protected void imgFileGuaty_Click(object sender, ImageClickEventArgs e)
        {
            string jogosGuaty = "jogosGuaty.txt";
           
            string pathJogosGuaty = Server.MapPath(Path.Combine("~", jogosGuaty));

            // Prompts user to save file
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
          
            response.AddHeader("Content-Length", pathJogosGuaty.Length.ToString());
            response.ContentType = "text/plain";
            response.AppendHeader("Content-Disposition", "attachment; filename=" + jogosGuaty + ";");
            response.TransmitFile(pathJogosGuaty);
           
            response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();



        }

        protected void imgFileEdmilson_Click(object sender, ImageClickEventArgs e)
        {
            string jogos = "jogosByConfig.txt";

            string pathJogos = Server.MapPath(Path.Combine("~", jogos));

            // Prompts user to save file
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

            response.AddHeader("Content-Length", pathJogos.Length.ToString());
            response.ContentType = "text/plain";
            response.AppendHeader("Content-Disposition", "attachment; filename=" + jogos + ";");
            response.TransmitFile(pathJogos);
            
            response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}