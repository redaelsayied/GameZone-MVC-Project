using GameZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GameZone.Services;

namespace GameZone.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IGamesService _iGamesService;

		public HomeController(IGamesService iGamesService)
		{
			_iGamesService = iGamesService;
		}

		public IActionResult Index()
		{
			var games = _iGamesService.GetAll();
			return View(games);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
