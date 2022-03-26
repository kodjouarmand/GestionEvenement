using GestionEvenement.Domain.Entities;
using System;
using GestionEvenement.Domain.Contexts;
using GestionEvenement.DataAccess.Repositories.Contracts;

namespace GestionEvenement.DataAccess.Repositories
{
    public class EvenementRepository : BaseRepository<Evenement, Guid>, IEvenementRepository
    {
        public EvenementRepository(ApplicationDbContext dbContext) : base(dbContext) 
        { 
        }

        public virtual void Update(Evenement evenementToUpdate)
        {
            var originalEntity = GetById(evenementToUpdate.Id);

            if (!string.IsNullOrWhiteSpace(evenementToUpdate.Name)) originalEntity.Name = evenementToUpdate.Name;
            if (evenementToUpdate.StartDateAndTime != default) originalEntity.StartDateAndTime = evenementToUpdate.StartDateAndTime;
            if (evenementToUpdate.EndDateAndTime != default) originalEntity.EndDateAndTime = evenementToUpdate.EndDateAndTime;
            if (!string.IsNullOrWhiteSpace(evenementToUpdate.Description)) originalEntity.Description = evenementToUpdate.Description;

            dbSet.Update(originalEntity);
        }
    }

}
