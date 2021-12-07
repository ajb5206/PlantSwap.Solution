using System;

namespace PlantSwap.Models

{
  public class Request
    {       
        public int RequestId { get; set; }
        public int TraderId { get; set; }
        public int PlantId { get; set; }
        public DateTime ListingDate { get; set; }
        public int HaveToOfferPlantId { get; set; }
        public string HaveToOfferPlantCommonName { get; set; }
        public bool IsCutting { get; set; }
        public int MaxDistance { get; set; }
        public virtual Trader Trader { get; set; }
        public virtual Plant Plant { get; set; }
    }
}