using System.ComponentModel.DataAnnotations;

namespace UCLDreamTeam.SharedInterfaces.Mail
{
    public enum Template
    {
        [Display(Name = "Booking bekræftelse")]
        BookingConfirmation,

        [Display(Name = "Aflysning bekræftelse")]
        CancellationConfirmation
    }
}