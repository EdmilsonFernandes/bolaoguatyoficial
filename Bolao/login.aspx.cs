using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bolao
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text == "sjc01" && txtUser.Text == "quina")
            {
                Session["ok"] = true;
                Response.Redirect("AlterandoQuina.aspx");

            }
            else
            {
                Session["ok"] = false;
                Response.Write("<script>alert('Usuário ou senha inválida!!');</script>");
            }

        }
    }
}