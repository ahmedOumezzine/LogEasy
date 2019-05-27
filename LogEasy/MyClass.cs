using LogEasy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LogEasy
{
 
    public class MyClass
    {
        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
        private string GetUser_IP()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        public string getAddressMac()
        {
            String firstMacAddress = NetworkInterface
    .GetAllNetworkInterfaces()
    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
    .Select(nic => nic.GetPhysicalAddress().ToString())
    .FirstOrDefault();
            return firstMacAddress;
        }

        Stopwatch sw;
        public string AreaName = null;
        public string ActionName = null;
        public string ControllerName = null;

        Models.InstanceLogger logger = new Models.InstanceLogger();

        /// <summary>
        /// Constructeur complet
        /// </summary>
        /// <param name="controller"></param>
        public MyClass(Controller controller)
        {
            try
            {

                this.ControllerName = controller.RouteData.GetRequiredString("controller");
                Type type = this.GetType().UnderlyingSystemType;
                var address = GetUser_IP();
                logger.LocalIP = LocalIPAddress().ToString();
                logger.PublicIP = GetUser_IP();
                logger.AddressMac = getAddressMac();
                logger.InstanceName = type.Name;
                logger.ControllerName = ControllerName;
                logger.ActionName = controller.RouteData.GetRequiredString("action");
                sw = Stopwatch.StartNew();
                logger.CreatedDateInst = DateTime.Now;
            }
            catch (Exception ex)
            {
                var nbr = 2;
            }
        }



        public MyClass()
        {
            Type type = this.GetType().UnderlyingSystemType;
            var address = GetUser_IP();
            logger.LocalIP = LocalIPAddress().ToString();
            logger.PublicIP = GetUser_IP();
            logger.AddressMac = getAddressMac();
            logger.InstanceName = type.Name;
            sw = Stopwatch.StartNew();
            logger.CreatedDateInst = DateTime.Now;
        }



        ~MyClass()
        {
            sw.Stop();
            logger.DisposedDateInst = DateTime.Now;
            logger.Stopwatch = sw.Elapsed.ToString();
            ApplicationDbContext ctx = new ApplicationDbContext();
            ctx.ExceptionLoggers.Add(logger);
            ctx.SaveChanges();
        }
    }
}
