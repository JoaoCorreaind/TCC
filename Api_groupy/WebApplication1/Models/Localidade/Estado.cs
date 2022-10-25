using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class State
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<City> Citys { get; set; }

    }
}
