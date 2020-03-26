using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.ViewModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using XamarinFormsApp.Helpers.Graphish;
using Models.Interfaces;

namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingRessourcePage : ContentPage
    {
        private BookRessourceViewModel page;

        public BookingRessourcePage(Guid Id)
        {
            InitializeComponent();
            var proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            var _mapper = AutofacHelper.Container.Resolve<Mapper>();
            BindingContext = page = proxy.Get<ApiResponse<BookRessourceViewModel>>($"Resource/Guid={Id}").Value;
            page.reftesh = Refresh;
        }
        
        private void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            e.Surface.Canvas.Clear();
            if(sender == Monday)
            {
                int addDayese = (((int)DayOfWeek.Monday - (int)DateTime.Now.DayOfWeek + 7) % 7);
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }else if(sender == Tuesday)
            {
                int addDayese = (((int)DayOfWeek.Tuesday-(int)DateTime.Now.DayOfWeek+7) % 7);
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if(sender == Wednesday)
            {
                int addDayese = (((int)DayOfWeek.Wednesday - (int)DateTime.Now.DayOfWeek+ 7) % 7);
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if(sender == Thursday)
            {
                int addDayese = (((int)DayOfWeek.Thursday - (int)DateTime.Now.DayOfWeek + 7) % 7);
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if(sender == Friday)
            {
                int addDayese = (((int)DayOfWeek.Friday - (int)DateTime.Now.DayOfWeek + 7) % 7);
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if(sender == Saturday)
            {
                int addDayese = (((int)DayOfWeek.Saturday - (int)DateTime.Now.DayOfWeek + 7) % 7);
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
            else if(sender == Sunday)
            {
                int addDayese = (((int)DayOfWeek.Sunday - (int)DateTime.Now.DayOfWeek + 7) % 7);
                page.DrawDay(e.Surface.Canvas, e.Info.Width, e.Info.Height, DateTime.Now.AddDays(addDayese));
            }
        }
        async void OnReseverButtonClicked(object sender, EventArgs e)
        {
            page.Reserver();
        }
        private void TimePicker_PropertyChanged(object sender, PropertyChangingEventArgs e)
        {
            if (page != null)
            {
                Refresh(page.Date.DayOfWeek);
            }
        }
        private void DatePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (page != null)
            {
                Refresh();
            }
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