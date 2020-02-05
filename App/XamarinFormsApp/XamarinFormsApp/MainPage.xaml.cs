using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsApp
{
  // Learn more about making custom code visible in the Xamarin.Forms previewer
  // by visiting https://aka.ms/xamarinforms-previewer
  [DesignTimeVisible(false)]
  public partial class MainPage : ContentPage
  {
    private string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
      "notes.txt");
    public MainPage()
    {
      InitializeComponent();

      if (File.Exists(_fileName))
      {
        Editor.Text = File.ReadAllText(_fileName);
        Debug.WriteLine($"File loaded from path: '{_fileName}' with text '{Editor.Text}'.");
      }
    }

    private void SaveButton_OnClicked(object sender, EventArgs e)
    {
      File.WriteAllText(_fileName, Editor.Text);
    }

    private void DeleteButton_OnClicked(object sender, EventArgs e)
    {
      if (File.Exists(_fileName))
        File.Delete(_fileName);
      Editor.Text = String.Empty;
    }
  }
}
