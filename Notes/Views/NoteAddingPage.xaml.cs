using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [QueryProperty(nameof(ItemId),nameof(ItemId))]
    public partial class NoteAddingPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }
        private async void LoadNote(string value)
        {
            try 
            {
                int id = Convert.ToInt32(value);
                    Note note = await App.NotesDB.GetNoteAsync(id);
                    BindingContext = note;
                
            }
            catch { }
        }
        public NoteAddingPage()
        {
            BindingContext = new Note();
            InitializeComponent();
            

        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;
            note.Date = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(note.ContentNotesText))
            {
                await App.NotesDB.SaveNoteAsync(note);
            }

            await Shell.Current.GoToAsync("..");

        }

        private async void DelButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;
            
            await App.NotesDB.DelNoteAsync(note);
            
            await Shell.Current.GoToAsync("..");
        }
    }
}