using Epsilon.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Web.ViewModels.Computer
{
    public class ComputerViewModel
    {
        // TODO: add attributes and finish
        public string Name { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int ManufacturerId { get; set; }

        public int CategoryId { get; set; }

        // TODO: implement the things below
        // public Category Category { get; set; }

        // public Manufacturer Manufacturer { get; set; }

        // public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}
