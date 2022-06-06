namespace lijohnttle.Media.Photo.Filters.Median.Histograms
{
    public class HistogramsMedianFilterMetrics
    {
        public int PixelsProcessed { get; set; }


        public override string ToString()
        {
            return $"Pixels processed: {PixelsProcessed}";
        }
    }
}
