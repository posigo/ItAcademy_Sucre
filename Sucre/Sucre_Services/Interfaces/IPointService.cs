using Sucre_Core.DTOs;

namespace Sucre_Services.Interfaces
{
    public interface IPointService
    {
        Task<bool> UpsertCanalToPoint(int Id, int IdCannale, bool upsert = false);
        Task<bool> DeletePointAsync(PointDto pointDto);
        Task<bool> DeletePointByIdAsync(int Id);        
        Task<PointDto> GetPointByIdAsync(int Id);
        Task<IEnumerable<PointTableDto>> GetListPointsByStrAsync();
        Task<PointTableDto> GetPointByIdStrAsync(int Id);
        Task<PointCanalesDto> GetPointCanalesAsync(int id);
        List<PointShortNameDto> GetListPointsForId(
            List<int> listIds = null,
            bool tEqual = false,
            bool paramName = true);
        Task<IEnumerable<PointDto>> GetListPointsAsync();
        Task<bool> UpsertPointAsync(PointDto pointDto);
    }
}
