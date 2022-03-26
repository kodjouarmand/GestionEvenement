using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEvenement.Domain.Entities
{
    public class Evenement : BaseEntity<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDateAndTime { get; set; }

        [Required]
        public DateTime EndDateAndTime { get; set; }
    }

}
