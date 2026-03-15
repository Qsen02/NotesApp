using System.ComponentModel.DataAnnotations;

namespace NotesApp.ViewModels
{
    public class FolderViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
    }
}
