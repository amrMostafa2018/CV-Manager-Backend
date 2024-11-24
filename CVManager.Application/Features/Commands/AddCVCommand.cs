using CVManager.Application.Common.Response;
using CVManager.Application.Features.ViewModels;
using CVManager.Application.Interfaces;

namespace CVManager.Application.Features.Commands
{
    public class AddCVCommand : IRequest<ResponseVM>
    {
        public CVRequestModel CVRequest { get; set; }
    }
    public class AddCVCommandHandler : IRequestHandler<AddCVCommand, ResponseVM>
    {

        private readonly ICVManagerService _CVManagerService;

        public AddCVCommandHandler(ICVManagerService CVManagerService)
        {
            _CVManagerService = CVManagerService;
        }
        public async Task<ResponseVM> Handle(AddCVCommand request, CancellationToken cancellationToken)
        {
            return await _CVManagerService.AddCV(request.CVRequest);
        }
    }
}
