
using GestionEvenement.DataAccess.Repositories.Contracts;

namespace GestionEvenement.DataAccess
{
    public interface IUnitOfWork
    {
        IEvenementRepository Evenement { get; }

        void Save();
    }

}
