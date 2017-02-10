using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bolao.DAO
{
    public class BolaoUTIL:DAO.AcessoDAO
    {

        public List<string> buscaJogos()
        {

            List<string> buscaJogosNumero = new List<string>();

            string clausulaSQL = "SP_BUSCA_EXTRATO";
            try
            {
                SqlCommand comandoSql = new SqlCommand(clausulaSQL);
                DataTable tbExtato = ExecutaQuery(comandoSql);

                return populaExtrato(tbExtato);

            }
            catch (Exception)
            {

                throw;
            }
        }


        public int validaInsert(int numero)
        {

            List<string> buscaJogosNumero = new List<string>();

            string clausulaSQL = "SP_VALIDA_INSERCAO";
            try
            {
                SqlCommand comandoSql = new SqlCommand(clausulaSQL);

                comandoSql.Parameters.AddWithValue("@CONCURSO", numero);
               

                DataTable tbExtato = ExecutaQuery(comandoSql);

                return populaExtratoValida(tbExtato);

            }
            catch (Exception)
            {

                throw;
            }
        }



        private int populaExtratoValida(DataTable dt)
        {
            int valida = 0;


            foreach (DataRow itemOcorrencia in dt.Rows)
            {

                valida = Convert.ToInt32(itemOcorrencia["RESULTADO"]);
          
            }

            return valida;
        }

        private List<string> populaExtrato(DataTable dt)
        {
            List<string> listaExtrato = new List<string>();


            foreach (DataRow itemOcorrencia in dt.Rows)
            {
                
                listaExtrato.Add(itemOcorrencia["JOGOS"].ToString());
            
              
            }

            return listaExtrato;
        }


        public void cadastrarJogos(string jogos, string tipAcerto, int quantidades, decimal valor, int refinitial, int reffinal, string numero)
        {
            string clausulaSQL = "SP_INSERT_BOLAOGUATY";

            try
            {

                SqlCommand comandoSql = new SqlCommand(clausulaSQL);
                comandoSql.Parameters.AddWithValue("@JOGOS", jogos );
                comandoSql.Parameters.AddWithValue("@TIPO_ACERTO", tipAcerto);
                comandoSql.Parameters.AddWithValue("@CONCURSO", quantidades);
                comandoSql.Parameters.AddWithValue("@VALOR", valor);
                comandoSql.Parameters.AddWithValue("@REFINITIAL", refinitial);
                comandoSql.Parameters.AddWithValue("@REFFINAL", reffinal);
                comandoSql.Parameters.AddWithValue("@NUMERO", numero);


                ExecutaComando(comandoSql);

            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}