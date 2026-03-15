using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using NotesApp.Services;
using NotesApp.ViewModels;

namespace NotesApp.Controllers
{
    public class FoldersController : Controller
    {
        private readonly FolderService _folderService;

        public FoldersController(FolderService folderService) 
        {
            _folderService = folderService;
        }
        public IActionResult Create()
        {
            return View();
        }
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
        public async Task<IActionResult> Delete(int id) 
        {
            FolderModel folder = await _folderService.GetFolderById(id);
            return View(folder);
        }
        public async Task<IActionResult> OnDelete(int id) 
        {
            if (id != null) 
            {
                await _folderService.DeleteFolder(id);
            }
            return Redirect("/Home/Index");
        }
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

        public async Task<IActionResult> Details(int id) 
        {
            FolderModel folder = await _folderService.GetFolderById(id);
            return View(folder);
        }
    }
}
