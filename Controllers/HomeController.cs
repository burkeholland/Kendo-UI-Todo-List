using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KendoDataSourceCRUD.Models;
using System;

namespace KendoDataSourceCRUD.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")] 
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var items = new List<Item>();
            items.Add(new Item { ID = 1, Name = "Download Kendo UI" });
            items.Add(new Item { ID = 2, Name = "Build Amazing Apps" });

            if (Session["Items"] == null) {
                Session.Add("Items", items);
            }

            return View();
        }

        private IList<Item> Items() {
            return (IList<Item>)Session["Items"];
        }

        private void SaveItems(IList<Item> items) {
            Session["Items"] = items;
        }

        public JsonResult Read()
        {
            return this.Json(Items(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(IEnumerable<Item> item) {
            var items = Items();
            var newItem = new Item { Name = item.ElementAt(0).Name, ID = 1 };

            if (items.Count > 0) {
                var nextID = (from i in items
                          select i.ID).Max() + 1;

                newItem.ID = nextID;
            }

            items.Add(newItem);

            SaveItems(items);

            return this.Json(newItem);
        }

        public JsonResult Delete(IEnumerable<Item> itemsToDelete) {
            
            var items = Items();

            foreach(var item in itemsToDelete) {
                var itemToDelete = (from d in items
                                   where d.ID == item.ID
                                   select d).FirstOrDefault();

                if (itemToDelete != null) items.Remove(itemToDelete);
            }

            SaveItems(items);

            return this.Json(items);
        }
    }
}
