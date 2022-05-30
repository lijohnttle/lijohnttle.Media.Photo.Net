using System;

namespace lijohnttle.Media.Photo.Filters.Gaussian
{
    /// <summary>
    /// Gaussian filter options.
    /// </summary>
    public class GaussianFilterOptions
    {
        private int radius = 1;
        private double weight = 5.5;


        /// <summary>
        /// Gets the radius of the filter. Must be a positive number.
        /// </summary>
        public int Radius
        {
            get => radius;
            init
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "Radius of the gaussian filter must be a positive number.");
                }

                radius = value;
            }
        }

        /// <summary>
        /// Gets the weight of the filter. Must be a positive number.
        /// </summary>
        public double Weight
        {
            get => weight;
            init
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "Radius of the gaussian filter must be a positive number.");
                }

                weight = value;
            }
        }
    }
}
