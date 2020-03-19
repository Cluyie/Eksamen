using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.ViewModel;


namespace XamarinFormsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewRessourceView : ContentPage
    {

        


        public ViewRessourceView()
        {
            InitializeComponent();

            BindingContext = new ViewRessourceViewModel();

            
            //_proxy = DependencyService.Get<ApiClientProxy>();

        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //}
    }

}