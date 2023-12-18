using Microsoft.AspNetCore.Mvc.Rendering;
using Sucre_Core.DTOs;

namespace Sucre_Services.Interfaces
{
    public interface ICexService
    {
        Task<bool> DeleteCexAsync(CexDto cexDto);
        Task<bool> DeleteCexByIdAsync(int Id);        
        Task<CexDto> GetCexByIdAsync(int Id);
        IEnumerable<SelectListItem> GetCexSelectList(bool addFirstSelect = true,
            string valueFirstSelect = null);
        Task<IEnumerable<CexDto>> GetListCexsAsync();
        Task<bool> UpsertCexAsync(CexDto cexDto);
    }
}
