using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Services
{
    public class NoteService
    {
        private readonly NotesContext _context;
        /// <summary>
        /// Инжектиране на конткеста на базата данни
        /// </summary>
        /// <param name="context">Контекста на базата данни</param>
        public NoteService(NotesContext context) 
        {
            _context = context;
        }
        /// <summary>
        /// Метод за взимане на бележка по id
        /// </summary>
        /// <param name="noteId">ID на бележка</param>
        public async Task<NotesModel> GetNoteById(int noteId) 
        {
            NotesModel note = await _context.Notes.Include(el=>el.Folder).FirstOrDefaultAsync(el => el.Id == noteId);
            return note;
        }
        /// <summary>
        /// Метод за създаване на нова бележка
        /// </summary>
        /// <param name="folderId">ID на папка в която ще се намира бележката</param>
        /// <param name="title">Заглавие на бележката</param>
        /// <param name="description">Текст в самата бележка</param>
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
        /// <summary>
        /// Метод за изтриване на бележка
        /// </summary>
        /// <param name="noteId">ID на бележката</param>
        /// <param name="folderId">ID на папката в която се намира бележката</param>
        public async Task DeleteNote(int noteId,int folderId) 
        {
            FolderModel folder = await _context.Folders.FirstOrDefaultAsync(el => el.Id == folderId);
            NotesModel note = await _context.Notes.FirstOrDefaultAsync(el => el.Id == noteId);
            folder?.Notes.Remove(note);
            await _context.Notes.Where(el => el.Id == noteId).ExecuteDeleteAsync();
        }
        /// <summary>
        /// Метод за редактиране на бележка
        /// </summary>
        /// <param name="noteId">ID на бележката</param>
        /// <param name="title">Заглавие на бележката</param>
        /// <param name="description">Текст в самата бележка</param>
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
