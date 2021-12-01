using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlantSwap.Models
{
  public class Trader
  {
    public Trader()
      {
        this.RequestJoinEntity = new HashSet<Request>();
        this.OfferJoinEntity = new HashSet<Offer>();
      }

      public int TraderId { get; set; }

      [DisplayName("Trader's Handle")]
      public string TraderHandle { get; set; }
      [DisplayName("Trader's Name")]
      public string TraderName { get; set; }
      [DisplayName("Trader's preferred contact information")]
      public string PreferredContact { get; set; }
      [Required]
      [DisplayName("Trader's Zip Code")]
      public int ZipCode { get; set; }
      
      public virtual ICollection<Request> RequestJoinEntity { get; set; }
      public virtual ICollection<Offer> OfferJoinEntity { get; set; }
    }
}