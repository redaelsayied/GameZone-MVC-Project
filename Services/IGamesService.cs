using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
	public interface IGamesService
	{
		public Task Create(CreateGameFormViewModel model);
		public Game? GetById(int id);
		public List<Game> GetAll();
		public Task<Game?> Update(EditGameFormViewMode model);
		public bool Delete(int id);

		public void SaveChanges();

	}
}
