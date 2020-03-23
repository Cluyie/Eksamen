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
    public partial class ResourceView : ContentPage
    {

        ResourceViewModel _resourceViewModel;


        public ResourceView()
        {
            InitializeComponent();
            
            var viewRessourceViewModel = new ResourceViewModel();
            _resourceViewModel = viewRessourceViewModel.InitializeWithResourceData();
            BindingContext = _resourceViewModel ??= viewRessourceViewModel;





        }

        
    }

}