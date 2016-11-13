using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using SQLite;

namespace Todo2.Models
{
    [ImplementPropertyChanged]
    [Table(nameof(TaskItem))]
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Priority { get; set; }
        public DateTime Date { get; set; }

        public bool IsValid()
        {
            return (!String.IsNullOrWhiteSpace(Name));
        }
    }
}
