
namespace GameZone.Repostiries
{
    public class  Projection :IProjection{ 
        private readonly GameDbContext _context;


        public Projection(GameDbContext context)
        {
            _context = context;
        }

       
        public List<SelectListItem> ProjectToSelectedList<TItem>()
         where TItem : class
        {
            var flags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;
            var idProperty = typeof(TItem).GetProperty("id", flags);
            var nameProperty = typeof(TItem).GetProperty("Name", flags);

            if (idProperty == null || nameProperty == null)
            {
                return new List<SelectListItem>();
            }
            var rawData = _context.Set<TItem>().AsNoTracking().ToList();

            var projected = rawData
            .Select(T => new SelectListItem
            {
                
                Text = nameProperty.GetValue(T)!.ToString(),
                Value = idProperty.GetValue(T)!.ToString()
            }).OrderBy(T => T.Text.ToString()).ToList();
            

            return projected;
        }
    }
}
