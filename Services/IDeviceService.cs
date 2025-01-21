using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
	public interface IDeviceService
	{
		public void Create(Device device);
		public Device GetById(int id);
		public List<Device> GetAll();
		public void Update(Device device);
		public void Delete(int id);

		public void SaveChanges();

		public IEnumerable<SelectListItem> GetSelectList();
	}
}