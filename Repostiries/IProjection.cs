
namespace GameZone.Repostiries
{
    public interface IProjection
    {
        public List<SelectListItem> ProjectToSelectedList<TItem>()
                where TItem : class;

        
    }
}
