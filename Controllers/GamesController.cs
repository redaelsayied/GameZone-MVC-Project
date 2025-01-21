using GameZone.Models;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
	public class GamesController : Controller
	{
		private ICategoryService _iCategory;
		private IDeviceService _iDeviceService;
		private IGamesService _gamesService;

		public GamesController(ICategoryService iCategory, IDeviceService iDeviceService, IGamesService gamesService)
		{
			_iCategory = iCategory;
			_iDeviceService = iDeviceService;
			_gamesService = gamesService;
		}

		public IActionResult Index()
		{
			var games = _gamesService.GetAll();
			return View(games);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var model = new CreateGameFormViewModel();

			model.Categories = _iCategory.GetSelectList();
			model.Devices = _iDeviceService.GetSelectList();

			return View(model);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CreateGameFormViewModel model)
		{
			if (ModelState.IsValid)
			{
				_gamesService.Create(model);
				_gamesService.SaveChanges();
				return RedirectToAction(nameof(Index));
			}


			model.Categories = _iCategory.GetSelectList();
			model.Devices = _iDeviceService.GetSelectList();

			return View(model);
		}

		public IActionResult Details(int id)
		{
			var game = _gamesService.GetById(id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
		}

        public IActionResult Edit(int id)
        {
            var game = _gamesService.GetById(id);

            if (game == null)
            {
                return NotFound();
            }

            EditGameFormViewMode viewModel = new EditGameFormViewMode
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                CategoryID = game.CategoryID,
				SelectedDevices = game.GameDevices.Select(d=>d.DeviceId).ToList(),
                Categories = _iCategory.GetSelectList(),
                Devices = _iDeviceService.GetSelectList(),
				CurrentCover = game.Cover,
            };

            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewMode model)
        {
	        if (!ModelState.IsValid)
	        {
		        model.Categories = _iCategory.GetSelectList();
		        model.Devices = _iDeviceService.GetSelectList();

		        return View(model);
	        }

	        var game=await _gamesService.Update(model);

	        if (game is null)
		        return BadRequest();
	        
	        return RedirectToAction("Details", new { id = model.Id });

        }
        
    }
}