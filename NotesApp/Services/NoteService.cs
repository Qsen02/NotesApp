using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Services
{
    public class NoteService
    {
        private readonly NotesContext _context;
        public NoteService(NotesContext context) 
        {
            _context = context;
        }
        public async Task<NotesModel> GetNoteById(int noteId) 
        {
            NotesModel note = await _context.Notes.Include(el=>el.Folder).FirstOrDefaultAsync(el => el.Id == noteId);
            return note;
        }
        public async Task<NotesModel> CreateNote(int folderId,string title, string description) 
        {
            NotesModel note = new NotesModel()
            {
                FolderId = folderId,
                Title = title,
                Description = description
            };
            _context.Notes.Add(note);
            FolderModel folder = await _context.Folders.FirstOrDefaultAsync(el => el.Id == folderId);
            folder?.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }
        public async Task DeleteNote(int noteId,int folderId) 
        {
            FolderModel folder = await _context.Folders.FirstOrDefaultAsync(el => el.Id == folderId);
            NotesModel note = await _context.Notes.FirstOrDefaultAsync(el => el.Id == noteId);
            folder?.Notes.Remove(note);
            await _context.Notes.Where(el => el.Id == noteId).ExecuteDeleteAsync();
        }
        public async Task<NotesModel> EditNote(int noteId, string title, string description) 
        {
            NotesModel note = await _context.Notes.Include(el=>el.Folder).FirstOrDefaultAsync(el => el.Id == noteId);
            note.Title = title;
            note.Description = description;
            await _context.SaveChangesAsync();
            return note;
        }
    }
}
