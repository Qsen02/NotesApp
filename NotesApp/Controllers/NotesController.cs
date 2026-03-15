using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using NotesApp.Services;
using NotesApp.ViewModels;

namespace NotesApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly NoteService _noteService;
        /// <summary>
        /// Инжектиране на сървиса за бележки в конструктора
        /// </summary>
        /// <param name="noteService">Сървис за бележки</param>
        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }
        /// <summary>
        /// Зареждане на страница за създаване на бележка
        /// </summary>
        /// <param name="id">ID на папката в която се намира бележката</param>
        public IActionResult Create(int id)
        {
            NotesViewModel model = new NotesViewModel()
            {
                FolderId = id,
                Title = "",
                Description = ""
            };
            return View(model);
        }
        /// <summary>
        /// Действието за създаване на нова бележка
        /// </summary>
        /// <param name="newNote">Данни за бележката</param>
        /// <param name="id">ID на папката в която се намира бележката</param>
        [HttpPost]
        public async Task<IActionResult> OnCreate([Bind("Title,Description")] NotesViewModel newNote, int id) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _noteService.CreateNote(id,newNote.Title,newNote.Description);
                    return Redirect("/Folders/Details/" + id);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Create", newNote);
        }
        /// <summary>
        /// Зареждане на страница за изтриване на бележка
        /// </summary>
        /// <param name="id">ID на бележката</param>
        public async Task<IActionResult> Delete(int id) 
        {
            NotesModel note = await _noteService.GetNoteById(id);
            return View(note);
        }
        /// <summary>
        /// Действие за изтриване на бележка
        /// </summary>
        /// <param name="id">ID на бележката</param>
        /// <param name="folderId">ID на папката в която се намира бележката</param>
        public async Task<IActionResult> OnDelete(int id, int folderId) 
        {
            await _noteService.DeleteNote(id, folderId);
            return Redirect("/Folders/Details/" + folderId);
        }
        /// <summary>
        /// Зареждане на страницата за редактиране на бележка
        /// </summary>
        /// <param name="id">ID на бележката</param>
        public async Task<IActionResult> Edit(int id) 
        {
            NotesModel note = await _noteService.GetNoteById(id);
            NotesViewModel model = new NotesViewModel()
            {
                FolderId = note.Folder.Id,
                NoteId = id,
                Title = note.Title,
                Description= note.Description,
            };
            return View(model);
        }
        /// <summary>
        /// Действие за редактиране на бележка
        /// </summary>
        /// <param name="note">Данни за бележката</param>
        /// <param name="id">ID на бележката</param>
        [HttpPost]
        public async Task<IActionResult> OnEdit([Bind("Title,Description")] NotesViewModel note, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NotesModel updatedNote = await _noteService.EditNote(id, note.Title,note.Description);
                    return Redirect("/Folders/Details/" + updatedNote.Folder.Id);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Create", note);
        }
    }
}
