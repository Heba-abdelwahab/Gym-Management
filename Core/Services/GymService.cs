using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GymService: IGymService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GymService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task RequestAddGym (GymDto gymDto)
        {
            Gym gym =  mapper.Map<Gym>(gymDto);
            gym.Media = "f";
            unitOfWork.GetRepositories<Gym,int>().Insert(gym);
            await unitOfWork.CompleteSaveAsync();
        }
    }
}
