using System;

namespace PlantSwap.Models

{
  public class Trade
    {       
        public int TradeId { get; set; }
        public int UserId { get; set; }
        public int PlantId { get; set; }
        public bool Offered { get; set; }
        public bool Wanted { get; set; }
        public bool Cutting { get; set; }
        public DateTime ListingDate { get; set; }
        public int MaxDistance { get; set; }
        public bool ImperfectMatch { get; set; }
        public virtual User User { get; set; }
        public virtual Plant Plant { get; set; }
    }
}