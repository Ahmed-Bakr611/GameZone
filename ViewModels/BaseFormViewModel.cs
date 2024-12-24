namespace GameZone.ViewModels
{
    public class BaseFormViewModel
    {
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Category")]
        [Range(minimum: 2, maximum: 200, ErrorMessage = "the value 'select category' is not valid for category")]
        public int CategoryID { get; set; }

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;


        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

       

    }
}
