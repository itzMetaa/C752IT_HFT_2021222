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
    public enum GameType
    { 
        FPS, RPG, Indie, Puzzle, Horror, Looter, Rhythm
    }
    [Table("Games")]
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public GameType Type { get; set; }
        //Developer link
        [NotMapped]
        [JsonIgnore]
        public virtual Developer Developer { get; set; }

        [ForeignKey(nameof(Developer))]
        public int DeveloperId { get; set; }

        public Game(int id, string title, int price, string description, GameType type, int developerId)
        {
            Id = id;
            Title = title;
            Price = price;
            Description = description;
            Type = type;
            DeveloperId = developerId;
        }
    }


}
