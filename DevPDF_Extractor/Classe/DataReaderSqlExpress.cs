using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace DevPDF_Extractor.Classe
{
    public static class Layer
    {
      
        public static string googleKey = "";//"AIzaSyAOLJnmdlVvnvpGTOkyYsAMONS8jls3JS0";

        // public static string connStringXPO= System.Configuration.ConfigurationManager.ConnectionStrings["ReleasedDBXPO"].ConnectionString;
  
        //public static string connStringSQlite = System.Configuration.ConfigurationManager.ConnectionStrings["ReleasedDBSQlite"].ConnectionString;
        public static string ftpUser = "";// "0000222700";
        public static string ftpPass = "";//"xasu2RasuFra";
        public static string NumTelefonoUtilizzato = "";
        public static string NumTelefonoDefault = "";// "05211523414";
        public static bool sendSMS = true;
        public static string PrefissoFile = "";



        public static string smsUserSkebby = "";
        public static string smsPassSkeby = "";
        public static string mailNoReply = "";
        public static string passNoReply = "";
        public static string portaNoReply = "";
        public static string smtpNoReply = "";
      
        public static string normalize(string input)
        {
            // Strip letters for tel: protocol
            if (input.StartsWith("tel:"))
                input = input.Substring(4);

            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (Char.IsLetter(c))
                {
                    switch (new string(c, 1).ToUpper())
                    {
                        case "A": // fall down
                        case "B": // fall down
                        case "C": sb.Append('2'); break;
                        case "D": // fall down
                        case "E": // fall down
                        case "F": sb.Append('3'); break;
                        case "G": // fall down
                        case "H": // fall down
                        case "I": sb.Append('4'); break;
                        case "J": // fall down
                        case "K": // fall down
                        case "L": sb.Append('5'); break;
                        case "M": // fall down
                        case "N": // fall down
                        case "O": sb.Append('6'); break;
                        case "P": // fall down
                        case "Q": // fall down
                        case "R": // fall down
                        case "S": sb.Append('7'); break;
                        case "T": // fall down
                        case "U": // fall down
                        case "V": sb.Append('8'); break;
                        case "W": // fall down
                        case "X": // fall down
                        case "Y": // fall down
                        case "Z": sb.Append('9'); break;
                    }
                }
                else if (Char.IsDigit(c) || c == '+' || c == '#' || c == '*')
                    sb.Append(c);
            }
            return "+39" + sb.ToString();
        }

  
     
    }
    class configClass
    {
        public string Server_SQL_EXPRESS;
        public string Nome_Sql_Instance;
        public string Tipo_authentificazione;
        public string Utente_server;
        public string Password_server;
        public string DATABASE;
        public string Utente_accesso_DB;
        public string Password_DB;
        public bool isTrustedConnection;
        public bool IsLocalServer;

        public bool securizzatoProgramma = false;
        public string usernameProgramma = "";
        public string passwordProgramma = "";
        public string mailRegistrazioneUtenteProgramma = "";

        public string ACCESSDATABASELOCALEPATH = "";
        public string ACCESSDATABASEPASSWORD = "";

        public string MYSQLSERVER = "";
        public string MYSQLDATABASE = "";
        public string MYSQLUSERNAME = "";
        public string MYSQLPASSWORD = "";

        public string connectionStringACCESS = "";
        public string connectionStringMYSQL;
        public string connectionStringSQLEXPRESS;

        public string DirectoryPath = "";
        public string ApplicationPath = "";
        public string BackupsPATH = "";
        public string TempFilesPATH = "";
        public string RapportiPDFPATH = "";


        public string EmailRisposta = "";
        public string Smtp = "";
        public int Porta = 21;
        public bool enableSSL;
        public string CredentialsUsername = "";
        public string CredentialsPassword = "";



        public string FTPServer = "";
        public int FTPPorta = 21;
        public string FTPUser = "";
        public string FTPPass = "";
        public string FTPPathAggiornamenti = "";
        public string FTPPath = "";
        public string FTPPathKitInstallazione = "";





        private string[] configStringArray;
        private string returnValueFromConfig(string[] array, string field)
        {
            string valoareGasita = "";
            bool found = false;
            foreach (string val in array)
            {
                if (val.Contains(field + "=") && val.IndexOf(field) == 0)
                {
                    found = true;
                    valoareGasita = val.Substring(val.IndexOf("=") + 1);
                }
            }
            if (found) return valoareGasita.TrimEnd('\r');
            else return "";
        }

        string PATH = "";
        private void loadConfig(string path)
        {
            PATH = path;
            string dirPathOfApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            String configString = "";
            path = dirPathOfApplication + "\\" + path;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    configString += sr.ReadToEnd();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: leggere file config" + ex.ToString());
            }


            //String configStringGood = SSTCryptographer.Decrypt(configString, "123");
            //if (configStringGood.Contains("Wrong Input"))
            //{
            //    configStringGood = configString;
            //}

            String configStringGood = configString;
            configStringArray = Regex.Split(configStringGood, "\n");


        }

        public string writeConfig()
        {
            string text = "";

            text += "Server_SQL_EXPRESS=" + Server_SQL_EXPRESS.ToString() + "\r\n";
            text += "Nome_Sql_Instance=" + Nome_Sql_Instance.ToString() + "\r\n";
            text += "Tipo_authentificazione=" + Tipo_authentificazione.ToString() + "\r\n";
            text += "Utente_server=" + Utente_server.ToString() + "\r\n";
            text += "Password_server=" + Password_server.ToString() + "\r\n";
            text += "DATABASE=" + DATABASE.ToString() + "\r\n";
            text += "Utente_accesso_DB=" + Utente_accesso_DB.ToString() + "\r\n";
            text += "Password_DB=" + Password_DB.ToString() + "\r\n";
            text += "IsTrustedConnection=" + Utente_accesso_DB.ToString() + "\r\n";
            text += "IsLocalServer=" + Password_DB.ToString() + "\r\n";


            text += "securizzatoProgramma=" + securizzatoProgramma.ToString() + "\r\n";
            text += "usernameProgramma=" + usernameProgramma.ToString() + "\r\n";
            text += "passwordProgramma=" + passwordProgramma.ToString() + "\r\n";
            text += "mailRegistrazioneUtenteProgramma=" + mailRegistrazioneUtenteProgramma.ToString() + "\r\n";
            text += "\r\n";
            text += "ACCESSDATABASELOCALEPATH=" + ACCESSDATABASELOCALEPATH.ToString().Replace(applPathForDB, "%") + "\r\n";
            text += "ACCESSDATABASEPASSWORD=" + ACCESSDATABASEPASSWORD.ToString() + "\r\n";
            text += "\r\n";
            text += "MYSQLSERVER=" + MYSQLSERVER.ToString() + "\r\n";
            text += "MYSQLDATABASE=" + MYSQLDATABASE.ToString() + "\r\n";
            text += "MYSQLUSERNAME=" + MYSQLUSERNAME.ToString() + "\r\n";
            text += "MYSQLPASSWORD=" + MYSQLPASSWORD.ToString() + "\r\n";
            text += "\r\n";
            text += "DirectoryPath=" + DirectoryPath.ToString().Replace(applPath, "%") + "\r\n";
            text += "ApplicationPath=" + ApplicationPath.ToString().Replace(applPath, "%") + "\r\n";
            text += "BackupsPATH=" + BackupsPATH.ToString().Replace(applPath, "%") + "\r\n";
            text += "TempFilesPATH=" + TempFilesPATH.ToString().Replace(applPath, "%") + "\r\n";
            text += "RapportiPDFPATH=" + RapportiPDFPATH.ToString().Replace(applPath, "%") + "\r\n";
            text += "\r\n";

            text += "EmailRisposta=" + EmailRisposta.ToString() + "\r\n";
            text += "Smtp=" + Smtp.ToString() + "\r\n";
            text += "Porta=" + Porta.ToString() + "\r\n";
            text += "enableSSL=" + enableSSL.ToString() + "\r\n";
            text += "CredentialsUsername=" + CredentialsUsername.ToString() + "\r\n";
            text += "CredentialsPassword=" + CredentialsPassword.ToString() + "\r\n";
            text += "\r\n";


            text += "FTPServer=" + FTPServer.ToString() + "\r\n";
            text += "FTPPorta=" + FTPPorta.ToString() + "\r\n";
            text += "FTPUser=" + FTPUser.ToString() + "\r\n";
            text += "FTPPass=" + FTPPass.ToString() + "\r\n";
            text += "FTPPathAggiornamenti=" + FTPPathAggiornamenti.ToString() + "\r\n";
            text += "FTPPath=" + FTPPath.ToString() + "\r\n";
            text += "FTPPathKitInstallazione=" + FTPPathKitInstallazione.ToString() + "\r\n";




            return text;
        }
        public void SaveConfig()
        {
            string saveString = writeConfig();



            // This text is added only once to the file. 
            if (File.Exists(PATH))
            {
                // Create a file to write to. 
                File.Move(PATH, PATH.Replace(Path.GetExtension(PATH), " " + DateTime.Now.ToShortTimeString().Replace(":", ".") + ".bkp"));

            }
            File.WriteAllText(PATH, saveString.Replace("\r\n", Environment.NewLine));
        }

        string applPath = Path.GetDirectoryName(Path.GetDirectoryName(Application.ExecutablePath));
        string applPathForDB = Path.GetDirectoryName(Application.ExecutablePath);

        string path = "config.txt";
        private void load()
        {
            loadConfig(path);

            Server_SQL_EXPRESS = returnValueFromConfig(configStringArray, "Server_SQL_EXPRESS");
            Nome_Sql_Instance = returnValueFromConfig(configStringArray, "Nome_Sql_Instance");
            Tipo_authentificazione = returnValueFromConfig(configStringArray, "Tipo_authentificazione");
            Utente_server = returnValueFromConfig(configStringArray, "Utente_server");
            Password_server = returnValueFromConfig(configStringArray, "Password_server");
            DATABASE = returnValueFromConfig(configStringArray, "DATABASE");
            Utente_accesso_DB = returnValueFromConfig(configStringArray, "Utente_accesso_DB");
            Password_DB = returnValueFromConfig(configStringArray, "Password_DB");


            if (returnValueFromConfig(configStringArray, "securizzatoProgramma").ToUpper() == "TRUE")
                securizzatoProgramma = true;
            else securizzatoProgramma = false;
            usernameProgramma = returnValueFromConfig(configStringArray, "usernameProgramma");
            passwordProgramma = returnValueFromConfig(configStringArray, "passwordProgramma");
            mailRegistrazioneUtenteProgramma = returnValueFromConfig(configStringArray, "mailRegistrazioneUtenteProgramma");

            ACCESSDATABASELOCALEPATH = returnValueFromConfig(configStringArray, "ACCESSDATABASELOCALEPATH").Replace("%", applPathForDB);
            ACCESSDATABASEPASSWORD = returnValueFromConfig(configStringArray, "ACCESSDATABASEPASSWORD");



            MYSQLSERVER = returnValueFromConfig(configStringArray, "MYSQLSERVER");
            MYSQLDATABASE = returnValueFromConfig(configStringArray, "MYSQLDATABASE");
            MYSQLUSERNAME = returnValueFromConfig(configStringArray, "MYSQLUSERNAME");
            MYSQLPASSWORD = returnValueFromConfig(configStringArray, "MYSQLPASSWORD");


            DirectoryPath = returnValueFromConfig(configStringArray, "DirectoryPath");
            ApplicationPath = returnValueFromConfig(configStringArray, "ApplicationPath").Replace("%", applPath);
            BackupsPATH = returnValueFromConfig(configStringArray, "BackupsPATH").Replace("%", applPath);
            TempFilesPATH = returnValueFromConfig(configStringArray, "TempFilesPATH").Replace("%", applPath);
            RapportiPDFPATH = returnValueFromConfig(configStringArray, "RapportiPDFPATH").Replace("%", applPath);

            //if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
            //if (!Directory.Exists(ApplicationPath)) Directory.CreateDirectory(ApplicationPath);
            //if (!Directory.Exists(BackupsPATH)) Directory.CreateDirectory(BackupsPATH);
            //if (!Directory.Exists(TempFilesPATH)) Directory.CreateDirectory(TempFilesPATH);
            //if (!Directory.Exists(RapportiPDFPATH)) Directory.CreateDirectory(RapportiPDFPATH);
            //if (!Directory.Exists(DirectoryPath + @"\Ftp\Agg. ricevuti")) Directory.CreateDirectory(DirectoryPath + @"\Ftp\Agg. ricevuti");
            //if (!Directory.Exists(DirectoryPath + @"\Ftp\Backup")) Directory.CreateDirectory(DirectoryPath + @"\Ftp\Backup");
            //if (!Directory.Exists(DirectoryPath + @"\Ftp\Da inviare")) Directory.CreateDirectory(DirectoryPath + @"\Ftp\Da inviare");


            EmailRisposta = returnValueFromConfig(configStringArray, "EmailRisposta");
            Smtp = returnValueFromConfig(configStringArray, "Smtp");
            if (returnValueFromConfig(configStringArray, "enableSSL").ToUpper() == "TRUE")
                enableSSL = true;
            else enableSSL = false;
            CredentialsUsername = returnValueFromConfig(configStringArray, "CredentialsUsername");
            CredentialsPassword = returnValueFromConfig(configStringArray, "CredentialsPassword");
            Porta = Convert.ToInt32(returnValueFromConfig(configStringArray, "Porta"));

            //   IsLocalServer
            connectionStringMYSQL = createConnStringMysql();
            connectionStringACCESS = createConnStringAccess();
            connectionStringSQLEXPRESS = createConnStringSQLServer();

            FTPServer = returnValueFromConfig(configStringArray, "FTPServer");
            FTPPorta = Convert.ToInt32(returnValueFromConfig(configStringArray, "FTPPorta"));
            FTPUser = returnValueFromConfig(configStringArray, "FTPUser");
            FTPPass = returnValueFromConfig(configStringArray, "FTPPass");
            FTPPathAggiornamenti = returnValueFromConfig(configStringArray, "FTPPathAggiornamenti");
            FTPPath = returnValueFromConfig(configStringArray, "FTPPath");
            FTPPathKitInstallazione = returnValueFromConfig(configStringArray, "FTPPathKitInstallazione");
        }
        public configClass()
        {
            load();
        }
        public configClass(string _path)
        {

            path = _path;
            load();

        }

        private string createConnStringMysql()
        {
            return "server=" + MYSQLSERVER + ";User Id=" + MYSQLUSERNAME + ";Password=" + MYSQLPASSWORD + ";database=" + MYSQLDATABASE + "";
        }
        private string createConnStringAccess()
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;data source=" + ACCESSDATABASELOCALEPATH + ";Jet OLEDB:Database Password=" + ACCESSDATABASEPASSWORD + ";";
        }

        private string createConnStringSQLServer()
        {
            string conStrin = "";
            if (returnValueFromConfig(configStringArray, "IsTrustedConnection") == "true")
            {
                isTrustedConnection = true;
            }
            else
            {
                isTrustedConnection = false;
            }
            if (returnValueFromConfig(configStringArray, "IsLocalServer") == "true")
            {
                IsLocalServer = true;
            }
            else
            {
                IsLocalServer = false;
            }
            //   IsLocalServer

            if (IsLocalServer)
            {
                conStrin = @"Server=localhost;Database=DBGranClima_Nuovo;Trusted_Connection=True";
            }
            else
            {
                if (Nome_Sql_Instance != "")
                {
                    if (isTrustedConnection)
                        conStrin = "Server=" + Server_SQL_EXPRESS + @"\" + Nome_Sql_Instance + ";Database=" + DATABASE + ";Trusted_Connection=True";
                    else
                    {
                        conStrin = "Server=" + Server_SQL_EXPRESS + @"\" + Nome_Sql_Instance + ";Uid=" + Utente_accesso_DB + ";Pwd=" + Password_DB + ";Database=" + DATABASE;
                    }
                }
                else
                    conStrin = "Server=" + Server_SQL_EXPRESS + ";Database=" + DATABASE + ";User Id=" + Utente_accesso_DB + ";Password=" + Password_DB + ";";
            }


            return conStrin;
        }
        public bool inviaEmailOneAttachementMaximum_HTML(string to, string subject, string testoDaInviare, System.Net.Mail.Attachment attach)
        {
            bool inviato = false;


            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(to);
                message.Subject = subject;

                message.From = new System.Net.Mail.MailAddress(Layer.mailNoReply);
                message.Body = testoDaInviare;
                message.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(Layer.smtpNoReply);
                smtp.Port = Convert.ToInt32(Layer.portaNoReply);

                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(Layer.mailNoReply, Layer.passNoReply);

                if (attach != null) message.Attachments.Add(attach);

                try
                {
                    smtp.Send(message);
                    try
                    { message.Attachments.Dispose(); }
                    catch { }
                    try
                    { attach.Dispose(); }
                    catch { }
                    try
                    { smtp.Dispose(); }
                    catch { }

                    inviato = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    inviato = false;
                }
            }
            catch
            {
                MessageBox.Show("Non inviato!");
            }

            return inviato;
        }

        public bool inviaEmailFromDevSoftExpressSRL(string to, string subject, string testoDaInviare, System.Net.Mail.Attachment attach)
        {
            bool inviato = false;


            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(to);
                message.Subject = subject;

                message.From = new System.Net.Mail.MailAddress("devsoftexpress@gmail.com");
                message.Body = testoDaInviare;
                message.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.Port = 587;

                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("devsoftexpress@gmail.com", "nwkfsprbfsngjplc");

                if (attach != null) message.Attachments.Add(attach);

                try
                {
                    smtp.Send(message);
                    try
                    { message.Attachments.Dispose(); }
                    catch { }
                    try
                    { attach.Dispose(); }
                    catch { }
                    try
                    { smtp.Dispose(); }
                    catch { }

                    inviato = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    inviato = false;
                }
            }
            catch
            {
                MessageBox.Show("Non inviato!");
            }

            return inviato;
        }


        public bool inviaEmailWithMoreAttachments(string to, string subject, string testoDaInviare, System.Net.Mail.Attachment[] attachments)
        {
            bool inviato = false;



            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(to);
            message.Subject = subject;
            message.From = new System.Net.Mail.MailAddress(EmailRisposta);
            message.Body = testoDaInviare;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(Smtp);
            smtp.Port = Porta;

            smtp.EnableSsl = enableSSL;
            smtp.Credentials = new System.Net.NetworkCredential(CredentialsUsername, CredentialsPassword);


            for (int i = 0; i < attachments.Length; i++)
            {
                message.Attachments.Add(attachments[i]);
            }


            try
            {
                smtp.Send(message);
                inviato = true;
            }
            catch
            {
                inviato = false;
            }


            return inviato;
        }

        public string GetProductVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

    }
    class DataReaderSqlExpress
    {
        public DataReaderSqlExpress()
        {

        }
        public bool executeQuery(string query)
        {
            configClass config = new configClass();
            DataTable tabela = new DataTable();
            SqlConnection con = new SqlConnection(config.connectionStringSQLEXPRESS);
            try
            {

                con.Open();
                query = "SET QUOTED_IDENTIFIER OFF;SET DATEFORMAT DMY;" + query;
                query = query.Replace(",''", ",null");
                query = query.Replace(",\"\"", ",null");
                query = query.Replace("'',", "null,");
                query = query.Replace("\"\",", "null,");
                query = query.Replace("'')", "null)");
                query = query.Replace("\"\")", "null)");
                query = query.Replace("(''", "(null");
                query = query.Replace("(\"\"", "(null");
                query = query.Replace("'01/01/0001 00:00:00'", "null");
                query = query.Replace(" €", "");
                query = query.Replace("€ ", "");
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();

                //System.IO.File.AppendAllText(@"c:\QUERY.txt", "\r\n" + query);

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"c:\QUERY.txt", "\r\n" + query);
                //System.IO.File.AppendAllText(@"c:\QUERY.txt", "\r\n" + ex.ToString());
                MessageBox.Show(ex.ToString());
                return false;
            }

            return false;

        }
        public string[] executeQueryOnServer(string connString, string query)
        {
            string[] resultat = new string[2];
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            query = "SET QUOTED_IDENTIFIER OFF;SET DATEFORMAT DMY;" + query;
            query = query.Replace(",''", ",null");
            query = query.Replace(",\"\"", ",null");
            query = query.Replace("'')", "null)");
            query = query.Replace("\"\")", "null)");
            query = query.Replace("(''", "(null");
            query = query.Replace("(\"\"", "(null");
            query = query.Replace("'01/01/0001 00:00:00'", "null");
            SqlCommand executeCommand = new SqlCommand(query, con);

            resultat[0] = "-1";
            resultat[1] = "";
            try
            {
                resultat[0] = executeCommand.ExecuteNonQuery().ToString();
                resultat[1] = "FATTO";
            }
            catch (Exception ex)
            {
                resultat[0] = "-1";
                resultat[1] = " ERRORE :" + ex.ToString();
            }
            con.Close();
            return resultat;
        }
        public DataTable getDataFromSQL(string query)
        {
            configClass config = new configClass();
            DataTable tabela = new DataTable();
            SqlConnection con = new SqlConnection(config.connectionStringSQLEXPRESS);
            try
            {
                con.Open();


                query = "SET QUOTED_IDENTIFIER OFF;SET DATEFORMAT DMY;" + query;
                query = query.Replace(",''", ",null");
                query = query.Replace(",\"\"", ",null");
                query = query.Replace("'')", "null)");
                query = query.Replace("\"\")", "null)");
                query = query.Replace("(''", "(null");
                query = query.Replace("(\"\"", "(null");
                query = query.Replace("'01/01/0001 00:00:00'", "null");

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                //System.IO.File.AppendAllText(@"c:\QUERY.txt", "\r\n" + query);
                DataSet set = new DataSet();
                da.Fill(set);

                tabela = set.Tables[0];



                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return tabela;
        }
        public DataTable getDataFromSQL_NoTraceQuery(string query)
        {
            configClass config = new configClass();
            DataTable tabela = new DataTable();
            SqlConnection con = new SqlConnection(config.connectionStringSQLEXPRESS);
            try
            {
                con.Open();


                query = "SET QUOTED_IDENTIFIER OFF;SET DATEFORMAT DMY;" + query;
                query = query.Replace(",''", ",null");
                query = query.Replace(",\"\"", ",null");
                query = query.Replace("'')", "null)");
                query = query.Replace("\"\")", "null)");
                query = query.Replace("(''", "(null");
                query = query.Replace("(\"\"", "(null");
                query = query.Replace("'01/01/0001 00:00:00'", "null");

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                // //System.IO.File.AppendAllText(@"c:\QUERY.txt", "\r\n" + query);
                DataSet set = new DataSet();
                da.Fill(set);

                tabela = set.Tables[0];



                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return tabela;
        }
        public DataTable getDataFromSQL_NoTraceQuery_NoMessageError(string query)
        {
            configClass config = new configClass();
            DataTable tabela = new DataTable();
            SqlConnection con = new SqlConnection(config.connectionStringSQLEXPRESS);
            try
            {
                con.Open();


                query = "SET QUOTED_IDENTIFIER OFF;SET DATEFORMAT DMY;" + query;
                query = query.Replace(",''", ",null");
                query = query.Replace(",\"\"", ",null");
                query = query.Replace("'')", "null)");
                query = query.Replace("\"\")", "null)");
                query = query.Replace("(''", "(null");
                query = query.Replace("(\"\"", "(null");
                query = query.Replace("'01/01/0001 00:00:00'", "null");

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                // //System.IO.File.AppendAllText(@"c:\QUERY.txt", "\r\n" + query);
                DataSet set = new DataSet();
                da.Fill(set);

                tabela = set.Tables[0];



                con.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
            return tabela;
        }
        public DataTable getDataFromSQL_NoDateFormatDMY(string query)
        {
            configClass config = new configClass();
            DataTable tabela = new DataTable();
            SqlConnection con = new SqlConnection(config.connectionStringSQLEXPRESS);
            try
            {
                con.Open();


                query = "SET QUOTED_IDENTIFIER OFF;" + query;
                query = query.Replace(",''", ",null");
                query = query.Replace(",\"\"", ",null");
                query = query.Replace("'')", "null)");
                query = query.Replace("\"\")", "null)");
                query = query.Replace("(''", "(null");
                query = query.Replace("(\"\"", "(null");
                query = query.Replace("'01/01/0001 00:00:00'", "null");

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                //System.IO.File.AppendAllText(@"c:\QUERY.txt", "\r\n" + query);
                DataSet set = new DataSet();
                da.Fill(set);

                tabela = set.Tables[0];



                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return tabela;
        }
        public DataTable getDataFromSQL_NoReplacements(string query, bool withQuotedIdentifier)
        {
            if (withQuotedIdentifier) query = "SET QUOTED_IDENTIFIER OFF;" + query;
            // query = query.Replace("SET QUOTED_IDENTIFIER OFF;SET QUOTED_IDENTIFIER OFF;", "SET QUOTED_IDENTIFIER OFF;");
            configClass config = new configClass();
            DataTable tabela = new DataTable();
            SqlConnection con = new SqlConnection(config.connectionStringSQLEXPRESS);
            try
            {
                con.Open();




                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.CommandTimeout = 180;
                //System.IO.File.AppendAllText(@"c:\QUERY.txt", "\r\n" + query);
                DataSet set = new DataSet();
                da.Fill(set);

                tabela = set.Tables[0];



                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return tabela;
        }
        public DataTable getDataFromSQL_noreplaceNull(string query)
        {
            configClass config = new configClass();
            DataTable tabela = new DataTable();
            SqlConnection con = new SqlConnection(config.connectionStringSQLEXPRESS);
            try
            {
                con.Open();


                query = "SET QUOTED_IDENTIFIER OFF;SET DATEFORMAT DMY;" + query;
                query = query.Replace("'01/01/0001 00:00:00'", "null");

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                //System.IO.File.AppendAllText(@"c:\QUERY.txt", "\r\n" + query);
                DataSet set = new DataSet();
                da.Fill(set);

                tabela = set.Tables[0];



                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return tabela;
        }
        public DataTable getDataFromSQL(string connString, string nomeTabella, string ColumnNames, string whereCondition, string groupByCondition, string sortedColumns, string sortingOrder, int top)
        {
            DataTable tabela = new DataTable();
            SqlConnection con = new SqlConnection(@connString);

            try
            {
                con.Open();

                string sqlSelectString = "SELECT ";
                if (top > 0) sqlSelectString += " TOP " + top;
                sqlSelectString += ColumnNames + " FROM " + nomeTabella;
                if (whereCondition != "") sqlSelectString += " WHERE " + whereCondition;
                if (groupByCondition != "") sqlSelectString += " GROUP BY " + groupByCondition;
                if (sortedColumns != "") sqlSelectString += " ORDER BY " + sortedColumns;
                if (sortingOrder != "") sqlSelectString += " " + sortingOrder;

                sqlSelectString = "SET DATEFORMAT DMY;" + sqlSelectString;
                SqlDataAdapter da = new SqlDataAdapter(sqlSelectString, con);

                DataSet set = new DataSet();
                da.Fill(set);

                tabela = set.Tables[0];



                con.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                //MessageBox.Show("Impossibile aprire il database locale");
            }
            return tabela;
        }

        public bool InsertRow(string connString, string nomeTabella, string fields, string values)
        {

            SqlConnection con = new SqlConnection(@connString);
            try
            {
                con.Open();

                string sqlSelectString = "INSERT INTO  " + nomeTabella + " (" + fields + ")  VALUES (" + values + ") ";
                sqlSelectString = "SET QUOTED_IDENTIFIER OFF;SET DATEFORMAT DMY;" + sqlSelectString;
                sqlSelectString = sqlSelectString.Replace("''", "null");
                sqlSelectString = sqlSelectString.Replace("\"\"", "null");
                SqlCommand insertCommand = new SqlCommand(sqlSelectString, con);

                try
                {
                    insertCommand.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch (SqlException ex)
                {
                    //MessageBox.Show(ex.ToString());
                    con.Close();
                    // MessageBox.Show(ex.ToString());
                    return false;
                }





            }
            catch
            {
                MessageBox.Show("Impossibile aprire il database Sql Express");
                return false;

            }

        }

        public bool InsertDataRow(string connString, string nomeTabella, string[] fields, string[] types, DataRow values)
        {

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            DataTable tabDestinazione = getDataFromSQL(connString, nomeTabella, "*", "", "", "", "", 0);

            string queryFields = "";
            for (int i = 0; i < fields.Length; i++)
            {
                if (tabDestinazione.Columns.Contains(fields[i]))
                    queryFields += fields[i] + ",";

            }
            queryFields = queryFields.TrimEnd(',');
            string queryValues = "";
            for (int i = 0; i < fields.Length; i++)
            {

                queryValues += "?,";

            }
            queryValues = queryValues.TrimEnd(',');

            string command = "Insert into " + nomeTabella + " (" + queryFields + ") Values (";





            for (int i = 0; i < fields.Length; i++)
            {
                if (!tabDestinazione.Columns.Contains(fields[i])) continue;

                if (types[i].Contains("Int"))
                // comm.Parameters.Add("@" + fields[i], OleDbType.Integer).Value = values[i];
                {
                    if (values[i] != null && !(values[i] is DBNull)) command += values[i] + ",";
                    else command += "null,";
                }
                else
                    if (types[i].Contains("Date"))
                {
                    if (values[i] != null && values[i].ToString() != "" && !(values[i] is DBNull))
                        // comm.Parameters.Add("@" + fields[i], OleDbType.Date).Value = Convert.ToDateTime(values[i]);
                        command += "'" + Convert.ToDateTime(values[i].ToString()).ToString() + "',";
                    else
                        //comm.Parameters.Add("@" + fields[i], OleDbType.Date).Value = Convert.ToDateTime("01/01/2000");
                        command += "null,";
                }
                else

                        if (types[i].Contains("String"))
                {
                    if (values[i] != null && !(values[i] is DBNull)) command += "'" + values[i].ToString().Replace("\"", "''").Replace("'", "''") + "',";
                    else command += "'',";
                }

                else
                            if (types[i].Contains("Boolean"))
                {

                    if (values[i] != null && !(values[i] is DBNull)) command += "'" + values[i] + "',";
                    else command += "null,";
                }
                else
                                if (types[i].Contains("Decimal"))
                {
                    // comm.Parameters.Add("@" + fields[i], OleDbType.VarChar).Value = values[i];
                    // command += values[i] + ",";
                    if (!(values[i] is DBNull) && values[i] != null && values[i].ToString() != "")
                        command += values[i].ToString().Replace(",", ".") + ",";
                    else command += "null,";
                }
                else
                    MessageBox.Show("");

            }
            command = command.TrimEnd(',');
            command += ")";

            command = "SET DATEFORMAT DMY;" + command;
            SqlCommand comm = new SqlCommand(command, con);
            try
            {
                comm.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString().Substring(0, 200));
                con.Close();
                return false;

            }







        }
        public string InsertDataRowStringResult(string connString, string nomeTabella, string[] fields, string[] types, DataRow values)
        {
            string resultatOKSauErroare = "";
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            DataTable tabDestinazione = getDataFromSQL(connString, nomeTabella, "*", "", "", "", "", 0);

            string queryFields = "";
            for (int i = 0; i < fields.Length; i++)
            {
                if (tabDestinazione.Columns.Contains(fields[i]))
                    queryFields += fields[i] + ",";

            }
            queryFields = queryFields.TrimEnd(',');
            string queryValues = "";
            for (int i = 0; i < fields.Length; i++)
            {

                queryValues += "?,";

            }
            queryValues = queryValues.TrimEnd(',');

            string command = "Insert into " + nomeTabella + " (" + queryFields + ") Values (";





            for (int i = 0; i < fields.Length; i++)
            {
                if (!tabDestinazione.Columns.Contains(fields[i])) continue;

                if (types[i].Contains("Int"))
                // comm.Parameters.Add("@" + fields[i], OleDbType.Integer).Value = values[i];
                {
                    if (values[i] != null && !(values[i] is DBNull)) command += values[i] + ",";
                    else command += "null,";
                }
                else
                    if (types[i].Contains("Date"))
                {
                    if (values[i] != null && values[i].ToString() != "" && !(values[i] is DBNull))
                        // comm.Parameters.Add("@" + fields[i], OleDbType.Date).Value = Convert.ToDateTime(values[i]);
                        command += "'" + Convert.ToDateTime(values[i].ToString()).ToString() + "',";
                    else
                        //comm.Parameters.Add("@" + fields[i], OleDbType.Date).Value = Convert.ToDateTime("01/01/2000");
                        command += "null,";
                }
                else

                        if (types[i].Contains("String"))
                {
                    if (values[i] != null && !(values[i] is DBNull)) command += "'" + values[i].ToString().Replace("\"", "''").Replace("'", "''") + "',";
                    else command += "'',";
                }

                else
                            if (types[i].Contains("Boolean"))
                {

                    if (values[i] != null && !(values[i] is DBNull)) command += "'" + values[i] + "',";
                    else command += "null,";
                }
                else
                                if (types[i].Contains("Decimal"))
                {
                    // comm.Parameters.Add("@" + fields[i], OleDbType.VarChar).Value = values[i];
                    // command += values[i] + ",";
                    if (!(values[i] is DBNull) && values[i] != null && values[i].ToString() != "")
                        command += values[i].ToString().Replace(",", ".") + ",";
                    else command += "null,";
                }
                else
                    MessageBox.Show("");

            }
            command = command.TrimEnd(',');
            command += ")";

            command = "SET DATEFORMAT DMY;" + command;
            SqlCommand comm = new SqlCommand(command, con);
            try
            {
                comm.ExecuteNonQuery();
                con.Close();
                return "";

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString().Substring(0, 200));
                resultatOKSauErroare = ex.ToString().Substring(0, 200);
                con.Close();
                return resultatOKSauErroare;

            }







        }
        public string[] DeleteRow(string connString, string nomeTabella, string condition, string columnFieldReturned)
        {
            string[] valuesReturned = null;

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            if (columnFieldReturned != "")
            {

                SqlCommand selCommand = new SqlCommand("Select " + columnFieldReturned + " From " + nomeTabella + " Where " + condition, con);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = selCommand;
                DataSet set = new DataSet();
                da.Fill(set);
                if (set.Tables[0].Rows.Count > 0)
                {
                    valuesReturned = new string[set.Tables[0].Rows.Count];
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {

                        try
                        {
                            valuesReturned[i] = set.Tables[0].Rows[i][0].ToString();
                        }
                        catch
                        {
                            valuesReturned[i] = "";
                        }

                    }
                }
                else
                    return null;


            }


            SqlCommand deleteCommand = new SqlCommand("Delete from " + nomeTabella + " WHERE " + condition, con);
            deleteCommand.ExecuteNonQuery();



            con.Close();


            return valuesReturned;
        }
        public int DeleteRow(string connString, string nomeTabella, string condition)
        {

            int rowsAffected = 0;

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            SqlCommand deleteCommand = new SqlCommand("Delete  from " + nomeTabella + " WHERE " + condition, con);
            try
            {

                rowsAffected = deleteCommand.ExecuteNonQuery();
            }
            catch { }


            con.Close();


            return rowsAffected;
        }

        public bool InsertDataRows(string connString, string nomeTabella, DataTable tabela)
        {

            string[] fields = GetColumnNames(tabela);
            string[] types = GetColumnTypes(tabela);
            //DataRow values


            DataTable tabDestinazione = getDataFromSQL(connString, nomeTabella, "*", "", "", "", "", 0);

            string queryFields = "";
            for (int i = 0; i < fields.Length; i++)
            {
                if (tabDestinazione.Columns.Contains(fields[i]))
                    queryFields += "`" + fields[i] + "`,";

            }
            queryFields = queryFields.TrimEnd(',');
            string queryValues = "";
            for (int i = 0; i < fields.Length; i++)
            {

                queryValues += "?,";

            }
            queryValues = queryValues.TrimEnd(',');




            int numberOfErrors = 0;
            List<SqlParameter> parametri = new List<SqlParameter>();
            foreach (DataRow values in tabela.Rows)
            {


                string command = "Insert into " + nomeTabella + " (" + queryFields + ") Values (";





                for (int i = 0; i < fields.Length; i++)
                {
                    if (!tabDestinazione.Columns.Contains(fields[i])) continue;
                    if (types[i].Contains("Int"))
                    // comm.Parameters.Add("@" + fields[i], OleDbType.Integer).Value = values[i];
                    {
                        if (values[i] != null && !(values[i] is DBNull)) command += values[i] + ",";
                        else command += "null,";
                    }
                    else
                        if (types[i].Contains("Date"))
                    {
                        if (values[i] != null && values[i].ToString() != "" && !(values[i] is DBNull))
                            // comm.Parameters.Add("@" + fields[i], OleDbType.Date).Value = Convert.ToDateTime(values[i]);
                            command += "'" + Convert.ToDateTime(values[i].ToString()).ToString() + "',";
                        else
                            //comm.Parameters.Add("@" + fields[i], OleDbType.Date).Value = Convert.ToDateTime("01/01/2000");
                            command += "null,";
                    }
                    else

                            if (types[i].Contains("String"))
                    {
                        if (values[i] != null && !(values[i] is DBNull)) command += "'" + values[i] + "',";
                        else command += "'',";
                    }

                    else
                                if (types[i].Contains("Boolean"))
                    {

                        if (values[i] != null && !(values[i] is DBNull)) command += values[i] + ",";
                        else command += "null,";
                    }
                    else
                                    if (types[i].Contains("Decimal"))
                    {
                        // comm.Parameters.Add("@" + fields[i], OleDbType.VarChar).Value = values[i];
                        // command += values[i] + ",";
                        if (!(values[i] is DBNull) && values[i] != null && values[i].ToString() != "")
                            command += values[i].ToString().Replace(",", ".") + ",";
                        else command += "null,";
                    }
                    else
                    {
                        //"0x" + BitConverter.ToString(arraytoinsert).Replace("-", "")
                        // command += "0x" + BitConverter.ToString((byte[]) values[i]).Replace("-", "") + ",";
                        //  command = command.Replace("`"+fields[i]+"`", "@" + fields[i]);

                        command += "@" + fields[i] + ",";
                        SqlParameter parameter = new SqlParameter("@" + fields[i], values[i]);
                        parametri.Add(parameter);
                    }


                }
                command = command.TrimEnd(',');
                command += ")";

                command = "SET DATEFORMAT DMY;" + command;
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                SqlCommand comm = new SqlCommand(command, con);
                foreach (SqlParameter par in parametri)
                {
                    try
                    {
                        comm.Parameters.Add(par);
                    }
                    catch { }
                }

                try
                {
                    comm.ExecuteNonQuery();
                    con.Close();


                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString().Substring(0, 900));
                    con.Close();
                    numberOfErrors++;

                }



            }

            if (numberOfErrors == 0)
            {
                return true;
            }
            else return false;

        }
        public bool UpdateRow(string connString, string nomeTabella, string condition, string settedValues)
        {

            SqlConnection con = new SqlConnection(@connString);
            try
            {
                con.Open();

                string sqlSelectString = "UPDATE  " + nomeTabella + " SET " + settedValues + " WHERE " + condition;

                sqlSelectString = "SET DATEFORMAT DMY;" + sqlSelectString;
                SqlCommand insertCommand = new SqlCommand(sqlSelectString, con);

                try
                {
                    insertCommand.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Errore di aggiornamento : <" + sqlSelectString + ">\nMessage error:" + ex.ToString());
                    con.Close();
                    // MessageBox.Show(ex.ToString());
                    return false;
                }





            }
            catch
            {
                MessageBox.Show("Impossibile aprire il database SQL EXPRESS");
                return false;

            }
        }

        public bool UpdateDataRow(string connString, string nomeTabella, string[] fields, string[] types, DataRow values, string condition)
        {
            string campoCollegamento = condition.Substring(0, condition.IndexOf("="));
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            DataTable tabDestinazione = getDataFromSQL(connString, nomeTabella, "*", "", "", "", "", 0);



            string command = "UPDATE " + nomeTabella + " SET ";





            for (int i = 0; i < fields.Length; i++)
            {
                if (!tabDestinazione.Columns.Contains(fields[i]) || fields[i] == campoCollegamento) continue;

                command += fields[i] + "=";

                if (types[i].Contains("Int"))
                // comm.Parameters.Add("@" + fields[i], OleDbType.Integer).Value = values[i];
                {
                    if (values[i] != null && !(values[i] is DBNull)) command += values[i] + ",";
                    else command += "null,";
                }
                else
                    if (types[i].Contains("Date"))
                {
                    if (values[i] != null && values[i].ToString() != "" && !(values[i] is DBNull))
                        // comm.Parameters.Add("@" + fields[i], OleDbType.Date).Value = Convert.ToDateTime(values[i]);
                        command += "'" + Convert.ToDateTime(values[i].ToString()).ToString() + "',";
                    else
                        //comm.Parameters.Add("@" + fields[i], OleDbType.Date).Value = Convert.ToDateTime("01/01/2000");
                        command += "null,";
                }
                else

                        if (types[i].Contains("String"))
                {
                    if (values[i] != null && !(values[i] is DBNull)) command += "'" + values[i].ToString().Replace("\"", "''").Replace("'", "''") + "',";
                    else command += "'',";
                }

                else
                            if (types[i].Contains("Boolean"))
                {

                    if (values[i] != null && !(values[i] is DBNull)) command += "'" + values[i] + "',";
                    else command += "null,";
                }
                else
                                if (types[i].Contains("Decimal"))
                {
                    // comm.Parameters.Add("@" + fields[i], OleDbType.VarChar).Value = values[i];
                    // command += values[i] + ",";
                    if (!(values[i] is DBNull) && values[i] != null && values[i].ToString() != "")
                        command += "'" + values[i].ToString().Replace(",", ".") + "',";
                    else command += "null,";
                }
                else
                    MessageBox.Show("");

            }
            command = command.TrimEnd(',');
            command += " WHERE " + condition;

            command = "SET DATEFORMAT DMY;" + command;
            SqlCommand comm = new SqlCommand(command, con);
            try
            {
                int nrRow = comm.ExecuteNonQuery();
                con.Close();

                if (nrRow > 0) return true;
                else return false;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString().Substring(0, 200));
                con.Close();
                return false;

            }







        }

        public string[] GetColumnNames(DataTable table)
        {

            if (table != null)
            {
                string[] v = new string[table.Columns.Count];

                for (int i = 0; i < v.Length; i++)
                {
                    v[i] = table.Columns[i].ColumnName;
                }

                return v;

            }

            return null;
        }
        public string[] GetColumnTypes(DataTable table)
        {

            if (table != null)
            {
                string[] v = new string[table.Columns.Count];

                for (int i = 0; i < v.Length; i++)
                {
                    v[i] = table.Columns[i].DataType.ToString();
                }

                return v;

            }

            return null;
        }
    }
}
