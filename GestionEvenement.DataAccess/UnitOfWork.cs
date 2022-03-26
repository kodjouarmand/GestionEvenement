using GestionEvenement.Domain.Contexts;
using GestionEvenement.DataAccess.Repositories.Contracts;
using GestionEvenement.DataAccess.Repositories;

namespace GestionEvenement.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IEvenementRepository Evenement { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Evenement = new EvenementRepository(_dbContext);
        }
        
        public void Dispose() => _dbContext.Dispose();

        public void Save() => _dbContext.SaveChanges();
    }

}
