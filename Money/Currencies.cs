using System.IO;

namespace System
{
    using Net;
    using Xml;
    using Xml.Xsl;

    public class Currencies
    {
        public const string SourceUrl =
            "https://www.six-group.com/dam/download/financial-information/data-center/iso-currrency/lists/list-one.xml";

        public const string XmlFolder = ".\\XML\\";
        public const string XsltPath = XmlFolder + "Currencies.xslt";
        public const string InputPath = XmlFolder + "list-one.xml";
        public const string OutputPath = XmlFolder + "Currencies.xml";

        static void Main()
        {
            Transform(SourceUrl, InputPath, XsltPath, OutputPath);
        }

        public static void Transform(string sourceUrl, string inputPath, string xsltPath, string outputPath)
        {
            Console.WriteLine("sourceUrl: " + sourceUrl);
            Console.WriteLine("inputPath: "+inputPath);
            Console.WriteLine("xsltPath: "+xsltPath);
            Console.WriteLine("outputPath: "+outputPath);
            try
            {
                var fileInfo = new FileInfo(inputPath);
                Console.WriteLine("inputPath FileInfo:"+fileInfo.FullName);
                if (!fileInfo.Exists)
                {
                    // Download the XML file
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(sourceUrl, inputPath);
                    }
                }

                // Load the downloaded XML
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(inputPath);

                // Load the XSLT
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                // Apply the XSLT transformation
                using (XmlWriter writer = XmlWriter.Create(outputPath))
                {
                    xslt.Transform(xmlDoc, writer);
                }

                Console.WriteLine("Transformation completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}