using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.Mvc;

using System.Web.Routing;

using DimondDating.Models;



namespace DimondDating.Controllers

{

    public class HelloworldController : Controller

    {

        List<QuickSearch> families;



        public HelloworldController()

        {

            families = new List<QuickSearch>

            {

                new QuickSearch() { id=0, familyname = "Madeira", address1 = "123 Hastings Dr", city = "Cranberry Township", state = "PA", zip = "16066", homephone = "7247797964" },

                new QuickSearch() { id=1, familyname = "Johns", address1 = "3200 College Ave", city = "Beaver Falls", state = "PA", zip = "15010", homephone = "7248461298" },

                new QuickSearch() { id=2, familyname = "Ellis", address1 = "1 Sycamore Hollow", city = "Pittsburgh", state = "PA", zip = "15212", homephone = "4122371212" },

                new QuickSearch() { id=3, familyname = "Braddock", address1 = "23 Livingstone Dr", city = "Monroeville", state = "PA", zip = "15010", homephone = "4123277486" }

            };





        }



        protected override void Initialize(RequestContext requestContext)

        {

            base.Initialize(requestContext);

            if (Session["familyList"] == null)

            {

                Session["familyList"] = families;

            }

        }



        // GET: Family

        public ActionResult Index()

        {

            var f = (List<QuickSearch>)Session["familyList"];

            return View(f);

        }



        // GET: Family/Details/5

        public ActionResult Details(int id)

        {

            // Get the list of people from the session

            var fList = (List<QuickSearch>)Session["familyList"];



            // Get the person with the passed in ID

            var f = fList[id];



            // Return the person data to the view

            return View(f);

        }



        // GET: Family/Create

        public ActionResult Create()

        {

            return View();

        }



        // POST: Family/Create

        [HttpPost]

        public ActionResult Create(FormCollection collection)

        {

            try

            {

                families = (List<QuickSearch>)Session["familyList"];

                QuickSearch newFamily = new QuickSearch()

                {

                    id = families.Count(),

                    familyname = collection["familyname"],

                    address1 = collection["address1"],

                    city = collection["city"],

                    state = collection["state"],

                    zip = collection["zip"],

                    homephone = collection["homephone"]

                };



                // Add the person to the list

                families = (List<QuickSearch>)Session["familyList"];

                families.Add(newFamily);



                // Save the list to the session

                Session["familyList"] = families;



                return RedirectToAction("Index");

            }

            catch

            {

                return View();

            }

        }

        // GET: Family/Edit/5

        public ActionResult Edit(int id)

        {

            var fList = (List<QuickSearch>)Session["familyList"];

            var f = families[id];



            // Return the person data to the view

            return View(f);

        }



        // POST: Family/Edit/5

        [HttpPost]

        public ActionResult Edit(int id, FormCollection collection)

        {

            try

            {

                var families = (List<QuickSearch>)Session["familyList"];



                var f = families[id];



                QuickSearch newFamily = new QuickSearch()

                {

                    id = families.Count(),

                    familyname = collection["familyname"],

                    address1 = collection["address1"],

                    city = collection["city"],

                    state = collection["state"],

                    zip = collection["zip"],

                    homephone = collection["homephone"]

                };



                families.Where(x => x.id == id).First().familyname = collection["familyname"];

                families.Where(x => x.id == id).First().address1 = collection["address1"];

                families.Where(x => x.id == id).First().city = collection["city"];

                families.Where(x => x.id == id).First().state = collection["state"];

                families.Where(x => x.id == id).First().zip = collection["zip"];

                families.Where(x => x.id == id).First().homephone = collection["homephone"];







                return RedirectToAction("Index");

            }

            catch

            {

                return View();

            }

        }



        // GET: Family/Delete/5

        public ActionResult Delete(int id)

        {

            var fList = (List<QuickSearch>)Session["familyList"];

            var f = families[id];



            // Return the person data to the view

            return View(f);

        }



        // POST: Family/Delete/5

        [HttpPost]

        public ActionResult Delete(int id, FormCollection collection)

        {

            try

            {

                // Add the person to the list

                families = (List<QuickSearch>)Session["familyList"];

                var f = families[id];

                families.Remove(f);



                // Save the list to the session

                Session["familyList"] = families;



                for (int x = id; x < families.Count(); x++)

                {

                    if (families[x] != null)

                        families[x].id = x;

                }

                return RedirectToAction("Index");

            }

            catch

            {

                return View();

            }

        }

    }

}