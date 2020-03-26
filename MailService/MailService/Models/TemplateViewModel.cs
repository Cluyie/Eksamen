using BusinessLayer.Models;

namespace MailService.Models
{
  public class TemplateViewModel
  {
      public string Title { get; set; }

      public User User { get; set; }

      public Reservation Reservation { get; set; }

      public Resource Resource{ get; set; }
  }
}
