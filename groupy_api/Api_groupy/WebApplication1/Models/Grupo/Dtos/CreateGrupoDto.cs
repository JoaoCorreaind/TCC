using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CreateGrupoDto
    {
        public string Descrition { get; set; }
        public string Title { get; set; }
        public int MaximoUsuarios { get; set; } = 100;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string LiderId { get; set; }
        public List<int> Tags { get; set; } = null;
        public String GroupyMainImage { get; set; } = null;
        public List<string> GroupyImages { get; set; } = null;

        public string CityId { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string ReferencePoint { get; set; }

    }
}
