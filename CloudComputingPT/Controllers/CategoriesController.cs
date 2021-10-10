using CloudComputingPT.Data;
using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
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
            var value = new Categories();
            List<Categories> catList = new List<Categories>();
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379,allowAdmin=true"))
            {
                StackExchange.Redis.IDatabase db = redis.GetDatabase();

                var keys = redis.GetServer("localhost", 6379).Keys();

                string[] keysArr = keys.Select(key => (string)key).ToArray();
                if (keysArr.Length != 0)
                {
                    foreach (string item in keysArr)
                    {
                        foreach (var item1 in a)
                        {
                            var values = JsonConvert.DeserializeObject<Categories>(_cacheAccess.FetchData(item));
                            if (values.Id == item1.Id && values.flatPrice == item1.flatPrice && values.categories == item1.categories)
                            {
                                value = values;
                                catList.Add(values);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    return View(catList);
                }
                else
                {
                    return View(a);
                }

            }

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

                    _applicationDBContext.Categories.Add(details);
                    _applicationDBContext.SaveChanges();

                    _applicationDBContext.PricesDictionary.Add(new PricesDictionary()
                    {
                        Id = details.Id.ToString(),
                        Value = JsonConvert.SerializeObject(details)
                    });

                    _applicationDBContext.SaveChanges();

                    _cacheAccess.SaveData(new PricesDictionary()
                    {
                        Id = details.Id.ToString(),// Guid.NewGuid().ToString(),
                        Value = JsonConvert.SerializeObject(details)

                    });

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
