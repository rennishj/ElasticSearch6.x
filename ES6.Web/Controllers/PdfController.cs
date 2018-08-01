using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ES6.Web.Controllers
{
    public class PdfController : Controller
    {
        // GET: Pdf
        public ActionResult Generate()
        {
            var data = GenerateData();            
            return File(data, "application/pdf", "DownloadName.pdf");
        }

        /// <summary>
        /// https://stackoverflow.com/questions/27015644/how-to-add-a-rich-textbox-html-to-a-table-cell
        /// </summary>
        /// <returns></returns>
        private byte[] GenerateData()
        {
            byte[] data = null;
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        string htmlString = @"<p><strong>Lorem Ipsum</strong> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>";                                             
                        //var css = @".headline{font-size:200%}";                        
                        PdfPTable table = new PdfPTable(2);
                        table.AddCell("Some Rich Text");
                        PdfPCell cell = new PdfPCell();
                        foreach (var item in XMLWorkerHelper.ParseToElementList(htmlString,null))
                        {
                            cell.AddElement(item);
                        }
                        table.AddCell(cell);
                        doc.Add(table);
                        doc.Close();
                    }                   
                }                
                data = ms.ToArray();
            }
            return data;
        }
    }
}