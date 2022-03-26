using System;
using GestionEvenement.Domain.Entities;

namespace GestionEvenement.DataAccess.Repositories.Contracts
{
    public interface IEvenementRepository : IBaseRepository<Evenement, Guid>
    {
        public void Update(Evenement Evenement);        
    }
}
