using Microsoft.AspNetCore.Mvc;
using PlantSwap.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlantSwap.Controllers
{
  public class HomeController : Controller
  {

    private readonly PlantSwapContext _db;

    public HomeController(PlantSwapContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Plant> PlantList = _db.Plants.OrderBy(x => x.CommonName).ToList();

      Dictionary <string, object> model = new Dictionary <string, object>() { {"Plants", PlantList} };
      
      return View(model);
    }
  }
}