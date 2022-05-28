namespace lijohnttle.Media.Photo.App.Events
{
    public class ImageFileSelectedEvent
    {
        public ImageFileSelectedEvent(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }
    }
}
