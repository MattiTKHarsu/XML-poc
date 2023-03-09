// See https://aka.ms/new-console-template for more information
using System;
using System.IO.Enumeration;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace XmlTest
{
    class Xml
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a demo program to test XML modification for RDLC file");
            updateLabelLayout("Report1","Image4", 12.0, 12.0, 12.0, 12.0);
        }


        public static void updateLabelLayout(string report, string box, double xloc, double yloc, double width, double height)
        {
            string strXloc    = xloc.ToString()   + "cm";
            string strYloc    = yloc.ToString()   + "cm";
            string strWidth   = width.ToString()  + "cm";
            string strHeight  = height.ToString() + "cm";


            String projectPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            var fileName = projectPath + "\\" + report + ".rdlc";

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlNodeList node = doc.GetElementsByTagName("Image");

            foreach (XmlNode n in node)
            {
                int leafCount = n.ChildNodes.Count;
                if (n.OuterXml.Contains(box))
                {
                    for (int i = 0; i < leafCount; i++)
                    {
                        if (n.ChildNodes[i].Name == "Width")
                        {
                            n.ChildNodes[i].InnerText = strWidth;
                        }

                        if (n.ChildNodes[i].Name == "Height")
                        {
                            n.ChildNodes[i].InnerText = strHeight;
                        }

                        if (n.ChildNodes[i].Name == "Top")
                        {
                            n.ChildNodes[i].InnerText = strYloc;
                        }

                        if (n.ChildNodes[i].Name == "Left")
                        {
                            n.ChildNodes[i].InnerText = strXloc;
                        }
                    }
                }
            }
            doc.Save(fileName);
        }
    }


}
