using GameZone.Models;
using GameZone.Repostiries;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly IGenericCrudRepository<Category> _repository;

        public CategoriesController(IGenericCrudRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
		{
			var lst = await _repository.GetAllAsync();
			lst = lst.ToList();

			return View(lst);
		}

        public async Task<IActionResult> Get(int id)
        {
            var entity = await  _repository.GetByIdAsync(id);
            
            if (entity == null)
                return NotFound();

            return View(entity);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			await _repository.AddAsync(entity);
			await _repository.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
			var Category = await _repository.GetByIdAsync(id);
			return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category entity)
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
