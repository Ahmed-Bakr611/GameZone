namespace GameZone.ViewModels
{
    public class UpdateGameFormViewModel:BaseFormViewModel
    {
        public int Id { get; set; }
        public string? currentCover { get; set; }

        [Display(Name = "Cover ")]
        [AllowedExtensions(FileSettings.AllowedExtensions),
        MaxFileSize(FileSettings.MaxFileSizeinMB)]
        public IFormFile? CoverUrl { get; set; } = default!;

    }
}
