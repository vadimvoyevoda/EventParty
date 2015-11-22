using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_EvetsParty_MVC.Models
{
    public interface IWideModel : IModel
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

        [Required]
        int SelectedTypeId { get; set; }
        int SelectedPersonsCategoryId { get; set; }

        int CityId { get; set; }
        string MainPhoto { get; set; }
        List<string> Photos { get; set; }
    }
}
