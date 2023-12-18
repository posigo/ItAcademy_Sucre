using LinqKit;
using Microsoft.Extensions.Configuration;
using Sucre_Core.DTOs;
using Sucre_Core.LoggerExternal;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Mappers;
using Sucre_Services.Interfaces;
using Sucre_Utility;
using System.Linq.Expressions;

namespace Sucre_Services
{
    public class CanalService: ICanalService
    {
        private readonly IConfiguration _configuration;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;
        private readonly AsPazMapper _asPazMapper;
        private readonly CanalMapper _canalMapper;
        private readonly ParameterTypeMapper _parameterTypeMapper;
        private readonly PointMapper _pointMapper;        

        public CanalService(IConfiguration configuration,
            ISucreUnitOfWork sucreUnitOfWork,
            AsPazMapper asPazMapper,
            CanalMapper canalMapper,
            ParameterTypeMapper parameterTypeMapper,
            PointMapper pointMapper)
        {
            _configuration = configuration;
            _sucreUnitOfWork = sucreUnitOfWork;
            _asPazMapper = asPazMapper;
            _canalMapper = canalMapper;
            _parameterTypeMapper = parameterTypeMapper;
            _pointMapper = pointMapper;
        }

        public async Task<CanalDto> GetCannaleByIdAsync(int Id)
        {
            try
            {
                var cannaleDb = await _sucreUnitOfWork.repoSucreCanal.FindAsync(Id);
                return (cannaleDb != null)
                    ? _canalMapper.CanalToCanalDto(cannaleDb)
                    : null;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CanalService-> {nameof(GetCannaleByIdAsync)}");
            }
            return null;
        }

        public async Task<IEnumerable<CanalDto>> GetListCannalesAsync()
        {
            try
            {
                var cannalesDb = await _sucreUnitOfWork
                    .repoSucreCanal
                    .GetAllAsync();
                IEnumerable<CanalDto> cannalesDto = cannalesDb
                    .Select(cannale => _canalMapper.CanalToCanalDto(cannale));
                return cannalesDto;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"!!!-->Error, CexService->  {nameof(GetListCannalesAsync)}");
            }
            return null;
        }

        public async Task<CannaleFullDto> GetCannaleByIdFullAsync(int Id)
        {
            try
            {
                CannaleFullDto cannaleFullDto = new CannaleFullDto();
                cannaleFullDto = (await GelListCannalesFullAsync(Id)).ToList()[0];


                return cannaleFullDto;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CanalService-> {nameof(GetCannaleByIdFullAsync)}");
            }
            return null;
        }

