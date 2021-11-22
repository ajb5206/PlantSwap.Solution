using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlantSwap.Models
{
  public class User
  {
    public User()
      {
        this.TradeJoinEntity = new HashSet<Trade>();
      }

      public int UserId { get; set; }

      [DisplayName("User's Handle")]
      public string UserHandle { get; set; }
      [DisplayName("User's Name")]
      public string UsersName { get; set; }
      [DisplayName("User's preferred contact information")]
      public string PreferredContact { get; set; }
      [Required]
      [DisplayName("User's Zip Code")]
      public int ZipCode { get; set; }
      
      public virtual ICollection<Trade> TradeJoinEntity { get; set; }
    }
}