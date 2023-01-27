namespace TestProject.Models
{
    public class FileRecord
    {
       

        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileFormat { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string Create_password { get; set; }
        public string Confirm_password { get; set; }

    }
}
