using GameZone.ViewModels;

namespace GameZone.Services
{
	public class GamesService : IGamesService
	{
		private readonly ApplicationDbContext _context;
		private IWebHostEnvironment _webHostEnvironment;
		private string _imagePath;

		public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
			_imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
		}

		public async Task Create(CreateGameFormViewModel model)
		{
			string coverName = await SaveCover(model.Cover);

			Game game = new Game
			{
				Name = model.Name,
				Description = model.Description,
				CategoryID = model.CategoryID,
				Cover = coverName,
				GameDevices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
			};

			_context.Games.Add(game);
		}

		public Game? GetById(int id)
		{
			return _context.Games.Include(g => g.Category)
                .Include(g => g.GameDevices)
                .ThenInclude(gd => gd.Device)
                .AsNoTracking().SingleOrDefault(g=>g.Id==id);
		}

		public List<Game> GetAll()
		{
			return _context.Games.Include(g=>g.Category)
                .Include(g=>g.GameDevices)
                .ThenInclude(gd=>gd.Device)
                .AsNoTracking().ToList();
		}

		public async Task<Game?> Update(EditGameFormViewMode model)
		{
			var game = await _context.Games.Include(g => g.GameDevices)
				.FirstOrDefaultAsync(g=>g.Id==model.Id);
			
			if (game == null)
			{
				return null;
			}
			
			game.Name = model.Name;
			game.Description = model.Description;
			game.CategoryID = model.CategoryID;
			game.GameDevices = model.SelectedDevices.Select(d => new GameDevice{ DeviceId = d }).ToList();
			
			var isCoverUpdated=model.Cover is not null;
			var oldCover = game.Cover;
			if(isCoverUpdated)
				game.Cover =await SaveCover(model.Cover);
			
			var numberOfChanges=await _context.SaveChangesAsync();
			if (numberOfChanges > 0)
			{
				if (isCoverUpdated)
				{
					string cover = Path.Combine(_imagePath, oldCover);
					File.Delete(cover);
				}
				return game;
			}
			else
			{
				string cover = Path.Combine(_imagePath, game.Cover);
				File.Delete(cover);
				return null;
			}
		}

		public bool Delete(int id)
		{
			bool isDeleted = false;
			var game = _context.Games.Find(id);
			if (game is null)
				return isDeleted;
			
			_context.Games.Remove(game);
			var numberOfChanges = _context.SaveChanges();
			if (numberOfChanges > 0)
			{
				isDeleted = true;
		
					string cover = Path.Combine(_imagePath, game.Cover);
					File.Delete(cover);
			}

			return isDeleted;

		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		private async Task<string> SaveCover(IFormFile cover)
		{
			string coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
			string path = Path.Combine(_imagePath, coverName);
			await using var stream = File.Create(path);
			await cover.CopyToAsync(stream);
			
			return coverName;
		}
	}
}