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
    [Table("Publishers")]
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //Developer Link
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Developer> Developers { get; set; }

        public Publisher(int id, string name)
        {
            Id = id;
            Name = name;
            Developers = new HashSet<Developer>();
        }

        public Publisher()
        {
            Developers = new HashSet<Developer>();
        }

    }
}
