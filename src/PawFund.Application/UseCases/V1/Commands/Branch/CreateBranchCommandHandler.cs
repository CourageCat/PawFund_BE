using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Branchs;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using PawFund.Contract.Enumarations.MessagesList;
using Microsoft.AspNetCore.Http;
using PawFund.Contract.DTOs.MediaDTOs;

namespace PawFund.Application.UseCases.V1.Commands.Branch
{
    public sealed class CreateBranchCommandHandler : ICommandHandler<Command.CreateBranchCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
        private readonly IRepositoryBase<Domain.Entities.Account, Guid> _accountRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IConfiguration _configuration;
        private readonly IMediaService _mediaService;

        public CreateBranchCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IRepositoryBase<Domain.Entities.Account, Guid> accountRepository, IEFUnitOfWork efUnitOfWork, IPasswordHashService passwordHashService, IConfiguration configuration, IMediaService mediaService)
        {
            _branchRepository = branchRepository;
            _accountRepository = accountRepository;
            _efUnitOfWork = efUnitOfWork;
            _passwordHashService = passwordHashService;
            _configuration = configuration;
            _mediaService = mediaService;
        }

        public async Task<Result> Handle(Command.CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var staffAccountCreated = Domain.Entities.Account.CreateStaffAccount(_passwordHashService.HashPassword(_configuration["AccountStaffAssistant:Password"]), request.Name);
            _accountRepository.Add(staffAccountCreated);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            ImageDTO imageCreate = new ImageDTO();
            if (request.Image == null)
            {
                imageCreate.ImageUrl = "";
                imageCreate.PublicImageId = "";
            }
            else
            {
                var uploadImage = await _mediaService.UploadImagesAsync(new List<IFormFile>
            {
                request.Image,
            });
                imageCreate.ImageUrl = uploadImage[0].ImageUrl;
                imageCreate.PublicImageId = uploadImage[0].PublicImageId;
            }


            //Create Branch
            var branchCreated = Domain.Entities.Branch.CreateBranch(request.Name, request.PhoneNumberOfBranch, request.EmailOfBranch, request.Description, request.NumberHome, request.StreetName, request.Ward, request.District, request.Province, request.PostalCode, imageCreate.ImageUrl, imageCreate.PublicImageId, staffAccountCreated.Id, DateTime.Now, DateTime.Now, false);
            _branchRepository.Add(branchCreated);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);

            //Return Result
            return Result.Success(new Success(MessagesList.BranchCreateBranchSuccess.GetMessage().Code, MessagesList.BranchCreateBranchSuccess.GetMessage().Message));

        }
    }
}
