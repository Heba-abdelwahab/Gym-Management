using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;
using Services.Abstractions;
using Services.Specifications;
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

    public async Task<bool> AssignCoachToTrainee(AssignCoachToTraineeDto assignCoachToTrainee)
    {
        Trainee trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(assignCoachToTrainee.TraineeId);
        Coach coach = await _unitOfWork.GetRepositories<Coach, int>().GetByIdAsync(assignCoachToTrainee.CoachId);
        GymCoach gymCoach = await _unitOfWork.GetRepositories<GymCoach, int>().GetByIdWithSpecAsync(new GymCoachesSpec(assignCoachToTrainee.CoachId));
        if (trainee == null)
        {
            throw new TraineeNotFoundException(assignCoachToTrainee.TraineeId);
        }
        if (coach == null)
        {
            throw new CoeachesNotFoundException(assignCoachToTrainee.CoachId);
        }
        if (coach.CurrentCapcity < gymCoach.Capcity) { 
        trainee.CoachId = assignCoachToTrainee.CoachId;
            coach.CurrentCapcity++;

        _unitOfWork.GetRepositories<Trainee, int>().Update(trainee);
            _unitOfWork.GetRepositories<Coach, int>().Update(coach);

        }
        var result = await _unitOfWork.CompleteSaveAsync();

        return result;
        


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

    public async Task<List<TraineeToReturnDto>> GetTrineesByGem(int gymId)
    {
        var Trainees = await _unitOfWork.GetRepositories<Trainee, int>().
            GetAllWithSpecAsync(new GetTraineeByGemSpec(gymId));
        if (!Trainees.Any())
        {
            throw new GymNotFoundException(gymId);
        }
        var result = _mapper.Map<List<TraineeToReturnDto>>(Trainees);

        return result;
    }

}
