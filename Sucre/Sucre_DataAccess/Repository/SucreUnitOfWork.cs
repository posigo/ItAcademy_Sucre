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
        private IDbSucreAsPaz _repoSukreAsPaz;
        private IDbSucreCanal _repoSukreCanal;
        private IDbSucreCex _repoSukreCex;
        private IDbSucreEnergy _repoSukreEnergy;
        private IDbSucreParameterType _repoSukreParameterType;
        private IDbSucrePoint _repoSukrePoint;

        public SucreUnitOfWork(ApplicationDbContext dbSucre,
                            IDbSucreAsPaz repoSukreAsPaz,
                            IDbSucreCanal repoSukreCanal,
                            IDbSucreCex repoSukreCex,
                            IDbSucreEnergy repoSukreEnergy,
                            IDbSucreParameterType repoSucreParameterType,
                            IDbSucrePoint repoSucrePoint)
        {
            _dbSucre = dbSucre;
            _repoSukreAsPaz = repoSukreAsPaz;
            _repoSukreCanal = repoSukreCanal;
            _repoSukreCex = repoSukreCex;
            _repoSukreEnergy = repoSukreEnergy;
            _repoSukreParameterType = repoSukreParameterType;
            _repoSukrePoint = repoSukrePoint;
        }

        public IDbSucreAsPaz repoSukreAsPaz => _repoSukreAsPaz;

        public IDbSucreCanal repoSukreCanal => _repoSukreCanal;

        public IDbSucreCex repoSukreEnergyCex => _repoSukreCex;

        public IDbSucreEnergy repoSukreEnergy
        {
            get { return _repoSukreEnergy; }
        }        
        public IDbSucreParameterType repoSukreParameterType => _repoSukreParameterType;

        public IDbSucrePoint repoSukrePoint => _repoSukrePoint;

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
