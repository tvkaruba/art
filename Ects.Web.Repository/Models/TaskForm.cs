using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Art.Persistence.ReferenceData;

namespace Art.Web.Client.Models
{
    public class TaskForm
    {
        [Required]
        [EnumDataType(typeof(Module))]
        public Module Module { get; set; }

        [Required]
        public IEnumerable<string> Topics { get; set; }

        [Required]
        public IEnumerable<string> Tags { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [EnumDataType(typeof(TaskType))]
        public TaskType Type { get; set; }

        [Required]
        public IEnumerable<string> Answers { get; set; }

        [Required]
        public IEnumerable<string> Rights { get; set; }
    }
}
