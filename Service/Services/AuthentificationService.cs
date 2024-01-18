using AutoMapper;
using Domain.Authentication;
using Domain.Models;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IAuthentificationRepository _authentificationRepository;
        private readonly IUnitOfWork _unitOfWork;


        public AuthentificationService(IAuthentificationRepository authentificationRepository, IUnitOfWork unitOfWork)
        {
            _authentificationRepository = authentificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserModel?> GetUserInfos(string userId)
        {
            return await _authentificationRepository.GetUserInfos(userId);
        }
    }
}
