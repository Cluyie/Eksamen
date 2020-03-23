using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Mail
{
    public enum Template
    {
        [Display(Name = "Booking bekræftelse")]
        BookingConfirmation,
        [Display(Name = "Aflysning bekræftelse")]
        CancellationConfirmation,
    }

}
