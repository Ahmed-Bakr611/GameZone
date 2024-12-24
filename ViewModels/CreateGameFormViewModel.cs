namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel:BaseFormViewModel
    {

        [Display(Name = "Cover ")]
        [AllowedExtensions(FileSettings.AllowedExtensions),
         MaxFileSize(FileSettings.MaxFileSizeinMB)]   
        public IFormFile CoverUrl { get; set; } = default!;

    }
}
