using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportEngine;
using WeighingsWEB.Util;

namespace WeighingsWEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ReportPDFController : ControllerBase
    {
        private IConverter synchronizedConverter { get; set; }
        public ReportPDFController(IConverter pdfConverter)
        {
            this.synchronizedConverter = pdfConverter;
        }

        // GET: api/Report/ RETURNS base64 pdf file.
        [HttpGet]
        public byte[] GetPDF(string templatePath, string parameterNames, string parameterValues)
        {
            PDFReportFactory reportFactory = new PDFReportFactory(synchronizedConverter, 
                templatePath, parameterNames, parameterValues
            );

            return reportFactory.BuildReport();

        }

    }
}
