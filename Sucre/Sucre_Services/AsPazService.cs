using Microsoft.Extensions.Configuration;
using Sucre_Core.DTOs;
using Sucre_Core.LoggerExternal;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Mappers;
using Sucre_Services.Interfaces;
using Sucre_Utility;

namespace Sucre_Services
{
    public class AsPazService: IAsPazService
    {
        private readonly IConfiguration _configuration;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;
        private readonly AsPazMapper _asPazMapper;        

        public AsPazService(
            IConfiguration configuration,
            ISucreUnitOfWork sucreUnitOfWork,
            AsPazMapper asPazMapper)
        {
            _configuration = configuration;
            _sucreUnitOfWork = sucreUnitOfWork;
            _asPazMapper = asPazMapper;
        }

        public async Task<int> CheckAndDelByChanaleIdAsync(int IdCanale)
        {
            try
            {
                AsPaz asPaz = await _sucreUnitOfWork.repoSucreAsPaz
                    .FirstOrDefaultAsync(item => item.CanalId == IdCanale);
                if (asPaz != null)
                {
                    await _sucreUnitOfWork.repoSucreAsPaz
                        .RemoveByIdAsync(asPaz.Id);
                    return 2;
                }
                else 
                    return 1;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, AsPazService-> {nameof(CheckAndDelByChanaleIdAsync)}");
                
            }            
            return 0; 
        }
        public async Task<bool> DeleteAsPazAsync(AsPazDto asPazDto)
        {
            try
            {
                if (asPazDto == null) return false;
                
                await _sucreUnitOfWork.repoSucreAsPaz
                    .RemoveAsync(_asPazMapper.AsPazDtoToAsPaz(asPazDto));
                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, AsPazService->{nameof(DeleteAsPazAsync)}");
            }
            return false;
        }

        public async Task<bool> DeleteAsPazByIdAsync(int Id)
        {
            try
            {
                if (Id == 0) return false;
                await _sucreUnitOfWork.repoSucreAsPaz.RemoveByIdAsync(Id);
                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, AsPazService-> {nameof(DeleteAsPazByIdAsync)}");
            }
            return false;
        }

        public async Task<AsPazDto> GetAsPazByIdAsync(int Id)
        {
            try
            {
                var asPazDb = await _sucreUnitOfWork.repoSucreAsPaz
                    .FindAsync(Id);// .FirstOrDefaultAsync(asPaz => asPaz.CanalId == Id);
                return (asPazDb != null)
                    ? _asPazMapper.AsPazToAsPazDto(asPazDb)
                    : null;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"*-->Error, AsPazService-> {nameof(GetAsPazByIdAsync)}");
            }
            return null;
        }



        //public async Task<IEnumerable<CannaleFullDto>> GelListCannalesFullAsync(int? Id)
        //{
        //    try
        //    {
        //        Expression<Func<Canal, bool>> epFilter = null;
        //        if (Id != null && Id.Value != 0)
        //        {
        //            epFilter = item => item.Id == Id.Value;
        //        }
        //        var cannalesDb = await _sucreUnitOfWork.repoSucreCanal
        //            .GetAllAsync(
        //                filter: epFilter,
        //                includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");

        //        //var cannalesFullDto =
        //        //    new HashSet<CannaleFullDto>();

        //        //var cannalesFullDto = cannalesDb
        //        //    .Select(cannale => new CannaleFullDto()
        //        //    {
        //        //        CannaleDto = _canalMapper.CanalToCanalDto(cannale),
        //        //        ParameterTypeDto = _parameterTypeMapper
        //        //            .ParameterToParameterDto(cannale.ParameterType),
        //        //        AsPazDto = _canalMapper.AsPazToAsPazDto(cannale.AsPaz)


        //        //    });

        //        //IEnumerable<CannaleFullDto> cannalesFullDto =  
        //        //    new HashSet<CannaleFullDto>();
        //        var cannalesFullDto =
        //            new HashSet<CannaleFullDto>();
        //        foreach (var cannale in cannalesDb)
        //        {
        //            CannaleFullDto cannaleFullDto = new CannaleFullDto();
        //            cannaleFullDto.CannaleDto = _canalMapper
        //                .CanalToCanalDto(cannale);
        //            cannaleFullDto.ParameterTypeDto = _parameterTypeMapper
        //                .ParameterToParameterDto(cannale.ParameterType);
        //            if (cannale.AsPaz != null)
        //            {
        //                cannaleFullDto.AsPazDto = _canalMapper
        //                    .AsPazToAsPazDto(cannale.AsPaz);
        //            }
        //            else
        //            {
        //                cannaleFullDto.AsPazDto = null;
        //            }
        //            cannalesFullDto.Add(cannaleFullDto);
        //            //sdf.Add(cannaleFullDto);
        //        }

