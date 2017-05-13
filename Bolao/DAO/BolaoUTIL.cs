using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Bolao.Models;
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

        public ListaIntervalo buscaIntervalos()
        {
            ListaIntervalo intervalo;
            List<string> list = new List<string>();
            string cmdText = "select distinct  concat(referencia_inicial ,' - ', referencia_final) as display ,  concat(referencia_inicial ,' - ', referencia_final) as value from extrato_bolao order by display desc";
            try
            {
                SqlCommand cmd = new SqlCommand(cmdText);
                DataTable dt = base.ExecutaSelect(cmd);
                intervalo = populaDropdownIntervalosJogos(dt);
            }
            catch (Exception)
            {
                throw;
            }
            return intervalo;
        }



        public DataSet buscaIntervalosGrid(int inicial, int final)
        {
            DataSet set2;
            object[] objArray1 = new object[] { "SELECT [JOGOS], [TIPO_ACERTO], [VALOR], [QUANTIDADES], [NUMERO_SORTEIO] FROM [EXTRATO_BOLAO] WHERE [REFERENCIA_INICIAL] =", inicial, " AND REFERENCIA_FINAL =", final, " AND VALOR <> 0" };
            string cmdText = string.Concat(objArray1);
            try
            {
                SqlCommand cmd = new SqlCommand(cmdText);
                set2 = base.ExecutaProcQuerySelect(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return set2;
        }




       

        public List<string> buscaJogosEstatistica(int inicial, int final)
        {
            List<string> list2;
            List<string> list = new List<string>();
            string cmdText = "SP_BUSCA_EXTRATO_ESTATISTICA";
            try
            {
                SqlCommand cmd = new SqlCommand(cmdText);
                cmd.Parameters.AddWithValue("@INITIAL", inicial);
                cmd.Parameters.AddWithValue("@FINAL", final);
                DataTable dt = base.ExecutaQuery(cmd);
                list2 = this.populaExtrato(dt);
            }
            catch (Exception)
            {
                throw;
            }
            return list2;
        }


        private ListaIntervalo populaDropdownIntervalosJogos(DataTable dt)
        {
            List<string> list = new List<string>();
            ListaIntervalo intervalo = new ListaIntervalo();
            foreach (DataRow row in dt.Rows)
            {
                IntervaloBolao item = new IntervaloBolao
                {
                    display = row["display"].ToString(),
                    value = row["value"].ToString()
                };
                intervalo.Add(item);
            }
            return intervalo;
        }









    }
}