using Chamada.Database;
using Chamada.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chamada.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        Group _group;
        ObservableCollection<Student> _students;
        ObservableCollection<Register> _registers;
        SQLiteAsyncConnection _connection;

        public RegisterPage(Group group)
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _group = group;
        }

        protected async override void OnAppearing()
        {
            //carregar lista de alunos do banco
            var students = await _connection.Table<Student>().Where(s => s.GroupId == _group.Id).ToListAsync();
            _students = new ObservableCollection<Student>(students);

            //carregar lista de chamadas do banco
            var registers = await _connection.Table<Register>().Where(r => r.GroupId == _group.Id).ToListAsync();
            _registers = new ObservableCollection<Register>(registers);

            RegisterList.ItemsSource = _registers;

            base.OnAppearing();
        }

        private void RegisterList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Register selectedRegister = (Register)((ListView)sender).SelectedItem;
            Navigation.PushAsync(new RegisterDetailPage(selectedRegister));
        }

        private void FAB_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddRegisterForm(_group, _students));
        }

        private void OnEdit(object sender, EventArgs e)
        {

        }

        private void OnDelete(object sender, EventArgs e)
        {

        }
    }
}