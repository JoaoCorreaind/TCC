using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class City
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public string StateId { get;set;}
        public List<Group> Groups { get; set; }
        public List<User> Users { get; set; }

    }
}
