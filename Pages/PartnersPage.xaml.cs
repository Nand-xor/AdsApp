using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdsApp.Pages
{
    public partial class PartnersPage : Page
    {
        public PartnersPage()
        {
            InitializeComponent();

            var currentPartners = Entities.GetContext().Партнёр.ToList();
            PartnersList.ItemsSource = currentPartners;
        }

        private void PartnersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Партнёр partner = PartnersList.SelectedItem as Партнёр;
            if (partner == null)
                return;

            NavigationService.Navigate(new AddEditPage(partner));
            PartnersList.SelectedItem = null;
        }
    }
}
