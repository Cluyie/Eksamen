using System;
using System.Collections.Generic;
using System.Text;
using XamarinFormsApp.Helpers;
using Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;

namespace XamarinFormsApp.ViewModel
{
   
        public class ViewRessourceViewModel: INotifyPropertyChanged
    {

            public string Id { get; set; }
            public string strName { get; set; }
            
     
        private ObservableCollection<Resource> items;
        public ObservableCollection<Resource> Items
        {
            get { return items; }
            set
            {

                items = value;
            }
        }


        public ViewRessourceViewModel()
        {
           



            Items = new ObservableCollection<Resource>() {
                new Resource()
                {
                    Id= Guid.NewGuid(),
                    strName= "Toiletpapir"

                },
                new Resource()
                {
                    Id= Guid.NewGuid(),
                    strName= "Håndsprit"

                },
                new Resource()
                {
                    strName= "[REDACTED]",
                    Id= Guid.NewGuid()
                    
                },
                new Resource()
                {
                    strName= "The backrooms",
                    Id= Guid.NewGuid()
                }
            };



            Resource res = new Resource();
            res.Id = Guid.NewGuid();
            res.strName = "Meeting room 1";

            Items.Add(res);

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}
