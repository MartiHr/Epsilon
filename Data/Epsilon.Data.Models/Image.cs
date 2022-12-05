using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Epsilon.Data.Common.Models;

namespace Epsilon.Data.Models
{
    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            Id = Guid.NewGuid().ToString();
        }

        public int ComputerId { get; set; }

        public Computer Computer { get; set; }

        [Required]
        [ForeignKey(nameof(Creator))]
        public string CreatorId { get; set; }

        public Editor Creator { get; set; }

        [Required]
        public string Extension { get; set; }
    }
}
