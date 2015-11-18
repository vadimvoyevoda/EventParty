using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Table("Events")]
    public class EventModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public virtual EventType Type { get; set; }
        [Required]
        [UIHint("DateTimeLong")]
        public DateTime StartTime { get; set; }
        [UIHint("DateTimeLong")]
        public DateTime? EndTime { get; set; }
        [Required]
        [MaxLength(256)]
        public string Place { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string ShortDescription { get; set; }
        [Required]
        public bool IsCharitable { get; set; }
        [Required]
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public string Sponsors { get; set; }
        public string MainPhoto { get; set; }
        public string Enter { get; set; }
        public bool isFreeEntrance { get; set; }
        [Required]
        public string Organizers { get; set; }

        [Required]
        public virtual EventCity City { get; set; }
        public virtual EventPersonCategory PersonsCategory { get; set; }
        [Required]
        public virtual EventCustomer Publisher { get; set; }
        public virtual List<EventCustomer> Members { get; set; }
        public virtual List<EventCustomer> MayAttend { get; set; }
        public virtual List<EventCustomer> NoAttend { get; set; }
        public virtual List<EventLike> Likes { get; set; }
        public virtual List<EventComment> Comments { get; set; }
        public virtual List<EventPhoto> Photos { get; set; }
    }

    [Table("Regions")]
    public class EventRegion
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string RegionName { get; set; }

        public virtual List<EventCity> Cities { get; set; }
        [Required]
        public virtual EventCountry Country { get; set; }
    }

    [Table("Countries")]
    public class EventCountry
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Country { get; set; }

        public virtual List<EventRegion> Regions { get; set; }
    }

    [Table("Cities")]
    public class EventCity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string CityName { get; set; }

        [Required]
        public virtual EventRegion Region { get; set; }
        public virtual List<EventModel> SuchEvents { get; set; }
    }

    [Table("Types")]
    public class EventType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Type { get; set; }

        public virtual List<EventModel> SuchEvents { get; set; }
    }

    [Table("PersonCategories")]
    public class EventPersonCategory
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Category { get; set; }

        public virtual List<EventModel> SuchEvents { get; set; }
    }

    [Table("Customers")]
    public class EventCustomer
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        public string Salt { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(13)]
        [MinLength(13)]
        public string Mobile { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public bool IsBan { get; set; }
        public bool IsDeleted { get; set; }
        public bool ShowContacts { get; set; }
        public string Photo { get; set; }
        [Required]
        public virtual EventRating Rating { get; set; }

        [Required]
        public virtual EventPermission Permissions { get; set; }
        public virtual List<EventModel> OrganizedEvents { get; set; }
        public virtual List<EventModel> AttendedEvents { get; set; }
    }

    [Table("Ratings")]
    public class EventRating
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual List<EventCustomer> Customers { get; set; }
    }

    [Table("Permissions")]
    public class EventPermission
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Index(IsUnique=true)]
        public string Type { get; set; }

        public virtual List<EventCustomer> Customers { get; set; }
    }

    [Table("Likes")]
    public class EventLike
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual LikeCommentType Type { get; set; }
        [Required]
        public virtual EventCustomer Customer { get; set; }
        [Required]
        public virtual EventModel Event { get; set; }
    }

    [Table("Comments")]
    public class EventComment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime WriteTime { get; set; }
        [Required]
        public virtual LikeCommentType Type { get; set; }
        [Required]
        [MaxLength(256)]
        public string Text { get; set; }
        [Required]
        public virtual EventCustomer Customer { get; set; }
        [Required]
        public virtual EventModel Event { get; set; }
    }

    [Table("LikeCommentTypes")]
    public class LikeCommentType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
    }

    [Table("Photos")]
    public class EventPhoto
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }

        public virtual List<EventModel> PhotoEvents { get; set; }
    }
}
