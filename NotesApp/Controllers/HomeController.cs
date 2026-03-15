using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using NotesApp.Services;

namespace NotesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FolderService _folderService;

        public HomeController(ILogger<HomeController> logger,FolderService folderService)
        {
            _logger = logger;
            _folderService = folderService;
        }

        public async Task<IActionResult> Index()
        {
            List<FolderModel> folders = await _folderService.GetAllFolders();
            return View(folders);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
