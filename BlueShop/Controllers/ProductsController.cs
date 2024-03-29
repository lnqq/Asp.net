﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlueShop.Models;

namespace BlueShop.Controllers
{
    public class ProductsController : Controller
    {
        private ShopOnlineEntities db = new ShopOnlineEntities();
        //List Name
        public ActionResult ListName(string key, string LSP) {
            var product = from p in db.Products
                        where p.IsDeleted == false
                        select p;
            if (!string.IsNullOrEmpty(key))
            {
                product = product.Where(e => e.Name.Contains(key));

            }
            else {
                if (!string.IsNullOrEmpty(LSP))
                {
                    product = product.Where(e => e.CategoryProduct.Desciption.Contains(LSP));

                }
            }
            

            return View(product.ToList());
        }

        // GET: Products1
        public ActionResult Index()
        {
            var product = from p in db.Products
                          where p.IsDeleted == false
                          select p;
            return View(product.ToList());
        }

        // GET: Products1/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products1/Create
  
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryProductID = new SelectList(db.CategoryProducts, "CategoryProductID", "Desciption", product.CategoryProductID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "NameCity", product.CityID);
            ViewBag.ShopID = new SelectList(db.Shops, "ShopID", "NameShop", product.ShopID);
            return View(product);
        }

        // POST: Products1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,CategoryProductID,Name,Desciption,Price,CityID,Image,ShopID,Count")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryProductID = new SelectList(db.CategoryProducts, "CategoryProductID", "Desciption", product.CategoryProductID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "NameCity", product.CityID);
            ViewBag.ShopID = new SelectList(db.Shops, "ShopID", "NameShop", product.ShopID);
            return View(product);
        }

        // GET: Products1/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
