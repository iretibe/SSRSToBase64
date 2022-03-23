using SSRSToBase64.Pdf.psldbservervm;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SSRSToBase64.Pdf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReportExecutionService rs = new ReportExecutionService();
            rs.Credentials = CredentialCache.DefaultCredentials;
            rs.Url = "http://psl-dbserver-vm/ReportServer_SSRS/reportexecution2005.asmx";

            rs.ExecutionHeaderValue = new ExecutionHeader();
            var executionInfo = new ExecutionInfo();
            executionInfo = rs.LoadReport("/eVAT/VATInvoiceReport", null);

            List<ParameterValue> parameters = new List<ParameterValue>();

            parameters.Add(new ParameterValue { Name = "InvoiceNo", Value = "2203229018" });

            rs.SetExecutionParameters(parameters.ToArray(), "en-US");

            string deviceInfo = "<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";
            string mimeType;
            string encoding;
            string[] streamId;
            Warning[] warning;

            var result = rs.Render("PDF", deviceInfo, out mimeType, out encoding, out encoding, out warning, out streamId);
            File.WriteAllBytes("C:\\IMPORTANT\\VATInvoiceReport.pdf", result);
        }
    }
}
