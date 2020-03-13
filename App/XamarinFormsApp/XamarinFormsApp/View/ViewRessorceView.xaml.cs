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

        ViewRessourceViewModel viewRessourceViewModel;
        private ApiClientProxy _proxy;


        public ViewRessourceView()
        {
            InitializeComponent();

            viewRessourceViewModel  = new ViewRessourceViewModel();
                       

            BindingContext = viewRessourceViewModel.AllRessources;

            _proxy = DependencyService.Get<ApiClientProxy>();

        }
    }
}