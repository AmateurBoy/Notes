using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
   
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NoteAddingPage), typeof(NoteAddingPage));
        }
    }
}