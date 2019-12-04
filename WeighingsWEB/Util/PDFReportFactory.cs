using Database;
using DinkToPdf;
using DinkToPdf.Contracts;
using ReportEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeighingsWEB.Util
{
    public class PDFReportFactory
    {
        private string templateName { get; set; }
        private string[] parameterNames { get; set; }
        private string[] parameterValues { get; set; }
        private IConverter synchronizedConverter { get; set; }

        public PDFReportFactory(IConverter converter, string templateName, string parameterNames, string parameterValues)
        {
            this.synchronizedConverter = converter;


            if (parameterNames != null && parameterNames != null)
            {
                this.parameterNames = parameterNames.Split(",");
                this.parameterValues = parameterValues.Split(",");

                if (this.parameterValues.Length != this.parameterNames.Length)
                {
                    throw new ArgumentException("Количество параметров должно быть равно количеству значений");
                }
            }

            this.templateName = templateName;

        }

        public byte[] BuildReport()
        {
            DatabaseConfiguration configuration = new DatabaseConfiguration("mssql-connection.cfg");

            Report report = new Report(
                $"./ReportTemplates/{templateName}.template", 
                configuration.BuildConnectionString()
            );

            if (parameterNames != null && parameterValues != null)
            {
                for (int i = 0; i < parameterNames.Length; i++)
                {
                    report.AddVarHeap(parameterNames[i], parameterValues[i]);
                }
            }


            byte[] result;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                {

                    report.ExportInto(streamWriter);

                    streamWriter.Flush();
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    byte[] htmlContent = memoryStream.ToArray();
                    string htmlText = Encoding.UTF8.GetString(htmlContent);

                    var doc = new HtmlToPdfDocument()
                    {
                        GlobalSettings = {
                            ColorMode = ColorMode.Color,
                            Orientation = Orientation.Portrait,
                            PaperSize = PaperKind.A4,
                            Margins = new MarginSettings() { Top = 10 },
                        },
                        Objects = {
                                new ObjectSettings() {
                                PagesCount = true,
                                HtmlContent = htmlText,
                                HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                            }
                        }
                    };

                    result = synchronizedConverter.Convert(doc);

                }
            }

            return result;
        }

    }
}
