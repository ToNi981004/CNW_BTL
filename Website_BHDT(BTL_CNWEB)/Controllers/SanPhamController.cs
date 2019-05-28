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
using System.Net;

namespace Website_BHDT_BTL_CNWEB_.Controllers
{
    public class SanPhamController : Controller
    {
        DBContext_Entities db = new DBContext_Entities();
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Forms()
        {
            return View();
        }
        //[HttpPost]
        public ActionResult ProductInformation(string ID)
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
        public ActionResult Tables()
        {
            
            var model = new SanPhamF().SanPhams.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            
            return View();
           
        }

        [HttpPost]
        public ActionResult Create(SanPham sp, HttpPostedFileBase fileUpLoad)
        {

            SanPhamF spf = new SanPhamF();
          

            string fileName = Path.GetFileNameWithoutExtension(fileUpLoad.FileName);
            string extension = Path.GetExtension(fileUpLoad.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
            sp.Anh = "/Content/Image_Product/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/Image_Product/"), fileName);
            fileUpLoad.SaveAs(fileName);


            if (spf.Insert_SanPham(sp) == true)
            {
                return RedirectToAction("Tables");
            }
            else
            {
                return View();
            }


        }
        public ActionResult Edit_SanPham(string IDSanPham)
        { 
          
            SanPham sp = db.SanPhams.Find(IDSanPham);
            ViewBag.NhaCungCap = new SelectList(db.NhaCungCaps, "IDNCC", "TenNCC", sp.IDSanPham);
          
            return View(sp);

        }

        [HttpPost]
        public ActionResult Edit_SanPham(SanPham sp,HttpPostedFileBase UploadAnh)
        {
            ViewBag.IDNCC = new SelectList(db.NhaCungCaps, "IDNCC", "TenNCC", sp.IDNCC);
            try
            {
                if(UploadAnh ==  null)
                {
                    ModelState.AddModelError("File", "Chưa upload file ảnh");
                }
                else
                {
                    if(UploadAnh.ContentLength>0)
                    {
                        var fileName = Path.GetFileName(UploadAnh.FileName);
                        var path = Path.Combine(Server.MapPath("/Content/Image_Product"), fileName);
                        UploadAnh.SaveAs(path);


                        SanPhamF spf = new SanPhamF();
                        sp.Anh = "/Content/Image_Product/" + fileName;
                        if(spf.Update(sp))
                        {
                            return RedirectToAction("Tables");
                        }
                    }                   
                }
                return View();
            }
            catch
            {
                return View();
            }           
        }
        public ActionResult Delete_SanPham(string IDSanPham)
        {
            SanPhamF spf = new SanPhamF();
            if(spf.Delete_SanPham(IDSanPham)==true)
            {
                return RedirectToAction("Tables");
            }
            else
            {
                return View();
            }
           
        }
    }
}