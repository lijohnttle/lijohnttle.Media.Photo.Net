namespace lijohnttle.Media.Photo.Core
{
    /// <summary>
    /// Abstraction of a color.
    /// </summary>
    public interface IColor
    {
        /// <summary>
        /// Converts color into RGB color.
        /// </summary>
        RgbColor AsRgbColor();
    }
}