        /// <summary>
        /// Get a channale and assigned metering points
        /// </summary>
        /// <param name="Id">Id channale</param>       
        /// <returns></returns>
        public async Task<ChannalePointsDto> GetChannalePointesAsync(int Id)
        {
            try
            {
                var channaleDb = await _sucreUnitOfWork.repoSucreCanal
                    .FirstOrDefaultAsync(
                    filter: channale => channale.Id == Id,
                    includeProperties: $"{WC.PointsName}");
                
                if (channaleDb == null) { return null; }

                ChannalePointsDto channalePointsDto = new ChannalePointsDto(); 
                channalePointsDto.ChannaleDto = _canalMapper.CanalToCanalDto(channaleDb);
                if (channaleDb.Points.Count == 0)
                {
                    channalePointsDto.PointsDto = new HashSet<PointDto>();
                }
                else
                {
                    channalePointsDto.PointsDto = channaleDb.Points
                        .Select(point => _pointMapper.PointToPointDto(point));
                }
                                
                return channalePointsDto;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, "!!!->Error when getting a channale with points");
                return null;
            }
        }

        public async Task<IEnumerable<CannaleFullDto>> GelListCannalesFullAsync(int? Id)
        {
            try
            {
                Expression<Func<Canal, bool>> epFilter = null;
                if (Id != null && Id.Value != 0)
                {
                    epFilter = item => item.Id == Id.Value;
                }
                var cannalesDb = await _sucreUnitOfWork.repoSucreCanal
                    .GetAllAsync(
                        filter: epFilter,
                        includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");

                //var cannalesFullDto =
                //    new HashSet<CannaleFullDto>();

                //var cannalesFullDto = cannalesDb
                //    .Select(cannale => new CannaleFullDto()
                //    {
                //        CannaleDto = _canalMapper.CanalToCanalDto(cannale),
                //        ParameterTypeDto = _parameterTypeMapper
                //            .ParameterToParameterDto(cannale.ParameterType),
                //        AsPazDto = _canalMapper.AsPazToAsPazDto(cannale.AsPaz)


                //    });

                //IEnumerable<CannaleFullDto> cannalesFullDto =  
                //    new HashSet<CannaleFullDto>();
                var cannalesFullDto =
                    new HashSet<CannaleFullDto>();
                foreach (var cannale in cannalesDb)
                {
                    CannaleFullDto cannaleFullDto = new CannaleFullDto();
                    cannaleFullDto.CannaleDto = _canalMapper
                        .CanalToCanalDto(cannale);
                    cannaleFullDto.ParameterTypeDto = _parameterTypeMapper
                        .ParameterToParameterDto(cannale.ParameterType);
                    if (cannale.AsPaz != null)
                    {
                        cannaleFullDto.AsPazDto = _asPazMapper
                            .AsPazToAsPazDto(cannale.AsPaz);
                    }
                    else
                    {
                        cannaleFullDto.AsPazDto = null;
                    }
                    cannalesFullDto.Add(cannaleFullDto);
                    //sdf.Add(cannaleFullDto);
                }

                return cannalesFullDto;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CanalService->{nameof(GelListCannalesFullAsync)}");
            }            
            return null;
        }
        
        public List<CanalShortNameDto> GetListCanalesForId(
            List<int> listIds = null, 
            bool tEqual = false,
            bool paramName = false)
        {
            Expression<Func<Canal, bool>> epFilter = null;
            if (listIds.Count != 0)
            {
                bool begId = true;
                foreach (var idCanale in listIds)
                {
                    if (begId)
                    {
                        if (!tEqual)
                            epFilter = item => item.Id != idCanale;
                        else
                            epFilter = item => item.Id == idCanale;
                        begId = false;
                    }
                    else
                    {
                        if (!tEqual)
                            epFilter = epFilter.And(item => item.Id != idCanale);
                        else
                            epFilter = epFilter.And(item => item.Id == idCanale);
                    }
                }
            };
            string includeValue = (paramName) ? WC.ParameterTypeName : null;
            var cannalesDb = _sucreUnitOfWork.repoSucreCanal.GetAll(
                filter: epFilter,
                includeProperties: ((paramName) ? WC.ParameterTypeName : null),
                isTracking: false);
            List<CanalShortNameDto> cannalesShortNameDto = cannalesDb
                .Select(canalDb => new CanalShortNameDto
                {
                    Id = canalDb.Id,
                    Name = canalDb.Name,
                    ParameterTypeId = canalDb.ParameterTypeId,
                    ParameterTypeName = ((paramName)
                        ? $"{canalDb.ParameterType.Mnemo},{canalDb.ParameterType.UnitMeas}"
                        : string.Empty)
                }).ToList();
            return cannalesShortNameDto;
        }

        public async Task<bool> UpsertCanalAsync(CanalDto canalDto)
        {
            try
            {
                if (canalDto == null) { return false; }
                Canal canal = _canalMapper.CanalDtoToCanal(canalDto);
                if (canalDto.Id == null || canalDto.Id == 0)
                {
                    await _sucreUnitOfWork.repoSucreCanal.AddAsync(canal);
                }
                else
                {
                    //energy.Id = energyDto.Id;

                    await _sucreUnitOfWork.repoSucreCanal.Patch(canal.Id, new List<PatchTdo>()
                        {
                            new() {PropertyName = nameof(canal.Name),PropertyValue = canal.Name},
                            new() {PropertyName = nameof(canal.Description), PropertyValue = canal.Description},
                            new() {PropertyName = nameof(canal.ParameterTypeId),PropertyValue = canal.ParameterTypeId},
                            new() {PropertyName = nameof(canal.Reader),PropertyValue = canal.Reader},
                            new() {PropertyName = nameof(canal.SourceType),PropertyValue = canal.SourceType},
                            new() {PropertyName = nameof(canal.AsPazEin),PropertyValue = canal.AsPazEin}
                        });

                    //await _sucreUnitOfWork.repoSucrePoint .UpdateAsync(point);
                }
                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"!!!-->Error, PointService->   {nameof(UpsertCanalAsync)}");
            }

            return false;
        }

        /// <summary>
        /// Remove/Add menering point in channale
        /// </summary>
        /// <param name="Id">Id channale</param>
        /// <param name="IdPoint">Id point</param>
        /// <param name="upsert">false-Remove/true-Add</param>
        /// <returns>result (boolean)</returns>
        public async Task<bool> UpsertPointToCanal(int Id, int IdPoint, bool upsert = false)
        {
            try
            {
                Canal canalDb = await _sucreUnitOfWork
                    .repoSucreCanal
                    .FirstOrDefaultAsync(
                        filter: item => item.Id == Id,
                        includeProperties: WC.PointsName);

                Point point;
                if (!upsert)
                {
                    point = canalDb.Points.FirstOrDefault(item => item.Id == IdPoint);
                    canalDb.Points.Remove(point);
                }
                else
                {
                    Point addPointDb = await _sucreUnitOfWork
                        .repoSucrePoint
                        .FirstOrDefaultAsync(
                            filter: item => item.Id == IdPoint,
                            includeProperties: $"{WC.EnergyName},{WC.CexName}");
                    canalDb.Points.Add(addPointDb);                    
                }

                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CanalService->{nameof(UpsertPointToCanal)}");
            }
            return false;

        }


        public async Task<bool> DeleteChannaleAsync(CanalDto    channaleDto)
        {
            try
            {
                if (channaleDto == null) return false;
                Canal channale = new Canal();
                channale = _canalMapper.CanalDtoToCanal(channaleDto);
                await _sucreUnitOfWork.repoSucreCanal.RemoveAsync(channale);
                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CanalService->{nameof(DeleteChannaleAsync)}");
            }
            return false;
        }

        public async Task<bool> DeleteChannaleByIdAsync(int Id)
        {
            try
            {
                if (Id == 0) return false;
                await _sucreUnitOfWork.repoSucreCanal.RemoveByIdAsync(Id);
                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CanalService-> {nameof(DeleteChannaleByIdAsync)}");
            }
            return false;
        }

    }
}
