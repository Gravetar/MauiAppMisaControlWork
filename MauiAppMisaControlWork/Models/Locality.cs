using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppMisaControlWork.Models
{
    public class Locality
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public double? numberresidantsth { get; set; }
        public double? budgetmlrd { get; set; }
        public string? mayor { get; set; }
    }
}