        //        return cannalesFullDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerExternal.LoggerEx.Error(ex, $"*-->Error, CanalService->{nameof(GelListCannalesFullAsync)}");
        //    }            
        //    return null;
        //}
        //public List<CanalShortNameDto> GetListCanalesForId(
        //    List<int> listIds = null, 
        //    bool tEqual = false,
        //    bool paramName = false)
        //{
        //    Expression<Func<Canal, bool>> epFilter = null;
        //    if (listIds.Count != 0)
        //    {
        //        bool begId = true;
        //        foreach (var idCanale in listIds)
        //        {
        //            if (begId)
        //            {
        //                if (!tEqual)
        //                    epFilter = item => item.Id != idCanale;
        //                else
        //                    epFilter = item => item.Id == idCanale;
        //                begId = false;
        //            }
        //            else
        //            {
        //                if (!tEqual)
        //                    epFilter = epFilter.And(item => item.Id != idCanale);
        //                else
        //                    epFilter = epFilter.And(item => item.Id == idCanale);
        //            }
        //        }
        //    };
        //    string includeValue = (paramName) ? WC.ParameterTypeName : null;
        //    var cannalesDb = _sucreUnitOfWork.repoSucreCanal.GetAll(
        //        filter: epFilter,
        //        includeProperties: ((paramName) ? WC.ParameterTypeName : null),
        //        isTracking: false);
        //    List<CanalShortNameDto> cannalesShortNameDto = cannalesDb
        //        .Select(canalDb => new CanalShortNameDto
        //        {
        //            Id = canalDb.Id,
        //            Name = canalDb.Name,
        //            ParameterTypeId = canalDb.ParameterTypeId,
        //            ParameterTypeName = ((paramName)
        //                ? $"{canalDb.ParameterType.Mnemo},{canalDb.ParameterType.UnitMeas}"
        //                : string.Empty)
        //        }).ToList();
        //    return cannalesShortNameDto;
        //}






        ////public async Task<IEnumerable<PointTableDto>> GetListPointsByStrAsync()
        ////{
        ////    try
        ////    {
        ////        var pointsDb = await _sucreUnitOfWork
        ////            .repoSucrePoint
        ////            .GetAllAsync(includeProperties: $"{WC.EnergyName},{WC.CexName}");
        ////        IEnumerable<PointTableDto> pointsTableDto = pointsDb
        ////            .Select(point => new PointTableDto
        ////            {
        ////                PointDto = _pointMapper.PointToPointDto(point),
        ////                EnergyName = point.Energy.EnergyName,
        ////                CexName = WM.GetStringName(new string[]
        ////                {
        ////                    point.Cex.Management,
        ////                    point.Cex.CexName,
        ////                    point.Cex.Area,
        ////                    point.Cex.Device,
        ////                    point.Cex.Location
        ////                })
        ////            });
        ////        return pointsTableDto;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        LoggerExternal.LoggerEx.Error(ex, $"!!!-->Error, PointService->  {nameof(GetListPointsByStrAsync)}");
        ////    }
        ////    return null;
        ////}



        /////// <summary>
        /////// Get a metering point and assigned canales. Get canales not assigned point
        /////// </summary>
        /////// <param name="id">id metering point</param>       
        /////// <returns></returns>
        ////public async Task<PointCanalesDto> GetPointCanalesAsync(int id)
        ////{
        ////    try
        ////    {
        ////        var pointDb = await _sucreUnitOfWork.repoSucrePoint
        ////            .FirstOrDefaultAsync(
        ////            filter: item => item.Id == id,
        ////            includeProperties: WC.CanalsName);

        ////        //var appUserDb = _sucreUnitOfWork.repoSucreAppUser.FirstOrDefault(
        ////        //    filter: user => user.Id == id,
        ////        //    includeProperties: $"{WC.AppRolesName}",
        ////        //    isTracking: false);

        ////        if (pointDb == null) { return null; }

        ////        PointCanalesDto pointCanalesDto = new PointCanalesDto();
        ////        pointCanalesDto.PointDto = _pointMapper.PointToPointDto(pointDb);
        ////        if (pointDb.Canals.Count == 0)
        ////        {
        ////            pointCanalesDto.CanalesDto = new HashSet<CanalDto>();
        ////        }
        ////        else
        ////        {
        ////            pointCanalesDto.CanalesDto = pointDb.Canals
        ////                .Select(canal => _canalMapper.CanalToCanalDto(canal));
        ////        }
        ////        return pointCanalesDto;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        LoggerExternal.LoggerEx.Error(ex, "!!!->Error when getting a point with canales");
        ////        return null;
        ////    }
        ////}

