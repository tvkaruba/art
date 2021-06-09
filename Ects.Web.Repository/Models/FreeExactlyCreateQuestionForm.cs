using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ects.Web.Repository.Models
{
    public class FreeExactlyCreateQuestionForm
    {
        [Required]
        public string Namespace { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IEnumerable<string> Tags { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string Right { get; set; }
    }
}
