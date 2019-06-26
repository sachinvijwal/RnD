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
//using Col = System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        public static double CalculateAmountForPoints()
        {
            int balancePoints = 9670;
            double balancePointsAmount = 0;
            if (balancePoints > 0)
            {
                int PointsForCashValue = 100;
                int CashValuePerPoints = 20;
                bool IsPointsRedeemInMultiple = true;
                int PointsRedeemInMultiples = 10;
                if (IsPointsRedeemInMultiple)
                {
                    int MultCnt = (int)Math.Truncate((Convert.ToDouble(balancePoints) / PointsRedeemInMultiples));
                    //Re-calculating points according to PointsRedeemInMultiples setting 
                    balancePoints = (PointsRedeemInMultiples * MultCnt);
                    balancePointsAmount = Convert.ToDouble(((Convert.ToDouble(balancePoints) / PointsForCashValue) * CashValuePerPoints));
                }
                else
                {
                    double pointsForAmount = Convert.ToDouble(balancePoints) / PointsForCashValue;
                    balancePointsAmount = Convert.ToDouble(pointsForAmount * CashValuePerPoints);
                }
            }

            Console.WriteLine("Amount - " + balancePointsAmount);

            return balancePointsAmount;
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
            private A() { }
            public static DBContext Instance { get { if (context == null) { context = new DBContext(); } return context; } }
        }

        static void Main(string[] args)
        {
            //Calling
            Console.WriteLine(A.Instance.ExecuteQuery());
            Console.WriteLine(A.Instance.ExecuteQuery());


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

                    CalculateAmountForPoints();

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
            string apiKey = "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";
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