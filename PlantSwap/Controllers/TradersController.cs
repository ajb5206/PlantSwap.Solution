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
  public class TradersController : Controller
  {
    private readonly PlantSwapContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TradersController(UserManager<ApplicationUser> userManager, PlantSwapContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      return View(_db.Traders.OrderBy(x => x.TraderHandle).ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Trader trader)
    {
      ViewBag.ErrorMessage = "";
      bool isUnique = true;
      List<Trader> traderList = _db.Traders.ToList();
      foreach(Trader iteration in traderList)
      {
        if (trader.TraderHandle == iteration.TraderHandle) 
        {
          isUnique = false;
          ModelState.AddModelError("DuplicateTrader", iteration.TraderHandle + " already exists");
          return View();
        }
      }
      if (isUnique)
      {
        _db.Traders.Add(trader);
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Trader thisTrader = _db.Traders
          .Include(trader => trader.OfferJoinEntity)
          .ThenInclude(join => join.Plant)
          .Include(trader => trader.RequestJoinEntity)
          .ThenInclude(join => join.Plant)
          .FirstOrDefault(trader => trader.TraderId == id);
      return View(thisTrader);
    }

    public ActionResult Edit(int id)
    {
      Trader thisTrader = _db.Traders.FirstOrDefault(trader => trader.TraderId == id);
      ViewBag.PlantId = new SelectList(_db.Plants, "PlantId", "CommonName");
      return View(thisTrader);
    }

    [HttpPost]
    public ActionResult Edit(Trader trader, int PlantId)
    {
      if (PlantId != 0)
      {
        _db.Offers.Add(new Offer() {TraderId = trader.TraderId, PlantId = PlantId});
        _db.Requests.Add(new Request() { TraderId = trader.TraderId, PlantId = PlantId });
      }
      _db.Entry(trader).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddOffer(int id)
    {
      Trader thisTrader = _db.Traders.FirstOrDefault(trader => trader.TraderId == id);
      ViewBag.Plants = _db.Plants.ToList();
      return View(thisTrader);
    }

    [HttpPost]
    public ActionResult AddOffer(Trader trader, int plantOfferedId, bool isCutting, DateTime listingDate, int exchangeId, string exchangeCommonName, bool imperfectMatch, int maxDistance)
    {
      if (plantOfferedId != 0)
      {
        ViewBag.ErrorMessage = "";
        bool isUnique = true;
        List<Offer> offerList = _db.Offers.ToList();
        foreach(Offer iteration in offerList)
        {
          if (trader.TraderId == iteration.TraderId && plantOfferedId == iteration.PlantId) 
          {
            isUnique = false;
            Plant thisPlant = _db.Plants.FirstOrDefault( plant => plant.PlantId == plantOfferedId);
            ModelState.AddModelError("DuplicatePlant", "This trader is already offering " + thisPlant.CommonName);
            Trader thisTrader = _db.Traders.FirstOrDefault(trader => trader.TraderId == plantOfferedId);
            ViewBag.Plants = _db.Plants.ToList();
            return View(thisTrader);
          }
        }
        if (isUnique)
        {
          _db.Offers.Add(new Offer() { PlantId = plantOfferedId, TraderId = trader.TraderId, IsCutting = isCutting, ListingDate = listingDate, WillAcceptPlantId = exchangeId, WillAcceptPlantCommonName = exchangeCommonName, ImperfectMatch = imperfectMatch, MaxDistance = maxDistance });
        }
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult AddRequest(int id)
    {
      Trader thisTrader = _db.Traders.FirstOrDefault(trader => trader.TraderId == id);
      ViewBag.Plants = _db.Plants.ToList();
      return View(thisTrader);
    }

    [HttpPost]
    public ActionResult AddRequest(Trader trader, int plantRequestedId, DateTime listingDate, int exchangeId, string exchangeCommonName, bool isCutting, int maxDistance)
    {
      if (plantRequestedId != 0)
      {
        ViewBag.ErrorMessage = "";
        bool isUnique = true;
        List<Request> requestList = _db.Requests.ToList();
        foreach(Request iteration in requestList)
        {
          if (trader.TraderId == iteration.TraderId && plantRequestedId == iteration.PlantId) 
          {
            isUnique = false;
            Plant thisPlant = _db.Plants.FirstOrDefault( plant => plant.PlantId == plantRequestedId);
            ModelState.AddModelError("DuplicatePlant", "This trader has already requested " + thisPlant.CommonName);
            Trader thisTrader = _db.Traders.FirstOrDefault(trader => trader.TraderId == plantRequestedId);
            ViewBag.Plants = _db.Plants.ToList();
            return View(thisTrader);
          }
        }
        if (isUnique)
        {
          _db.Requests.Add(new Request() { PlantId = plantRequestedId, TraderId = trader.TraderId, ListingDate = listingDate, HaveToOfferPlantId = exchangeId, HaveToOfferPlantCommonName = exchangeCommonName, IsCutting = isCutting, MaxDistance = maxDistance }
          );
        }
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Trader thisTrader = _db.Traders.FirstOrDefault(trader=> trader.TraderId == id);
      return View(thisTrader);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Trader thisTrader = _db.Traders.FirstOrDefault(trader => trader.TraderId == id);
      _db.Traders.Remove(thisTrader);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

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