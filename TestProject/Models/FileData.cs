namespace TestProject.Models
{
    public partial class FileData
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string MimeType { get; set; }
        public string FilePath { get; set; }

        internal static object FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        internal static object Where(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
