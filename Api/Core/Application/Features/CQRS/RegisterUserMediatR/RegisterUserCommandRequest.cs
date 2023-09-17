using Api.Core.Application.Interfaces;
using Api.Core.Domain;
using MediatR;

namespace Api.Core.Application.Features.CQRS.RegisterUserMediatR
{
    public class RegisterUserCommandRequest : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest>
    {
        private readonly IRepository<AppUser> _appuseRepository;

        public RegisterUserCommandHandler(IRepository<AppUser> appuseRepository)
        {
            _appuseRepository = appuseRepository;
        }

        public async Task Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _appuseRepository.CreateAsync(new()
            {
                Username = request.Username,
                Password = request.Password,
                AppRoleId = 2,
            });
        }
    }
}
