using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Http;

public class AllowedExtensionsForCollectionAttribute : ValidationAttribute
{
    private readonly string[] extensions;

    public AllowedExtensionsForCollectionAttribute(string[] _extensions)
    {
        extensions = _extensions;
    }

    protected override ValidationResult IsValid(
    object value, ValidationContext validationContext)
    {
        var files = value as ICollection<IFormFile>;

        foreach (var file in files)
        {
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage(extension));
                }
            }
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage(string extension)
    {
        return $"Invalid image extension {extension}";
    }
}
