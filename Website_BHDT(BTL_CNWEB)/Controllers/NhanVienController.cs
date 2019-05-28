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
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        public ActionResult Index()
        {
            var model = new NhanVienF().NhanViens.ToList();
            return View(model);
        }
    }
}