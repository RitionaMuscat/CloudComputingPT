using CloudComputingPT.Data;
using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Linq;

namespace CloudComputingPT.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _applicationDBContext;
        private readonly UserManager<IdentityUser> _userManager;

        private ICacheAccess _cacheAccess;

        public CategoriesController(ApplicationDbContext applicationDBContext, UserManager<IdentityUser> userManager, ICacheAccess cacheAccess)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
            _cacheAccess = cacheAccess;
        }
        // GET: CategoriesController
        public ActionResult Index()
        {
            var a = (from b in _applicationDBContext.Categories
                     select b).ToList();
            return View(a);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories details)
        {
            try
            {
                if (details.categories == "Luxury" || details.categories == "Economy" || details.categories == "Business")
                {
                    _applicationDBContext.PricesDictionary.Add(new PricesDictionary()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Value = JsonConvert.SerializeObject(details)
                    });

                    _applicationDBContext.SaveChanges();

                    _cacheAccess.SaveData(new PricesDictionary()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Value = JsonConvert.SerializeObject(details)

                    });

                    _applicationDBContext.Categories.Add(details);
                    _applicationDBContext.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Category is Incorrect");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(Guid id)
        {
            Categories categories_to_edit = new Categories();
            var CategoriesEdit = (from CategoriesToEdit in _applicationDBContext.Categories
                                  where CategoriesToEdit.Id.Equals(id)
                                  select CategoriesToEdit).ToList();

            var value = new Categories();
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379,allowAdmin=true"))
            {
                StackExchange.Redis.IDatabase db = redis.GetDatabase();

                var keys = redis.GetServer("localhost", 6379).Keys();

                string[] keysArr = keys.Select(key => (string)key).ToArray();

                foreach (string item in keysArr)
                {
                    foreach (var item1 in CategoriesEdit)
                    {
                        var values = JsonConvert.DeserializeObject<Categories>(_cacheAccess.FetchData(item));
                        if (values.Id == id && values.categories == item1.categories && values.flatPrice == item1.flatPrice)
                            value = values;
                        else
                            continue;
                    }

                }

            }

            categories_to_edit.categories = value.categories;
            categories_to_edit.flatPrice = value.flatPrice;
            categories_to_edit.Id = value.Id;

            return View(categories_to_edit);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Categories category)
        {
            try
            {
                _applicationDBContext.Categories.Update(category);
                _applicationDBContext.SaveChanges();

                _cacheAccess.SaveData(new PricesDictionary()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = JsonConvert.SerializeObject(category)

                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
