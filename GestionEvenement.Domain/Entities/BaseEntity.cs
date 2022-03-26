using System;
using System.ComponentModel.DataAnnotations;

namespace GestionEvenement.Domain.Entities
{
    public abstract class BaseEntity<TEntityKey>
    {
        [Key]
        public TEntityKey Id { get; set; }
    }
}
