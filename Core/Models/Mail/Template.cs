using System.ComponentModel.DataAnnotations;

namespace Models.Mail
{
    public enum Template
    {
        [Display(Name = "Booking bekræftelse")]
        BookingConfirmation,

        [Display(Name = "Aflysning bekræftelse")]
        CancellationConfirmation
    }
}