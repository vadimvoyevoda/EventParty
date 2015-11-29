using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General_EvetsParty_MVC.Models
{
    public class EventDetailsViewModel : IModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [UIHint("DateTimeLong")]
        public DateTime StartTime { get; set; }
        [UIHint("DateTimeLong")]
        public DateTime? EndTime { get; set; }
        [Required]
        public EventType Type { get; set; }
        public EventPersonCategory PersonsCategory { get; set; }
        [Required]
        [MaxLength(256)]
        public string Place { get; set; }
        [Required]
        public string Organizers { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string ShortDescription { get; set; }
        public string Sponsors { get; set; }
        public bool IsCharitable { get; set; }
        public bool IsFreeEntrance { get; set; }
        public string Enter { get; set; }

        [Required]
        public EventCity City { get; set; }
        public string MainPhoto { get; set; }
        public List<string> Photos { get; set; }
        public EventCustomer Publisher { get; set; }

        public List<EventCustomer> Members { get; set; }
        public List<EventCustomer> MayAttend { get; set; }
        public List<EventCustomer> NoAttend { get; set; }

        public List<EventLike> Likes { get; set; }
        public List<EventLike> DisLikes { get; set; }
    }
}