using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfMvvmJsonSample.Models;

namespace WpfMvvmJsonSample.Services
{
    public interface INoteStorageService
    {
        Task<Note> LoadAsync();
        Task SaveAsync(Note note);
    }

    public class JsonNoteService : INoteStorageService
    {
        private readonly string _filePath;
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };

        public JsonNoteService(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public async Task SaveAsync(Note note)
        {
            if (note is null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            var json = JsonConvert.SerializeObject(note, SerializerSettings);
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using var writer = new StreamWriter(_filePath, false);
            await writer.WriteAsync(json).ConfigureAwait(false);
        }

        public async Task<Note> LoadAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new Note();
            }

            using var reader = File.OpenText(_filePath);
            var content = await reader.ReadToEndAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new Note();
            }

            return JsonConvert.DeserializeObject<Note>(content) ?? new Note();
        }
    }
}
