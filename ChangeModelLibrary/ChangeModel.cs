using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeModelLibrary
{
    public class ChangeModel
    {   
        public int Id { get; set; }        
        public string Title { get; set; }        
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int SelectedTypeId { get; set; }
        public int SelectedPersonsCategoryId { get; set; }
        public string Place { get; set; }
        public string Organizers { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Sponsors { get; set; }
        public bool IsCharitable { get; set; }
        public bool IsFreeEntrance { get; set; }
        public string Enter { get; set; }

        public List<string> Photos { get; set; }
    }
}
