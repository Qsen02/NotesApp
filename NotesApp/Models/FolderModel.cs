namespace NotesApp.Models
{
    public class FolderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<NotesModel> Notes { get; set; }
    }
}
