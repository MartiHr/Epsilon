using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] extensions;

    public AllowedExtensionsAttribute(string[] _extensions)
    {
        extensions = _extensions;
    }

    protected override ValidationResult IsValid(
    object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage()
    {
        return $"This photo extension is not allowed!";
    }
}