namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                var fileSizeinMB = file.Length / (1024 * 1024);

                if (fileSizeinMB > _maxFileSize)
                    return new ValidationResult($"Maximum allowed File Size is {_maxFileSize} MB");
            }

            return ValidationResult.Success;
        }
    }
}
