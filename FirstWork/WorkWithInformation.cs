using FirstWork.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FirstWork
{
    public class WorkWithInformation
    {
        public List<Item> items = new List<Item>();
        public int count { get; set; } = 0;
        public void GetInfoToList(XmlDocument document)
        {
            //XmlDocument doc = new XmlDocument();
            document.Load("https://habrahabr.ru/rss/interesting/");
            var RootElement = document.DocumentElement;

            foreach (XmlNode item in RootElement.ChildNodes)
            {

                foreach (XmlNode it in item.ChildNodes)
                {
                    if (it.Name == "item")
                    {
                        Item im = new Item();

                        foreach (XmlNode i in it.ChildNodes)
                        {
                            if (i.Name == "title")
                                im.Title = i.InnerText;

                            else if (i.Name == "link")
                                im.Link = i.InnerText;

                            else if (i.Name == "description")
                                im.Description = i.InnerText;

                            else if (i.Name == "pubDate")
                                im.PubDate = DateTime.Parse(i.InnerText);
                        }
                        items.Add(im);
                        count++;
                    }
                }
            }
        }


        public void WriteInfoToFile()
        {
            FileInfo fi = new FileInfo(@"News.xml");
            FileStream fs = fi.Create();

            fs.Close();

            StreamWriter sw = new StreamWriter(@"News.xml");
            foreach (var item in items)
            {
                sw.Write(item.Title);
                sw.Write(item.Link);
                sw.Write(item.Description);
                sw.Write(item.PubDate);

            }

        }

    }
}
