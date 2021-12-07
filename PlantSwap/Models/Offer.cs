using System;

namespace PlantSwap.Models

{
  public class Offer
    {       
        public int OfferId { get; set; }
        public int TraderId { get; set; }
        public int PlantId { get; set; }
        public bool IsCutting { get; set; }
        public DateTime ListingDate { get; set; }
        public int WillAcceptPlantId { get; set; }
        public string WillAcceptPlantCommonName { get; set; }
        public bool ImperfectMatch { get; set; }
        public int MaxDistance { get; set; }
        public virtual Trader Trader { get; set; }
        public virtual Plant Plant { get; set; }
    }
}