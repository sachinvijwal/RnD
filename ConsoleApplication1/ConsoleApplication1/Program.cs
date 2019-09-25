using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Web;
using System.Net;
using System.Threading;
using System.Diagnostics;
using Postal;
using System.Globalization;
using Microsoft.SqlServer.Types;
//using Col = System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int caseNo = 99;
            switch (caseNo)
            {
                case 100:
                   
                    #region CASE 100

                    string filepath = @"E:\Projects\git-repos\GroupRevMax_Web_Production\WebMARDesktopApp\WebMARDesktopApp\ClientSideJavascripts\SearchEvents.js";
                    var fileLines = File.ReadAllLines(filepath);

                    List<string> l1 = new List<string>();
                    int diff1 = 9249 - 9186;
                    for (int i = 9186; i < 9249; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(fileLines[i].Trim()))
                            l1.Add(fileLines[i].Trim());
                    }
                    List<string> l2 = new List<string>();
                    int diff2 = 9288 + diff1;
                    for (int i = 9288; i < diff2; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(fileLines[i].Trim()))
                            l2.Add(fileLines[i].Trim());
                    }

                    List<string> l3 = new List<string>();
                    int diff3 = 9506 + diff1;
                    for (int i = 9506; i < diff3; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(fileLines[i].Trim()))
                            l3.Add(fileLines[i].Trim());
                    }

                    int index = 0;
                    foreach (var line in l1)
                    {
                        if (line.IndexOf(l2[index], StringComparison.OrdinalIgnoreCase) < 0)
                            Console.WriteLine("Un-Matched Line found : " + line);

                        if (line.IndexOf(l3[index], StringComparison.OrdinalIgnoreCase) < 0)
                            Console.WriteLine("Un-Matched Line found : " + line);
                        index++;
                    }

                    Console.WriteLine("Matched Done");
                    Console.ReadLine();

                    #endregion

                    break;

                case 99:
                   
                    #region CASE 99

                    string password = "_encrypt";
                    Console.WriteLine("Please enter a string to encrypt:");
                    string plaintext = Console.ReadLine();
                    Console.WriteLine("");

                    Console.WriteLine("Your encrypted string is:");
                    string encryptedstring = StringCipher.Encrypt(plaintext, password);
                    Console.WriteLine(encryptedstring);
                    Console.WriteLine("");

                    Console.WriteLine("Your decrypted string is:");
                    string decryptedstring = StringCipher.Decrypt(encryptedstring, password);
                    Console.WriteLine(decryptedstring);
                    Console.WriteLine("");

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadLine();

                    #endregion

                    break;
                default:
                    break;
            }
            return;

            #region String Compression

            /////https://rawgit.com/nodeca/pako/master/dist/pako.js
            /////Ex1 - http://jsfiddle.net/9yH7M/1/
            /////Ex2 - http://jsfiddle.net/9yH7M/


            //List<string> l1 = new List<string>() { "1", "2" };
            //List<string> l2 = new List<string>() { "2", "3" };
            //List<string> l3 = new List<string>() { "3", "4" };
            //List<string> l4 = new List<string>() { "5", "6" };
            ////var final = l1.Union(l2).Union(l3).Union(l4).ToList();
            //var final = l1.Union(l4).Union(l3).Union(l2).ToList();
            //l1.AddRange(l4);
            //l1.AddRange(l3);
            //l1.AddRange(l2);
            //var final1 = l1.Distinct().ToList();

            //StringBuilder bulkData = new StringBuilder();
            //for (int i = 0; i < 500; i++)
            //    bulkData.Append("What is Lorem Ipsum? Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
            //byte[] compressedBytes = StringCompression.Zip(bulkData.ToString());
            //string uncompressedData = StringCompression.Unzip(compressedBytes);

            ////string e = Base64Conversions.EncodeBase64(bulkData.ToString());
            ////string d = Base64Conversions.DecodeBase64(e);

            //Console.WriteLine("Compressed Bytes Length:" + compressedBytes.Length + " bytes");
            //Console.WriteLine("Compressed KB Length:" + (Convert.ToDouble(compressedBytes.Length) / 1000) + " KB");
            //Console.WriteLine("Uncompressed Bytes Length:" + uncompressedData.Length + " bytes");
            //Console.WriteLine("Uncompressed KB Length:" + (Convert.ToDouble(uncompressedData.Length) / 1000) + " KB");

            //Console.WriteLine();

            //StringCompression.StringContentToFile(@"E:\temp_Original.txt", bulkData.ToString());
            //StringCompression.ByteArrayToFile(@"E:\temp_Compressed.txt", compressedBytes);
            //compressedBytes = StringCompression.GetByteArrayFromFile(@"E:\temp_Compressed.txt");
            //uncompressedData = StringCompression.Unzip(compressedBytes);

            //Console.WriteLine("Compressed Bytes Length:" + compressedBytes.Length + " bytes");
            //Console.WriteLine("Compressed KB Length:" + (Convert.ToDouble(compressedBytes.Length) / 1000) + " KB");
            //Console.WriteLine("Uncompressed Bytes Length:" + uncompressedData.Length + " bytes");
            //Console.WriteLine("Uncompressed KB Length:" + (Convert.ToDouble(uncompressedData.Length) / 1000) + " KB");

            //Console.ReadLine();

            //StringCompression.DeleteFile(@"E:\temp_Original.txt");
            //StringCompression.DeleteFile(@"E:\temp_Compressed.txt");

            //return;

            #endregion

            #region Get aspx page list

            List<string> pages = new List<string>();

            pages.Add("HomePage.Master".ToLower());
            pages.Add("Login.aspx".ToLower());
            pages.Add("CreateUser.aspx".ToLower());
            pages.Add("CreateAccount.aspx".ToLower());
            pages.Add("SearchAccount.aspx".ToLower());
            pages.Add("DataImportViewStatusNewUI.aspx".ToLower());
            pages.Add("MyQuote.aspx".ToLower());
            pages.Add("CreateQuote.aspx".ToLower());
            pages.Add("CreateMultiQuote.aspx".ToLower());
            pages.Add("AttendeeCrossChecks.aspx".ToLower());
            pages.Add("LeadManagement.aspx".ToLower());
            pages.Add("CreateLead.aspx".ToLower());
            pages.Add("SearchLead.aspx".ToLower());
            pages.Add("SearchContacts.aspx".ToLower());
            pages.Add("CreateContacts.aspx".ToLower());
            pages.Add("BulkEmailHistory.aspx".ToLower());
            pages.Add("SearchEvents.aspx".ToLower());
            pages.Add("CreateEvents.aspx".ToLower());
            pages.Add("BanquetCheck.aspx".ToLower());
            pages.Add("SmallHotel_Report.aspx".ToLower());
            pages.Add("WebBooking.aspx".ToLower());
            pages.Add("UploadRecords.aspx".ToLower());
            pages.Add("PMSSettings.aspx".ToLower());
            pages.Add("UploadHistory.aspx".ToLower());
            pages.Add("AllReport.aspx".ToLower());
            pages.Add("CalendarReport.aspx".ToLower());
            pages.Add("FunctionDiary.aspx".ToLower());
            pages.Add("PaceReport.aspx".ToLower());
            pages.Add("SalesTrackReport.aspx".ToLower());
            pages.Add("CreateTask.aspx".ToLower());
            pages.Add("ViewTasks.aspx".ToLower());
            pages.Add("ImportDataNewUI.aspx".ToLower());
            pages.Add("OnBoardingMain.aspx".ToLower());
            pages.Add("RoomTypes.aspx".ToLower());
            pages.Add("LeadsMapping.aspx".ToLower());
            pages.Add("LeadSources.aspx".ToLower());
            pages.Add("ConferenceRoomPricing.aspx".ToLower());
            pages.Add("Rules.aspx".ToLower());
            pages.Add("SpacequoteSettings.aspx".ToLower());
            pages.Add("UploadRates.aspx".ToLower());
            pages.Add("EmailServerSettings.aspx".ToLower());
            pages.Add("PMSIntegration.aspx".ToLower());
            pages.Add("GroupReservation.aspx".ToLower());//-- - AngularJs used, Bundles not working properly in it - (changed it working now)
            pages.Add("SalesTracking.aspx".ToLower());
            pages.Add("FoodandBeverageSetup.aspx".ToLower());
            pages.Add("Localization.aspx".ToLower());
            pages.Add("HotelAddOns.aspx".ToLower());
            pages.Add("MarketPlaceSetting.aspx".ToLower());
            pages.Add("MarketPlaceConfiguration.aspx".ToLower());
            pages.Add("MeetingPackages.aspx".ToLower());
            pages.Add("LeadsScoring.aspx".ToLower());
            pages.Add("ConferenceRooms.aspx".ToLower());
            pages.Add("ConferenceGrouping.aspx".ToLower());
            pages.Add("RoomQuoteSettings.aspx".ToLower());
            pages.Add("RoomtoSpaceRatio.aspx".ToLower());
            pages.Add("ManageUsersNewUI.aspx".ToLower());
            pages.Add("Templates.aspx".ToLower());
            pages.Add("TemplatesNew.aspx".ToLower());
            pages.Add("OverrideRequest.aspx".ToLower());
            pages.Add("DataImportNewUI.aspx".ToLower());
            pages.Add("DataImportEditCsvNewUI.aspx".ToLower());
            pages.Add("AttendeeMobileSettings.aspx".ToLower());// - AngularJs used, Bundles not working properly in it(changed it working now)
            pages.Add("MenuPackageSettingNewUI.aspx".ToLower());
            pages.Add("AudioVisualInventory.aspx".ToLower());
            pages.Add("ManageRegionNewUI.aspx".ToLower());
            pages.Add("GroupSaleSettings.aspx".ToLower());
            pages.Add("CompanyAndHotel.aspx".ToLower());
            pages.Add("Agent_PartnerSettings.aspx".ToLower());
            pages.Add("SettingWizard.aspx".ToLower());//---------------- No Js and No Css on page
            pages.Add("Myprofile.aspx".ToLower());
            pages.Add("GroupBookingNew.aspx".ToLower());
            pages.Add("WebTemplate.aspx".ToLower());
            pages.Add("AgencyBooking.aspx".ToLower());
            pages.Add("AgencySettings.aspx".ToLower());
            pages.Add("ManageWebsite.aspx".ToLower());
            pages.Add("ChangesPassword.aspx".ToLower());
            pages.Add("ManagePermission.aspx".ToLower());
            pages.Add("ManageUser.aspx".ToLower());
            pages.Add("EProposals.aspx".ToLower());
            pages.Add("GenerateContractNew.aspx".ToLower());
            pages.Add("EProposal.aspx".ToLower());
            pages.Add("AttendeeMobileNewUI.aspx".ToLower());


            string directoryPath = @"E:\Projects\git-repos\GroupRevMax_Web_Production\WebMARDesktopApp\WebMARDesktopApp";

            foreach (string path in Directory.GetFiles(directoryPath, "*.aspx", SearchOption.AllDirectories))
            {
                if (pages.Contains(Path.GetFileName(path).ToLower()))
                    continue;

                //string parentDIR = Path.GetDirectoryName(path).Replace(directoryPath, string.Empty);
                //if (!string.IsNullOrEmpty(parentDIR))
                //    parentDIR += " > ";
                //Console.WriteLine(parentDIR + Path.GetFileName(path));

                var fileContent = File.ReadAllText(path).ToLower();
                //if (fileContent.Contains(".js") || fileContent.Contains(".css"))
                if (!fileContent.Contains("tinymce.min.js") && fileContent.Contains(".js") && fileContent.Contains("HomePage.Master"))
                    Console.WriteLine(Path.GetFileName(path));

            }
            Console.ReadLine();

            #endregion


            GEOGraphy();


            string userLanguage = "it-IT";
            CultureInfo cultureItalian = new CultureInfo(userLanguage);
            DateTime checkin = DateTime.ParseExact("07/16/2019", cultureItalian.DateTimeFormat.ShortDatePattern, cultureItalian);

            //Random r = new Random();
            //var a1 = r.Next(1, 500);
            //var a2 = r.Next(1, 500);
            //var a3 = r.Next(1, 500);
            //var a4 = r.Next(1, 500);
            //var a5 = r.Next(1, 500);
            //var a6 = r.Next(1, 500);
            //Console.WriteLine(a1 * a2 * a3 / a4 * a5 * a6);
            //Console.WriteLine((a1 * a2 * a3) / a4 * a5 * a6);
            //Console.WriteLine(a1 * a2 * (a3 / a4 * a5 * a6));
            //Console.ReadLine();

            //int i = 1;
            //while (true)
            //{
            //    try
            //    {
            //        string address = "Chennai";
            //        LatLong objLatLong = new GEOLocation(address).LatLong;

            //        Console.WriteLine(i++ + ". " + objLatLong.Latitude + "," + objLatLong.Longitude);

            //        Thread.Sleep(500);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        break;
            //    }
            //}
            //Console.ReadLine();
            //return;




            //use any european culture
            var cultureInfo = CultureInfo.GetCultureInfo("it-IT");
            Console.WriteLine(String.Format(cultureInfo, "{0:C}", 1000.77));

            double amount = 24201.78;
            string currencyCode = "EUR";
            string result = string.Empty;
            CultureInfo[] _cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            Func<int, RegionInfo> GetRegion = (int cultureId) => { try { return new RegionInfo(cultureId); } catch { return null; } };
            var cultures = (from c in _cultures
                            let region = GetRegion(c.LCID)
                            where region != null && region.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
                            select c).ToList();
            var culture = (from c in _cultures
                           let r1 = GetRegion(c.LCID)
                           where r1 != null && r1.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
                           select c).FirstOrDefault();
            if (culture == null)
                result = amount.ToString("0.00");
            result = string.Format(culture, "{0:C}", amount);



            test t = new ConsoleApplication1.Program.test();
            Console.WriteLine(t.GetData());

            test t1 = new ConsoleApplication1.Program.test();
            Console.WriteLine(t1.GetData());

            Example a = new ConsoleApplication1.Program.Example();

            IInterface1 objHelloWorld1 = new ConsoleApplication1.Program.Example();
            objHelloWorld1.test();
            IInterface2 objHelloWorld2 = new ConsoleApplication1.Program.Example();
            objHelloWorld2.test();

            //Calling
            Console.WriteLine(A.Instance.ExecuteQuery());
            Console.WriteLine(A.Instance.ExecuteQuery());

            //A a = new ConsoleApplication1.Program.A();
            //Console.WriteLine(a.GetInstance().ExecuteQuery());

            StringBuilder sb = new StringBuilder();

            sb.Append("TEST FDF #1# SFD SFS #2# DFSD FDS F #3#");

            sb.Replace("#1#", "1111111111111111111");
            sb.Replace("#2#", "2222222222222222222");
            sb.Replace("#3#", "3333333333333333333");




            //dynamic email = new Email("Example");
            //email.To = "sachin.inncrewin@gmail.com";
            //email.FunnyLink = "http://www.emeetingbooker.com";
            //email.Send();


            Console.WriteLine("INR".Equals("inr", StringComparison.OrdinalIgnoreCase));


            Func<double, double, int, double> CalculateDiscount1 = new Func<double, double, int, double>(delegate (double rate, double discount, int quantity) { return ((rate - discount) * quantity); });

            Console.WriteLine(CalculateDiscount1(100, 10, 5));

            Func<double, double, int, double> CalculateDiscount2 = (rate, discount, quantity) => { return ((rate - discount) * quantity); };

            Console.WriteLine(CalculateDiscount2(100, 10, 5));


            Func<double, double, int, double> CalculateDiscount3 = (rate, discount, quantity) => ((rate - discount) * quantity);

            Console.WriteLine(CalculateDiscount3(100, 10, 5));



            String s1 = new String("%".ToArray());

            if (s1 == "%")
            {

            }


            if (ParellelEx2())
            {
                //Thread.Sleep(50000);
                //return;
            }

            #region 333333333333

            //Thread[] ts = new Thread[4];
            //for (int i = 0; i < 4; i++)
            //{
            //    ts[i] = new Thread(() =>
            //    {
            //        int temp = i;
            //        Task.Run(() => CallDummyURL());

            //    });
            //    ts[i].Start();
            //}
            //for (int i = 0; i < 4; i++)
            //    ts[i].Join();
            //Console.WriteLine("done");

            #endregion

            #region 222222222222

            //List<Thread> threads = new List<Thread>();
            //for (int i = 0; i < 100; ++i)
            //{
            //    int numCopy = i;
            //    Thread th = new Thread(() => {
            //        string html = string.Empty;
            //        string url = @"https://www.xyz.com/careers/apply/Pages/index.aspx";
            //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //        request.AutomaticDecompression = DecompressionMethods.GZip;
            //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //        using (Stream stream = response.GetResponseStream())
            //        using (StreamReader reader = new StreamReader(stream))
            //        {
            //            html = reader.ReadToEnd();
            //        }
            //        counter++;
            //        Console.WriteLine(counter);
            //        Thread.Sleep(1000);
            //    });
            //    threads.Add(th);
            //}

            //for (int i = 0; i < 100; ++i)
            //{
            //    threads[i].Start();
            //}
            //for (int i = 0; i < 100; ++i)
            //{
            //    threads[i].Join();
            //}

            //Console.WriteLine("Done");

            #endregion

            #region Other code

            if (false)
            {
                try
                {

                    //var numbers = new Col::List<int> { 1, 2, 3 };

                    //string mailBody = "DFDSA aaa dfsf sdf bbb fdsf dasf ccc";
                    //List<string> lstKeys = (new string[] { "aaa", "bbb", "ccc" }).ToList();

                    //var output = lstKeys.Aggregate(mailBody, (current, x) => current.Replace(x, "-------"));

                    //Console.WriteLine(output);

                    ////Replacing  Labels
                    //foreach (var key in lstKeys)
                    //    mailBody = mailBody.Replace(key, "xxxxxxxxxxxx");

                    //Console.WriteLine(mailBody);
                }
                finally
                {

                }


                Uri baseUri = new Uri("http://www.contoso.com/");
                Uri myUri = new Uri(baseUri, "shownew.htm?date=today");

                Console.WriteLine(myUri.Authority);

                //Regex regex = new Regex("^((http://)|(https://))*([a-zA-Z0-9]([a-zA-Z0-9\\-]{0,61}[a-zA-Z0-9])?\\.)+[a-zA-Z]{2,6}[/]*", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
                //Regex regex = new Regex(@"^(http[s]?:\/\/\[a-z]*\.[a-z]{3}\.[a-z]{2})|([a-z]*\.[a-z]{3})", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

                List<string> lstDomain = new List<string>();
                //lstDomain.Add("http://www.google.com");
                //lstDomain.Add("http://net.domain.com/Pages/Pages/gov/test.html");
                //lstDomain.Add("http://domain.com");
                //lstDomain.Add("https://domain.com");
                //lstDomain.Add("https://domain.com:1818");
                //lstDomain.Add("http://somedomain.subdomain.com.au");
                //lstDomain.Add("http://somedomain.subdomain.net.jp");
                //lstDomain.Add("http://dfedsdf.fgfsdfsdf");
                //lstDomain.Add("https://tools.ietf.org/html/rfc3986#appendix-B");
                //lstDomain.Add("https://social.msdn.microsoft.com/Forums/zh-CN/491b86fe-c062-4f81-96fc-1cfae37f3aa2/c-regex-best-validation-of-domain?forum=csharpgeneral");
                //lstDomain.Add("http://www.example.co.uk");
                //lstDomain.Add("http://example.co.uk");
                //lstDomain.Add("http://subdomain.example.co.uk");

                lstDomain.Add("http://www.instagathering.com");
                lstDomain.Add("http://www.instagathering.co.in");
                lstDomain.Add("http://test.instagathering.com");
                lstDomain.Add("http://test.instagathering.co.in");

                lstDomain.Add("http://www.facilemeeting.com");
                lstDomain.Add("http://test.facilemeeting.com");

                lstDomain.Add("http://localhost:8080");
                lstDomain.Add("http://facilemeeting.co.in");


                //http(s)://yahoo.com -> domain:yahoo.com | subdomain:
                //http(s)://test.yahoo.com -> domain:yahoo.com | subdomain:test
                //http(s)://test.testing.yahoo.com -> domain:yahoo.com | subdomain:test.testing
                //http(s)://localhost:55555 -> domain:localhost | subdomain:
                //http(s)://test.localhost:55555 -> domain:localhost | subdomain:test
                //http(s)://test.testing.localhost:55555 -> domain:localhost | subdomain:test.testing
                //http(s)://yahoo.com.uk -> domain:yahoo.com.uk | subdomain:
                //http(s)://test.yahoo.com.uk -> domain:yahoo.com.uk | subdomain:test


                foreach (var input in lstDomain)
                {
                    var newURL = input.ToLower().Replace("www.", string.Empty);
                    Uri uri = new Uri(newURL);
                    //Console.WriteLine($"\t ok - {uri.Authority}");
                    Console.WriteLine($"\t ok - {uri.Host}");
                    //Console.WriteLine($"\t ok3 - {(uri.Host + (uri.IsDefaultPort ? "" : ":" + uri.Port))}");

                    newURL = newURL.Replace(":" + Convert.ToString(uri.Port), string.Empty);

                    var fromEmailAdd = "donotreply@" + Regex.Replace(newURL, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", string.Empty, RegexOptions.IgnoreCase);
                    bool isEmail = Regex.IsMatch(fromEmailAdd, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                    if (!isEmail)
                        fromEmailAdd = null;

                    Console.WriteLine($"\t email - {fromEmailAdd}");
                    Console.WriteLine();

                    var hostUrl = uri.Host;
                    hostUrl = hostUrl.Substring(hostUrl.IndexOf('.') + 1);
                    Console.WriteLine($"\t hostUrl - {hostUrl}");

                    //Execute(fromEmailAdd, uri.Host).Wait();



                }

                //Console.WriteLine("\n Email list");
                //foreach (var input in lstDomain)
                //{
                //    Console.WriteLine(GetFromEmail(input));
                //}

                Console.WriteLine("\n Urls");
                foreach (var input in lstDomain)
                {
                    //Uri uri = new Uri(input);
                    //Console.WriteLine();
                    //Console.WriteLine(URLExtensions.Domain(input));
                    //Console.WriteLine(URLExtensions.CurrentURL(input));
                    //Console.WriteLine(GetFromEmail(input));

                    Console.WriteLine(URLHelper.CurrentURL(input));
                    Console.WriteLine(URLHelper.GetBaseURL(input));

                }

                Console.WriteLine("\n----------------------- ExtractDomainFromURL -----------------------");
                foreach (var input in lstDomain)
                {
                    Console.WriteLine("\n" + input);
                    Console.WriteLine(GetDomain(input));
                    Console.WriteLine(Regex.Replace(input, @"^(?:http(?:s)?://)?(?:www(?:[0-9]+)?\.)?", string.Empty, RegexOptions.IgnoreCase));
                    //Console.WriteLine(GetSubDomain(new Uri(input), GetSubDomainEnum.ExcludeWWW));
                }

                //Execute("sachin@inncrewin.com", "Test email...");

            }

            #endregion

            Console.ReadKey();
        }

        #region parallel for async c# - example 1

        public static void StartThreadEx1()
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine("Waiting for all tasks to complete");
            RunWorkers().Wait();
            Console.WriteLine("All tasks completed in " + sw.Elapsed);
        }

        public static async Task RunWorkers()
        {
            await Task.WhenAll(
                JobDispatcher(6000, "task 1"),
                JobDispatcher(5000, "task 2"),
                JobDispatcher(4000, "task 3"),
                JobDispatcher(3000, "task 4"),
                JobDispatcher(2000, "task 5"),
                JobDispatcher(1000, "task 6")
            );
        }
        public static async Task JobDispatcher(int time, string query)
        {
            var results = await Task.WhenAll(
                worker(time, query + ": Subtask 1"),
                worker(time, query + ": Subtask 2"),
                worker(time, query + ": Subtask 3")
            );

            Console.WriteLine(string.Join("\n", results));
        }

        static async Task<string> worker(int time, string query)
        {
            return await Task.Run(() =>
            {
                Console.WriteLine("Starting worker " + query);
                Thread.Sleep(time);
                Console.WriteLine("Completed worker " + query);
                return query + ": " + time + ", thread id: " + Thread.CurrentThread.ManagedThreadId;
            });
        }
        #endregion

        #region Run unlimited thread parellel - Example 2

        public static int counter = 0;
        public static object fileLock = new object();
        //public static void CallDummyURL()
        public static async Task CallDummyURL()
        {
            //Task.Run(() =>
            //await Task.Run(() =>
            //{
            try
            {
                string html = string.Empty;
                string url = @"https://www.test.com/careers/apply/Pages/index.aspx";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                counter++;
                Console.WriteLine(counter);

                #region WriteLog in File

                //lock (fileLock)
                //{
                //    string filePath = @"C:\Temp\WriteLines.txt";
                //    if (!File.Exists(filePath))
                //    {
                //        using (File.Create(filePath)) { }
                //    }
                //    System.IO.File.AppendAllText(filePath, counter + "\r\n");
                //}

                #endregion

                //Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //});
        }
        public static bool ParellelEx2()
        {
            #region Run unlimited thread

            var sw = Stopwatch.StartNew();
            Console.WriteLine("Waiting for all tasks to complete");
            int max = 1000;
            //List<Task> tasks = new List<Task>();
            List<Task> tasks = new List<Task>();

            //Parallel.For(0, max, async (i) =>
            Parallel.For(0, max, (i) =>
            {
                //tasks.Add(CallDummyURL());
                //Task.Run(() => CallDummyURL());
                new Thread(async () =>
                {
                    await CallDummyURL();

                }).Start();

                //Thread.Sleep(3000);
            });
            //await CallDummyURL();

            #region trying to join in current thread

            //Task.WaitAll(tasks.ToArray());

            //-------------------- OR --------------------

            //var stopwatch = Stopwatch.StartNew();
            //// Create an array of Thread references.
            //Thread[] array = new Thread[tasks.Count];
            //int i = 0;
            //foreach (var t in tasks)
            //{
            //    // Start the thread with a ThreadStart.
            //    array[i] = new Thread(new ThreadStart(CallDummyURL()));
            //    array[i].Start();
            //    i++;
            //}
            //// Join all the threads.
            //for (i = 0; i < array.Length; i++)
            //{
            //    array[i].Join();
            //}
            //Console.WriteLine("DONE: {0}", stopwatch.ElapsedMilliseconds);

            #endregion

            if (counter >= max)
            {
                Console.WriteLine("All tasks completed in " + sw.Elapsed);
            }

            #endregion

            return true;
        }

        #endregion

        /// <summary>
        /// Singleton pattern
        /// </summary>
        public class DBContext { public string ExecuteQuery() { return "SQL DB CONTEXT"; } }
        public class A
        {
            private static DBContext context = null;
            //private A() { }
            public static DBContext Instance { get { if (context == null) { context = new DBContext(); } return context; } }

            public DBContext GetInstance() { return context; }
        }

        private interface IInterface1 { void test(); }
        private interface IInterface2 { void test(); }
        public class Example : IInterface1, IInterface2
        {
            public void test()
            {
                Console.WriteLine("test 1");
            }
        }

        public class test
        {
            static int aaa;
            static test()
            {
                aaa++;
            }
            //private test()
            //{

            //}
            public int GetData()
            {
                return aaa;
            }
        }

        //protected class AbstractClassEx
        //{
        //}

        //class AbstractClassEx1
        //{
        //    public void GetName()
        //    {
        //        Console.WriteLine("AbstractClassEx1");
        //    }
        //}
        //abstract class AbstractClassEx2 : AbstractClassEx1
        //{
        //    public new void GetName()
        //    {
        //        this.GetName();
        //    }
        //}

        //public class AbstractDerivedClass : AbstractClassEx
        //{
        //}


        public static string GetDomain(string sURL)
        {
            string result = string.Empty;
            Uri uri = new Uri(sURL);
            string[] splitUrl = uri.Host.Split('.');

            int index = (splitUrl.Length > 2 || splitUrl.Contains("www")) ? 1 : 0;

            for (int i = index; i < splitUrl.Length; i++)
                result += splitUrl[i] + ".";

            return result.Trim('.');
        }

        //public static string ExtractDomainFromURL(string sURL)
        //{
        //    Regex rg = new Regex(@"://(?<host>([a-z\d][-a-z\d]*[a-z\d]\.)*[a-z][-a-z\d]+[a-z])");
        //    if (rg.IsMatch(sURL))
        //        return rg.Match(sURL).Result("${host}");
        //    else
        //        return String.Empty;
        //}

        //public enum GetSubDomainEnum
        //{
        //    ExcludeWWW,
        //    IncludeWWW
        //};

        //public static string GetSubDomain(Uri uri, GetSubDomainEnum getSubDomainOption = GetSubDomainEnum.ExcludeWWW)
        //{
        //    var subdomain = new StringBuilder();
        //    for (var i = 0; i < uri.Host.Split(new char[] { '.' }).Length - 2; i++)
        //    {
        //        if (getSubDomainOption == GetSubDomainEnum.ExcludeWWW && uri.Host.Split(new char[] { '.' })[i].ToLowerInvariant() == "www") continue;
        //        subdomain.Append((i < uri.Host.Split(new char[] { '.' }).Length - 3 &&
        //                          uri.Host.Split(new char[] { '.' })[i + 1].ToLowerInvariant() != "www") ?
        //                               uri.Host.Split(new char[] { '.' })[i] + "." :
        //                               uri.Host.Split(new char[] { '.' })[i]);
        //    }
        //    return subdomain.ToString();
        //}


        static void Execute(string fromEmail, string fromName)
        {
            string apiKey = "fdfdfsdfdsf";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var subject = fromName;
            var to = new EmailAddress("sachin.inncrewin@gmail.com", "S V");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            var plainTextContent = string.Empty;
            //var htmlContent = "1. <strong data=\"return $(\"a[ghelpcontext = long_header]\").parentElement.remove();\">and easy to do anywhere, even with C#</strong>";
            var htmlContent = "1. <strong onmouseover='test'>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg).Result;
        }

        public static string GetFromEmail(string url)
        {
            bool isValidEmail = false;
            string fromEmailAddress = string.Empty;
            try
            {
                string host = new Uri(url).Host.ToLower();
                if (!host.Contains("localhost"))
                {
                    host = host.Replace("www.", "");

                    //generating email address
                    fromEmailAddress = "donotreply@" + host;

                    //Validate from Email
                    isValidEmail = Regex.IsMatch(fromEmailAddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                }
            }
            catch { }

            //If email is invalid or any exception comes, Set default From-Email from config setting
            if (!isValidEmail)
                fromEmailAddress = "NOT FOUND";

            return fromEmailAddress;
        }

        //public void SendEmailForCloud(string FromName, string fromAddress, List<String> ToAddress, string Subject, string body, Stream defaultLogo, List<string> Attachments = null, List<string> ToCC = null, List<string> ToBCC = null, List<CRMAttachment> _crmAttachment = null, string replyToAddress = null)
        //{
        //    try
        //    {
        //        FromName = "Test Email";
        //        string sgAPIKey = "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";
        //        string sguid = "sachin";
        //        string sgpwd = "sachin";

        //        List<EmailAddress> recipients = new List<EmailAddress>();

        //        foreach (var email in ToAddress)
        //            recipients.Add(new EmailAddress() { Email = email });


        //        string text = "";

        //        if (string.IsNullOrEmpty(Subject))
        //            Subject = " ";

        //        // HTML Body

        //        string html = body;

        //        if (string.IsNullOrEmpty(body))
        //        {
        //            html = "<table><tr><td></td></tr></table>";
        //        }

        //        var myMessage = new SendGridMessage();

        //        myMessage.AddTos(recipients);

        //        myMessage.From = new EmailAddress(fromAddress);

        //        myMessage.Subject = Subject;

        //        myMessage.PlainTextContent = text;

        //        myMessage.HtmlContent = html;


        //        // Recipients

        //        //myMessage.Header.SetTo(ToAddress);

        //        // CC
        //        if (ToCC != null)
        //        {
        //            foreach (var str in ToCC)
        //            {
        //                if (!string.IsNullOrEmpty(str))
        //                    myMessage.AddCc(str);
        //            }
        //        }

        //        //BCC
        //        if (ToBCC != null)
        //        {
        //            foreach (var str in ToBCC)
        //            {
        //                if (!string.IsNullOrEmpty(str))
        //                    myMessage.AddBcc(str);
        //            }
        //        }

        //        //

        //        /* SEND THE MESSAGE

        //             * ===================================================*/

        //        //var credentials = new NetworkCredential(sgUsername, sgPassword);

        //        //// Create a Web transport for sending email.

        //        //var transportWeb = new Web(credentials);

        //        ////Attachments
        //        Dictionary<String, byte[]> StreamedAttachments = new Dictionary<string, byte[]>();
        //        if (Attachments != null)
        //        {
        //            //string fileName = string.Empty;
        //            //foreach (var files in Attachments)
        //            //{
        //            //    string[] fileArr = files.Split('\\');
        //            //    if (fileArr.Length > 0)
        //            //        fileName = fileArr[fileArr.Length - 1];
        //            //    else
        //            //        fileName = fileArr[0];

        //            //    using (FileStream fs = new FileStream(files, FileMode.Open, FileAccess.Read))
        //            //    {
        //            //        byte[] fileData = new byte[fs.Length];
        //            //        fs.Read(fileData, 0, fileData.Length);
        //            //        StreamedAttachments.Add(fileName, fileData);
        //            //    }
        //            //}

        //            //foreach (var item in StreamedAttachments)
        //            //{
        //            //    myMessage.AddAttachment(new MemoryStream(item.Value), item.Key);
        //            //}
        //        }

        //        if (_crmAttachment != null)
        //        {
        //            //string fileName = string.Empty;
        //            //foreach (var files in _crmAttachment)
        //            //{
        //            //    fileName = files.FileName;
        //            //    //using (FileStream fs = new FileStream(files.FileGUIDPath, FileMode.Open, FileAccess.Read))
        //            //    using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(files.FileGUIDPath), FileMode.Open, FileAccess.Read))
        //            //    {
        //            //        byte[] fileData = new byte[fs.Length];
        //            //        fs.Read(fileData, 0, fileData.Length);
        //            //        StreamedAttachments.Add(fileName, fileData);
        //            //    }
        //            //}

        //            //foreach (var item in StreamedAttachments)
        //            //{
        //            //    myMessage.AddAttachment(new MemoryStream(item.Value), item.Key);
        //            //}
        //        }



        //        if (replyToAddress != null && replyToAddress.Length > 0)
        //        {
        //            //List<EmailAddress> replyEmails = new List<EmailAddress>();
        //            EmailAddress replyEmail = new EmailAddress(replyToAddress);
        //            //replyEmails.Add(replyEmail);
        //            myMessage.ReplyTo = replyEmail;
        //        }

        //        //// Send the email.

        //        //transportWeb.Deliver(myMessage);

        //        SendGridClient client = new SendGridClient();
        //        SendGridMessage m = new SendGridMessage();

        //        var transportWeb = new SendGrid.Web(new System.Net.NetworkCredential(sguid, sgpwd));

        //        //transportWeb.DeliverAsync(myMessage);
        //        transportWeb.Deliver(myMessage);
        //    }
        //    catch (Exception ex) {
        //    }
        //}

        #region GEOGraphy

        public static void GEOGraphy()
        {
            double earthRadius = 6378137; // meters => from both nad83 & wgs84
            var a = new { lat = 12.9715987, lng = 77.5945627 };
            var b = new { lat = 12.9894459, lng = 77.5949382 };

            // sql geography lib
            SqlGeographyBuilder sgb;
            sgb = new SqlGeographyBuilder();
            sgb.SetSrid(4326);
            sgb.BeginGeography(OpenGisGeographyType.Point);
            sgb.BeginFigure(a.lat, a.lng);
            sgb.EndFigure();
            sgb.EndGeography();
            SqlGeography geoA = sgb.ConstructedGeography;

            sgb = new SqlGeographyBuilder();
            sgb.SetSrid(4326);
            sgb.BeginGeography(OpenGisGeographyType.Point);
            sgb.BeginFigure(b.lat, b.lng);
            sgb.EndFigure();
            sgb.EndGeography();
            SqlGeography geoB = sgb.ConstructedGeography;

            // distance cast from SqlDouble
            double geoDistance = (double)geoA.STDistance(geoB);

            // math!
            double d2r = Math.PI / 180; // for converting degrees to radians
            double lat1 = a.lat * d2r,
                lat2 = b.lat * d2r,
                lng1 = a.lng * d2r,
                lng2 = b.lng * d2r,
                dLat = lat2 - lat1,
                dLng = lng2 - lng1,
                sin_dLat_half = Math.Pow(Math.Sin(dLat / 2), 2),
                sin_dLng_half = Math.Pow(Math.Sin(dLng / 2), 2),
                distance = sin_dLat_half + Math.Cos(lat1) * Math.Cos(lat2) * sin_dLng_half;

            // math distance
            double mathDistance = (2 * Math.Atan2(Math.Sqrt(distance), Math.Sqrt(1 - distance))) * earthRadius;

            // haversine
            double sLat1 = Math.Sin(a.lat * d2r),
                    sLat2 = Math.Sin(b.lat * d2r),
                    cLat1 = Math.Cos(a.lat * d2r),
                    cLat2 = Math.Cos(b.lat * d2r),
                    cLon = Math.Cos((a.lng * d2r) - (b.lng * d2r)),
                    cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon,
                    d = Math.Acos(cosD);

            // math distance
            double methDistance = d * earthRadius;


            // write the outputs
            Console.WriteLine("geo distance:\t" + geoDistance / 1000);    // 1422.99560435875
            Console.WriteLine("math distance:\t" + mathDistance / 1000);  // 1421.73656776243
            Console.WriteLine("meth distance:\t" + methDistance / 1000);  // 1421.73656680185
            Console.WriteLine("geo vs math:\t" + (geoDistance - mathDistance) / 1000);     // 1.25903659632445
            Console.WriteLine("haversine vs math:\t" + (methDistance - methDistance) / 1000); // ~0.00000096058011
        }

        #endregion
    }

    public static class URLExtensions
    {
        // e.g. azurewebsites.net
        public static string Domain(string url)
        {
            string Host = string.Empty;
            try
            {
                return new Uri(url).Host.ToLower();
            }
            catch { }
            return Host;
        }
        // e.g. https://pavey.azurewebsites.net/
        public static string CurrentURL(string url)
        {
            return new Uri(url).GetLeftPart(UriPartial.Authority).TrimStart('/').TrimEnd('/');
        }
    }


    public class URLHelper
    {
        private URLHelper() { }

        public static string CurrentURL(string url)
        {
            string currentURL = url;
            try
            {
                Uri uri = new Uri(url);
                currentURL = uri.GetLeftPart(UriPartial.Authority).Trim('/');
            }
            catch { }
            return currentURL;
        }

        public static string GetBaseURL(string url)
        {
            string Host = url;
            try
            {
                return new Uri(url).Host.ToLower().Trim('/');
            }
            catch { }
            return Host;
        }
    }
}