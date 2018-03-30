using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PosMvcFinal.Models;

namespace PosMvcFinal.Controllers
{
    
    public class PosController : Controller
    {
        
        public ApplicationDbContext ctx;

        public PosController()
        {
            ctx = new ApplicationDbContext();
        }
        // GET: Pos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminLogInPanel()
        {
            
            return View();
        }
        
        public ActionResult AdminPanel(Admin admin)
        {
            var loggedInAdmin = ctx.Admins.FirstOrDefault(a => a.PassWord.ToString().ToLower() == admin.PassWord && a.Name.ToString() == admin.Name);
            if (loggedInAdmin == null)
            {
               return RedirectToAction("AdminLogInPanel", "Pos");
            }
            else
            {
                return RedirectToAction("ShowItem", "Pos");
            }
            
           
        }
        public ActionResult ShowItem(Item item)
        {
            var items = ctx.Items.ToList();
            
            return View(items);
        }
        public ActionResult AddItem()
        {
            return View();
        }
        public ActionResult StoreItem(Item item)
        {
            ctx.Items.Add(item);
            ctx.SaveChanges();
            return RedirectToAction("ShowItem", "Pos");
            
        }
        public ActionResult Edit(int id)
        {
            var item = ctx.Items.Where(a => a.Id == id).FirstOrDefault();
            return View(item);
        }
        public ActionResult Delete(int id)
        {

            ctx.Items.RemoveRange(ctx.Items.Where(m => m.Id == id));
            ctx.SaveChanges();
            return RedirectToAction("ShowItem", "Pos");
        }
       
        public ActionResult Update(Item item, int id)
        {
            var items = ctx.Items.Where(m => m.Id == id).FirstOrDefault();
            items.Id = id;
            items.Price = item.Price;
            items.Stock = item.Stock;
            
            ctx.SaveChanges();
            return RedirectToAction("ShowItem", "Pos");
        }
        public ActionResult ShowItemForCustomer(Item item)
        {
            var items = ctx.Items.ToList();
            return View(items);
        }
        public ActionResult Customer()
        {
            Session["userId"] = Guid.NewGuid();
            return RedirectToAction("ShowItemForCustomer", "Pos");
        }
        public ActionResult BuyItem(int id)
        {
            Session["itemId"] = id;
            var items = ctx.Items.ToList();
            return View(items);
        }
        public ActionResult Buy(int id, int Quantity)
        {
            var _Item = ctx.Items.Where(m => m.Id == id).FirstOrDefault();
            if (_Item.Stock >= Quantity && Quantity > 0)
            {
                _Item.Stock -= Quantity;
                var boughtItem = new BoughtItem()
                {
                    Quantity = Quantity,
                    UserId = (Guid)Session["userId"],
                    Item = _Item,
                    ItemId = id,
                    TotalPrice = _Item.Price * Quantity
                };
                ctx.BoughtItems.Add(boughtItem);
                ctx.SaveChanges();
                var boughtItemList = ctx.BoughtItems.Include("Item").ToList();
                ViewBag.customer = boughtItem.UserId;
                return View(boughtItemList);
            }
            else
            {
                Session["msg"] = "Input Valid quantity";
                return RedirectToAction("BuyItem", "Pos", new {Id = _Item.Id});
            }
            
        }
        //incomplete
        public ActionResult EditCustomerItem()
        {
            return View();
        }
        //incomplete
        public ActionResult DeleteCustomerItem()
        {
            return View();
        }
    }
}