        public async Task<AsPazChannaleDto> GetAsPazChannaleByIdAsync(int Id)
        {
            if (Id == 0) return null;
            var asPazCanaleDb = _sucreUnitOfWork.repoSucreAsPaz
                .FirstOrDefault(
                    filter: asPaz => asPaz.Id == Id,
                    includeProperties: $"{WC.CanalName}");
            var asPazCanaleDto = new AsPazChannaleDto()
            {
                AsPazDto = _asPazMapper.AsPazToAsPazDto(asPazCanaleDb),
                CanalName = asPazCanaleDb.Canal.Name
            };
            return asPazCanaleDto;
        }

        public async Task<AsPazChannaleDto> GetAsPazChannaleByIdCanAsync(int IdCanale)
        {
            if (IdCanale == 0) return null;
            var asPazCanaleDb = _sucreUnitOfWork.repoSucreAsPaz
                .FirstOrDefault(
                    filter: asPaz => asPaz.CanalId == IdCanale,
                    includeProperties: $"{WC.CanalName}");
            var asPazCanaleDto = new AsPazChannaleDto()
            {
                AsPazDto = _asPazMapper.AsPazToAsPazDto(asPazCanaleDb),
                CanalName = asPazCanaleDb.Canal.Name
            };
            return asPazCanaleDto;
        }

        public async Task<IEnumerable<AsPazChannaleDto>> GetListAsPasChannaleAsync()
        {
            try
            {
                var asPazsDb = await _sucreUnitOfWork
                    .repoSucreAsPaz
                    .GetAllAsync(includeProperties: $"{WC.CanalName}");
                IEnumerable<AsPazChannaleDto> asPazsChanaleDto = asPazsDb
                    .Select(asPaz => new AsPazChannaleDto
                    {
                        AsPazDto = _asPazMapper.AsPazToAsPazDto(asPaz),
                        CanalName = asPaz.Canal.Name
                    });
                return asPazsChanaleDto;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"!!!-->Error, AsPazService->  {nameof(GetListAsPasChannaleAsync)}");
            }
            return null;
        }

        public async Task<bool> UpsertAsPazAsync(AsPazDto asPazDto)
        {
            try
            {
                if (asPazDto == null) { return false; }
                AsPaz asPaz = _asPazMapper.AsPazDtoToAsPaz(asPazDto);

                if (asPaz.Id == null || asPaz.Id == 0)
                {
                    await _sucreUnitOfWork.repoSucreAsPaz.AddAsync(asPaz);
                }
                else
                {
                    await _sucreUnitOfWork.repoSucreAsPaz.Patch(asPaz.Id, new List<PatchTdo>()
                        {
                            new() {PropertyName = nameof(asPaz.CanalId),PropertyValue = asPaz.CanalId},
                            new() {PropertyName = nameof(asPaz.High), PropertyValue = asPaz.High},
                            new() {PropertyName = nameof(asPaz.Low),PropertyValue = asPaz.Low},
                            new() {PropertyName = nameof(asPaz.A_HighEin),PropertyValue = asPaz.A_HighEin},
                            new() {PropertyName = nameof(asPaz.A_HighType),PropertyValue = asPaz.A_HighType},
                            new() {PropertyName = nameof(asPaz.A_High),PropertyValue = asPaz.A_High},
                            new() {PropertyName = nameof(asPaz.W_HighEin),PropertyValue = asPaz.W_HighEin},
                            new() {PropertyName = nameof(asPaz.W_HighType),PropertyValue = asPaz.W_HighType},
                            new() {PropertyName = nameof(asPaz.W_High),PropertyValue = asPaz.W_High},
                            new() {PropertyName = nameof(asPaz.W_LowEin),PropertyValue = asPaz.W_LowEin},
                            new() {PropertyName = nameof(asPaz.W_LowType),PropertyValue = asPaz.W_LowType},
                            new() {PropertyName = nameof(asPaz.W_Low),PropertyValue = asPaz.W_Low},
                            new() {PropertyName = nameof(asPaz.A_LowEin),PropertyValue = asPaz.A_LowEin},
                            new() {PropertyName = nameof(asPaz.A_LowType),PropertyValue = asPaz.A_LowType},
                            new() {PropertyName = nameof(asPaz.A_Low),PropertyValue = asPaz.A_Low}
                        });

                    //await _sucreUnitOfWork.repoSucrePoint .UpdateAsync(point);
                }
                await _sucreUnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LoggerExternal.LoggerEx.Error(ex, $"!!!-->Error, AsPazService->   {nameof(UpsertAsPazAsync)}");
            }

            return false;
        }

    }
}
