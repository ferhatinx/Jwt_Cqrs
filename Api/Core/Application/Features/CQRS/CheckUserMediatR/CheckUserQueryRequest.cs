using Api.Core.Application.Interfaces;
using Api.Core.Domain;
using Api.Core.Dto;
using MediatR;
using System.Data;

namespace Api.Core.Application.Features.CQRS.CheckUserMediatR
{
    public class CheckUserQueryRequest : IRequest<CheckUserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class CheckUserHandler : IRequestHandler<CheckUserQueryRequest, CheckUserDto>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppRole> _appRoleRepository;
        public CheckUserHandler(IRepository<AppUser> appUserRepository, IRepository<AppRole> appRoleRepository)
        {
            _appUserRepository = appUserRepository;
            _appRoleRepository = appRoleRepository;
        }

        public async Task<CheckUserDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _appUserRepository.GetByFilter(x => x.Username == request.UserName && x.Password == request.Password);
            var checkuser = new CheckUserDto();
            if (user != null)
            {
                var role = await _appRoleRepository.GetByFilter(x => x.Id == user.AppRoleId);


                checkuser.UserName = request.UserName;
                checkuser.Role = role.Definition;
                checkuser.Id = user.Id;
                checkuser.isExist = true;
            }
            else
            {
                checkuser.isExist = false;
            }
            return checkuser;

        }
    }
}

