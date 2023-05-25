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
    public partial class DatabasePage : ContentPage
    {
        public DatabasePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.Database.SavePersonAsync(new Person
                {
                    Name = nameEntry.Text,
                    Password = PasswordEntry.Text,
                    IsAdmin = isAdmin.IsChecked
                });
                nameEntry.Text = string.Empty;
                PasswordEntry.Text = string.Empty;
                isAdmin.IsChecked = false;
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }
        }
        Person LastSelection;
        void collectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            LastSelection = e.CurrentSelection.FirstOrDefault() as Person;
            nameEntry.Text = LastSelection.Name;
            isAdmin.IsChecked = LastSelection.IsAdmin;
        }

        async  void OnDeleteClicked(object sender, EventArgs e)
        {
            if (LastSelection != null)
            {
                await App.Database.DeletePersonAsync(LastSelection);
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
                nameEntry.Text = string.Empty;
                PasswordEntry.Text = string.Empty;
                isAdmin.IsChecked = false;
            }
        }
        
        private async void NavigateButton_OnClicked4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        async void OnUpdateClicked(object sender, EventArgs e)
        {
            if (LastSelection != null)
            {
                LastSelection.Name = nameEntry.Text;
                LastSelection.IsAdmin = isAdmin.IsChecked;
                LastSelection.Password = PasswordEntry.Text;
                await App.Database.UpdatePersonAsync(LastSelection);
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
                
            }
        }

        async void OnShowAdminsClicked(object sender, EventArgs e)
        {
            collectionView.ItemsSource = await App.Database.QueryIsAdminAsync();
        }

        async void OnShowNonAdminsClicked(object sender, EventArgs e)
        {
            collectionView.ItemsSource = await App.Database.LinqNoAdminAsync();
        }

        async void OnShowAllClicked(object sender, EventArgs e)
        {
            collectionView.ItemsSource = await App.Database.LinqShowAllAsync();
        }

        private void OnButtonClickedshowButtons(object sender, EventArgs e)
        {
            var clicked = Buttons.IsVisible;
            if (!clicked)
            {
                Buttons.IsVisible = true;
            }
            else
            {
                Buttons.IsVisible = false;
            }
          
            
        }
    }
}