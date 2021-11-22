using System.Collections.Generic;
using System.ComponentModel;

namespace PlantSwap.Models
{
  public class Plant
  {
    public Plant()
    {
      this.TradeJoinEntity = new HashSet<Trade>();
    }
    public int PlantId { get; set; }

    public string Genus { get; set; }
    public string Species { get; set; }
    [DisplayName("Common Name")]

    public string CommonName { get; set; }
    public string Variety { get; set; }
    public virtual ICollection<Trade> TradeJoinEntity { get;}
  }
}