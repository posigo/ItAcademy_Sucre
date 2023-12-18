using Sucre_Core.DTOs;

namespace Sucre_Services.Interfaces
{
    public interface ICanalService
    {
        Task<bool> DeleteChannaleByIdAsync(int Id);
        Task<CanalDto> GetCannaleByIdAsync(int Id);
        Task<CannaleFullDto> GetCannaleByIdFullAsync(int Id);
        Task<ChannalePointsDto> GetChannalePointesAsync(int Id);
        Task<IEnumerable<CanalDto>> GetListCannalesAsync();
        Task<IEnumerable<CannaleFullDto>> GelListCannalesFullAsync(int? Id);
        List<CanalShortNameDto> GetListCanalesForId(
            List<int> listIds = null,
            bool tEqual = false,
            bool paramName = false);
        Task<bool> UpsertCanalAsync(CanalDto canalDto);
        Task<bool> UpsertPointToCanal(int Id, int IdPoint, bool upsert = false);
        
    }
}
