using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository.IRepository
{
    public interface ISucreUnitOfWork: IDisposable
    {
        IDbSucreAsPaz repoSucreAsPaz
        { get; }
        IDbSucreCanal repoSucreCanal
        { get; }
        IDbSucreCex repoSucreCex
        { get; }
        IDbSucreEnergy repoSucreEnergy
        { get; }
        IDbSucreParameterType repoSucreParameterType
        { get; }
        IDbSucrePoint repoSucrePoint
        { get; }

        void Commit();
        Task CommitAsync();
    }
}
