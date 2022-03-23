//namespace SSRSToBase64.API
//{
//    [8:59 AM]
//    Michael Ameyaw[Persol]
//public class ReportExecConfig : IReportExec, IDisposable
//    {
//        public ReportExecConfig(IOptions<ReportOptions> configuration) : this(configuration.Value)
//        {
//            Config = configuration.Value;
//        }
//        public ReportExecConfig(ReportOptions configuration)
//        {
//            Config
//            = configuration
//            ?? throw new ArgumentNullException(nameof(configuration));
//        }
//        private ReportOptions Config { get; }
//        public void Dispose()
//        {
//            if (Config.ReportServer.AutoSave)
//                PersistAsync();
//        }
//        public void PersistAsync()
//        {
//            //ExportLocalReportAsync(GenerateReportAsync());
//        }
//        public string DownloadFileNameAsync
//        {
//            get => Config.ReportServer.Filename;
//            set { }
//        }
//        public string FileExtAsync
//        {
//            get => FileHelpers.GetMimeTypes(Config.ReportServer.Ext);
//            set { }
//        }
//        public byte[] GenerateReportAsync(Guid companyId, Guid userId, string type, string reportType, string language = "en")
//        {
//            //var inputparams = $"uUserId={userId}&uCompanyId={companyId}&szType={type}&szLanguage={language}"; if (reportType == "sikasem")
//            {
//                var inputparams = $"szTranRefNo={type}"; // &szLanguage={language}"; Config.ReportServer.SikaSemPath = Config.ReportServer.SikaSemPath.Replace("/", "%2f").Replace(" ", "+");
//                var url = Config.ReportServer.Server + "?" + Config.ReportServer.SikaSemPath + "&rs:Command=Render&rs:Format=" +
//                Config.ReportServer.Format + "&rs:" + Config.ReportServer.Parameters + "&" + inputparams;
//                var req = WebRequest.Create(url);
//                req.Credentials = new NetworkCredential(Config.Username, Config.Password, Config.Domain);
//                req.Timeout = Convert.ToInt32(Config.ReportServer.Timeout);
//                var webStream = req.GetResponse().GetResponseStream();
//                var memStream = new MemoryStream();
//                if (webStream == null) return Array.Empty<byte>();
//                webStream.CopyTo(memStream);
//                var len = memStream.Length;
//                var reportReturn = new byte[len];
//                memStream.Seek(0, SeekOrigin.Begin);
//                memStream.Read(reportReturn, 0, (int)len);
//                webStream.Close();
//                memStream.Close();
//                memStream.Dispose();
//                return reportReturn;
//            }
//else
//            {
//                var inputparams = $"szTranRefNo={type}"; // &szLanguage={language}"; Config.ReportServer.Path = Config.ReportServer.Path.Replace("/", "%2f").Replace(" ", "+");
//                var url = Config.ReportServer.Server + "?" + Config.ReportServer.Path + "&rs:Command=Render&rs:Format=" +
//                Config.ReportServer.Format + "&rs:" + Config.ReportServer.Parameters + "&" + inputparams;
//                var req = WebRequest.Create(url);
//                req.Credentials = new NetworkCredential(Config.Username, Config.Password, Config.Domain);
//                req.Timeout = Convert.ToInt32(Config.ReportServer.Timeout);
//                var webStream = req.GetResponse().GetResponseStream();
//                var memStream = new MemoryStream();
//                if (webStream == null) return Array.Empty<byte>();
//                webStream.CopyTo(memStream);
//                var len = memStream.Length;
//                var reportReturn = new byte[len];
//                memStream.Seek(0, SeekOrigin.Begin);
//                memStream.Read(reportReturn, 0, (int)len);
//                webStream.Close();
//                memStream.Close();
//                memStream.Dispose();
//                return reportReturn;
//            }
//        }
//        public void DestroyLocalReportAsync()
//        {
//            var filepath = FileHelpers.GetLocalStoreFilePath(Config.ReportServer.Folder,
//            $"{Config.ReportServer.Filename}{Config.ReportServer.Ext}");
//            if (File.Exists(filepath))
//                File.Delete(FileHelpers.GetLocalStoreFilePath(Config.ReportServer.Folder,
//                $"{Config.ReportServer.Filename}{Config.ReportServer.Ext}"));
//        }
//        public bool ExportLocalReportAsync(byte[] reportData)
//        {
//            var filepath = FileHelpers.GetLocalStoreFilePath(Config.ReportServer.Folder,
//            $"{Config.ReportServer.Filename}{Config.ReportServer.Ext}");
//            File.WriteAllBytes(filepath, reportData);
//            return File.Exists(filepath);
//        }
//        public byte[] GetExportLocalReportAsync(byte[] reportData)
//        {
//            return File.ReadAllBytes(
//            FileHelpers.GetLocalStoreFilePath(Config.ReportServer.Folder,
//            $"{Config.ReportServer.Filename}{Config.ReportServer.Ext}"));
//        }
//        public byte[] GenerateEmployeeSavingSchemeReportAsync(
//      Guid companyId, int iGroupBy, string szGroupBy, string szQueryFilter,
//      string reportQuerySelection, string uBeginPeriodId,
//      string uEndPeriodId, int iSavingSchemeType,
//      Guid userId, string language, int SavingsOrSnnit = 0)
//        { //szQueryFilter = (szQueryFilter == null) ? "''" : szQueryFilter;
//          //reportQuerySelection = (reportQuerySelection == null) ? "''" : reportQuerySelection; var inputparams =
//            $"uUserId={userId}" +
//            $"&uCompanyId={companyId}" +
//            $"&iSavingSchemeType={iSavingSchemeType}" +
//            $"&uBeginPeriodId={uBeginPeriodId}" +
//            $"&uEndPeriodId={uEndPeriodId}" +
//            $"&ReportQuerySelection={reportQuerySelection}" +
//            $"&szQueryFilter={szQueryFilter}" +
//            $"&szLanguage={language}" +
//            $"&szGroupBy={szGroupBy}" +
//            $"&iGroupBy={iGroupBy}"; Config.ReportServer.Format = "EXCEL"; Config.ReportServer.Path = Config.ReportServer.Path.Replace("/", "%2f").Replace(" ", "+");
//            var url = Config.ReportServer.Server + "?" + Config.ReportServer.Path + "&rs:Command=Render&rs:Format=" +
//            Config.ReportServer.Format + "&rs:" + Config.ReportServer.Parameters + "&" + inputparams; if (SavingsOrSnnit == 1)
//            {
//                url = url.Replace("/HCM_Reports/EmployeeSavingSchemeExport", "/HCM_Reports/SSNITReport");
//            }
//            var req = WebRequest.Create(url);
//            req.Credentials = new NetworkCredential(Config.Username, Config.Password, Config.Domain);
//            req.Timeout = Convert.ToInt32(Config.ReportServer.Timeout);
//            var webStream = req.GetResponse().GetResponseStream();
//            var memStream = new MemoryStream();
//            if (webStream == null) return Array.Empty<byte>();
//            webStream.CopyTo(memStream);
//            var len = memStream.Length;
//            var reportReturn = new byte[len];
//            memStream.Seek(0, SeekOrigin.Begin);
//            memStream.Read(reportReturn, 0, (int)len);
//            webStream.Close();
//            memStream.Close();
//            memStream.Dispose();
//            return reportReturn;
//        }
//    }


//    public class Config
//    {

//    }
//}
