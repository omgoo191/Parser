using System;
using Parser;
using System.Text.Encodings.Web;    // dotnet add package System.Text.Encodings.Web 
using System.Text;
namespace MainParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // для перевода csv на Кириллицу         
            new ParseMedObr("http://www.med-obr.info/education/education/6/", $" Медицина Приволжский 
округ.csv").getDataInCSV("out/ СПО / ");

        }
    }
}