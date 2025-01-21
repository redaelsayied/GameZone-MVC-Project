
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
	public interface ICategoryService
	{
		public void Create(Category category);
		public Category GetById(int id);
		public List<Category> GetAll();
		public void Update(Category category);
		public void Delete(int id);

		public void SaveChanges();

		public IEnumerable<SelectListItem> GetSelectList();

	}
}
