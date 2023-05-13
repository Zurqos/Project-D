using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HamburgerMenu : ContentPage
    {
        public HamburgerMenu()
        {
            //IconImageSource = "yourHamburgerIcon.png"; TODO: Als we IOS gaan implenteren dubbel checken
            InitializeComponent();
        }
        
    }

    
}