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
        IDbSucreAsPaz repoSukreAsPaz
        { get; }
        IDbSucreCanal repoSukreCanal
        { get; }
        IDbSucreCex repoSukreEnergyCex
        { get; }
        IDbSucreEnergy repoSukreEnergy
        { get; }        
        IDbSucreParameterType repoSukreParameterType
        { get; }
        IDbSucrePoint repoSukrePoint
        { get; }

        void Commit();
        Task CommitAsync();
    }
}
