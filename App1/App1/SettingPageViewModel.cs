using System.Windows.Input;
using App1;
using Xamarin.Forms;


public class SettingPageViewModel
{
    public ICommand GoHomeCommand { get; set; }
    public ICommand GoSecondCommand { get; set; }
    public ICommand GoThirdCommand { get; set; }

    public SettingPageViewModel()
    {
        GoHomeCommand = new Command(GoHome);
        GoSecondCommand = new Command(GoSecond);
        GoThirdCommand = new Command(GoThird);
    }
    void GoHome()
    {
        //Application.NavigationPage.Navigation.PopToRootAsync();
        //App.MenuisPresented = false;
    }
    void GoSecond(object obj)
    {
        App.NavigationPage.Navigation.PushAsync(new Home()); //the content page you wanna load on this click event 
        App.MenuIsPresented = false;
    }

    void GoThird(object obj)
    {
        //App.NavigationPage.Navigation.PushAsync(new ClinicInformation());
        //App.MenuIsPresented = false;
    }
}