using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfMvvmJsonSample.Commands;
using WpfMvvmJsonSample.Models;
using WpfMvvmJsonSample.Services;

namespace WpfMvvmJsonSample.ViewModels
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        private readonly INoteStorageService _storageService;
        private string _title = string.Empty;
        private string _body = string.Empty;

        public NoteViewModel(INoteStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            SaveCommand = new RelayCommand(async () => await SaveAsync(), CanExecuteSave);
            LoadCommand = new RelayCommand(async () => await LoadAsync());
            NewCommand = new RelayCommand(ResetNote);
        }

        public string Title
        {
            get => _title;
            set
            {
                var trimmed = value ?? string.Empty;
                if (trimmed.Length > 20)
                {
                    trimmed = trimmed.Substring(0, 20);
                }

                if (_title != trimmed)
                {
                    _title = trimmed;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PreviewBody));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string Body
        {
            get => _body;
            set
            {
                var trimmed = value ?? string.Empty;
                if (trimmed.Length > 300)
                {
                    trimmed = trimmed.Substring(0, 300);
                }

                if (_body != trimmed)
                {
                    _body = trimmed;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PreviewBody));
                }
            }
        }

        public string PreviewBody
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Body))
                {
                    return "미리보기할 본문이 없습니다.";
                }

                var builder = new StringBuilder();
                builder.Append(Body);
                if (Body.Length == 300)
                {
                    builder.Append("…");
                }

                return builder.ToString();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand NewCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool CanExecuteSave() => !string.IsNullOrWhiteSpace(Title);

        private async Task SaveAsync()
        {
            try
            {
                await _storageService.SaveAsync(new Note { Title = Title, Body = Body }).ConfigureAwait(false);
                MessageBox.Show("저장되었습니다.", "성공", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"저장 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadAsync()
        {
            try
            {
                var note = await _storageService.LoadAsync().ConfigureAwait(false);
                Title = note.Title;
                Body = note.Body;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"불러오기 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetNote()
        {
            Title = string.Empty;
            Body = string.Empty;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
