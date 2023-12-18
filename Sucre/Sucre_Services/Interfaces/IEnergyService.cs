using Microsoft.AspNetCore.Mvc.Rendering;
using Sucre_Core.DTOs;

namespace Sucre_Services.Interfaces
{
    public interface IEnergyService
    {
        Task<bool> DeleteEnergyTypeAsync(EnergyDto energyDto);
        Task<bool> DeleteEnergyTypeByIdAsync(int Id);
        Task<EnergyDto> GetEnergyTypeByIdAsync(int Id);
        Task<IEnumerable<EnergyDto>> GetListEnergyTypesAsync();
        IEnumerable<SelectListItem> GetEnergySelectList(bool addFirstSelect = true, 
            string valueFirstSelect = null);
        Task<bool> UpsertEnergyTypeAsync(EnergyDto energyDto);
    }
}
