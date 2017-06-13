using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace PokemonCards.Models
{
    public class Trade
    {
       [Key]
       public int ID { get; set; }
        public IEnumerable<Cards> Card { get; internal set; }
        public virtual ICollection<Cards> Cardss { get; set; }
        public string ApplicationUserID { get; internal set; }

       IPrincipal currentUser = HttpContext.Current.User;
       public virtual ICollection<Cards> Cardes { get; set; }
    }
    public class TradeBcontext : DbContext
    {
        public IEnumerable<Cards> Cards { get; internal set; }
        public DbSet<Trade> Trades { get; set; }
    }
}