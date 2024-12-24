
using NuGet.Protocol.Core.Types;

namespace GameZone.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IGenericCrudRepository<Device> _repository;

        public DevicesController(IGenericCrudRepository<Device> repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var lst  =await  _repository.GetAllAsync();
            lst = lst.ToList();

            return View(lst);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

         
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            var Device = await _repository.GetByIdAsync(id);
            return View(Device);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Device entity)
        {
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
           
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            

            return RedirectToAction(nameof(Index));
        }
    }
}
