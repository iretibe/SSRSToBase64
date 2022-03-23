using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;

namespace SSRSToBase64.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private IConfiguration _config;

        public ReportsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("GenerateVATInvoiceReportAsync")]
        public byte[] GenerateVATInvoiceReportAsync(string InvoiceNo)
        {
            string strReportServer = _config["ReportOptions:Server"];
            string strReportPath = _config["ReportOptions:Path"];
            string strReportFormat = _config["ReportOptions:Format"];
            string strReportParameters = _config["ReportOptions:Parameters"];
            string strReportServerDomain = _config["ReportOptions:Domain"];
            string strReportServerUserId = _config["ReportOptions:Username"];
            string strReportServerUserPassword = _config["ReportOptions:Password"];
            string strReportTimeout = _config["ReportOptions:Timeout"];

            var inputparams = $"InvoiceNo={InvoiceNo}";

            //var url = "http://psl-dbserver-vm/ReportServer_SSRS" + "?" + "/eVAT/VATInvoiceReport" + "&rs:Command=Render&rs:Format=" +
            //    "PDF" + "&rs:" + "ParameterLanguage=en-GB" + "&" + inputparams;

            var url = strReportServer + "?" + strReportPath + "&rs:Command=Render&rs:Format=" +
                strReportFormat + "&rs:" + strReportParameters + "&" + inputparams;

            var req = WebRequest.Create(url);
            
            req.Credentials = new NetworkCredential(strReportServerUserId, strReportServerUserPassword, strReportServerDomain);
            req.Timeout = Convert.ToInt32(strReportTimeout);

            var webStream = req.GetResponse().GetResponseStream();
            var memStream = new MemoryStream();
            
            if (webStream == null) return Array.Empty<byte>();
            webStream.CopyTo(memStream);
            var len = memStream.Length;
            var reportReturn = new byte[len];
            memStream.Seek(0, SeekOrigin.Begin);
            memStream.Read(reportReturn, 0, (int)len);
            webStream.Close();
            memStream.Close();
            memStream.Dispose();

            return reportReturn;
        }
    }
}
