using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Sucre_Core.DTOs;
using Sucre_Core.LoggerExternal;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Mappers;
using Sucre_Services.Interfaces;

namespace Sucre_Services
{
    public class CexService: ICexService
    {
        private readonly IConfiguration _configuration;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;
        private readonly CexMapper _cexMapper;

        public CexService(IConfiguration configuration,
            ISucreUnitOfWork sucreUnitOfWork,
            CexMapper cexMapper)
        {
            _configuration = configuration;
            _sucreUnitOfWork = sucreUnitOfWork;
            _cexMapper = cexMapper;
        }

        public async Task<bool> DeleteCexAsync(CexDto cexDto)
        {
            try
            {
                if (cexDto == null) return false;                
                Cex cex = _cexMapper.CexDtoToCex(cexDto);
                await _sucreUnitOfWork.repoSucreCex.RemoveAsync(cex);
                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CexService->{nameof(DeleteCexAsync)}");
            }
            return false;
        }

        public async Task<bool> DeleteCexByIdAsync(int Id)
        {
            try
            {
                if (Id == 0) return false;
                await _sucreUnitOfWork.repoSucreCex.RemoveByIdAsync(Id);
                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CexService-> {nameof(DeleteCexByIdAsync)}");
            }
            return false;
        }

        public async Task<CexDto> GetCexByIdAsync(int Id)
        {
            try
            {
                var cexDb = await _sucreUnitOfWork.repoSucreCex.FindAsync(Id);                
                return (cexDb != null)
                    ? _cexMapper.CexToCexDto(cexDb)
                    : null;
            }
            catch(Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CexService-> {nameof(GetCexByIdAsync)}");
            }
            return null;
        }

        public async Task<IEnumerable<CexDto>> GetListCexsAsync()
        {
            try
            {
                var cexsDb = await _sucreUnitOfWork.repoSucreCex.GetAllAsync();                
                IEnumerable<CexDto> cexsDto = cexsDb
                    .Select(cex => _cexMapper.CexToCexDto(cex));
                return cexsDto;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"!!!-->Error, CexService->  {nameof(GetListCexsAsync)}");
            }
            return null;
        }

        public IEnumerable<SelectListItem> GetCexSelectList(bool addFirstSelect = true, 
            string valueFirstSelect = null)
        {
            return _sucreUnitOfWork.repoSucreCex.GetAllDropdownList(addFirstSelect, valueFirstSelect);
        }

        public async Task<bool> UpsertCexAsync(CexDto cexDto)
        {
            try
            {
                if (cexDto == null) { return false; }
                Cex cex = new Cex();
                cex = _cexMapper.CexDtoToCex(cexDto);                
                if (cexDto.Id == null || cexDto.Id == 0)
                {
                    await _sucreUnitOfWork.repoSucreCex.AddAsync(cex);
                }
                else
                {
                    //energy.Id = energyDto.Id;
                    await _sucreUnitOfWork.repoSucreCex.UpdateAsync(cex);
                }
                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch(Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"!!!-->Error, CexService->   {nameof(UpsertCexAsync)}");
            }
            
            return false;
        }

    }
}
