using CVManager.Application.Common.Response;
using CVManager.Application.Features.ViewModels;

namespace CVManager.Application.Interfaces
{
    public interface ICVManagerService
    {
        Task<ResponseVM<CVModel>> GetCVDetailsById(int cvId);
        Task<ResponseVM<List<CVModel>>> GetCVs();
        Task<ResponseVM> AddCV(CVRequestModel CVRequest);
        Task<bool> UpdateCV(CVRequestModel CVUpdateRequest);
        Task<bool> DeleteCV(int cvId);
    }
}
