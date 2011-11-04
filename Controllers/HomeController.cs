using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KendoDataSourceCRUD.Models;

namespace KendoDataSourceCRUD.Controllers
{
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

        public ActionResult Batch()
        {
            return View();
        }

        public ActionResult Template() {
            return View("Templates/_todoItem.tmpl.html");
        }

        public IList<Item> Items() {
            return (IList<Item>)Session["Items"];
        }
				 
        public JsonResult Read()
        {
            return this.Json(Items(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Json() {
            return this.Json("you are here", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(string name) {
            var items = Items();
            var item = new Item { Name = name, ID = 1 };

            if (items.Count > 0) {
                var nextID = (from i in items
                          select i.ID).Max() + 1;

                item.ID = nextID;
            }

            items.Add(item);

            Session["Items"] = items;

            return this.Json(item, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Item item) {
            var items = Items();
            var itemToDelete = (from i in items
                                where i.ID == item.ID
                                select i).FirstOrDefault();

            if (item != null) items.Remove(itemToDelete);
            
            return this.Json("");
        }
    }
}
