using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_BHDT_BTL_CNWEB_.Models.Functions;
using Website_BHDT_BTL_CNWEB_.Models.MD_Entities;
using System.Data;
using System.Data.SqlClient;

namespace Website_BHDT_BTL_CNWEB_.Controllers
{
    public class MyController : Controller
    {
        DBContext_Entities db = new DBContext_Entities();

        // GET: My
        public ActionResult Index(string IDKhachHang)
        {
            if (IDKhachHang == null)
            {
                var model = new SanPhamF().SanPhams.ToList();
                return View(model);
            }
           else
            {
                Session["TenTK"] = IDKhachHang;
                ViewBag.TaiKhoan = IDKhachHang;
                var model = new SanPhamF().SanPhams.ToList();
                return View(model);
            }
                
            
                
           
        }
        public ActionResult Shop(string IDNCC)
        {
            
            if(IDNCC==null)
            {
                var model = new SanPhamF().SanPhams.ToList();
                return View(model);
                
            }
            else
            {
                NhaCungCap ncc = db.NhaCungCaps.SingleOrDefault(n => n.IDNCC == IDNCC);
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                List<SanPham> lis_SP = db.SanPhams.Where(n => n.IDNCC == IDNCC).OrderBy(n => n.IDNCC).ToList();
                return View(lis_SP);
            }
            
            //string IDNCC = "NCC01";
            //var Sp = db.Database.SqlQuery<SanPham>("Select*from SanPham where @IDNCC=IDNCC", new SqlParameter("@IDNCC", IDNCC)).ToList();
            //var SP = new SanPhamF().SanPhams.ToList();
            //if (SP != null)
            //{
            //    return View(SP);
            //}
            //else return View();
        }
        public ActionResult Cart()
        {
            if(Session["TenTK"] == null)
            {
                return View("Login");
            }
            else
            {
                ViewBag.TenTK = Session["TenTK"];
                var model = new SanPhamF().DSSP_KH().ToList();
                return View(model);
            }
           
        }

        public ActionResult SingleProduct(string ID)
        {

            if (ID == null)
            {
               
                ID = "SP02";
                var model = new SanPhamF().FindEntity(ID);
                return View(model);
            }
            else
            {
                
                var model = new SanPhamF().FindEntity(ID);
                return View(model);
            }   
        }
        public ActionResult Information()
        {
            return View();
        }
        public ActionResult Contacts()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();  
        }
        public ActionResult ThongBao_DN(string username,string pass)
        {
            if ( username== "Admin" && pass == "tvL041098@")
            {
                KhachHang kh = new KhachHang();
                kh.IDKhachHang = username;
                return RedirectToAction("Index", "Admin",kh);
            }
            else
            {
                foreach (var i in db.KhachHangs)
                {
                    if (i.IDKhachHang.ToString() == username && i.MatKhau.ToString() == pass)
                    {
                        i.IDKhachHang = username;
                        Session["username"] = username;
                        return RedirectToAction("Index", "My", i);
                    }

                }
                TempData["msg"] = "Tài khoản hoặc mật khẩu không chính xác !";
                return RedirectToAction("Login");

            }

        }
        public ActionResult NguoiDung(string IDKhachHang)
        {
            ViewBag.TenTK = IDKhachHang;
            var model = new KhachHangF().FindEntity(IDKhachHang);
            return View(model);
        }
     
        public ActionResult DangKiTaiKhoan()
        {
            return View();
          
        }
        public ActionResult KiemTraTK_MK(string Ten, string MK1, string MK2)
        {

            KhachHang kh = new KhachHang();
            KhachHangF spf = new KhachHangF();
            if (MK2 != MK1)
            {
                TempData["msg"] = "Lỗi: Mật khẩu lần 2 không đúng với mật khẩu lần 1!";
                return RedirectToAction("DangKiTaiKhoan");
            }
            else
            {
                kh.IDKhachHang = Ten;
                kh.MatKhau = MK2;
                if (spf.Insert(kh) == true)
                {
                    return View("NguoiDung",kh);
                }
                else
                {
                    TempData["msg1"] = "Lỗi: Tài Khoản Đã Tồn Tại Trong Hệ Thống!";
                    return RedirectToAction("DangKiTaiKhoan");
                }
            }
        }

    }
}