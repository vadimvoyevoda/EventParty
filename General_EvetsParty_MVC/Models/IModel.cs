using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General_EvetsParty_MVC.Models
{
    public interface IModel
    {
        [Key]
        [Required]
        int Id { get; set; }
        [Required]
        string Title { get; set; }
        [Required]
        [UIHint("DateTimeLong")]
        DateTime StartTime { get; set; }
        [UIHint("DateTimeLong")]
        DateTime? EndTime { get; set; }
        [Required]
        [MaxLength(256)]
        string Place { get; set; }
        [Required]
        string Organizers { get; set; }
        [Required]
        string Description { get; set; }
        [Required]
        [MaxLength(100)]
        string ShortDescription { get; set; }
        string Sponsors { get; set; }
        bool IsCharitable { get; set; }
        bool IsFreeEntrance { get; set; }
        string Enter { get; set; }

        string MainPhoto { get; set; }
        List<string> Photos { get; set; }

        List<EventLike> Likes { get; set; }
        List<EventLike> DisLikes { get; set; }

    }
}