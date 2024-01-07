using System.Text;
using System.Xml;
using System.IO;

namespace EcommerceApplication
{
    public class DataExtract
    {
        public static byte[] GetFileInput()
        {
            string FileInputPath = "C:\\Users\\tinu\\Downloads\\User.XML";
            byte[] FileInputData = File.ReadAllBytes(FileInputPath);
            return FileInputData;
        }

        public static (string? Username, string? Password) ExtractOutput(byte[] fileInputData)
        {
            //var extractedData = new List<(string?, string?)>();

            var OutputXmlDocument = new XmlDocument();
            OutputXmlDocument.LoadXml(Encoding.UTF8.GetString(fileInputData));

            var usernameNode = OutputXmlDocument.SelectSingleNode("//ArrayOfUser/User/Username");
            var passwordNode = OutputXmlDocument.SelectSingleNode("//ArrayOfUser/User/Password");
            //Console.WriteLine("XML Document: " + OutputXmlDocument.OuterXml);

            var username = usernameNode?.InnerText;
            var password = passwordNode?.InnerText;

            return (username, password);
        }
    }
}
