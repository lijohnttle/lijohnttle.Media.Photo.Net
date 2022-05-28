namespace lijohnttle.Media.Photo.Core
{
    public interface IImage
    {
        int Width { get; }

        int Height { get; }

        IColor GetPixelColor(int x, int y);
    }
}
