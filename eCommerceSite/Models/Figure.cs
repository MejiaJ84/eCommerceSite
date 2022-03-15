using System.ComponentModel.DataAnnotations;

namespace eCommerceSite.Models
{
    /// <summary>
    /// Represents a single Warhammer figure for purchase
    /// </summary>
    public class Figure
    {
        /// <summary>
        /// Unique identifier for each figure
        /// </summary>
        [Key]
        public int FigureId { get; set; }

        /// <summary>
        /// Legion from which the figure belongs 
        /// determines color, primarch, etc.
        /// </summary>
        [Required]
        public string Legion { get; set; }

        /// <summary>
        /// Whether its a Primarch, Rank and file soldier, etc.
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// The sales price of each figure.
        /// </summary>
        [Range(0, 2000)]
        public double Price { get; set; }
    }

    /// <summary>
    /// A single figure that has been added to the users
    /// shopping cart cookie
    /// </summary>
    public class CartFigureViewModel
    {
        public int FigureId { get; set; }

        public string Legion { get; set; }

        public string Type { get; set; }

        public double Price { get; set; }
    }
}
