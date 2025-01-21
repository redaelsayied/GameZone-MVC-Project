using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
	public class CategoryService : ICategoryService
	{
		private ApplicationDbContext _context;

		public CategoryService(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Create(Category category)
		{
			_context.Categories.Add(category);
		}

		public Category GetById(int id)
		{
			return _context.Categories.First(g => g.Id == id);
		}

		public List<Category> GetAll()
		{
			return _context.Categories.ToList();
		}

		public void Update(Category category)
		{
			_context.Categories.Update(category);
		}

		public void Delete(int id)
		{
			var category = GetById(id);
			_context.Categories.Remove(category);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public IEnumerable<SelectListItem> GetSelectList()
		{
			return _context.Categories
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
				.AsNoTracking().ToList()
				.OrderBy(c => c.Text);
		}
	}
}