using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Services
{
    public class FolderService
    {
        private readonly NotesContext _context;
        /// <summary>
        /// Инежктиране на контекста на базата данни
        /// </summary>
        /// <param name="context">Контекст на базата данни</param>
        public FolderService(NotesContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод за взимане на всички папки
        /// </summary>
        public async Task<List<FolderModel>> GetAllFolders()
        {
            List<FolderModel> folders = await _context.Folders.Include(el => el.Notes).ToListAsync();
            return folders;
        }
        /// <summary>
        /// Метод за взимане на папка по id
        /// </summary>
        /// <param name="id">ID на папка</param>
        public async Task<FolderModel> GetFolderById(int id) 
        {
            FolderModel folder = await _context.Folders.Include(el => el.Notes).FirstOrDefaultAsync(el => el.Id == id);
            return folder;
        }
        /// <summary>
        /// Метод за създаване на папка
        /// </summary>
        /// <param name="name">Име на папка</param>
        public async Task<FolderModel> CreateFolder(string name) 
        {
            FolderModel folder = new FolderModel() 
            { 
                Name = name,
                Notes=new List<NotesModel>() 
            };
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return folder;
        }
        /// <summary>
        /// Метод за изтриване на папка по id
        /// </summary>
        /// <param name="id">ID на папка</param>
        public async Task DeleteFolder(int id) 
        {
            await _context.Folders.Where(el => el.Id == id).ExecuteDeleteAsync();
        }
        /// <summary>
        /// Метод за редактиране на папка
        /// </summary>
        /// <param name="id">ID на папка</param>
        /// <param name="name">Име на папка</param>
        public async Task<FolderModel> EditFolder(int id, string name) 
        {
            FolderModel folder = await _context.Folders.FirstOrDefaultAsync(el => el.Id == id);
            if (folder != null)
            {
                folder.Name = name;
            }
            await _context.SaveChangesAsync();
            return folder;
        }
    }
}
