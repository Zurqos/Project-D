using System.Windows.Input;
using App1;
using Xamarin.Forms;


public class HamburgerMenuViewModel
{
    public ICommand GoHomeCommand { get; set; }
    public ICommand GoSecondCommand { get; set; }
    public ICommand GoThirdCommand { get; set; }

    public HamburgerMenuViewModel()
    {
        GoHomeCommand = new Command(GoHome);
        GoSecondCommand = new Command(GoSecond);
        GoThirdCommand = new Command(GoThird);
    }
    void GoHome()
    {
        App.NavigationPage.Navigation.PopToRootAsync();
        App.MenuIsPresented = false;
    }
    void GoSecond(object obj)
    {
        App.NavigationPage.Navigation.PushAsync(new Home());  
        App.MenuIsPresented = false;
    }

    void GoThird(object obj)
    {
        //App.NavigationPage.Navigation.PushAsync(new ClinicInformation());
        //App.MenuIsPresented = false;
    }
}