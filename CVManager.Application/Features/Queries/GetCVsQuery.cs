using CVManager.Application.Common.Response;
using CVManager.Application.Features.ViewModels;
using CVManager.Application.Interfaces;

namespace CVManager.Application.Features.Queries
{
    public class GetCVsQuery : IRequest<ResponseVM<List<CVModel>>>
    {

    }
    public class GGetCVsQueryHandler : IRequestHandler<GetCVsQuery, ResponseVM<List<CVModel>>>
    {
        private readonly ICVManagerService _CVManagerService;
        public GGetCVsQueryHandler(ICVManagerService CVManagerService)
        {
            _CVManagerService = CVManagerService;
        }

        public async Task<ResponseVM<List<CVModel>>> Handle(GetCVsQuery request, CancellationToken cancellationToken)
        {
            return await _CVManagerService.GetCVs();
        }
    }
}
