using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

namespace Database
{
    public class DatabaseConfiguration
    {   
        private string xmlConfigurationFile;
        

        public DatabaseConfiguration(string xmlConfigurationFile) {
            this.xmlConfigurationFile=xmlConfigurationFile;
        }
        /* 
        Server                  = "Server",
        Database                = "Database",
        WindowsAuthentication   = "WindowsAuthentication",
        Login                   = "Login",
        Password                = "Password" */

        public void ApplyParameters(string[] columns, string[] values) {
            
            if(columns.Length != values.Length)
                throw new ArgumentNullException("Количество столбцов должно соответствовать количеству значений");
            
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlConfigurationFile); 

            XmlElement rootElement      = xmlDocument.DocumentElement;
            for(int i = 0; i < columns.Length; i++) {
                rootElement[columns[i]].InnerText = values[i];
            }

            xmlDocument.Save(xmlConfigurationFile);


        }


        public string BuildConnectionString() {

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlConfigurationFile); 

            string connectionString     = null;
            XmlElement rootElement      = xmlDocument.DocumentElement;
            NameValueCollection connectionParams    = new NameValueCollection();

            foreach(XmlNode key in rootElement) {
                connectionParams[key.Name] = key.InnerText.Trim();
            }

            bool bWindowsAuthentication = false;
            bool.TryParse(connectionParams["WindowsAuthentication"], out bWindowsAuthentication);

            if(bWindowsAuthentication) {
                if(connectionParams["Server"] != null && connectionParams["Database"] != null)
                    connectionString = (connectionParams["Server"].Trim().Length != 0 && connectionParams["Database"].Trim().Length != 0) ? 
                        ($"Data Source={connectionParams["Server"]};Initial Catalog={connectionParams["Database"]};Integrated Security=True") :
                        (null);
            } else {
                if(connectionParams["Server"] != null && connectionParams["Database"] != null && 
                    connectionParams["Login"] != null && connectionParams["Password"] != null)
                        connectionString = (connectionParams["Server"].Trim().Length != 0 && connectionParams["Database"].Trim().Length != 0 && 
                            connectionParams["Login"].Trim().Length != 0 && connectionParams["Password"].Trim().Length != 0) ?
                            ($"Data Source={connectionParams["Server"]};Initial Catalog={connectionParams["Database"]};User Id={connectionParams["Login"]}; Password={connectionParams["Password"]};"):
                            (null);
            }

            /* Console.WriteLine("-------");
            Console.WriteLine(connectionString);
            Console.WriteLine("-------"); */

            return connectionString;
        }

    }
}