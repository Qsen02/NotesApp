using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Services
{
    public class FolderService
    {
        private readonly NotesContext _context;

        public FolderService(NotesContext context)
        {
            _context = context;
        }

        public async Task<List<FolderModel>> GetAllFolders()
        {
            List<FolderModel> folders = await _context.Folders.Include(el => el.Notes).ToListAsync();
            return folders;
        }

        public async Task<FolderModel> GetFolderById(int id) 
        {
            FolderModel folder = await _context.Folders.Include(el => el.Notes).FirstOrDefaultAsync(el => el.Id == id);
            return folder;
        }

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
        public async Task DeleteFolder(int id) 
        {
            await _context.Folders.Where(el => el.Id == id).ExecuteDeleteAsync();
        }

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
