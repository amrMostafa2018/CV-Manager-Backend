using CVManager.Application.Common.Response;
using CVManager.Application.Interfaces;

namespace CVManager.Application.Features.Commands
{
    public class DeleteCVCommand : IRequest<ResponseVM>
    {
        public int Id { get; set; }
    }
    public class DeleteCVCommandHandler : IRequestHandler<DeleteCVCommand, ResponseVM>
    {

        private readonly ICVManagerService _CVManagerService;

        public DeleteCVCommandHandler(ICVManagerService CVManagerService)
        {
            _CVManagerService = CVManagerService;
        }
        public async Task<ResponseVM> Handle(DeleteCVCommand request, CancellationToken cancellationToken)
        {
            return new ResponseVM()
            {
                Data = await _CVManagerService.DeleteCV(request.Id),
            };
        }
    }
}
