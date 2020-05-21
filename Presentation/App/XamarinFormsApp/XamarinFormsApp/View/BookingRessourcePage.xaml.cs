using System;
using System.Collections.Generic;
using System.ComponentModel;
using Autofac;
using AutoMapper;
using SkiaSharp.Views.Forms;
using UCLToolBox;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;
using PropertyChangingEventArgs = Xamarin.Forms.PropertyChangingEventArgs;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingRessourcePage : ContentPage
    {
        private readonly BookRessourceViewModel page;

        public BookingRessourcePage(Guid Id)
        {
            InitializeComponent();
            var proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            var _mapper = AutofacHelper.Container.Resolve<Mapper>();
            BindingContext = page = proxy.Get<ApiResponse<BookRessourceViewModel>>($"Resource/Guid={Id}").Value;
            page.Reservations = proxy.Get<ApiResponse<List<Reservation<ReserveTime>>>>($"Reservation/Resource/{Id}").Value;
            page.reftesh = Refresh;
        }

        private void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            e.Surface.Canvas.Clear();
            if (sender == Monday)
            {
                var addDayese = ((int) DayOfWeek.Monday - (int) DateTime.Now.DayOfWeek + 7) % 7;
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if (sender == Tuesday)
            {
                var addDayese = ((int) DayOfWeek.Tuesday - (int) DateTime.Now.DayOfWeek + 7) % 7;
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if (sender == Wednesday)
            {
                var addDayese = ((int) DayOfWeek.Wednesday - (int) DateTime.Now.DayOfWeek + 7) % 7;
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if (sender == Thursday)
            {
                var addDayese = ((int) DayOfWeek.Thursday - (int) DateTime.Now.DayOfWeek + 7) % 7;
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if (sender == Friday)
            {
                var addDayese = ((int) DayOfWeek.Friday - (int) DateTime.Now.DayOfWeek + 7) % 7;
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if (sender == Saturday)
            {
                var addDayese = ((int) DayOfWeek.Saturday - (int) DateTime.Now.DayOfWeek + 7) % 7;
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if (sender == Sunday)
            {
                var addDayese = ((int) DayOfWeek.Sunday - (int) DateTime.Now.DayOfWeek + 7) % 7;
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
        }

        private async void OnReseverButtonClicked(object sender, EventArgs e)
        {
            page.Reserver();
        }

        private void TimePicker_PropertyChanged(object sender, PropertyChangingEventArgs e)
        {
            if (page != null) Refresh(page.Date.DayOfWeek);
        }

        private void DatePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (page != null) Refresh();
        }

        public void Refresh(DayOfWeek? day = null)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    Monday.InvalidateSurface();
                    break;
                case DayOfWeek.Tuesday:
                    Tuesday.InvalidateSurface();
                    break;
                case DayOfWeek.Wednesday:
                    Wednesday.InvalidateSurface();
                    break;
                case DayOfWeek.Thursday:
                    Thursday.InvalidateSurface();
                    break;
                case DayOfWeek.Friday:
                    Friday.InvalidateSurface();
                    break;
                case DayOfWeek.Saturday:
                    Friday.InvalidateSurface();
                    break;
                case DayOfWeek.Sunday:
                    Friday.InvalidateSurface();
                    break;
                default:
                    Monday.InvalidateSurface();
                    Tuesday.InvalidateSurface();
                    Wednesday.InvalidateSurface();
                    Thursday.InvalidateSurface();
                    Friday.InvalidateSurface();
                    Saturday.InvalidateSurface();
                    Sunday.InvalidateSurface();
                    break;
            }
        }
    }
}