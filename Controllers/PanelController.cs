﻿using GroceryDeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GroceryDeliverySystem.Controllers
{
    [Authorize(Roles = "A, S")]
    public class PanelController : Controller
    {
        groceryDBEntities gdb = new groceryDBEntities();
        // GET: Panel
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "A")]
        public ActionResult Users()
        {
            return View(gdb.Users.ToList());
        }

        [Authorize(Roles = "A")]
        public ActionResult Couriers()
        {
            return View(gdb.DeliveryPerson.ToList());
        }

        [Authorize(Roles = "A")]
        public ActionResult Cities()
        {
            return View(gdb.Cities.ToList());
        }

        public ActionResult Stores()
        {
            if (User.IsInRole("S"))
            {
                var userEmail = User.Identity.Name;
                var user = gdb.Users.FirstOrDefault(x => x.email == userEmail);
                if (user.managedStore != null)
                {
                    return View(gdb.Stores.Where(x => x.id == user.managedStore).ToList());
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View(gdb.Stores.ToList());
            }
        }

        public ActionResult Categories()
        {
            if (User.IsInRole("S"))
            {
                var userEmail = User.Identity.Name;
                var user = gdb.Users.FirstOrDefault(x => x.email == userEmail);
                if (user.managedStore != null)
                {
                    var store = gdb.Stores.FirstOrDefault(x => x.id == user.managedStore);
                    return View(gdb.Categories.Where(x => x.storeID == store.id).ToList());
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View(gdb.Categories.ToList());
            }
        }

        public ActionResult Products()
        {
            if (User.IsInRole("S"))
            {
                var userEmail = User.Identity.Name;
                var user = gdb.Users.FirstOrDefault(x => x.email == userEmail);
                if (user.managedStore != null)
                {
                    var store = gdb.Stores.FirstOrDefault(x => x.id == user.managedStore);
                    var ctg = gdb.Categories.Where(x => x.storeID == store.id).ToList();
                    List<Products> p = new List<Products>();

                    foreach (var c in ctg)
                    {
                        var products = from prod in gdb.Products
                                       where prod.categoryID == c.id
                                       select prod;

                        p.AddRange(products);
                    }

                    return View(p);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View(gdb.Products.ToList());
            }
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public string DeleteUser(int id)
        {
            Users u = gdb.Users.FirstOrDefault(x => x.id == id);
            try
            {
                gdb.Users.Remove(u);
                gdb.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult AddUser(Users user)
        {
            gdb.Users.AddOrUpdate(user);
            gdb.SaveChanges();
            if (user.cartID == null)
            {
                var c = new Carts { userID = user.id };
                gdb.Carts.Add(c);
                gdb.SaveChanges();
                user.cartID = c.id;
            }
            if (user.roles == null)
            {
                user.roles = "C";
            }
            gdb.SaveChanges();
            return RedirectToAction("Users");
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public string DeleteCourier(int id)
        {
            DeliveryPerson cour = gdb.DeliveryPerson.FirstOrDefault(x => x.id == id);
            try
            {
                gdb.DeliveryPerson.Remove(cour);
                gdb.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult AddCourier(DeliveryPerson cour)
        {
            gdb.DeliveryPerson.AddOrUpdate(cour);
            gdb.SaveChanges();
            return RedirectToAction("Couriers");
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public string DeleteCity(int id)
        {
            Cities c = gdb.Cities.FirstOrDefault(x => x.id == id);
            try
            {
                gdb.Cities.Remove(c);
                gdb.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult AddCity(Cities c)
        {
            gdb.Cities.AddOrUpdate(c);
            gdb.SaveChanges();
            return RedirectToAction("Cities");
        }

        [HttpPost]
        public string DeleteStore(int id)
        {
            Stores st = gdb.Stores.FirstOrDefault(x => x.id == id);
            try
            {
                gdb.Stores.Remove(st);
                gdb.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        [HttpPost]
        public ActionResult AddStore(Stores st)
        {
            gdb.Stores.AddOrUpdate(st);
            gdb.SaveChanges();
            return RedirectToAction("Stores");
        }

        [HttpPost]
        public string DeleteCategory(int id)
        {
            Categories ct = gdb.Categories.FirstOrDefault(x => x.id == id);
            try
            {
                gdb.Categories.Remove(ct);
                gdb.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        [HttpPost]
        public ActionResult AddCategory(Categories ct)
        {
            gdb.Categories.AddOrUpdate(ct);
            gdb.SaveChanges();
            return RedirectToAction("Categories");
        }

        [HttpPost]
        public string DeleteProduct(int id)
        {
            Products pr = gdb.Products.FirstOrDefault(x => x.id == id);
            try
            {
                gdb.Products.Remove(pr);
                gdb.SaveChanges();
                return "successful";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        [HttpPost]
        public ActionResult AddProduct(Products pr)
        {
            gdb.Products.AddOrUpdate(pr);
            gdb.SaveChanges();
            return RedirectToAction("Products");
        }
    }
}