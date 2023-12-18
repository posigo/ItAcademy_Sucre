using Sucre_Core.DTOs;

namespace Sucre_Services.Interfaces
{
    public interface IAsPazService
    {
        Task<int> CheckAndDelByChanaleIdAsync(int Id);
        Task<bool> DeleteAsPazAsync(AsPazDto asPazDto);
        Task<bool> DeleteAsPazByIdAsync(int Id);
        Task<AsPazDto> GetAsPazByIdAsync(int Id);
        Task<AsPazChannaleDto> GetAsPazChannaleByIdAsync(int Id);
        Task<AsPazChannaleDto> GetAsPazChannaleByIdCanAsync(int IdCanale);
        Task<IEnumerable<AsPazChannaleDto>> GetListAsPasChannaleAsync();
        Task<bool> UpsertAsPazAsync(AsPazDto asPazDto);
    }
}