using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Application.UseCases.V1.Commands.Account
{
    public sealed class UpdateUserProfileCommandHandler : ICommandHandler<Command.UpdateUserCommand>
    {

        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IRepositoryBase<PawFund.Domain.Entities.Account, Guid> _accountRepository;
        private readonly IMediaService _mediaService;

        public UpdateUserProfileCommandHandler(IEFUnitOfWork efUnitOfWork, IRepositoryBase<Domain.Entities.Account, Guid> accountRepository, IMediaService mediaService)
        {
            _efUnitOfWork = efUnitOfWork;
            _accountRepository = accountRepository;
            _mediaService = mediaService;
        }

        public async Task<Result> Handle(Command.UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.FindByIdAsync(request.ID);
            if (user == null)
            {
                throw new UserException.UserNotFoundException((Guid)request.ID);
            }

            //Upload Avatar File to cloud
            var media = await _mediaService.UploadImage($"avatar_{request.ID}", request.AvatarFile);

            user.UpdateProfileUser(request.FirstName, request.LastName, media.ImageUrl, false); 
            _accountRepository.Update(user);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            //Return result
            return Result.Success(new Success(MessagesList.UserUpdateProfileSuccess.GetMessage().Code, MessagesList.UserUpdateProfileSuccess.GetMessage().Message));
        }

    }
}
