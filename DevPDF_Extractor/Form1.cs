using DevExpress.XtraEditors;
using DevPDF_Extractor.Classe;
using Patagames.Pdf.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPDF_Extractor
{
    public partial class Form1 : XtraForm
    {
        public Form1()
        {
            InitializeComponent();

          


        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dg2 = new OpenFileDialog();
            dg2.ShowDialog();
            if (dg2.FileName != "")
            {
                boxPathFile.Text = dg2.FileName;
            }
            else
            {
                boxPathFile.Text = "";
            }

            string tipoDoc = checkTipoDocumento();
            boxTipoDocumento.Text = tipoDoc;
            lblTitolo.Text = tipoDoc;

            loadDocument();
            try
            {
                gridView1.RestoreLayoutFromXml("gridRisultati.xml");
            }
            catch { }
        }
        PdfDocument DOC;
       
        private void btnStartAnalize_Click(object sender, EventArgs e)
        {
       
         

        }

        public void CastToMyType<T>(object givenObject) where T : class
        {
            var newObject = givenObject as T;
        }

        private string checkTipoDocumento()
        {
             string path = AppDomain.CurrentDomain.BaseDirectory + "pdfium.dll";
            PdfCommon.Initialize("52433553494d50032923be84e16cd6ae0bce153446af7918d52303038286fd2baf0f9516e774720322fc372987e3ec353bbb19220ab7f7ee7334cb60480abf2e9c5aa5b8eaeecd6d66111ff5ffcb606885c4287b9d7fcac4e6c42377d5c045d345f1fd8182cd458f8c3e2fb6a67ca5796e46805d77fc17db", path);
            // PdfCommon.Initialize();

            OpenFileDialog dg = new OpenFileDialog();

            DOC = PdfDocument.Load(boxPathFile.Text, null, "");


            DataTable tb = reader.getDataFromSQL("SELECT * from Fields WHERE DeterminaTipo='true'");
            if(tb.Rows.Count>0)
            {
                foreach (DataRow r in tb.Rows)
                {
                    int L = Convert.ToInt32(r["L"]);
                    int T = Convert.ToInt32(r["T"]);
                    int B = Convert.ToInt32(r["B"]);
                    int R = Convert.ToInt32(r["R"]);

                    string testoTrovato = DOC.Pages[0].Text.GetBoundedText(L, T, R, B);

                    if(r["TipoDocumento"].ToString()== "PALLEX" && testoTrovato.Contains("Lettera"))
                    {
                        return r["TipoDocumento"].ToString();
                    }
                    if (r["TipoDocumento"].ToString() == "PREGNO" && testoTrovato.Contains("Bollettino"))
                    {
                        return r["TipoDocumento"].ToString();
                    }
                    if (r["TipoDocumento"].ToString() == "PALLEX_RITIRO" && testoTrovato.Contains("Ritiro"))
                    {
                        return r["TipoDocumento"].ToString();
                    }
                    if (r["TipoDocumento"].ToString() == "PREGNO_RITIRO" && testoTrovato.ToUpper().Contains("RITIRO"))
                    {
                        return r["TipoDocumento"].ToString();
                    }

                   
                }
                
            }

            return "";
        }
        private void loadDocument()
        {

         


            string path = AppDomain.CurrentDomain.BaseDirectory + "pdfium.dll";
            PdfCommon.Initialize("52433553494d50032923be84e16cd6ae0bce153446af7918d52303038286fd2baf0f9516e774720322fc372987e3ec353bbb19220ab7f7ee7334cb60480abf2e9c5aa5b8eaeecd6d66111ff5ffcb606885c4287b9d7fcac4e6c42377d5c045d345f1fd8182cd458f8c3e2fb6a67ca5796e46805d77fc17db", path);
            // PdfCommon.Initialize();

            OpenFileDialog dg = new OpenFileDialog();

            DOC = PdfDocument.Load(boxPathFile.Text, null, "");


            refreshFields(boxTipoDocumento.Text);


            DataTable tabella = new DataTable();
            tabella.Columns.Add("Commessa", typeof(string));
            tabella.Columns.Add("Progr", typeof(int));
            tabella.Columns.Add("Qta", typeof(int));
            tabella.Columns.Add("DescrizioneArticolo", typeof(string));
            tabella.Columns.Add("TipoArticolo1", typeof(string));
            tabella.Columns.Add("TipoArticolo2", typeof(string));
            foreach (DataRow r in fieldsTable.Rows)
            {

                
                Type tipo = Type.GetType(r["FieldType"].ToString());
                tabella.Columns.Add(r["FieldName"].ToString(), tipo);

             

            }




            // DataTable tabellaCarte = proc.getCarteDelPeriodo("15/06/2016", "30/06/2016");

            List<string> tutteLeRigheDelDoc = new List<string>();

            int pgNumber = 0;
            foreach (PdfPage pag in DOC.Pages)
            {
                string stringSeparators = "\r\n";
                string TESTOPAGINA = pag.Text.GetText(0, pag.Text.CountChars);
                TESTOPAGINA = TESTOPAGINA.Replace(stringSeparators, "+");
                TESTOPAGINA = TESTOPAGINA.Replace("\n", "+");

                string[] testo = TESTOPAGINA.Split('+');

                //foreach (string riga in testo)
                //{
                //    tutteLeRigheDelDoc.Add(riga);
                //}

                DataRow doc = tabella.Rows.Add();
                doc["Progr"] = pgNumber+1;
                doc["Commessa"] = boxTipoDocumento.Text;
                int variabilaUpDown = 0;

                if (boxTipoDocumento.Text == "PREGNO_RITIRO")
                {

                    string testoTotaleColli= DOC.Pages[pgNumber].Text.GetBoundedText(0, 470, 88, 465);

                    try { int numeroColli = Convert.ToInt32(testoTotaleColli); }
                    catch 
                    {
                        variabilaUpDown = 10;
                    }


                }


                foreach (DataRow r in fieldsTable.Rows)
                {

                    string field = r["FieldName"].ToString();

                    if (field == "TotalePeso" && variabilaUpDown > 0)
                    { 
                    }
                    Type tipo = Type.GetType(r["FieldType"].ToString());
                    //tabella.Columns.Add(r["FieldName"].ToString(), tipo);

                    int L = Convert.ToInt32(r["L"]);
                    int T = Convert.ToInt32(r["T"]);
                    int B = Convert.ToInt32(r["B"]);
                    int R = Convert.ToInt32(r["R"]);


                    bool MoveDownWithVariable = Convert.ToBoolean(r["MoveDownWithVariable"]);


                    if (MoveDownWithVariable && variabilaUpDown > 0)
                    {
                        T -= variabilaUpDown;
                        B -= variabilaUpDown;
                    }

                    if (r["DopoStringa"] != DBNull.Value && r["DopoStringa"].ToString() != "")
                    {
                        string DopoStringa = "";
                        string PrimaDellaStringa = "";

                        try { DopoStringa = r["DopoStringa"].ToString(); }
                        catch { }

                        try { PrimaDellaStringa = r["PrimaDellaStringa"].ToString(); }
                        catch { }

                        if (L + T + B + R > 0)
                        {
                            try
                            {
                                string testoTrovato = DOC.Pages[pgNumber].Text.GetBoundedText(L, T, R, B);

                                if (DopoStringa != "")
                                {
                                    try
                                    {
                                        testoTrovato = testoTrovato.Substring(testoTrovato.IndexOf(DopoStringa) + DopoStringa.Length).Trim();
                                    }
                                    catch { }
                                }

                                if (PrimaDellaStringa != "")
                                {
                                    try
                                    {
                                        testoTrovato = testoTrovato.Substring(0,testoTrovato.IndexOf(PrimaDellaStringa)).Trim();
                                    }
                                    catch { }
                                }



                                if (r["FieldType"].ToString() == "System.String")
                                    doc[r["FieldName"].ToString()] = testoTrovato.Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim(' ');
                                else

                                if (r["FieldType"].ToString() == "System.Int32")
                                {
                                    testoTrovato = testoTrovato.Replace(".", "");
                                    doc[r["FieldName"].ToString()] = Convert.ToInt32(testoTrovato);
                                }

                                if (r["FieldType"].ToString() == "System.DateTime")
                                    doc[r["FieldName"].ToString()] = Convert.ToDateTime(testoTrovato);

                            }
                            catch { }
                        }
                        else
                        {
                            doc[r["FieldName"].ToString()] = extractTextFromPageBetweenFields(pag, DopoStringa, PrimaDellaStringa);
                        }
                    }
                    else
                    {
                       
                        try
                        {
                            string testoTrovato = DOC.Pages[pgNumber].Text.GetBoundedText(L, T, R, B);
                            testoTrovato = testoTrovato.Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim(' ');

                            if (field == "ServiziAccessori")
                            {
                            }


                            if (r["FieldType"].ToString() == "System.String")
                                doc[r["FieldName"].ToString()] = testoTrovato.Replace("\r\n", " ");
                            else

                            if (r["FieldType"].ToString() == "System.Int32")
                            {
                                //testoTrovato = testoTrovato.Replace(".", "").Replace(",","");
                                decimal v = Convert.ToInt32(Convert.ToDecimal(testoTrovato.Replace(".", "").Replace(" Kg","")));
                                doc[r["FieldName"].ToString()] = Convert.ToInt32(v);
                            }

                            if (r["FieldType"].ToString() == "System.DateTime")
                                doc[r["FieldName"].ToString()] = Convert.ToDateTime(testoTrovato);
                           
                        }
                        catch { }
                    }

                }

                pgNumber++;
            }


            string numeroCarta = "";
            string tipoCarta = "";




            //int progress = 0;
            //progressBar1.Value = 0;
            //progressBar1.Maximum = tutteLeRigheDelDoc.Count;
            //for (int i = 0; i < tutteLeRigheDelDoc.Count; i++)
            //{
            //    string riga = tutteLeRigheDelDoc[i];
            //    progress++;
            //    progressBar1.Value = progress;
            //    Application.DoEvents();




            //}





                ;



                gridControl1.DataSource = workTable(tabella, boxTipoDocumento.Text);
        }

        private DataTable workTable(DataTable tabella, string tipoDoc)
        {

            DataTable tbNew = tabella.Clone();

            foreach (DataRow r in tabella.Rows)
            {


                if (tipoDoc == "PALLEX")
                {
                    string arts1 = "";

                    if (r["DettaglioPezzi1"] != DBNull.Value && r["DettaglioPezzi1"].ToString() != "")
                    {
                       
                        arts1 = Convert.ToString(r["DettaglioPezzi1"].ToString());
                        while (arts1 != "")
                        {
                            int qta = Convert.ToInt32(arts1.Split(' ')[0].Replace("nr.", ""));

                            DataRow addedRow = tbNew.Rows.Add(r.ItemArray);


                            int indexFirstSpace = arts1.IndexOf(" ");

                            int indexParRotondaAperta = arts1.IndexOf("(");
                            int indexParRotondaChiusa = arts1.IndexOf(")");

                            int indexParQuadrataAperta = arts1.IndexOf("[");
                            int indexParuadrataChiusa = arts1.IndexOf("]");

                            addedRow["Qta"] = qta;

                            addedRow["DescrizioneArticolo"] = arts1.Substring(indexFirstSpace + 1, indexParRotondaAperta - indexFirstSpace - 1);
                            if (addedRow["DescrizioneArticolo"].ToString() == "") addedRow["DescrizioneArticolo"] = "GENERICO";

                            if (indexParRotondaAperta >= 0 && indexParRotondaChiusa >= 0) addedRow["TipoArticolo1"] = arts1.Substring(indexParRotondaAperta + 1, indexParRotondaChiusa - indexParRotondaAperta - 1);

                            if (indexParQuadrataAperta >= 0 && indexParuadrataChiusa >= 0) addedRow["TipoArticolo2"] = arts1.Substring(indexParQuadrataAperta + 1, indexParuadrataChiusa - indexParQuadrataAperta - 1);

                            arts1 = arts1.Substring(3);
                            try
                            {
                                arts1 = Convert.ToString(arts1.Substring(arts1.IndexOf("nr.")));
                            }
                            catch { arts1 = ""; }
                        }

                    }

                    string arts2 = "";
                    if (r["DettaglioPezzi2"] != DBNull.Value && r["DettaglioPezzi2"].ToString() != "")
                    {


                        arts2 = Convert.ToString(r["DettaglioPezzi2"].ToString());
                        while (arts2 != "")
                        {
                            int qta = 1;
                            try
                            {
                                qta = Convert.ToInt32(arts2.Split(' ')[0].Replace("nr.", ""));
                            }
                            catch { MessageBox.Show("errore su qta pagina:" + r["Progr"].ToString()); }

                            DataRow addedRow = tbNew.Rows.Add(r.ItemArray);


                            int indexFirstSpace = arts2.IndexOf(" ");

                            int indexParRotondaAperta = arts2.IndexOf("(");
                            int indexParRotondaChiusa = arts2.IndexOf(")");

                            int indexParQuadrataAperta = arts2.IndexOf("[");
                            int indexParuadrataChiusa = arts2.IndexOf("]");

                            addedRow["Qta"] = qta;

                            addedRow["DescrizioneArticolo"] = arts2.Substring(indexFirstSpace + 1, indexParRotondaAperta - indexFirstSpace - 1);
                            if (addedRow["DescrizioneArticolo"].ToString() == "") addedRow["DescrizioneArticolo"] = "GENERICO";

                            if (indexParRotondaAperta >= 0 && indexParRotondaChiusa >= 0)
                            {

                                addedRow["TipoArticolo1"] = arts2.Substring(indexParRotondaAperta + 1, indexParRotondaChiusa - indexParRotondaAperta - 1);
                            }

                            if (indexParQuadrataAperta >= 0 && indexParuadrataChiusa >= 0) addedRow["TipoArticolo2"] = arts2.Substring(indexParQuadrataAperta + 1, indexParuadrataChiusa - indexParQuadrataAperta - 1);


                            arts2 = arts2.Substring(3);
                            try
                            {
                                arts2 = Convert.ToString(arts2.Substring(arts2.IndexOf("nr.")));
                            }
                            catch { arts2 = ""; }
                        }
                    }
                    string articoli = arts1 + "+" + arts2;



                    r["DettaglioPezzi1"] = articoli;
                    r["DettaglioPezzi2"] = "";
                }
                else
                    
                if (tipoDoc == "PREGNO")
                {
                    if (r["DettaglioPezzi1"] != DBNull.Value && r["DettaglioPezzi1"].ToString() != "")
                    {

                        string OriginalString = r["DettaglioPezzi1"].ToString().Trim();


                        List<string> listaTipoArticoli = Regex.Replace(r["DettaglioPezzi1"].ToString(), @"[\d-]", string.Empty).Replace(",", "").Split(' ').Where(T => T.Length > 0).ToList();
                        int counter = listaTipoArticoli.Count();
                        string[] allfields = OriginalString.Split(' ').ToArray();


                  

                        var matrix = ConvertMatrix(OriginalString.Split(' ').ToArray(), counter, Convert.ToInt32(allfields.Count()/ counter));


                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            try
                            {
                                string Art = matrix[i, 0];
                                string Qta = matrix[i, 1];

                                string DescrizioneArticolo = "";
                                string Peso = "";
                                try
                                {
                                    string Lungh = matrix[i, 2];
                                    DescrizioneArticolo = Lungh.Replace(",00","") + "x";
                                }
                                catch { }

                                try
                                {
                                    string Largh = matrix[i, 3];
                                    DescrizioneArticolo += Largh.Replace(",00", "") + "x";
                                }
                                catch { }

                                try
                                {
                                    string Alt = matrix[i, 4];
                                    DescrizioneArticolo += Alt.Replace(",00", "");
                                }
                                catch
                                { }

                                try
                                {
                                    string pes = matrix[i, 5];
                                    Peso = pes + " Kg";
                                }
                                catch { }

                                DataRow addedRow = tbNew.Rows.Add(r.ItemArray);
                                addedRow["Qta"] = Qta;
                                addedRow["DescrizioneArticolo"] = Art;
                                addedRow["TipoArticolo1"] = DescrizioneArticolo;
                                addedRow["TipoArticolo2"] = Peso;
                            }
                            catch { }
                        }
                        
                        //int pos = 1;
                        //foreach (string art in listaTipoArticoli)
                        //{
                        //    string tipoArticolo = OriginalString.Split(' ')[pos];

                        //    string descrizione = OriginalString.Split(' ')[pos+counter*pos];
                        //    string descrizione2 = OriginalString.Split(' ')[pos+ counter * pos];
                        //}



                    }
                   
                }
                else
                      
                if (tipoDoc == "PALLEX_RITIRO")
                {
                    string arts1 = "";

                    if (r["DettaglioPezzi1"] != DBNull.Value && r["DettaglioPezzi1"].ToString() != "")
                    {

                        arts1 = Convert.ToString(r["DettaglioPezzi1"].ToString());
                        while (arts1 != "")
                        {
                            int qta = Convert.ToInt32(arts1.Split(' ')[0].Replace("nr.", ""));

                            DataRow addedRow = tbNew.Rows.Add(r.ItemArray);


                            int indexFirstSpace = arts1.IndexOf(" ");

                            int indexParRotondaAperta = arts1.IndexOf("(");
                            int indexParRotondaChiusa = arts1.IndexOf(")");

                            int indexParQuadrataAperta = arts1.IndexOf("[");
                            int indexParuadrataChiusa = arts1.IndexOf("]");

                            addedRow["Qta"] = qta;

                            addedRow["DescrizioneArticolo"] = arts1.Substring(indexFirstSpace + 1, indexParRotondaAperta - indexFirstSpace - 1);
                            if (addedRow["DescrizioneArticolo"].ToString() == "") addedRow["DescrizioneArticolo"] = "GENERICO";

                            if (indexParRotondaAperta >= 0 && indexParRotondaChiusa >= 0) addedRow["TipoArticolo1"] = arts1.Substring(indexParRotondaAperta + 1, indexParRotondaChiusa - indexParRotondaAperta - 1);

                            if (indexParQuadrataAperta >= 0 && indexParuadrataChiusa >= 0) addedRow["TipoArticolo2"] = arts1.Substring(indexParQuadrataAperta + 1, indexParuadrataChiusa - indexParQuadrataAperta - 1);

                            arts1 = arts1.Substring(3);
                            try
                            {
                                arts1 = Convert.ToString(arts1.Substring(arts1.IndexOf("nr.")));
                            }
                            catch { arts1 = ""; }
                        }

                    }

                    string arts2 = "";
                    if (r["DettaglioPezzi2"] != DBNull.Value && r["DettaglioPezzi2"].ToString() != "")
                    {


                        arts2 = Convert.ToString(r["DettaglioPezzi2"].ToString());
                        while (arts2 != "")
                        {
                            int qta = Convert.ToInt32(arts2.Split(' ')[0].Replace("nr.", ""));

                            DataRow addedRow = tbNew.Rows.Add(r.ItemArray);


                            int indexFirstSpace = arts2.IndexOf(" ");

                            int indexParRotondaAperta = arts2.IndexOf("(");
                            int indexParRotondaChiusa = arts2.IndexOf(")");

                            int indexParQuadrataAperta = arts2.IndexOf("[");
                            int indexParuadrataChiusa = arts2.IndexOf("]");

                            addedRow["Qta"] = qta;

                            addedRow["DescrizioneArticolo"] = arts2.Substring(indexFirstSpace + 1, indexParRotondaAperta - indexFirstSpace - 1);
                            if (addedRow["DescrizioneArticolo"].ToString() == "") addedRow["DescrizioneArticolo"] = "GENERICO";

                            if (indexParRotondaAperta >= 0 && indexParRotondaChiusa >= 0)
                            {

                                addedRow["TipoArticolo1"] = arts2.Substring(indexParRotondaAperta + 1, indexParRotondaChiusa - indexParRotondaAperta - 1);
                            }

                            if (indexParQuadrataAperta >= 0 && indexParuadrataChiusa >= 0) addedRow["TipoArticolo2"] = arts2.Substring(indexParQuadrataAperta + 1, indexParuadrataChiusa - indexParQuadrataAperta - 1);


                            arts2 = arts2.Substring(3);
                            try
                            {
                                arts2 = Convert.ToString(arts2.Substring(arts2.IndexOf("nr.")));
                            }
                            catch { arts2 = ""; }
                        }
                    }
                    string articoli = arts1 + "+" + arts2;



                    r["DettaglioPezzi1"] = articoli;
                    r["DettaglioPezzi2"] = "";
                }
                if (tipoDoc == "PREGNO_RITIRO")
                {


               

                    if (r["DettaglioPezzi1"] != DBNull.Value 
                        && r["DettaglioPezzi1"].ToString()!="")
                    {

                        string OriginalString = r["DettaglioPezzi1"].ToString().Trim();
                        OriginalString = OriginalString.Replace("Quantità", "");
                        OriginalString = OriginalString.Replace("Codice", "");
                        OriginalString = OriginalString.Replace("Descrizione", "");
                        OriginalString = OriginalString.Replace("  ", " ");

                        string[] array = OriginalString.Split(' ');


                        OriginalString = array[2] + " " + array[3] + " " + array[6] + " " + array[4] + " " + array[0] + " " + array[1] + " " + array[7] + " " + array[5];



                        List<string> listaTipoArticoli = Regex.Replace(r["DettaglioPezzi1"].ToString(), @"[\d-]", string.Empty).Replace(",", "").Split(' ').Where(T => T.Length > 0).ToList();
                        int counter = listaTipoArticoli.Count();
                        string[] allfields = OriginalString.Split(' ').ToArray();





                        string Art = allfields[1];
                        string Qta = allfields[6];

                        string DescrizioneArticolo = "";
                        string Peso = "";
                        try
                        {
                            string Lungh = allfields[3];
                            DescrizioneArticolo = Lungh.Replace(",00", "") + "x";
                        }
                        catch { }

                        try
                        {
                            string Largh = allfields[4];
                            DescrizioneArticolo += Largh.Replace(",00", "") + "x";
                        }
                        catch { }

                        try
                        {
                            string Alt = allfields[5];
                            DescrizioneArticolo += Alt.Replace(",00", "");
                        }
                        catch
                        { }

                        try
                        {
                            string pes = allfields[7];
                            Peso = pes + " Kg";
                        }
                        catch { }

                        DataRow addedRow = tbNew.Rows.Add(r.ItemArray);
                        try
                        {
                            addedRow["Qta"] = Qta;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Errore alla caricamento pagina " + r["Progr"].ToString());

                        }
                        addedRow["DescrizioneArticolo"] = Art;
                        addedRow["TipoArticolo1"] = DescrizioneArticolo;
                        addedRow["TipoArticolo2"] = Peso;
                        //var matrix = ConvertMatrix(OriginalString.Split(' ').ToArray(), counter, Convert.ToInt32(allfields.Count() / counter));


                        //for (int i = 0; i < matrix.GetLength(0); i++)
                        //{
                        //    try
                        //    {
                        //        string Art = matrix[i, 0];
                        //        string Qta = matrix[i, 1];

                        //        string DescrizioneArticolo = "";
                        //        string Peso = "";
                        //        try
                        //        {
                        //            string Lungh = matrix[i, 2];
                        //            DescrizioneArticolo = Lungh.Replace(",00", "") + "x";
                        //        }
                        //        catch { }

                        //        try
                        //        {
                        //            string Largh = matrix[i, 3];
                        //            DescrizioneArticolo += Largh.Replace(",00", "") + "x";
                        //        }
                        //        catch { }

                        //        try
                        //        {
                        //            string Alt = matrix[i, 4];
                        //            DescrizioneArticolo += Alt.Replace(",00", "");
                        //        }
                        //        catch
                        //        { }

                        //        try
                        //        {
                        //            string pes = matrix[i, 5];
                        //            Peso = pes + " Kg";
                        //        }
                        //        catch { }

                        //        DataRow addedRow = tbNew.Rows.Add(r.ItemArray);
                        //        addedRow["Qta"] = Qta;
                        //        addedRow["DescrizioneArticolo"] = Art;
                        //        addedRow["TipoArticolo1"] = DescrizioneArticolo;
                        //        addedRow["TipoArticolo2"] = Peso;
                        //    }
                        //    catch { }
                        //}

                        //int pos = 1;
                        //foreach (string art in listaTipoArticoli)
                        //{
                        //    string tipoArticolo = OriginalString.Split(' ')[pos];

                        //    string descrizione = OriginalString.Split(' ')[pos+counter*pos];
                        //    string descrizione2 = OriginalString.Split(' ')[pos+ counter * pos];
                        //}



                    }

                   // DataRow addedRow2 = tbNew.Rows.Add(r.ItemArray);

                }
                
            }

            return tbNew;
        }

        static string[,] ConvertMatrix(string[] flat, int m, int n)
        {

            string[,] resultat = new string[m, n];
            int pos = 0;

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < m; i++)
                {
                    resultat[i, j] = flat[pos];
                    pos++;
                }

            }

            return resultat;


        }

        static string TrimSuffix(string word)
        {
            int apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
            {
                word = word.Substring(0, apostropheLocation);
            }

            return word;
        }
        static string[] GetWords(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value)
                        select TrimSuffix(m.Value);

            return words.ToArray();
        }
        private void analizzaPagina(string[] testo)
        {
            foreach (string riga in testo)
            {
                
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void numPage_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (DOC != null)
                {

                    boxTuttoTesto.Text = DOC.Pages[Convert.ToInt32(numPage.Value) - 1].Text.GetText(0, DOC.Pages[Convert.ToInt32(numPage.Value) - 1].Text.CountChars);
                    boxRisultatoAnteprima.Text = DOC.Pages[Convert.ToInt32(numPage.Value)-1].Text.GetBoundedText((float)boxLeft.Value, (float)boxTop.Value, (float)boxRight.Value, (float)boxBottom.Value);
                }

            }
            catch { }
        }
        Classe.DataReaderSqlExpress reader = new Classe.DataReaderSqlExpress();
        DataTable fieldsTable = new DataTable();
        private void refreshFields(string tipoDoc)
        {
            fieldsTable = reader.getDataFromSQL("SELECT * from Fields WHERE TipoDocumento='"+ tipoDoc + "'");

            gridControlFields.DataSource = fieldsTable;

            lblIDCampo.Text = "";
            boxFieldName.Text = "";
            boxFieldType.Text = "System.String";


            btnAnnulla.Visible = false;
            btnAddField.Text = "Salva nuovo campo";
        }
        private void btnAddField_Click(object sender, EventArgs e)
        {
            saveField();

            refreshFields(boxTipoDocumento.Text);
        }

        private void saveField()
        {
            //if (tbControlTipoSalvataggio.SelectedTabPage == pagCoordinate)
            //{
            //    boxTestoDa.Text = "";
            //    boxTestoA.Text = "";
            //}
            //else
            //{
            //    boxLeft.Text = "0";
            //    boxTop.Text = "0";
            //    boxRight.Text = "0";
            //    boxBottom.Text = "0";
            //}



            string q = "";
            if (lblIDCampo.Text == "")
            {
                q = "Insert into Fields (TipoDocumento,FieldName,FieldType,Descrizione,L,T,R,B,DopoStringa,PrimaDellaStringa) values (\"@TipoDocumento@\",\"@FieldName@\",\"@FieldType@\",\"@Descrizione@\",\"@L@\",\"@T@\",\"@R@\",\"@B@\",\"@DopoStringa@\",\"@PrimaDellaStringa@\")";

            }

            else
            {
                q = "Update Fields set TipoDocumento=\"@TipoDocumento@\",FieldName=\"@FieldName@\",FieldType=\"@FieldType@\",Descrizione=\"@Descrizione@\",L=\"@L@\",T=\"@T@\",R=\"@R@\",B=\"@B@\",DopoStringa=\"@DopoStringa@\",PrimaDellaStringa=\"@PrimaDellaStringa@\" WHERE ID=@ID@";
            }
            q = q.Replace("@TipoDocumento@", boxTipoDocumento.Text);
            q = q.Replace("@FieldName@", boxFieldName.Text);
            q = q.Replace("@Descrizione@", boxDescrizione.Text);
            q = q.Replace("@L@", boxLeft.Text.Replace(",","."));
            q = q.Replace("@T@", boxTop.Text.Replace(",", "."));
            q = q.Replace("@R@", boxRight.Text.Replace(",", "."));
            q = q.Replace("@B@", boxBottom.Text.Replace(",", "."));
            q = q.Replace("@FieldType@", boxFieldType.Text);
            
            q = q.Replace("@DopoStringa@", boxTestoDa.Text);
            q = q.Replace("@PrimaDellaStringa@", boxTestoA.Text);

            q = q.Replace("@ID@", lblIDCampo.Text);

            reader.executeQuery(q);


        }

        private string extractTextFromPageBetweenFields(PdfPage pag, string testoDa, string testoA)
        {
            string stringSeparators = "\r\n";
            string TESTOPAGINA = pag.Text.GetText(0, pag.Text.CountChars);
            TESTOPAGINA = TESTOPAGINA.Replace(stringSeparators, "+");
            TESTOPAGINA = TESTOPAGINA.Replace("\n", "+");

            string[] testo = TESTOPAGINA.Split('+');

            string risultato = "";
            string primaRiga = "";


           
            for (int i=0;i<testo.Count();i++)
            {
                string rigaTesto = testo[i];

                if(rigaTesto.Contains(testoDa))
                {
                    if (testoA == "")
                    {
                        primaRiga = testo[i + 1];
                        return primaRiga;
                    }
                 
                    for (int j = i + 1; j < testo.Count() - 1; j++)
                    {
                      
                        string rigaTestoDaMettere = testo[j];

                        if (rigaTestoDaMettere.Contains(testoA))

                        {
                            return risultato;
                        }
                   
                        risultato += rigaTestoDaMettere+" ";

                        
                        
                    }



                    risultato = primaRiga;
                    break;
                }
                
            }

            return risultato;
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            PdfPage p = DOC.Pages[Convert.ToInt32(numPage.Value)-1];
            if (checkKeepCoordinatesAlso.Checked)
            {
             
                string testoTrovato = DOC.Pages[Convert.ToInt32(numPage.Value) - 1].Text.GetBoundedText((float)boxLeft.Value, (float)boxTop.Value, (float)boxRight.Value, (float)boxBottom.Value);

                string DopoStringa = "";
                string PrimaDellaStringa = "";

                try { DopoStringa = boxTestoDa.Text; } catch { }
                try { PrimaDellaStringa = boxTestoA.Text; } catch { }

                if (DopoStringa != "")
                {
                    try
                    {
                        testoTrovato = testoTrovato.Substring(testoTrovato.IndexOf(DopoStringa)+ DopoStringa.Length).Trim();
                    }
                    catch { }
                }

                if (PrimaDellaStringa != "")
                {
                    try
                    {
                        testoTrovato = testoTrovato.Substring(0, testoTrovato.IndexOf(PrimaDellaStringa)).Trim();
                    }
                    catch { }
                }
                boxRisultatoAnteprima.Text = testoTrovato;
            }
            else
            {
                boxRisultatoAnteprima.Text = extractTextFromPageBetweenFields(p, boxTestoDa.Text, boxTestoA.Text);
            }
        }

        private void dgvFields_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            boxLeft.Text = dgvFields.GetRowCellValue(e.FocusedRowHandle, "L").ToString();
            boxTop.Text = dgvFields.GetRowCellValue(e.FocusedRowHandle, "T").ToString();
            boxRight.Text = dgvFields.GetRowCellValue(e.FocusedRowHandle, "R").ToString();
            boxBottom.Text = dgvFields.GetRowCellValue(e.FocusedRowHandle, "B").ToString();

            lblIDCampo.Text = dgvFields.GetRowCellValue(e.FocusedRowHandle, "ID").ToString();
            boxFieldName.Text = dgvFields.GetRowCellValue(e.FocusedRowHandle, "FieldName").ToString();
            boxFieldType.Text = dgvFields.GetRowCellValue(e.FocusedRowHandle, "FieldType").ToString();

            btnAddField.Text = "Salva modifica campo";
            btnAnnulla.Visible = true;
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            lblIDCampo.Text = "";
            boxFieldName.Text = "";
            boxFieldType.Text = "System.String";


            btnAnnulla.Visible = false;
            btnAddField.Text = "Salva nuovo campo";
        }

        private void btnStartAnalizzaPREGNO_Click(object sender, EventArgs e)
        {
            loadDocument();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            //gridView1.OptionsPrint.AutoWidth = true;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";

            saveFileDialog.FilterIndex = 0;

            saveFileDialog.RestoreDirectory = true;

            saveFileDialog.CreatePrompt = true;

            saveFileDialog.Title = "Esporta come file excel ... ";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridView1.ExportToXlsx(saveFileDialog.FileName);
            }
        }

        private void btnSalvaLayout_Click(object sender, EventArgs e)
        {
            gridView1.SaveLayoutToXml("gridRisultati.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            configClass config = new configClass();
            textEditSqlStringConnection.Text = config.connectionStringSQLEXPRESS;

        }

        private void boxTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshFields(boxTipoDocumento.Text);
        }
    }
}

