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

        ViewRessourceViewModel _viewRessourceViewModel;


        public ViewRessourceView()
        {
            InitializeComponent();

            var viewRessourceViewModel = new ViewRessourceViewModel();
            _viewRessourceViewModel = viewRessourceViewModel.InitializeWithResourceData();
            BindingContext = _viewRessourceViewModel ??= viewRessourceViewModel;


            
            

        }

        
    }

}