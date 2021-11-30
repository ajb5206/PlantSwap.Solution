using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlantSwap.Models;
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

    public ActionResult Details(int id)
    {
      Plant thisPlant = _db.Plants
          .Include(plant => plant.TradeJoinEntity)
          .ThenInclude(join => join.AppUser)
          .FirstOrDefault(plant => plant.PlantId == id);
      return View(thisPlant);
    }

//Add User Authorization
    public ActionResult Edit(int id)
    {
      Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == id);
      ViewBag.AppUserId = new SelectList(_db.AppUsers, "AppUserId", "AppUserName");
      return View(thisPlant);
    }

    [HttpPost]
    public ActionResult Edit(Plant plant, int AppUserId)
    {
      if (AppUserId != 0)
      {
        _db.Trades.Add(new Trade() { AppUserId = AppUserId, PlantId = plant.PlantId });
      }
      _db.Entry(plant).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

//Needed Functionality: Create both sides of a trade: an Offer & a Request
    // public ActionResult AddAppUser(int id)
    // {
    //   Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == id);
    //   ViewBag.AppUserId = new SelectList(_db.AppUsers, "AppUserId", "AppUserName");
    //   return View(thisPlant);
    // }

    // [HttpPost]
    // public ActionResult AddAppUser(Plant plant, int AppUserId)
    // {
    //   if (AppUserId != 0)
    //   {
    //     ViewBag.ErrorMessage = "";
    //     bool isUnique = true;
    //     List<Trade> appUserPlantList = _db.Trade.ToList();
    //     foreach(Trade iteration in appUserPlantList)
    //     {
    //       if (plant.PlantId == iteration.PlantId && AppUserId == iteration.AppUserId) 
    //       {
    //         isUnique = false;
    //         AppUser thisAppUser = _db.AppUsers.FirstOrDefault(appUser => appUser.AppUserId == AppUserId);
    //         ModelState.AddModelError("DuplicateAppUser", "This plant is already licensed to repair " + thisAppUser.AppUserName);
    //         Plant thisPlant = _db.Plants.FirstOrDefault(plant => plant.PlantId == AppUserId);
    //         ViewBag.AppUserId = new SelectList(_db.AppUsers, "AppUserId", "AppUserName");
    //         return View(thisPlant);
    //       }
    //     }
    //     if (isUnique)
    //     {
    //       _db.Trade.Add(new Trade() { AppUserId = AppUserId, PlantId = plant.PlantId });
    //     }
    //     _db.SaveChanges();
    //   }
    //   return RedirectToAction("Index");
    // }

//Add User Authorization
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

//Needed Functionality: Delete your offers & requests WITH User Authorization
    // [HttpPost]
    // public ActionResult DeleteAppUser(int joinId)
    // {
    //   Trade joinEntry = _db.Trade.FirstOrDefault(entry => entry.TradeId == joinId);
    //   _db.Trade.Remove(joinEntry);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
  }
}