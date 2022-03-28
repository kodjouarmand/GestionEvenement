using GestionEvenement.Utility;
using System;
using System.ComponentModel.DataAnnotations;

namespace GestionEvenement.Domain.Assemblers
{
    public class EvenementDto : BaseDto<Guid>
    {
        [Required(ErrorMessage = StaticHelper.NameRequiredErrorMessage)]
        [Display(Name = "Name")]
        [MaxLength(32, ErrorMessage = StaticHelper.NameMaxLengthErrorMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = StaticHelper.DescriptionRequiredErrorMessage)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = StaticHelper.StartDateAndTimeRequiredErrorMessage)]
        [Display(Name = "Start Date and Time")]
        public DateTime StartDateAndTime { get; set; }

        [Required(ErrorMessage = StaticHelper.EndDateAndTimeRequiredErrorMessage)]
        [Display(Name = "End Date and Time")]
        public DateTime EndDateAndTime { get; set; }
    }
}
