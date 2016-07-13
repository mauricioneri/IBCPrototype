using IBC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IBC.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [CadastroCompletoFilter]
        public ActionResult Index()
        {
            ViewBag.IndexMode = "Dogs";
            return View();
        }

 
        [Authorize]
        public ActionResult IndexDogs()
        {
            ViewBag.IndexMode = "IndexDogs";
            return View("Index");
        }

        [Authorize]
        public ActionResult IndexContests()
        {
            ViewBag.IndexMode = "IndexContests";
            return View("Index");
        }


        [Authorize]
        public ActionResult IndexCadastro()
        {
            ViewBag.IndexMode = "IndexCadastro";
            return View("Index");
        }


        [Authorize]
        public ActionResult IndexAdministrativo()
        {
            ViewBag.IndexMode = "IndexAdministrativo";
            return View("Index");
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



         [Authorize(Roles = "MasterMTF")]
        public ActionResult Teste()
        {
            ViewBag.Message = "Página de Teste.";

            return View();
        }
    }
}