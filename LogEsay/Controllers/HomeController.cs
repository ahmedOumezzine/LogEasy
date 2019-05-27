using LogEsay.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogEsay.Controllers
{

    public class myfun: LogEasy.MyClass
    {
        public myfun(Controller controller) : base(controller)
        {
        }

        public byte[] GetImage()
    {
        using (var ms = new MemoryStream())
        {
            FileStream fs = new FileStream(@"C:\Users\AhmedOumezzine\source\repos\LogEsay\LogEsay\Content\roothelpersHome.png", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            return br.ReadBytes((int)fs.Length);
        }
    }
    }


    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            myfun myclass = new myfun(this);
            var img = myclass.GetImage();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}