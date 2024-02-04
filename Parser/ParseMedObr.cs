using HtmlAgilityPack;
using MainParser;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace MainParser
{
    class ParseMedObr : Parameters
    {
        public ParseMedObr(string url, string csvName)
        {
            this.url = url;
            this.csvName = csvName;

        }
        public void getDataInCSV(string savePATH)
        {
            WriteDataToCSV(Pars(), savePATH + this.csvName); ;
        }

        private List<DataPars> Pars()
        {
            List<DataPars> DataList = new List<DataPars>();
            DataList.Add(new DataPars("Наименование организации", "Адрес", "Телефон", "Почта"));
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler { AllowAutoRedirect = false })
                {
                    using (HttpClient client = new HttpClient(hdl))
                    {


                        for (int i = 1; i <= 1; i++)
                        {

                            string urlpage = url.Remove(url.Length, 0);

                            // инфа организации
                            HtmlNodeCollection nodeCollection =
 getHtmlDoc(client, urlpage).DocumentNode.SelectNodes(".//div[@class='param_item']");
                            if (nodeCollection != null)
                            {
                                foreach (HtmlNode item in nodeCollection)
                                {

                                    var name = item.SelectSingleNode(".//div[@class='item'][3]").InnerText.Trim();
                                    name = WebUtility.HtmlDecode(name).Substring(15);

                                    var adress = item.SelectSingleNode(".//div[@class='item'][7]").InnerText.Trim();
                                    adress = WebUtility.HtmlDecode(adress).Substring(21);

                                    var tel = item.SelectSingleNode(".//div[@class='item'][10]").InnerText.Trim();
                                    tel = WebUtility.HtmlDecode(tel).Substring(11);

                                    var email = item.SelectSingleNode(".//div[@class='item'][9]").InnerText.Trim();
                                    email = WebUtility.HtmlDecode(email).Substring(10);
                                    if (email != null)
                                    {
                                        if (!email.Contains("@"))
                                        {
                                            email = "";
                                        }
                                    }
                                    DataList.Add(new DataPars(name, adress, tel, email));
                                }
                            }

                        }
                        return DataList;
                    }
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error! \n{ex}");
                Console.ReadKey();
            }
            return null;
        }

    }
}