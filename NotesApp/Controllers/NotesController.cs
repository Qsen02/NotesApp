using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using NotesApp.Services;
using NotesApp.ViewModels;

namespace NotesApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly NoteService _noteService;
        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }

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
        public async Task<IActionResult> Delete(int id) 
        {
            NotesModel note = await _noteService.GetNoteById(id);
            return View(note);
        }

        public async Task<IActionResult> OnDelete(int id, int folderId) 
        {
            await _noteService.DeleteNote(id, folderId);
            return Redirect("/Folders/Details/" + folderId);
        }

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
