using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.Models
{
    [Table("Developers")]
    public class Developer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TeamSize { get; set; }
        // Game link
        [NotMapped]
        public virtual ICollection<Game> Games { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Game Game { get; set; }
        // Publisher link
        [NotMapped]
        [JsonIgnore]
        public virtual Publisher Publisher { get; set; }
        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }

        public Developer(int id, string name, int teamSize, int publisherId)
        {
            Id = id;
            Name = name;
            TeamSize = teamSize;
            PublisherId = publisherId;
            Games = new HashSet<Game>();
        }

        public Developer()
        {
        }
    }
}
