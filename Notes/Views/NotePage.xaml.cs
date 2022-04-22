using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Models;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {

        Grid GridLove = new Grid
        {
            Padding = 20,
            ColumnSpacing = 20,
            RowSpacing = 20,
            

        };

        //Метод который будет переберать заметки в БД и каждой назначать место нахожненее(При каждом переходе на оту строницу)
        public void UpdatePosition(List<Note> notes)
        {
            GridLove.Children.Clear();
            int Horizont = 0;
            int Vertikal = 0;
            StackLayout[] BoxLable = new StackLayout[notes.Count];
            Label[] TextMasiv = new Label[notes.Count];
            Label[] DataMasiv = new Label[notes.Count];
            Label[] ID = new Label[notes.Count];
            int count = 0;
            foreach (var item in notes)
            {
                if(Horizont>=2)
                { 
                    Horizont = 0;
                    Vertikal++;
                }


                BoxLable[count] = new StackLayout() { BackgroundColor=Color.FromHex("#FFDD8A"),Padding=30,HorizontalOptions=LayoutOptions.Center,VerticalOptions=LayoutOptions.Center};
                TextMasiv[count] = new Label() { TextColor = Color.Black, };
                DataMasiv[count] = new Label() { TextColor = Color.Black, };
                ID[count] = new Label() { TextColor = Color.FromHex("#FFDD8A"), };
                TapGestureRecognizer Test = new TapGestureRecognizer();
                Test.Tapped += Test_Tapped;
                BoxLable[count].GestureRecognizers.Add(Test);

                //Важно что бы довабление ID Было первой строчкой :) или все сломаеться нахуй.
                ID[count].Text = item.ID.ToString();

                TextMasiv[count].Text = item.ContentNotesText;
                DataMasiv[count].Text = Convert.ToString(item.Date);
                BoxLable[count].Children.Add(ID[count]);
                BoxLable[count].Children.Add(TextMasiv[count]);
                BoxLable[count].Children.Add(DataMasiv[count]);
                GridLove.Children.Add(BoxLable[count], Horizont, Vertikal);
                Horizont++;
                count++;
            }
            ScrollContent.Content = GridLove;
        }

        private void Test_Tapped(object sender, EventArgs e)
        {
            StackLayout Objeckt = (StackLayout)sender;
            Objeckt.BackgroundColor = Color.Black;
            
            foreach (var item in Objeckt.Children)
            {
                if(item is Label test)
                {
                    ChekBD(test.Text);
                    break;
                }
            }
            
        }

        

       

        public NotePage()
        {
            
            InitializeComponent();
            StartUpDateProgram();

        }
        protected override void OnAppearing()
        {
            StartUpDateProgram();
            base.OnAppearing();
        }
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NoteAddingPage));
        }
        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Note note = (Note)e.CurrentSelection.FirstOrDefault();
            await Shell.Current.GoToAsync($"{nameof(NoteAddingPage)}?{nameof(NoteAddingPage.ItemId)}={note.ID.ToString()}");
        }
        public async void ChekBD(string note)
        {
            await Shell.Current.GoToAsync($"{nameof(NoteAddingPage)}?{nameof(NoteAddingPage.ItemId)}={note}");
        }
        public async void StartUpDateProgram()
        {
            UpdatePosition(await App.NotesDB.GetNotesAsync());
        }
        private async void DelClick(object sender, EventArgs e)
        {
            
             App.NotesDB.AllDell();
             UpdatePosition(await App.NotesDB.GetNotesAsync());


        }

       
    }
}