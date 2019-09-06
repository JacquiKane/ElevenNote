using ElevenNote.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryListItem
    {
        public int CategoryId { get; set; }

        [Required]
        // Is it social, family etc?
        public CategoryNorms CategoryType { get; set; }

        // All about Carol's new job
        public string CategoryEventType { get; set; }

        // Who created this category
        public string CategoryOwner { get; set; }

        // Set up for notes about carol's new job on March 13th blah blah
        public string CategoryDescription { get; set; }
    }
}
