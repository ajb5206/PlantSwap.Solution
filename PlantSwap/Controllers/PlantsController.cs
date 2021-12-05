using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlantSwap.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlantSwap.Controllers
{
  [Authorize]
  public class PlantsController : Controller
  {
    private readonly PlantSwapContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PlantsController(UserManager<ApplicationUser> userManager, PlantSwapContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      return View(_db.Plants.OrderBy(x => x.CommonName).ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Plant plant)
    {
      ViewBag.ErrorMessage = "";
      bool isUnique = true;
      List<Plant> plantList = _db.Plants.ToList();
      foreach(Plant iteration in plantList)
      {
        if (plant.CommonName == iteration.CommonName) 
        {
          isUnique = false;
          ModelState.AddModelError("DuplicatePlant", iteration.CommonName + " already exists");
          return View();
        }
      }
      if (isUnique)
      {
        _db.Plants.Add(plant);
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    //The id passed into the Details action method comes from the Razor Action Link on line 17 of Views/Plants/Index (or line 25 of Views/Home/Index). That action link is inside a loop through the Index's model so it is only interacting with a single plant at a time (the plant of that iteration). The action link grabs the id from the iteration of the Index's model.
    public ActionResult Details(int id)
    {
      //Gives a list of Plant objects from the database (but not its associated offers or requests)
      Plant thisPlant = _db.Plants
          //Loads the OfferJoinEntity property of each Plant (this is just a collection of join entities, not the actual Trader or reciprocal plants)
          .Include(plant => plant.OfferJoinEntity)
          //Loads the Trader (not just the ID but the actual Trader object) of each OfferJoinEntity
          .ThenInclude(join => join.Trader)
          //Loads the RequestJoinEntity property of each Plant (this is just a collection of join entities, not the actual Trader or reciprocal plants)
          .Include(plant => plant.RequestJoinEntity)
          //Loads the Trader (not just the ID but the actual Trader object) of each RequestJoinEntity
          .ThenInclude(join => join.Trader)
          //Specifies which Plant from the database we're working with (i.e. for which plant we're displaying the details)
          .FirstOrDefault(plant => plant.PlantId == id);


        //Experiment that doesn't yet work
        Plant PlantsWillAcceptForAnOffer = _db.Plants
          .Include(plant => plant.OfferJoinEntity)
          //.Any(plant => plant.CommonName == WillAcceptPlantId);
          .Any(plant => plant.PlantId == OfferJoinEntity.WillAcceptPlantId);
          
      return View(thisPlant);
    }

//Add User Authorization
    public ActionResult Edit(int id)
    {
      Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == id);
      ViewBag.TraderId = new SelectList(_db.Traders, "TraderId", "TraderName");
      return View(thisPlant);
    }

    [HttpPost]
    public ActionResult Edit(Plant plant, int TraderId)
    {
      if (TraderId != 0)
      {
        _db.Offers.Add(new Offer() {TraderId = TraderId, PlantId = plant.PlantId});
        _db.Requests.Add(new Request() { TraderId = TraderId, PlantId = plant.PlantId });
      }
      _db.Entry(plant).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddOffer(int id)
    {
      Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == id);
      ViewBag.Traders = _db.Traders.ToList();
      ViewBag.Plants = _db.Plants.ToList();
      return View(thisPlant);
    }

    [HttpPost]
    public ActionResult AddOffer(Plant plant, int traderId, bool isCutting, DateTime listingDate, int exchangeId, bool imperfectMatch, int maxDistance)
    {
      if (traderId != 0)
      {
        ViewBag.ErrorMessage = "";
        bool isUnique = true;
        List<Offer> offerList = _db.Offers.ToList();
        foreach(Offer iteration in offerList)
        {
          if (plant.PlantId == iteration.PlantId && traderId == iteration.TraderId) 
          {
            isUnique = false;
            Trader thisTrader = _db.Traders.FirstOrDefault( trader => trader.TraderId == traderId);
            ModelState.AddModelError("DuplicateTrader", "This plant is already offered by " + thisTrader.TraderName);
            Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == traderId);
            ViewBag.Traders = _db.Traders.ToList();
            ViewBag.Plants = _db.Plants.ToList();
            return View(thisPlant);
          }
        }
        if (isUnique)
        {
          _db.Offers.Add(new Offer() { TraderId = traderId, PlantId = plant.PlantId, IsCutting = isCutting, ListingDate = listingDate, WillAcceptPlantId = exchangeId, ImperfectMatch = imperfectMatch, MaxDistance = maxDistance });
        }
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult AddRequest(int id)
    {
      Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == id);
      ViewBag.Traders = _db.Traders.ToList();
      ViewBag.Plants = _db.Plants.ToList();
      return View(thisPlant);
    }

    [HttpPost]
    public ActionResult AddRequest(Plant plant, int traderId, bool isCutting, DateTime listingDate, int exchangeId, bool imperfectMatch, int maxDistance)
    {
      if (traderId != 0)
      {
        ViewBag.ErrorMessage = "";
        bool isUnique = true;
        List<Request> requestList = _db.Requests.ToList();
        foreach(Request iteration in requestList)
        {
          if (plant.PlantId == iteration.PlantId && traderId == iteration.TraderId) 
          {
            isUnique = false;
            Trader thisTrader = _db.Traders.FirstOrDefault( trader => trader.TraderId == traderId);
            ModelState.AddModelError("DuplicateTrader", "This plant is already requested by " + thisTrader.TraderName);
            Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == traderId);
            ViewBag.Traders = _db.Traders.ToList();
            ViewBag.Plants = _db.Plants.ToList();
            return View(thisPlant);
          }
        }
        if (isUnique)
        {
          _db.Requests.Add(new Request() { TraderId = traderId, PlantId = plant.PlantId, IsCutting = isCutting, ListingDate = listingDate, HaveToOfferPlantId = exchangeId, ImperfectMatch = imperfectMatch, MaxDistance = maxDistance });
        }
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == id);
      return View(thisPlant);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == id);
      _db.Plants.Remove(thisPlant);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    //Add User Authorization
    [HttpPost]
    public ActionResult DeleteOffer(int joinId)
    {
      Offer joinEntry = _db.Offers.FirstOrDefault(entry => entry.OfferId == joinId);
      _db.Offers.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  
    [HttpPost]
    public ActionResult DeleteRequest(int joinId)
    {
      Request joinEntry = _db.Requests.FirstOrDefault(entry => entry.RequestId == joinId);
      _db.Requests.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}