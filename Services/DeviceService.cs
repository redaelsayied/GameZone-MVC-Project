using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
	public class DeviceService : IDeviceService
	{
		private ApplicationDbContext _context;

		public DeviceService(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Create(Device device)
		{
			_context.Devices.Add(device);
		}

		public Device GetById(int id)
		{
			return _context.Devices.First(d => d.Id == id);
		}

		public List<Device> GetAll()
		{
			return _context.Devices.ToList();
		}

		public void Update(Device device)
		{
			_context.Devices.Update(device);
		}

		public void Delete(int id)
		{
			var device = GetById(id);
			_context.Devices.Remove(device);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public IEnumerable<SelectListItem> GetSelectList()
		{
			return _context.Devices.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
				.AsNoTracking()
				.ToList().OrderBy(c => c.Text);
		}
	}
}