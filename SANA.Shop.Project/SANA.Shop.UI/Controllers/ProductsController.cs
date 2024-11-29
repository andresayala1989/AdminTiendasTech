using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SANA.Shop.BLL.DAO;
using SANA.Shop.DAL.ViewModels;
using System.Runtime.Caching;

namespace SANA.Shop.UI.Controllers
{
    public class ProductsController : Controller
    {

        /// <summary>
        /// Get all items of Products
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            string isMemory = "";
            var context = System.Web.HttpContext.Current;
            isMemory = context.Application["inMemory"] == null ? "" : context.Application["inMemory"].ToString();
            if(isMemory == "")
            {
                products = SQLDAOProducts.Instance.GetAll();
                var cache = MemoryCache.Default;
                cache.Contains("CacheKey");
                MemoryCache.Default.AddOrGetExisting("CacheKey", products, DateTime.Now.AddHours(12));
                ViewBag.sesion = "false";
                context.Application["inMemory"] = ViewBag.sesion;
            } else if(isMemory == "true")
            {
                object list = MemoryCache.Default.AddOrGetExisting("CacheKey", products, DateTime.Now.AddHours(12));
                products = (List<ProductViewModel>)list;
            }
            else
            {
                products = SQLDAOProducts.Instance.GetAll();
                MemoryCache.Default.Remove("CacheKey");
                MemoryCache.Default.AddOrGetExisting("CacheKey", products, DateTime.Now.AddHours(12));
            }
            ViewBag.sesion = context.Application["inMemory"];
            return View(products);
        }


        /// <summary>
        /// get view Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var context = System.Web.HttpContext.Current;
            ViewBag.sesion = context.Application["inMemory"];
            return View();
        }

        /// <summary>
        /// Post Create product
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Amount,Value,Status")] ProductViewModel product)
        {

            if (ModelState.IsValid)
            {

                List<ProductViewModel> lstProducts = new List<ProductViewModel>();
                string isMemory = "";
                var context = System.Web.HttpContext.Current;
                isMemory = context.Application["inMemory"].ToString();
                if (isMemory == "true")
                {
                    object list = MemoryCache.Default.AddOrGetExisting("CacheKey", product, DateTime.Now.AddHours(12));
                    lstProducts = (List<ProductViewModel>)list;
                    MemoryCache.Default.Remove("CacheKey");
                    lstProducts.Add(product);
                    MemoryCache.Default.AddOrGetExisting("CacheKey", lstProducts, DateTime.Now.AddHours(12));
                    return RedirectToAction("Index");
                }
                else
                {
                    bool result = SQLDAOProducts.Instance.Create(product);
                    if (result)
                    {
                        lstProducts = SQLDAOProducts.Instance.GetAll();
                        MemoryCache.Default.Remove("CacheKey");
                        MemoryCache.Default.AddOrGetExisting("CacheKey", lstProducts, DateTime.Now.AddHours(12));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(product);
                    }
                }
            }
            return View(product);
        }

        /// <summary>
        /// Get information about the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            var context = System.Web.HttpContext.Current;
            ViewBag.sesion = context.Application["inMemory"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel products = SQLDAOProducts.Instance.GetbyId(id.Value);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        /// <summary>
        /// Edit data by id
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Amount,Value,Status")] ProductViewModel product)
        {
                List<ProductViewModel> lstProducts = new List<ProductViewModel>();
                string isMemory = "";
                var context = System.Web.HttpContext.Current;
                isMemory = context.Application["inMemory"].ToString();
            if (isMemory == "true")
            {
                object list = MemoryCache.Default.AddOrGetExisting("CacheKey", product, DateTime.Now.AddHours(12));
                lstProducts = (List<ProductViewModel>)list;
                MemoryCache.Default.Remove("CacheKey");
                lstProducts = lstProducts.Where(x => x.Id != product.Id).ToList();
                lstProducts.Add(product);
                MemoryCache.Default.AddOrGetExisting("CacheKey", product, DateTime.Now.AddHours(12));
            }
            else
            {
                bool result = SQLDAOProducts.Instance.Edit(product);
                if (result)
                {
                    lstProducts = SQLDAOProducts.Instance.GetAll();
                    MemoryCache.Default.Remove("CacheKey");
                    MemoryCache.Default.AddOrGetExisting("CacheKey", lstProducts, DateTime.Now.AddHours(12));
                }
                else
                {
                    return View(product);
                }
            }
            return RedirectToAction("Index");
            
        }

        /// <summary>
        /// Get information about item to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var context = System.Web.HttpContext.Current;
            ViewBag.sesion = context.Application["inMemory"];
            ProductViewModel products = SQLDAOProducts.Instance.GetbyId(id.Value);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }


        /// <summary>
        /// Confirm deletion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool result = SQLDAOProducts.Instance.Delete(id);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// save data sesion
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool Sesion(bool flag)
        {
            var context = System.Web.HttpContext.Current;
            bool lastFlag = context.Application["inMemory"] == null ? false : bool.Parse(context.Application["inMemory"].ToString());
            List<ProductViewModel> products = new List<ProductViewModel>();
            if (flag && !lastFlag)
            {
                products = SQLDAOProducts.Instance.GetAll();
                MemoryCache.Default.Remove("CacheKey");
                MemoryCache.Default.AddOrGetExisting("CacheKey", products, DateTime.Now.AddHours(12));
            } else
            {
                object list = MemoryCache.Default.AddOrGetExisting("CacheKey", products, DateTime.Now.AddHours(12));
                products = (List<ProductViewModel>)list;
                products = products.Where(x => x.Id == 0).ToList();
                foreach (var item in products)
                {
                    SQLDAOProducts.Instance.Create(item);
                }
            }
            context.Application["inMemory"] = flag ? "true" : "false";
            ViewBag.sesion = context.Application["inMemory"];
            return flag;
        }    
    }
}
