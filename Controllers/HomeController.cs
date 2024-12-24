
namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _gamesService;

		public HomeController(IGamesService gamesService)
		{
			_gamesService = gamesService;
		}

		public async Task<IActionResult> Index()
        {
            var gameList = await _gamesService.GetAll();
            return View(gameList.ToList());
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
