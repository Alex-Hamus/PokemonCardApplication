using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonCards.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Security.Principal;
    using System.Web;

    public class Cards
    {
        [Key]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Weakness { get; set; }
        public string Rarity { get; set; }
        public int HitPoints { get; set; }
        public int Attack { get; set; }
        public int EvolutionStage { get; set; }
        public int Quantity { get; set; }
        public string ApplicationUserID { get; internal set; }

        IPrincipal currentUser = HttpContext.Current.User;

        public virtual ICollection<Trade> Trades { get; set; }
    }

    public class CardDBcontext : DbContext
    {
        public DbSet<Cards> Cards { get; set; }
    }
}