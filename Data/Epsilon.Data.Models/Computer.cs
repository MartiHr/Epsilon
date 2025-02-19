﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Epsilon.Data.Common.Models;

using static Epsilon.Data.Common.DataValidation.Computer;

namespace Epsilon.Data.Models
{
    public class Computer : BaseDeletableModel<int>
    {
        [MaxLength(ComputerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ComputerModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Range(typeof(decimal), ComputerPriceMinValue, ComputerPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(ComputerDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Creator))]
        public string CreatorId { get; set; }

        public Editor Creator { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<Part> Parts { get; set; } = new HashSet<Part>();

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();

        public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    }
}
