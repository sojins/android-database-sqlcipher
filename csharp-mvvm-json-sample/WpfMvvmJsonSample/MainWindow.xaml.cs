using System.IO;
using System.Windows;
using WpfMvvmJsonSample.Services;
using WpfMvvmJsonSample.ViewModels;

namespace WpfMvvmJsonSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "note.json");
            DataContext = new NoteViewModel(new JsonNoteService(dataPath));
        }
    }
}
