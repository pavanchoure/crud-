using CrudApplicationDataFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudApplicationDataFirst.Controllers
{
    public class HomeController : Controller
    {
        DatabaseFistEFEntities Db = new DatabaseFistEFEntities();
        // GET: Home
        public ActionResult Index(string searchBy, string search)
        {
            if(searchBy=="Name")
            {
                var data = Db.Students.Where(model => model.Name.StartsWith(search)).ToList();
                return View(data);
            }
            else if(searchBy == "Gender")
            {
                var data = Db.Students.Where(model => model.Gender == search).ToList();
                return View(data);
            }
            else
            {
            var data = Db.Students.ToList();
            return View(data);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid==true)
            {
                Db.Students.Add(s);
                int a = Db.SaveChanges();
                if (a > 0)
                {
                    TempData["Insert Message"] = "<script>alert(' Record Has Been Inserted!')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Insert Message"] = "<script>alert('Not Inserted!')</script>";
                }
            }
            return View();
        }

        public ActionResult Edit(int Id)
        {
            var row = Db.Students.Where(model => model.Id == Id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(Student s)
        {
            if (ModelState.IsValid == true)
            {
                Db.Entry(s).State = EntityState.Modified;
                int a = Db.SaveChanges();
                if (a > 0)
                {
                    TempData["Update Message"] = "<script>alert(' Record has been Updated!')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Update Message"] = "<script>alert('Not Updated!')</script>";
                }
            }
        
                return View();
        }

        public ActionResult Delete(int Id)
        {
            if(Id > 0)
            { 
            var DeletedRow = Db.Students.Where(model => model.Id == Id).FirstOrDefault();
            if(DeletedRow != null)
                {
                    Db.Entry(DeletedRow).State = EntityState.Deleted;
                    int a = Db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["Delete Message"] = "<script>alert(' Record Deleted Succesfull!')</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Delete Message"] = "<script>alert('Not Deleted!')</script>";
                    }

                }
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult Delete(Student s)
        //{
        //    if (ModelState.IsValid == true)
        //    {
        //        Db.Entry(s).State = EntityState.Deleted;
        //        int a = Db.SaveChanges();
        //        if (a > 0)
        //        {
        //            TempData["Delete Message"] = "<script>alert('Deleted!')</script>";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            TempData["Delete Message"] = "<script>alert('Not Deleted!')</script>";
        //        }
        //    }

        //    return View();

        //}
        public ActionResult Details(int Id)
        {
            var row = Db.Students.Where(model => model.Id == Id).FirstOrDefault();
            return View(row);
        }

    }
}