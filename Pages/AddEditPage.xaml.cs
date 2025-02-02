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
    public partial class AddEditPage : Page
    {
        private Партнёр _partner = new Партнёр();

        public AddEditPage(Партнёр selectedPartner)
        {
            InitializeComponent();

            if (selectedPartner != null)
            {
                _partner = selectedPartner;
                Title = "Редактирование";
            }
            else
                Title = "Добавление";

            DataContext = _partner;

            if (_partner.ПродажиПартнёра.Count == 0)
            {
                btnHistory.Visibility = Visibility.Hidden;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int zeroRate = 0;
            if (!int.TryParse(txtRate.Text, out zeroRate))
            {
                MessageBox.Show("Рейтинг должен быть целочисленным!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) || 
                string.IsNullOrWhiteSpace(txtDir.Text) || 
                string.IsNullOrWhiteSpace(txtAddress.Text) || 
                string.IsNullOrWhiteSpace(txtEmail.Text) || 
                string.IsNullOrWhiteSpace(txtINN.Text) || 
                string.IsNullOrWhiteSpace(txtPhone.Text) || 
                string.IsNullOrWhiteSpace(txtRate.Text) || 
                cmbType.SelectedIndex == 0 || 
                int.Parse(txtRate.Text) < 0)
            {
                MessageBox.Show("Заполните все поля корректно!");
                return;
            }
            
            if (_partner.ID == 0)
            {
                Entities.GetContext().Партнёр.Add(_partner);
                try
                {
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Партнёр успешно сохранён");
                    _partner = new Партнёр();
                    DataContext = _partner;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные о партнёре успешно изменены");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.PartnerSalePage(_partner.ПродажиПартнёра.ToList()));
        }
    }
}
