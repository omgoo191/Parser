using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using CsvHelper;                    // dotnet add package CsvHelper
using CsvHelper.Configuration;
using HtmlAgilityPack;              // dotnet add package HtmlAgilityPack
using System.Text.Encodings.Web;    // dotnet add package System.Text.Encodings.Web 
using System.Text;
using System;
using System.Xml;


namespace MainParser
{
    class Parameters
    {
        protected string url { get; set; }
        protected string csvName { get; set; }
        protected class DataPars
        {
            public string Name { get; set; }
            public string Adress { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Web { get; set; }

            public DataPars()
            {
                this.Name = "";
                this.Adress = "";
                this.PhoneNumber = "";
                this.Email = "";
                this.Web = "";
            }
            public DataPars(string name)
            {
                this.Name = name;
            }
            public DataPars(string name, string adress) : this(name)
            {
                this.Adress = adress;
            }
            public DataPars(string name, string adress, string phone) : this(name, adress)
            {
                this.PhoneNumber = phone;
            }

            public DataPars(string name, string adress, string phone, string email) : this(name, adress, phone)
            {
                this.Email = email;
            }
            public DataPars(string name, string adress, string phone, string email, string site) : this(name, adress, phone, email)
            {
                this.Web = site;
            }
        }
        protected HtmlDocument getHtmlDoc(HttpClient client, string url)
        {
            HtmlDocument doc = new HtmlDocument();

            using (HttpResponseMessage resp = client.GetAsync(url).Result)
            {
                if (resp.IsSuccessStatusCode)
                {
                    var html = resp.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(html))
                    {
                        doc.LoadHtml(html);
                    }
                }
                return doc;
            }
        }
        protected void WriteDataToCSV(List<DataPars> data, string filepath)

        { 
            var Configuration = newCsvConfiguration(CultureInfo.InvariantCulture);
        Configuration.Delimiter = ";";

            using (
                var sw = new StreamWriter(filepath))
            {
                using (var csv = new CsvWriter(sw, Configuration))
                {
                    foreach (var item in data)
                    {
                        if (data[0].Name != "")
                        {
                            csv.WriteField(item.Name);
                        }

                        if (data[0].Adress != "")
                        {
                            csv.WriteField(item.Adress);
                        }

                        if (data[0].PhoneNumber != "")
                        {
                            csv.WriteField(item.PhoneNumber);
                        }

                        if (data[0].Email != "")
                        {
                            csv.WriteField(item.Email);
                        }

                        if (data[0].Web != "")
                        {
                            csv.WriteField(item.Web);
                        }
                        csv.NextRecord();
                    }
                }
            }
            File.WriteAllText(filepath, File.ReadAllText(filepath, Encoding.UTF8), Encoding.GetEncoding(1251));
  }
}
}
