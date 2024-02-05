using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbOperations.Model{
    public class FilmModel{
        [Required]
        [Key]
        public int film_id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public short? release_year { get; set; }

        public byte language_id { get; set; }

        public byte? original_language_id { get; set; }

        public byte rental_duration { get; set; } = 3;

        [Column(TypeName = "decimal(4,2)")]
        public decimal rental_rate { get; set; }

        public ushort? length { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal replacement_cost { get; set; }

        public string rating { get; set; }

        public string special_features { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime last_update { get; set; }
    }

}