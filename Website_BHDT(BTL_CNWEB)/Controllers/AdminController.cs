using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_BHDT_BTL_CNWEB_.Models.Functions;
using Website_BHDT_BTL_CNWEB_.Models.MD_Entities;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Website_BHDT_BTL_CNWEB_.Controllers
{
    public class AdminController : Controller
    {
        DBContext_Entities db = new DBContext_Entities();

        // GET: Admin
        public ActionResult Index(string IDKhachHang)
        {
           
            return View();
        }
        public ActionResult DatabaseDocumentation()
        {
    
            return View();
        }

    }
}