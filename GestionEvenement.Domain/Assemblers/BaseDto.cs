using System;

namespace GestionEvenement.Domain.Assemblers
{
    public abstract class BaseDto<TEntityKey>
    {
        public TEntityKey Id { get; set; }
        public bool IsNew() => Id.Equals(default(TEntityKey));
    }
}
