using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFBindableStackLayout
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        public void OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (MyColor)e.SelectedItem;
            System.Diagnostics.Debug.WriteLine(selectedItem.Name);

        }
    }
}
