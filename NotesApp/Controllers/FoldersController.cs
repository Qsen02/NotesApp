using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using NotesApp.Services;
using NotesApp.ViewModels;

namespace NotesApp.Controllers
{
    public class FoldersController : Controller
    {
        private readonly FolderService _folderService;
        /// <summary>
        /// Инжектиране на сървиса за папки в конструктора
        /// </summary>
        /// <param name="folderService">Сървиса за папки</param>
        public FoldersController(FolderService folderService) 
        {
            _folderService = folderService;
        }
        /// <summary>
        /// Зареждане на страница за създаване на папка
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Действието за създаване на папка
        /// </summary>
        /// <param name="folder">Данни за папката</param>
        [HttpPost]
        public async Task<IActionResult> OnCreate([Bind("Name")] FolderViewModel folder) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _folderService.CreateFolder(folder.Name);
                    return Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Create", folder);
        }
        /// <summary>
        /// Зареждане на страница за изтриване на папка
        /// </summary>
        /// <param name="id">ID на папка</param>
        public async Task<IActionResult> Delete(int id) 
        {
            FolderModel folder = await _folderService.GetFolderById(id);
            return View(folder);
        }
        /// <summary>
        /// Действието за изтриване на папка
        /// </summary>
        /// <param name="id">ID на папка</param>
        public async Task<IActionResult> OnDelete(int id) 
        {
            if (id != null) 
            {
                await _folderService.DeleteFolder(id);
            }
            return Redirect("/Home/Index");
        }
        /// <summary>
        /// Зареждане на страница за редактиране на папка
        /// </summary>
        /// <param name="id">ID на папка</param>
        public async Task<IActionResult> Edit(int id) 
        {
            FolderModel folder = await _folderService.GetFolderById(id);
            FolderViewModel viewModel = new FolderViewModel()
            {
                Id = folder.Id,
                Name = folder.Name
            };
            return View(viewModel);
        }
        /// <summary>
        /// Действието за редактиране на папка
        /// </summary>
        /// <param name="folder">Данни за папката</param>
        /// <param name="id">ID на папка</param>
        [HttpPost]
        public async Task<IActionResult> OnEdit([Bind("Name")] FolderViewModel folder,int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _folderService.EditFolder(id,folder.Name);
                    return Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Create", folder);
        }
        /// <summary>
        /// Зареждане на страница с детайли за папка
        /// </summary>
        /// <param name="id">ID на папка</param>
        public async Task<IActionResult> Details(int id) 
        {
            FolderModel folder = await _folderService.GetFolderById(id);
            return View(folder);
        }
    }
}
