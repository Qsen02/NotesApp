namespace NotesApp.Models
{
    public class NotesModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int FolderId { get; set; }
        public FolderModel Folder { get; set; }
    }
}
