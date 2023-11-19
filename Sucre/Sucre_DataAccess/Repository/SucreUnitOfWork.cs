using Microsoft.Extensions.Logging;
using Sucre_Core.LoggerExternal;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository
{
    public class SucreUnitOfWork : ISucreUnitOfWork
    {
        private ApplicationDbContext _dbSucre;
        private readonly IDbSucreAsPaz _repoSucreAsPaz;
        private readonly IDbSucreCanal _repoSucreCanal;
        private readonly IDbSucreCex _repoSucreCex;
        private readonly IDbSucreEnergy _repoSucreEnergy;
        private readonly IDbSucreParameterType _repoSucreParameterType;
        private readonly IDbSucrePoint _repoSucrePoint;
        private readonly ILogger<SucreUnitOfWork> _log;

        public SucreUnitOfWork(ApplicationDbContext dbSucre,
                            IDbSucreAsPaz repoSucreAsPaz,
                            IDbSucreCanal repoSucreCanal,
                            IDbSucreCex repoSucreCex,
                            IDbSucreEnergy repoSucreEnergy,
                            IDbSucreParameterType repoSucreParameterType,
                            IDbSucrePoint repoSucrePoint,
                            ILogger<SucreUnitOfWork> log)
        {
            _dbSucre = dbSucre;
            _repoSucreAsPaz = repoSucreAsPaz;
            _repoSucreCanal = repoSucreCanal;
            _repoSucreCex = repoSucreCex;
            _repoSucreEnergy = repoSucreEnergy;
            _repoSucreParameterType = repoSucreParameterType;
            _repoSucrePoint = repoSucrePoint ;
            _log = log;
            _log.LogInformation("SucreUnitOfWork use");
            LoggerExternal.LoggerEx.Information("*->SucreUnitOfWork use");
        }

        public IDbSucreAsPaz repoSucreAsPaz => _repoSucreAsPaz;

        public IDbSucreCanal repoSucreCanal => _repoSucreCanal;

        public IDbSucreCex repoSucreCex => _repoSucreCex;

        public IDbSucreEnergy repoSucreEnergy
        {
            get { return _repoSucreEnergy; }
        }
        public IDbSucreParameterType repoSucreParameterType
        {
            get { return _repoSucreParameterType; }
        }

        public IDbSucrePoint repoSucrePoint
        {
            get { return _repoSucrePoint;}
        }

        public void Commit() => _dbSucre.SaveChanges();
        public async Task CommitAsync() => await _dbSucre.SaveChangesAsync();

        private bool disposed = false;
        public virtual void Dispose(bool disposing) 
        { 
            if (!this.disposed)
            {
                if (disposing) 
                    _dbSucre.Dispose();
                this.disposed = true;
            }

        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);            
        }
    }
}
