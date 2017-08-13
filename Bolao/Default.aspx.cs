using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Bolao.Models;
using System.Xml.Linq;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace Bolao
{
    public partial class Default : System.Web.UI.Page
    {
        List<string[]> numerosDaQuina;
       
        List<int> numerosSort = new List<int>();
        int[] ordemSorteioQuina = new int[5];
        DAO.BolaoUTIL oBolao = new DAO.BolaoUTIL();
        int isPersit = -1;
        string jsonParsed = "";
       
   
        public void escreveJsonFile(string json)
        {
          

            string jsonFileName = "json.txt";
            string path = base.Server.MapPath(Path.Combine("~", jsonFileName));
            System.IO.File.WriteAllText(path, json);

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {

                    jsonParsed =  MatchLoadPage.PreencheModelo(Server.MapPath("json.txt")).ToString();

                    

                   
                   
                }
                catch (System.Net.WebException wex)
                {
                    if (wex.Response != null)
                    {
                        wex.StackTrace.ToString();

                    }

                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                Loteria loteria = serializer.Deserialize<Loteria>(jsonParsed);

                preencheDropdownlist(loteria.NumeroConcurso);

                loadPage("L");
            }
        }

        public string readerJsonByFile()
        {
            string fileName = "json.txt";
            string path = Server.MapPath(Path.Combine("~", fileName));
            string jsonFile = "";

            using (StreamReader rJson = new StreamReader(path))
            {
                jsonFile = rJson.ReadToEnd();
            }

            return jsonFile;
            
            
        }

        public void preencheDropdownlist(int concursoAtual)
        {

            DropDownList1.Items.Clear();

            List<string> numberConcurso = new List<string>();

            numberConcurso.Add("" + concursoAtual);

            DropDownList1.DataSource = numberConcurso;
            DropDownList1.DataBind();

        }

        public ListaResultadoQuina contaAcertos(List<int> numerosSorteio)
        {
            preencheConfig();

            ListaResultadoQuina oListaResultado = new ListaResultadoQuina();
            int contador = 1;
            String numeroQuina = "";



            for (int i = 0; i < numerosDaQuina.Count; i++)
            {
                ResultadoQuina oResultado = new ResultadoQuina();



                foreach (var item in numerosSorteio)
                {




                    if ((numerosDaQuina[i][1]).PadLeft(2, '0').Trim() == item.ToString().PadLeft(2, '0'))
                    {
                        oResultado.numerosAcertados += item.ToString().PadLeft(2, '0') + "-";
                        oResultado.acertosQuina += contador;
                    }
                    if (numerosDaQuina[i][2].PadLeft(2, '0').Trim() == item.ToString().PadLeft(2, '0'))
                    {
                        oResultado.numerosAcertados += item.ToString().PadLeft(2, '0') + "-"; ;
                        oResultado.acertosQuina += contador;
                    }
                    if (numerosDaQuina[i][3].PadLeft(2, '0').Trim() == item.ToString().PadLeft(2, '0'))
                    {
                        oResultado.numerosAcertados += item.ToString().PadLeft(2, '0') + "-"; ;
                        oResultado.acertosQuina += contador;
                    }
                    if (numerosDaQuina[i][4].PadLeft(2, '0').Trim() == item.ToString().PadLeft(2, '0'))
                    {
                        oResultado.numerosAcertados += item.ToString().PadLeft(2, '0') + "-";
                        oResultado.acertosQuina += contador;
                    }
                    if (numerosDaQuina[i][5].PadLeft(2, '0').Trim() == item.ToString().PadLeft(2, '0'))
                    {
                        oResultado.numerosAcertados += item.ToString().PadLeft(2, '0') + "-";
                        oResultado.acertosQuina += contador;
                    }




                }


                if (oResultado.acertosQuina >= 1)
                {
                    oResultado.jogada = numerosDaQuina[i][0];
                    numeroQuina = numerosDaQuina[i][1] + " " + numerosDaQuina[i][2] + " " + numerosDaQuina[i][3] + " " + numerosDaQuina[i][4] + " " + numerosDaQuina[i][5];
                    oResultado.numeroSorteio = oResultado.jogada + ": " + numeroQuina;
                    oResultado.numerosAcertados = oResultado.numerosAcertados.Remove(oResultado.numerosAcertados.Length - 1);


                    if (oResultado.acertosQuina == 1)
                    {
                        oResultado.acertoTipoJogo = "ÁS";
                        oResultado.valorGanho = " R$ 0,00";
                        oResultado.quantidadeAcerto = oResultado.acertosQuina.ToString();
                        if (isPersit == 0)
                        {

                            oBolao.cadastrarJogos(oResultado.numerosAcertados, oResultado.acertoTipoJogo, Convert.ToInt32(lblNumeroConcurso.Text), Convert.ToDecimal(0), Convert.ToInt32(lblStartNumber.Text), Convert.ToInt32(lblEndNumber.Text), oResultado.numeroSorteio);

                        }
                    }
                    else if (oResultado.acertosQuina == 2)
                    {

                        string valorDuqueBanco = lblDuqueValor.Text;
                        oResultado.acertoTipoJogo = "DUQUE";
                        oResultado.valorGanho = "R$ " + lblDuqueValor.Text;
                        oResultado.quantidadeAcerto = oResultado.acertosQuina.ToString();


                        if (isPersit == 0)
                        {
                            valorDuqueBanco = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:N}", lblDuqueValor.Text);

                            oBolao.cadastrarJogos(oResultado.numerosAcertados, oResultado.acertoTipoJogo, Convert.ToInt32(lblNumeroConcurso.Text), Convert.ToDecimal(valorDuqueBanco), Convert.ToInt32(lblStartNumber.Text), Convert.ToInt32(lblEndNumber.Text), oResultado.numeroSorteio);
                        }


                        //salvaJogosQuina(Convert.ToInt32(lblNumeroConcurso.Text), oResultado.acertoTipoJogo, Convert.ToDouble(lblDuqueValor.Text));
                    }

                    else if (oResultado.acertosQuina == 3)
                    {
                        string valorTernoBanco = lblTerno.Text;
                        oResultado.acertoTipoJogo = "TERNO";
                        oResultado.valorGanho = "R$ " + lblTerno.Text;
                        oResultado.quantidadeAcerto = oResultado.acertosQuina.ToString();

                        if (isPersit == 0)
                        {
                            valorTernoBanco = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:N}", lblTerno.Text);
                            oBolao.cadastrarJogos(oResultado.numerosAcertados, oResultado.acertoTipoJogo, Convert.ToInt32(lblNumeroConcurso.Text), Convert.ToDecimal(valorTernoBanco), Convert.ToInt32(lblStartNumber.Text), Convert.ToInt32(lblEndNumber.Text), oResultado.numeroSorteio);
                        }

                    }

                    else if (oResultado.acertosQuina == 4)
                    {
                        string valorQuadraBanco = lblQuadra.Text;
                        oResultado.acertoTipoJogo = "QUADRA";
                        oResultado.valorGanho = "R$ " + lblQuadra.Text;
                        oResultado.quantidadeAcerto = oResultado.acertosQuina.ToString();


                        if (isPersit == 0)
                        {

                            valorQuadraBanco = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:N}", lblQuadra.Text);
                            oBolao.cadastrarJogos(oResultado.numerosAcertados, oResultado.acertoTipoJogo, Convert.ToInt32(lblNumeroConcurso.Text), Convert.ToDecimal(valorQuadraBanco), Convert.ToInt32(lblStartNumber.Text), Convert.ToInt32(lblEndNumber.Text), oResultado.numeroSorteio);
                        }
                    }
                    else if (oResultado.acertosQuina == 5)
                    {
                        string valorQuinaBanco = lblQuina.Text;
                        oResultado.acertoTipoJogo = "CARACA ESTAMOS RICOS!!!";
                        oResultado.valorGanho = lblQuina.Text;
                        oResultado.quantidadeAcerto = oResultado.acertosQuina.ToString();

                        if (isPersit == 0)
                        {
                            valorQuinaBanco = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:N}", lblQuina.Text);
                            oBolao.cadastrarJogos(oResultado.numerosAcertados, oResultado.acertoTipoJogo, Convert.ToInt32(lblNumeroConcurso.Text), Convert.ToDecimal(valorQuinaBanco), Convert.ToInt32(lblStartNumber.Text), Convert.ToInt32(lblEndNumber.Text), oResultado.numeroSorteio);
                        }

                    }


                    oListaResultado.Add(oResultado);

                }




            }


            return oListaResultado;

        }




        public string[] loadJogos(string tipo)
        {
            string fileName = "jogosByConfig.txt";

            if (tipo == "G")
                fileName = "jogosFamiliaFernandes.txt";
            
            string path = Server.MapPath(Path.Combine("~", fileName));
            StreamReader loadFile = new StreamReader(path);
            string[] jogosQuinaFile = new string[16];
            int count = 0;
            while (!loadFile.EndOfStream)
            {
                string linha = loadFile.ReadLine();
                jogosQuinaFile[count] = linha.Replace("\"", "''");
                count++;


            }
            return jogosQuinaFile;
        }

        public List<string[]> preencheNumero(int numeroJogo)
        {

            string[] jogosQuinaCarregado;

            if (numeroJogo <= 4375)
            {
                jogosQuinaCarregado = loadJogos("G");
            }
            else
            {
                jogosQuinaCarregado = loadJogos("N");
            }


            //string[] jogos01 = { "JG (01)", "15", "16", "44", "67", "78" };
            //string[] jogos02 = { "JG (02)", "22", "37", "49", "53", "76" };
            //string[] jogos03 = { "JG (03)", "04", "33", "51", "58", "62" };
            //string[] jogos04 = { "JG (04)", "05", "13", "14", "26", "71" };
            //string[] jogos05 = { "JG (05)", "28", "29", "40", "43", "69" };
            //string[] jogos06 = { "JG (06)", "08", "31", "50", "74", "77" };
            //string[] jogos07 = { "JG (07)", "10", "19", "24", "59", "66" };
            //string[] jogos08 = { "JG (08)", "01", "21", "39", "56", "68" };
            //string[] jogos09 = { "JG (09)", "11", "18", "36", "55", "79" };
            //string[] jogos10 = { "JG (10)", "02", "07", "23", "42", "75" };
            //string[] jogos11 = { "JG (11)", "03", "09", "17", "38", "41" };
            //string[] jogos12 = { "JG (12)", "25", "47", "46", "64", "72" };
            //string[] jogos13 = { "JG (13)", "06", "12", "27", "30", "45" };
            //string[] jogos14 = { "JG (14)", "32", "61", "70", "73", "80" };
            //string[] jogos15 = { "JG (15)", "20", "34", "52", "57", "65" };
            //string[] jogos16 = { "JG (16)", "35", "48", "54", "60", "63" };



            string[] jogos01 = jogosQuinaCarregado[0].ToString().Split(',');
            string[] jogos02 = jogosQuinaCarregado[1].ToString().Split(',');
            string[] jogos03 = jogosQuinaCarregado[2].ToString().Split(',');
            string[] jogos04 = jogosQuinaCarregado[3].ToString().Split(',');
            //string[] jogos05 = jogosQuinaCarregado[4].ToString().Split(',');
            //string[] jogos06 = jogosQuinaCarregado[5].ToString().Split(',');
            //string[] jogos07 = jogosQuinaCarregado[6].ToString().Split(',');
            //string[] jogos08 = jogosQuinaCarregado[7].ToString().Split(',');
            //string[] jogos09 = jogosQuinaCarregado[8].ToString().Split(',');
            //string[] jogos10 = jogosQuinaCarregado[9].ToString().Split(',');
            //string[] jogos11 = jogosQuinaCarregado[10].ToString().Split(',');
            //string[] jogos12 = jogosQuinaCarregado[11].ToString().Split(',');
            //string[] jogos13 = jogosQuinaCarregado[12].ToString().Split(',');
            //string[] jogos14 = jogosQuinaCarregado[13].ToString().Split(',');
            //string[] jogos15 = jogosQuinaCarregado[14].ToString().Split(',');
            //string[] jogos16 = jogosQuinaCarregado[15].ToString().Split(',');




            ////// Jogos extras
            //string[] jg17 = { "JG_17_SÃO_JOÃO", "50", "39", "12", "57", "54" };
            //string[] jg18 = { "JG_18_SÃO_JOÃO", "47", "26", "15", "20", "19" };
            //string[] jg19 = { "JG_19_SÃO_JOÃO", "74", "75", "04", "61", "44" };
            //string[] jg20 = { "JG_20_SÃO_JOÃO", "09", "28", "65", "66", "31" };
            //string[] jg21 = { "JG_21_SÃO_JOÃO", "46", "55", "36", "05", "38" };
            //string[] jg22 = { "JG_22_SÃO_JOÃO", "51", "56", "11", "40", "03" };
            //string[] jg23 = { "JG_23_SÃO_JOÃO", "30", "77", "08", "63", "52" };
            //string[] jg24 = { "JG_24_SÃO_JOÃO", "21", "58", "23", "42", "49" };
            //string[] jg25 = { "JG_25_SÃO_JOÃO", "02", "69", "14", "53", "06" };
            //string[] jg26 = { "JG_26_SÃO_JOÃO", "27", "34", "45", "32", "35" };
            //string[] jg27 = { "JG_27_SÃO_JOÃO", "68", "29", "48", "01", "10" };
            //string[] jg28 = { "JG_28_SÃO_JOÃO", "71", "18", "17", "62", "41" };
            //string[] jg29 = { "JG_29_SÃO_JOÃO", "72", "13", "78", "73", "60" };
            //string[] jg30 = { "JG_30_SÃO_JOÃO", "59", "76", "25", "80", "37" };
            //string[] jg31 = { "JG_31_SÃO_JOÃO", "24", "67", "70", "33", "64" };
            //string[] jg32 = { "JG_32_SÃO_JOÃO", "43", "16", "79", "22", "07" };
            //string[] jg33 = { "JG_33_SÃO_JOÃO", "43", "57", "69", "33", "28" };
            //string[] jg34 = { "JG_34_SÃO_JOÃO", "30", "49", "09", "25", "62" };
            //string[] jg35 = { "JG_35_SÃO_JOÃO", "35", "26", "01", "76", "07" };
            //string[] jg36 = { "JG_36_SÃO_JOÃO", "50", "80", "45", "78", "52" };
            //string[] jg37 = { "JG_37_SÃO_JOÃO", "08", "54", "06", "38", "60" };
            //string[] jg38 = { "JG_38_SÃO_JOÃO", "48", "15", "22", "02", "51" };
            //string[] jg39 = { "JG_39_SÃO_JOÃO", "75", "32", "58", "29", "65" };
            //string[] jg40 = { "JG_40_SÃO_JOÃO", "74", "27", "31", "23", "05" };
            //string[] jg41 = { "JG_41_SÃO_JOÃO", "64", "16", "34", "66", "03" };
            //string[] jg42 = { "JG_42_SÃO_JOÃO", "36", "61", "13", "10", "12" };
            //string[] jg43 = { "JG_43_SÃO_JOÃO", "71", "19", "79", "73", "53" };
            //string[] jg44 = { "JG_44_SÃO_JOÃO", "17", "47", "59", "63", "40" };
            //string[] jg45 = { "JG_45_SÃO_JOÃO", "72", "21", "37", "04", "70" };
            //string[] jg46 = { "JG_46_SÃO_JOÃO", "18", "11", "20", "77", "46" };
            //string[] jg47 = { "JG_47_SÃO_JOÃO", "55", "39", "14", "56", "68" };
            //string[] jg48 = { "JG_48_SÃO_JOÃO", "42", "24", "44", "41", "67" };
            //string[] jg49 = { "JG_49_SÃO_JOÃO", "64", "63", "49", "74", "20" };
            //string[] jg50 = { "JG_50_SÃO_JOÃO", "69", "01", "65", "36", "76" };
            //string[] jg51 = { "JG_51_SÃO_JOÃO", "60", "47", "40", "31", "32" };
            //string[] jg52 = { "JG_52_SÃO_JOÃO", "13", "70", "48", "21", "05" };
            //string[] jg53 = { "JG_53_SÃO_JOÃO", "50", "46", "37", "14", "19" };
            //string[] jg54 = { "JG_54_SÃO_JOÃO", "39", "51", "24", "07", "35" };
            //string[] jg55 = { "JG_55_SÃO_JOÃO", "03", "53", "75", "77", "26" };
            //string[] jg56 = { "JG_56_SÃO_JOÃO", "09", "73", "30", "38", "17" };
            //string[] jg57 = { "JG_57_SÃO_JOÃO", "79", "54", "56", "33", "29" };
            //string[] jg58 = { "JG_58_SÃO_JOÃO", "43", "68", "23", "61", "08" };
            //string[] jg59 = { "JG_59_SÃO_JOÃO", "59", "42", "16", "41", "66" };
            //string[] jg60 = { "JG_60_SÃO_JOÃO", "55", "34", "28", "71", "10" };
            //string[] jg61 = { "JG_61_SÃO_JOÃO", "78", "12", "11", "18", "06" };
            //string[] jg62 = { "JG_62_SÃO_JOÃO", "58", "22", "44", "45", "52" };
            //string[] jg63 = { "JG_63_SÃO_JOÃO", "15", "67", "02", "04", "25" };
            //string[] jg64 = { "JG_64_SÃO_JOÃO", "57", "72", "80", "27", "62" };
            //string[] jg65 = { "JG_65_SÃO_JOÃO", "30", "19", "23", "60", "29" };
            //string[] jg66 = { "JG_66_SÃO_JOÃO", "35", "66", "14", "12", "03" };
            //string[] jg67 = { "JG_67_SÃO_JOÃO", "42", "28", "04", "15", "52" };
            //string[] jg68 = { "JG_68_SÃO_JOÃO", "07", "21", "62", "08", "75" };
            //string[] jg69 = { "JG_69_SÃO_JOÃO", "45", "20", "31", "13", "44" };
            //string[] jg70 = { "JG_70_SÃO_JOÃO", "36", "50", "70", "09", "02" };
            //string[] jg71 = { "JG_71_SÃO_JOÃO", "11", "43", "39", "06", "78" };
            //string[] jg72 = { "JG_72_SÃO_JOÃO", "74", "69", "38", "16", "63" };
            //string[] jg73 = { "JG_73_SÃO_JOÃO", "58", "34", "46", "01", "51" };
            //string[] jg74 = { "JG_74_SÃO_JOÃO", "71", "72", "56", "37", "24" };
            //string[] jg75 = { "JG_75_SÃO_JOÃO", "55", "65", "49", "17", "59" };
            //string[] jg76 = { "JG_76_SÃO_JOÃO", "32", "22", "77", "67", "41" };
            //string[] jg77 = { "JG_77_SÃO_JOÃO", "79", "10", "57", "64", "73" };
            //string[] jg78 = { "JG_78_SÃO_JOÃO", "80", "05", "61", "40", "27" };
            //string[] jg79 = { "JG_79_SÃO_JOÃO", "33", "25", "47", "54", "68" };
            //string[] jg80 = { "JG_80_SÃO_JOÃO", "48", "18", "26", "76", "53" };
            //string[] jg81 = { "JG_81_SÃO_JOÃO", "32", "44", "18", "31", "08" };
            //string[] jg82 = { "JG_82_SÃO_JOÃO", "14", "04", "39", "71", "25" };
            //string[] jg83 = { "JG_83_SÃO_JOÃO", "47", "59", "09", "17", "34" };
            //string[] jg84 = { "JG_84_SÃO_JOÃO", "75", "58", "52", "40", "06" };
            //string[] jg85 = { "JG_85_SÃO_JOÃO", "28", "68", "79", "62", "35" };
            //string[] jg86 = { "JG_86_SÃO_JOÃO", "12", "42", "30", "16", "60" };
            //string[] jg87 = { "JG_87_SÃO_JOÃO", "05", "24", "63", "43", "41" };
            //string[] jg88 = { "JG_88_SÃO_JOÃO", "29", "77", "73", "54", "33" };
            //string[] jg89 = { "JG_89_SÃO_JOÃO", "01", "65", "80", "46", "57" };
            //string[] jg90 = { "JG_90_SÃO_JOÃO", "03", "45", "78", "66", "22" };
            //string[] jg91 = { "JG_91_SÃO_JOÃO", "48", "70", "55", "23", "56" };
            //string[] jg92 = { "JG_92_SÃO_JOÃO", "69", "74", "72", "67", "51" };
            //string[] jg93 = { "JG_93_SÃO_JOÃO", "07", "02", "21", "20", "37" };
            //string[] jg94 = { "JG_94_SÃO_JOÃO", "64", "50", "38", "26", "49" };
            //string[] jg95 = { "JG_95_SÃO_JOÃO", "19", "36", "13", "27", "61" };
            //string[] jg96 = { "JG_96_SÃO_JOÃO", "76", "10", "15", "53", "11" };
            //string[] jg97 = { "JG_97_SÃO_JOÃO", "70", "59", "27", "19", "69" };
            //string[] jg98 = { "JG_98_SÃO_JOÃO", "54", "71", "11", "52", "15" };
            //string[] jg99 = { "JG_99_SÃO_JOÃO", "06", "57", "38", "16", "09" };
            //string[] jg100 = { "JG_100_SÃO_JOÃO", "13", "67", "80", "62", "48" };
            //string[] jg101 = { "JG_101_SÃO_JOÃO", "77", "61", "21", "51", "79" };
            //string[] jg102 = { "JG_102_SÃO_JOÃO", "34", "73", "50", "44", "64" };
            //string[] jg103 = { "JG_103_SÃO_JOÃO", "74", "43", "08", "24", "20" };
            //string[] jg104 = { "JG_104_SÃO_JOÃO", "10", "76", "03", "58", "05" };
            //string[] jg105 = { "JG_105_SÃO_JOÃO", "39", "46", "07", "40", "63" };
            //string[] jg106 = { "JG_106_SÃO_JOÃO", "14", "32", "66", "25", "33" };
            //string[] jg107 = { "JG_107_SÃO_JOÃO", "68", "12", "04", "60", "23" };
            //string[] jg108 = { "JG_108_SÃO_JOÃO", "37", "02", "49", "18", "30" };
            //string[] jg109 = { "JG_109_SÃO_JOÃO", "35", "75", "26", "41", "28" };
            //string[] jg110 = { "JG_110_SÃO_JOÃO", "65", "01", "36", "47", "56" };
            //string[] jg111 = { "JG_111_SÃO_JOÃO", "42", "45", "31", "55", "29" };
            //string[] jg112 = { "JG_112_SÃO_JOÃO", "22", "53", "17", "72", "78" };
            //string[] jg113 = { "JG_113_SÃO_JOÃO", "36", "02", "39", "42", "24" };
            //string[] jg114 = { "JG_114_SÃO_JOÃO", "59", "58", "69", "47", "30" };
            //string[] jg115 = { "JG_115_SÃO_JOÃO", "07", "45", "72", "74", "56" };
            //string[] jg116 = { "JG_116_SÃO_JOÃO", "03", "32", "29", "12", "52" };
            //string[] jg117 = { "JG_117_SÃO_JOÃO", "25", "18", "55", "77", "43" };
            //string[] jg118 = { "JG_118_SÃO_JOÃO", "01", "06", "60", "65", "75" };
            //string[] jg119 = { "JG_119_SÃO_JOÃO", "48", "51", "80", "79", "64" };
            //string[] jg120 = { "JG_120_SÃO_JOÃO", "50", "08", "13", "40", "14" };
            //string[] jg121 = { "JG_121_SÃO_JOÃO", "05", "19", "35", "33", "28" };
            //string[] jg122 = { "JG_122_SÃO_JOÃO", "54", "78", "53", "21", "67" };
            //string[] jg123 = { "JG_123_SÃO_JOÃO", "34", "49", "41", "20", "11" };
            //string[] jg124 = { "JG_124_SÃO_JOÃO", "63", "22", "17", "70", "68" };
            //string[] jg125 = { "JG_125_SÃO_JOÃO", "71", "38", "27", "10", "23" };
            //string[] jg126 = { "JG_126_SÃO_JOÃO", "57", "73", "62", "16", "26" };
            //string[] jg127 = { "JG_127_SÃO_JOÃO", "44", "76", "66", "15", "46" };
            //string[] jg128 = { "JG_128_SÃO_JOÃO", "04", "09", "37", "31", "61" };
            //string[] jg129 = { "JG_129_SÃO_JOÃO", "28", "51", "20", "58", "08" };
            //string[] jg130 = { "JG_130_SÃO_JOÃO", "38", "11", "62", "42", "14" };
            //string[] jg131 = { "JG_131_SÃO_JOÃO", "70", "30", "61", "53", "71" };
            //string[] jg132 = { "JG_132_SÃO_JOÃO", "54", "27", "59", "60", "01" };
            //string[] jg133 = { "JG_133_SÃO_JOÃO", "57", "48", "47", "13", "74" };
            //string[] jg134 = { "JG_134_SÃO_JOÃO", "65", "43", "35", "17", "12" };
            //string[] jg135 = { "JG_135_SÃO_JOÃO", "67", "06", "32", "21", "05" };
            //string[] jg136 = { "JG_136_SÃO_JOÃO", "80", "16", "34", "64", "44" };
            //string[] jg137 = { "JG_137_SÃO_JOÃO", "04", "03", "39", "55", "26" };
            //string[] jg138 = { "JG_138_SÃO_JOÃO", "76", "68", "49", "69", "52" };
            //string[] jg139 = { "JG_139_SÃO_JOÃO", "37", "40", "15", "02", "78" };
            //string[] jg140 = { "JG_140_SÃO_JOÃO", "45", "63", "23", "41", "56" };
            //string[] jg141 = { "JG_141_SÃO_JOÃO", "72", "33", "31", "07", "75" };
            //string[] jg142 = { "JG_142_SÃO_JOÃO", "09", "22", "19", "77", "36" };
            //string[] jg143 = { "JG_143_SÃO_JOÃO", "25", "29", "50", "18", "79" };
            //string[] jg144 = { "JG_144_SÃO_JOÃO", "46", "66", "10", "73", "24" };
            //string[] jg145 = { "JG_145_SÃO_JOÃO", "04", "10", "47", "60", "03" };
            //string[] jg146 = { "JG_146_SÃO_JOÃO", "15", "12", "17", "49", "45" };
            //string[] jg147 = { "JG_147_SÃO_JOÃO", "67", "78", "16", "23", "31" };
            //string[] jg148 = { "JG_148_SÃO_JOÃO", "44", "06", "29", "19", "22" };
            //string[] jg149 = { "JG_149_SÃO_JOÃO", "55", "59", "70", "34", "26" };
            //string[] jg150 = { "JG_150_SÃO_JOÃO", "68", "28", "40", "71", "43" };
            //string[] jg151 = { "JG_151_SÃO_JOÃO", "52", "07", "11", "74", "32" };
            //string[] jg152 = { "JG_152_SÃO_JOÃO", "39", "02", "21", "18", "65" };
            //string[] jg153 = { "JG_153_SÃO_JOÃO", "76", "51", "54", "05", "79" };
            //string[] jg154 = { "JG_154_SÃO_JOÃO", "36", "30", "64", "69", "57" };
            //string[] jg155 = { "JG_155_SÃO_JOÃO", "41", "56", "08", "50", "25" };
            //string[] jg156 = { "JG_156_SÃO_JOÃO", "35", "75", "09", "73", "37" };
            //string[] jg157 = { "JG_157_SÃO_JOÃO", "20", "77", "58", "46", "01" };
            //string[] jg158 = { "JG_158_SÃO_JOÃO", "24", "80", "42", "27", "13" };
            //string[] jg159 = { "JG_159_SÃO_JOÃO", "38", "66", "53", "61", "72" };
            //string[] jg160 = { "JG_160_SÃO_JOÃO", "14", "33", "48", "63", "62" };
            //string[] jg161 = { "JG_161_SÃO_JOÃO", "73", "38", "80", "48", "09" };
            //string[] jg162 = { "JG_162_SÃO_JOÃO", "53", "68", "76", "41", "32" };
            //string[] jg163 = { "JG_163_SÃO_JOÃO", "79", "70", "35", "71", "25" };
            //string[] jg164 = { "JG_163_SÃO_JOÃO", "40", "17", "75", "07", "12" };
            //string[] jg165 = { "JG_163_SÃO_JOÃO", "19", "49", "11", "16", "44" };
            //string[] jg166 = { "JG_163_SÃO_JOÃO", "28", "24", "57", "26", "51" };
            //string[] jg167 = { "JG_163_SÃO_JOÃO", "56", "15", "05", "27", "42" };
            //string[] jg168 = { "JG_163_SÃO_JOÃO", "69", "66", "33", "61", "45" };
            //string[] jg169 = { "JG_163_SÃO_JOÃO", "34", "13", "10", "67", "18" };
            //string[] jg170 = { "JG_163_SÃO_JOÃO", "37", "46", "06", "72", "52" };
            //string[] jg171 = { "JG_163_SÃO_JOÃO", "02", "60", "04", "21", "01" };
            //string[] jg172 = { "JG_163_SÃO_JOÃO", "31", "36", "63", "20", "29" };
            //string[] jg173 = { "JG_163_SÃO_JOÃO", "77", "64", "03", "47", "55" };
            //string[] jg174 = { "JG_163_SÃO_JOÃO", "30", "43", "59", "58", "39" };
            //string[] jg175 = { "JG_163_SÃO_JOÃO", "23", "65", "74", "08", "78" };
            //string[] jg176 = { "JG_163_SÃO_JOÃO", "14", "50", "62", "22", "54" };




            List<string[]> jogosQuina = new List<string[]>();

            jogosQuina.Add(jogos01);
            jogosQuina.Add(jogos02);
            jogosQuina.Add(jogos03);
            jogosQuina.Add(jogos04);
            //jogosQuina.Add(jogos05);
            //jogosQuina.Add(jogos06);
            //jogosQuina.Add(jogos07);
            //jogosQuina.Add(jogos08);
            //jogosQuina.Add(jogos09);
            //jogosQuina.Add(jogos10);
            //jogosQuina.Add(jogos11);
            //jogosQuina.Add(jogos12);
            //jogosQuina.Add(jogos13);
            //jogosQuina.Add(jogos14);
            //jogosQuina.Add(jogos15);
            //jogosQuina.Add(jogos16);

            // Extra

            //jogosQuina.Add(jg17);
            //jogosQuina.Add(jg18);
            //jogosQuina.Add(jg19);
            //jogosQuina.Add(jg20);
            //jogosQuina.Add(jg21);
            //jogosQuina.Add(jg22);
            //jogosQuina.Add(jg23);
            //jogosQuina.Add(jg24);
            //jogosQuina.Add(jg25);
            //jogosQuina.Add(jg26);
            //jogosQuina.Add(jg27);
            //jogosQuina.Add(jg28);
            //jogosQuina.Add(jg29);
            //jogosQuina.Add(jg30);
            //jogosQuina.Add(jg31);
            //jogosQuina.Add(jg32);
            //jogosQuina.Add(jg33);
            //jogosQuina.Add(jg34);
            //jogosQuina.Add(jg35);
            //jogosQuina.Add(jg36);
            //jogosQuina.Add(jg37);
            //jogosQuina.Add(jg38);
            //jogosQuina.Add(jg39);
            //jogosQuina.Add(jg40);
            //jogosQuina.Add(jg41);
            //jogosQuina.Add(jg42);
            //jogosQuina.Add(jg43);
            //jogosQuina.Add(jg44);
            //jogosQuina.Add(jg45);
            //jogosQuina.Add(jg46);
            //jogosQuina.Add(jg47);
            //jogosQuina.Add(jg48);
            //jogosQuina.Add(jg49);
            //jogosQuina.Add(jg50);
            //jogosQuina.Add(jg51);
            //jogosQuina.Add(jg52);
            //jogosQuina.Add(jg53);
            //jogosQuina.Add(jg54);
            //jogosQuina.Add(jg55);
            //jogosQuina.Add(jg56);
            //jogosQuina.Add(jg57);
            //jogosQuina.Add(jg58);
            //jogosQuina.Add(jg59);
            //jogosQuina.Add(jg60);
            //jogosQuina.Add(jg61);
            //jogosQuina.Add(jg62);
            //jogosQuina.Add(jg63);
            //jogosQuina.Add(jg64);
            //jogosQuina.Add(jg65);
            //jogosQuina.Add(jg66);
            //jogosQuina.Add(jg67);
            //jogosQuina.Add(jg68);
            //jogosQuina.Add(jg69);
            //jogosQuina.Add(jg70);
            //jogosQuina.Add(jg71);
            //jogosQuina.Add(jg72);
            //jogosQuina.Add(jg73);
            //jogosQuina.Add(jg74);
            //jogosQuina.Add(jg75);
            //jogosQuina.Add(jg76);
            //jogosQuina.Add(jg77);
            //jogosQuina.Add(jg78);
            //jogosQuina.Add(jg79);
            //jogosQuina.Add(jg80);
            //jogosQuina.Add(jg81);
            //jogosQuina.Add(jg82);
            //jogosQuina.Add(jg83);
            //jogosQuina.Add(jg84);
            //jogosQuina.Add(jg85);
            //jogosQuina.Add(jg86);
            //jogosQuina.Add(jg87);
            //jogosQuina.Add(jg88);
            //jogosQuina.Add(jg89);
            //jogosQuina.Add(jg90);
            //jogosQuina.Add(jg91);
            //jogosQuina.Add(jg92);
            //jogosQuina.Add(jg93);
            //jogosQuina.Add(jg94);
            //jogosQuina.Add(jg95);
            //jogosQuina.Add(jg96);
            //jogosQuina.Add(jg97);
            //jogosQuina.Add(jg98);
            //jogosQuina.Add(jg99);
            //jogosQuina.Add(jg100);
            //jogosQuina.Add(jg101);
            //jogosQuina.Add(jg102);
            //jogosQuina.Add(jg103);
            //jogosQuina.Add(jg104);
            //jogosQuina.Add(jg105);
            //jogosQuina.Add(jg106);
            //jogosQuina.Add(jg107);
            //jogosQuina.Add(jg108);
            //jogosQuina.Add(jg109);
            //jogosQuina.Add(jg110);
            //jogosQuina.Add(jg111);
            //jogosQuina.Add(jg112);
            //jogosQuina.Add(jg113);
            //jogosQuina.Add(jg114);
            //jogosQuina.Add(jg115);
            //jogosQuina.Add(jg116);
            //jogosQuina.Add(jg117);
            //jogosQuina.Add(jg118);
            //jogosQuina.Add(jg119);
            //jogosQuina.Add(jg120);
            //jogosQuina.Add(jg121);
            //jogosQuina.Add(jg122);
            //jogosQuina.Add(jg123);
            //jogosQuina.Add(jg124);
            //jogosQuina.Add(jg125);
            //jogosQuina.Add(jg126);
            //jogosQuina.Add(jg127);
            //jogosQuina.Add(jg128);
            //jogosQuina.Add(jg129);
            //jogosQuina.Add(jg130);
            //jogosQuina.Add(jg131);
            //jogosQuina.Add(jg132);
            //jogosQuina.Add(jg133);
            //jogosQuina.Add(jg134);
            //jogosQuina.Add(jg135);
            //jogosQuina.Add(jg136);
            //jogosQuina.Add(jg137);
            //jogosQuina.Add(jg138);
            //jogosQuina.Add(jg139);
            //jogosQuina.Add(jg140);
            //jogosQuina.Add(jg141);
            //jogosQuina.Add(jg142);
            //jogosQuina.Add(jg143);
            //jogosQuina.Add(jg144);
            //jogosQuina.Add(jg145);
            //jogosQuina.Add(jg146);
            //jogosQuina.Add(jg147);
            //jogosQuina.Add(jg148);
            //jogosQuina.Add(jg149);
            //jogosQuina.Add(jg150);
            //jogosQuina.Add(jg151);
            //jogosQuina.Add(jg152);
            //jogosQuina.Add(jg153);
            //jogosQuina.Add(jg154);
            //jogosQuina.Add(jg155);
            //jogosQuina.Add(jg156);
            //jogosQuina.Add(jg157);
            //jogosQuina.Add(jg158);
            //jogosQuina.Add(jg159);
            //jogosQuina.Add(jg160);
            //jogosQuina.Add(jg161);
            //jogosQuina.Add(jg162);
            //jogosQuina.Add(jg163);
            //jogosQuina.Add(jg164);
            //jogosQuina.Add(jg165);
            //jogosQuina.Add(jg166);
            //jogosQuina.Add(jg167);
            //jogosQuina.Add(jg168);
            //jogosQuina.Add(jg169);
            //jogosQuina.Add(jg170);
            //jogosQuina.Add(jg171);
            //jogosQuina.Add(jg172);
            //jogosQuina.Add(jg173);
            //jogosQuina.Add(jg174);
            //jogosQuina.Add(jg175);
            //jogosQuina.Add(jg176);


            return jogosQuina;

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
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void lerXmlQuina()
        {

            if (!verifaArquivo())
            {
                XmlTextReader quinaXml = new XmlTextReader(@"D:\Meu Desenvolvimento\teste.xml");

                while (quinaXml.Read())
                {
                    if (quinaXml.NodeType == XmlNodeType.Element && quinaXml.Name == "NumeroConcurso")
                    {
                        this.lblNumeroConcurso.Text = (quinaXml.ReadString());
                    }
                }
            }

        }

        public bool verifaArquivo()
        {
            if (System.IO.File.Exists(@"D:\Meu Desenvolvimento\teste.xml"))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int num7;
            int jogadoresQtd = this.GridView2.Rows.Count;
            for (int i = 0; i < this.GridView2.Rows.Count; i = num7 + 1)
            {
                double num2 = 0.0;
                double num3 = 0.0;
                num2 = Convert.ToDouble(this.lblQuina.Text);
                num3 = Convert.ToDouble(this.lblEstimativaValorProximoSorteio.Text);
                if (!(num2 == 0.0))
                {
                    double num5 = num2 / jogadoresQtd;
                    this.GridView2.Rows[i].Cells[5].Text = "R$ " + string.Format("{0:N}", num5);
                }
                else if (!(num3 == 0.0))
                {
                    double num6 = Convert.ToDouble(this.lblEstimativaValorProximoSorteio.Text) / jogadoresQtd;
                    this.GridView2.Rows[i].Cells[5].Text = "R$ " + string.Format("{0:N}", num6);
                }
                double num4 = Convert.ToDouble(this.lblQuadra.Text)/ jogadoresQtd;
                Label label = (Label)this.GridView2.Rows[i].FindControl("Label4");
                this.GridView2.Rows[i].Cells[4].Text = "R$ " + string.Format("{0:N}", num4);
                if (label.Text == "SIM")
                {
                    this.GridView2.Rows[i].Cells[3].BackColor = Color.Green;
                }
                else
                {
                    this.GridView2.Rows[i].Cells[3].BackColor = Color.Red;
                }
                num7 = i;

            }

        }

        private void preencheConfig()
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
                Label6.Text = ds.Tables[1].Rows[i][0].ToString();
                lblStartNumber.Text = ds.Tables[1].Rows[i][1].ToString();
                lblEndNumber.Text = ds.Tables[1].Rows[i][2].ToString();
            }


            Session["Initial"] = lblStartNumber.Text;
            Session["Final"] = lblEndNumber.Text;
        }

        private void salvaJogosQuina(int jogo, string tipAcerto, double valor)
        {
            string path = Server.MapPath(@"jogos.txt");
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                StreamWriter sw = File.CreateText(path);

            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Write(jogo + "; ");
                sw.Write(tipAcerto + "; ");
                sw.Write(valor);
            }

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        protected void btnResultaDetalhado_Click(object sender, EventArgs e)
        {


        }

        protected void lnkDetalhado_Click(object sender, EventArgs e)
        {

        }

        public void loadPage(string status)
        {
           
            DateTime dateAgora = DateTime.Now;
            this.lblUserOnline.Text = base.Application["addUserBolao"].ToString();
            string time = dateAgora.TimeOfDay.ToString();
            Session["ok"] = false;
            dvGanhadores.Visible = false;
           
          
            try
            {
             

                    if (status == "D")
                    {
                        //Not yet implemented
                        //json = JSONHelper.GetJSONString(String.Format(ConfigurationManager.AppSettings["loteria1"], 2, Convert.ToInt32(DropDownList1.SelectedItem.Text)));
                    }
                    else
                    {
                        //json = JSONHelper.GetJSONString(String.Format(ConfigurationManager.AppSettings["loteria"], 2));
                    }
             

              
                
            }
            catch (System.Net.WebException wex)
            {
                if (wex.Response != null)
                {
                    wex.StackTrace.ToString();

                }

            }


            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Loteria loteria = serializer.Deserialize<Loteria>(jsonParsed);



            //XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(json, "BolaoQuina");

            //doc.Save(@"D:\Meu Desenvolvimento\"+loteria.NumeroConcurso+".xml");

            //Response.Write("<script>Alert("+ doc + ")</script>");


            if (loteria.Acumulou)
            {
                this.lblAcumulouSimOuNao.Text = "ACUMULOU: NOVAS SDN'S ABERTAS!!!!<br>";

            }
            this.lblNumeroConcurso.Text = loteria.NumeroConcurso.ToString();
            this.lblDataConcurso.Text = Convert.ToDateTime(loteria.Data, CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            this.lblSorteiRealizadoEm.Text = loteria.RealizadoEm;
            this.lblEstimativaValorProximoSorteio.Text = loteria.EstimativaPremio.ToString("#,0.00", new CultureInfo("pt-BR")).ToString();
            this.lblDataProximoConcurso.Text = Convert.ToDateTime(loteria.DataProximo).ToString("dd/MM/yyyy");
            this.lblAcumladoProximoConcurso.Text = loteria.ValorAcumulado.ToString("#,0.00", new CultureInfo("pt-BR")).ToString();
            this.isPersit = this.oBolao.validaInsert(Convert.ToInt32(this.lblNumeroConcurso.Text));

            numerosDaQuina = preencheNumero(loteria.NumeroConcurso);

            foreach (var item1 in loteria.Sorteios)
            {

                foreach (var item2 in item1.Numeros)
                {


                    numerosSort.Add(item2);



                }


                foreach (var item3 in item1.Premios)
                {

                    if (item3.Faixa == "Quina")
                    {

                        this.lblQuina.Text = item3.Valor.ToString("#,0.00", new CultureInfo("pt-BR")).ToString();
                        this.lblQuinaGanhadores.Text = item3.NumeroGanhadores.ToString();
                        int num5 = item1.Ganhadores.Count / 2;
                        if ((num5 == 0) && (item1.Ganhadores.Count > 0))
                        {
                            this.dvGanhadores.Visible = true;
                            object[] objArray1 = new object[] { item1.Ganhadores[1].CidadeEstado, " - ", item1.Ganhadores[0].CidadeEstado, "</br> ", 1, " aposta ganhou o pr\x00eamio para 5 acertos" };
                            this.lbGanhador.Text = string.Concat(objArray1);
                        }
                        else if (num5 == 2)
                        {
                            this.dvGanhadores.Visible = true;
                            object[] objArray2 = new object[] { num5, " aposta ganhou o pr\x00eamio para 5 acertos</br>", item1.Ganhadores[1].CidadeEstado, " - ", item1.Ganhadores[0].CidadeEstado, "</br> ", item1.Ganhadores[3].CidadeEstado, " - ", item1.Ganhadores[2].CidadeEstado };
                            this.lbGanhador.Text = string.Concat(objArray2);
                        }


                    }

                    if (item3.Faixa == "Quadra")
                    {
                        lblQuadra.Text = item3.Valor.ToString("#,0.00", new CultureInfo("pt-BR")).ToString(); 
                        lblGanhadoresQuadra.Text = item3.NumeroGanhadores.ToString();


                    }

                    if (item3.Faixa == "Terno")
                    {
                        lblTerno.Text = item3.Valor.ToString("#,0.00", new CultureInfo("pt-BR")).ToString();
                        lblTernoGanhadores.Text = item3.NumeroGanhadores.ToString();

                    }

                    if (item3.Faixa == "Duque")
                    {
                        lblDuqueValor.Text = item3.Valor.ToString("#,0.00", new CultureInfo("pt-BR")).ToString();
                        lblGanhadoresDuque.Text = item3.NumeroGanhadores.ToString();

                    }


                }


            }




            ListaResultadoQuina quina = this.contaAcertos(this.numerosSort);
            string str = "";
            int index = 0;
            this.numerosSort.Sort();
            foreach (int num6 in this.numerosSort)
            {
                this.ordemSorteioQuina[index] = num6;
                str = str + num6 + " - ";
                int num2 = index;
                index = num2 + 1;
            }
            this.lblNumberOne.Text = this.ordemSorteioQuina[0].ToString().PadLeft(2, '0');
            this.lblNumberTwo.Text = this.ordemSorteioQuina[1].ToString().PadLeft(2, '0');
            this.lblNumberTree.Text = this.ordemSorteioQuina[2].ToString().PadLeft(2, '0');
            this.lblNumberFor.Text = this.ordemSorteioQuina[3].ToString().PadLeft(2, '0');
            this.lblNumberFive.Text = this.ordemSorteioQuina[4].ToString().PadLeft(2, '0');
            this.GridView1.DataSource = quina;
            this.GridView1.DataBind();
            this.preencheConfig();
            this.carregaXMLGridview();
        }

   



        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["changeConcurso"] = true;

            loadPage("D");
        }
    }
}