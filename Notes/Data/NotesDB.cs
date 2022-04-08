using System.Collections.Generic;
using SQLite;
using Notes.Models;
using System.Threading.Tasks;

namespace Notes.Data
{
   public class NotesDB
    {
        readonly SQLiteAsyncConnection DB;
        public NotesDB(string connect)
        {
            DB = new SQLiteAsyncConnection(connect);
            DB.CreateTableAsync<Note>().Wait();
        }
        public Task<List<Note>> GetNotesAsync()
        {
            return DB.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            return DB.Table<Note>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }
        public Task<int> SaveNoteAsync(Note note)
        {
            if(note.ID !=0)
            {
                return DB.UpdateAsync(note);
            }
            else
            {
                return DB.InsertAsync(note);
            }
        }
        public Task<int> DelNoteAsync(Note note)
        {
            return DB.DeleteAsync(note);
        }
    }
}
