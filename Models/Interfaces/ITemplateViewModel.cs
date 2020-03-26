using Models.Mail;

namespace Models.Interfaces
{
    public interface ITemplateViewModel<TReservation, TReserveTime, TResource, TAvailableTime, TUser>
        where TReservation : IReservation<TReserveTime>
        where TReserveTime : IReserveTime
        where TResource : IResource<TReservation, TReserveTime, TAvailableTime>
        where TAvailableTime : IAvailableTime
        where TUser : IUser
    {
        Template Template { get; set; }
        string Title { get; set; }
        TUser Recipent { get; set; }
        TReservation Reservation { get; set; }
        TResource Resource { get; set; }
    }
}
