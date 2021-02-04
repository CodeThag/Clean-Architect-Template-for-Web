using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            var userId = _currentUserService.GetUserId() ?? string.Empty;

            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = _currentUserService.GetUserName();
            }

            _logger.LogInformation("WBC Template NG Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);

            return Task.FromResult(Unit.Value);
        }
    }
}
