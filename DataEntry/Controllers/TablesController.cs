using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataEntry.Models;
using DataEntry.DAL;
using System.Data.Entity.Infrastructure;

namespace DataEntry.Controllers
{
    
    public class TablesController : Controller
    {
        private WinterOperationsEntities db = new WinterOperationsEntities();
        
       
        // GET: Tables
        //public ActionResult Index(string sortOrder, string searchString)
        public ActionResult Index(string sortOrder, string searchString )
        {
            //DateTime UpcomingFlights = DateTime.Today;
            var tables = db.Tables.OrderBy(t => t.Flight).ToList();

            //ViewBag.GetSTD = new SelectList(tables, "STD", "Flight", UpcomingFlights);


          /*  IQueryable<Table> time = db.Tables
                .Where(s => s.STD >= UpcomingFlights)
                .OrderBy(d => d.Flight);
                
           */
           
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.TimeSortParm = sortOrder == "STD" ? "STD_desc" : "STD";
            var pastFlight = (from t in db.Tables where (t.STD < DateTime.Today) select t);
            var currentFlight = (from t in db.Tables where (t.STD > DateTime.Today) && !(t.In_Pad.Equals(null)) select t);
            var upcomingFlight = (from t in db.Tables where (t.STD > DateTime.Today) && (t.In_Pad.Equals(null)) select t);

            /*var table = from t in db.Tables
                        select t;
           if(!String.IsNullOrEmpty(searchString))
            {
                table = table.Where(t => t.Destination.ToUpper().Contains(searchString.ToUpper()));
            }
            switch(sortOrder)
            {
                case "Name":
                    table = table.OrderByDescending(t => t.Flight);
                    break;
                case "STD":
                    table = table.OrderBy(t => t.STD);
                    break;
                case "STD_desc":
                    table = table.OrderByDescending(t => t.STD);
                    break;
                default:
                    table = table.OrderBy(t => t.Id);
                    break;
            }*/


            //var course = unitOfWork.TableRepository.Get(includeProperties: "Table");

            return View(new TableViewModel
            {
                PastTime = pastFlight,
                CurrentFlight = currentFlight,
                UpcomingFlight = upcomingFlight
            });
        }

        // GET: Tables/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: Tables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Flight,Destination,STD,ATD,A_C_Type,A_C,Gate,In_Pad,Start_Spray_Type_1,Stop_Spray_Type_1,Type_1_Gallons,Start_Spray_Type_4,Stop_Spray_Type_4,Type_4_Gallons,Out_Pad,Precipitation_Type,Off,Runway,Out_to_In,In_to_Out,Type_1_Spray_Time,Type_4_Spray_Time,Total_Spray_Time,Hourly_TP,Out_to_Off,Total_Out_to_Off,Percent_of_Taxi_Time_to_Pad,Percent_of_Taxi_Time_On_Pad,Percent_On_Pad_vs_Spray_Time,Percent_of_Taxi_Time_From_Pad_to_Takeoff")] Table table)
        {
            if (ModelState.IsValid)
            {
                
                db.Tables.Add(table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table);
        }

        // GET: Tables/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Flight,Destination,STD,ATD,A_C_Type,A_C,Gate,In_Pad,Start_Spray_Type_1,Stop_Spray_Type_1,Type_1_Gallons,Start_Spray_Type_4,Stop_Spray_Type_4,Type_4_Gallons,Out_Pad,Precipitation_Type,Off,Runway,Out_to_In,In_to_Out,Type_1_Spray_Time,Type_4_Spray_Time,Total_Spray_Time,Hourly_TP,Out_to_Off,Total_Out_to_Off,Percent_of_Taxi_Time_to_Pad,Percent_of_Taxi_Time_On_Pad,Percent_On_Pad_vs_Spray_Time,Percent_of_Taxi_Time_From_Pad_to_Takeoff")] Table table)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(table).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Please contact administrator");
            }
            return View(table);
        }

        // GET: Tables/Delete/5
        public ActionResult Delete(string id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete Failed. If the problem persists see your system administrator";
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Table table = db.Tables.Find(id);
                db.Tables.Remove(table);
                db.SaveChanges();
            }
            catch(RetryLimitExceededException /* dex */)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
