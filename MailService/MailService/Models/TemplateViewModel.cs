using BusinessLayer.Models;
using Models.Interfaces;
using Models.Mail;

namespace MailService.Models
{
  public class TemplateViewModel : ITemplateViewModel<Reservation, ReserveTime, Resource, AvailableTime, User>
  {
      public Template Template { get; set; }

      public string Title { get; set; }

      public User Recipent { get; set; }

      public Reservation Reservation { get; set; }

      public Resource Resource{ get; set; }
  }
}
