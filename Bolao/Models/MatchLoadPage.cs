using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Text;

namespace Bolao.Models
{
    
    public class MatchLoadPage
    {




        public static StringBuilder PreencheModelo(String path)
        {
            string data = "";
           
            StringBuilder templateFile = new StringBuilder();

            templateFile.Append(File.ReadAllText(path));

            templateFile = templateFile.Replace("\r", "").Replace("\n", "").Replace("\t", "");

            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            string pageUrl = "http://loterias.caixa.gov.br/wps/portal/loterias/landing/quina/";
            Loteria oLoteria = new Loteria();
            Sorteio oSorteio = new Sorteio();
            Ganhadores oGanhadores = new Ganhadores();
            
            oSorteio.Numeros = new List<int>();
            oLoteria.Sorteios  = new List<Sorteio>();
            
            
            CookieContainer oCookieSecurity = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pageUrl);
            request.Method = WebRequestMethods.Http.Post;
            request.MaximumAutomaticRedirections = 2000;
            request.CookieContainer = oCookieSecurity;
            request.KeepAlive = false;
            request.ContentLength = 0;
            HttpWebResponse res = (HttpWebResponse)request.GetResponse();

            if (res.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = res.GetResponseStream();
                StreamReader readStream = null;

                if (res.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, System.Text.Encoding.GetEncoding(res.CharacterSet));
                }

                data = readStream.ReadToEnd();

                res.Close();
                readStream.Close();
            }


            HtmlAgilityPack.HtmlDocument docs = new HtmlAgilityPack.HtmlDocument();
            docs.LoadHtml(data);

          

            //Preencher os numeros da quina
            for (int i = 1; i <= 5; i++)
            {
                foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[2]/div/div/ul/li["+i+"]"))
                {

                    string positionNumber = "#" + i;
                    templateFile.Replace(positionNumber, row.InnerText);
                  
                }
            }
                
            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[1]/div/h2/span"))
            {
                string[] concurso = row.InnerText.Split(' ');
                templateFile.Replace("#NumeroConcurso", concurso[1]);
                templateFile.Replace("#DataOri", concurso[2].Replace("(","").Replace(")",""));
               

            }
            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[2]/div/div/div[1]/p[1]"))
            {
                string[] proximoSorteio = row.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Split(' ');
                templateFile.Replace("#DataProximo", proximoSorteio[5].Replace("(", "").Replace(")", "").Replace("concurso",""));
               

            }

            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[3]/div/p[5]/strong"))
            {
                double arrecadacao = Convert.ToDouble(row.InnerText.Replace("R$", ""));
                templateFile.Replace("#Arrecadacao", arrecadacao.ToString().Replace(",","."));
                

            }
            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[3]/div/p[1]"))
            {
                if (row.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Contains("houve"))
                {

                    templateFile.Replace("#Acumulou", "true");
                   
                }
                else
                {
                    templateFile.Replace("#Acumulou", "false");

                }


            }
            
            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[2]/div/div/div[1]/p[2]"))
            {
                double valorEstimativaPremio = Convert.ToDouble(row.InnerText.Replace("R$", ""));
                templateFile.Replace("#EstimativaPremio", valorEstimativaPremio.ToString().Replace(",", "."));

            }
            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[2]/div/div/p"))
            {

                templateFile.Replace("#RealizadoEm", System.Text.RegularExpressions.Regex.Replace(row.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", " "), @"\s{2,}", " "));



            }
                                                                    
            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[2]/div/div/div[2]/p[1]/span[2]"))
            {
                double valorAcumuladoOriginal = Convert.ToDouble(row.InnerText.Replace("R$", ""));
                templateFile.Replace("#ValorAcumulado", valorAcumuladoOriginal.ToString().Replace(",", "."));
             

            }


            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[3]/div/p[1]"))
            {
                if (row.InnerText.Contains("houve"))
                {
                    
                    templateFile.Replace("#NumeroGanhadoresQuina","0");
                    templateFile.Replace("#ValorQuina", "0");
                    oLoteria.RealizadoEm = row.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", ""); 
                }
                else
                {
                    string[] faixa = row.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Split(' ');
                    templateFile.Replace("#NumeroGanhadoresQuina", faixa[4].Replace("acertados", ""));
                    double valorQuinaDouble = Convert.ToDouble(faixa[8]);
                    templateFile.Replace("#ValorQuina", valorQuinaDouble.ToString().Replace(",", "."));
                    oLoteria.RealizadoEm = row.InnerText;
                }

            }

            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[3]/div/p[2]"))
            {
                string[] faixa = row.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Split(' ');
                templateFile.Replace("#NumeroGanhadoresQuadra", faixa[4].Replace("acertados", ""));
                double valorQuadra = Convert.ToDouble(faixa[8]);
                templateFile.Replace("#ValorQuadra", valorQuadra.ToString().Replace(",", "."));
                oLoteria.RealizadoEm = row.InnerText;

            }

            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[3]/div/p[3]"))
            {
                string[] faixa = row.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Split(' ');
                templateFile.Replace("#NumeroGanhadoresTerno", faixa[4].Replace("acertados",""));
                double valorTerno = Convert.ToDouble(faixa[8]);
                templateFile.Replace("#ValorTerno", valorTerno.ToString().Replace(",", "."));
                oLoteria.RealizadoEm = row.InnerText;

            }

            foreach (HtmlNode row in docs.DocumentNode.SelectNodes(".//*[@id='resultados']/div[3]/div/p[4]"))
            {
                string[] faixa = row.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Split(' ');
                templateFile.Replace("#NumeroGanhadoresDuque", faixa[4].Replace("acertados", ""));
                double valorDuque = Convert.ToDouble(faixa[8]);
                templateFile.Replace("#ValorDuque", valorDuque.ToString().Replace(",", "."));
                oLoteria.RealizadoEm = row.InnerText;

            }

            return templateFile;
           
        }

    }
}