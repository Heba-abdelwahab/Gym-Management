using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Domain.ValueObjects;
using Services.Abstractions;
using Shared;

namespace Services;

internal sealed class TraineeService : ITraineeService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userServices;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public TraineeService(
        IAuthenticationService authenticationService,
        IUnitOfWork unitOfWork,
        IUserService userServices,
        IMapper mapper,
        ITokenService tokenService)
    {
        _authenticationService = authenticationService;
        _unitOfWork = unitOfWork;
        _userServices = userServices;
        _mapper = mapper;
        _tokenService = tokenService;

    }
    public async Task<AuthTraineeResultDto> CreateTraineeAsync(RegisterTraineeDto request)
    {
        var registerUser = new RegisterUserDto
          (request.FirstName, request.LastName, request.UserName, request.Email, request.Password, Roles.Trainee);

        var authResult = await _authenticationService.RegisterUserAsync(registerUser);


        var trainee = new Trainee
        {
            AppUserId = authResult.AppUserId,
            Address = _mapper.Map<Address>(request.Address),
            Weight = request?.Weight,
            ReasonForJoining = request?.ReasonForJoining!,
            DateOfBirth = request?.DateOfBirth
        };

        _unitOfWork.GetRepositories<Trainee, int>().Insert(trainee);


        if (await _unitOfWork.CompleteSaveAsync())
        {

            var coachClaims = _tokenService.GenerateAuthClaims(
                    trainee.Id, registerUser.UserName,
                     registerUser.Email, registerUser.Role);

            return new AuthTraineeResultDto(
                authResult.UserName,
                _tokenService.GenerateAccessToken(coachClaims));

        }

        return null!;


    }
}
