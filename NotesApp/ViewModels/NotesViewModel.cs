using System.ComponentModel.DataAnnotations;

namespace NotesApp.ViewModels
{
    public class NotesViewModel
    {
        public int FolderId { get; set; }
        public int NoteId { get; set; }
        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }
    }
}
