using EducationPortal.Application.Abstractions.Services;
using EducationPortal.Application.Dtos.User;
using MediatR;


namespace EducationPortal.Application.Features.UserAuth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        readonly IUserService _userServie;

        public RegisterUserCommandHandler(IUserService userServie)
        {
            _userServie = userServie;
        }
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            RegisterUserResponseDto response = await _userServie.CreateAsync(new()
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username,
            //  ProfilePhoto = request.ProfilePhoto
            });
            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };
        }
    }
}
