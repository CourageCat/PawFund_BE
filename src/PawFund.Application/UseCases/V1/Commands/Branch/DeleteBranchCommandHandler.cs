using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.Services.Branchs;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Domain.Entities;
using PawFund.Contract.Abstractions.Services;

namespace PawFund.Application.UseCases.V1.Commands.Branch;
public sealed class DeleteBranchCommandHandler : ICommandHandler<Command.DeleteBranchCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
    private readonly IRepositoryBase<Domain.Entities.Event, Guid> _eventRepository;
    private readonly IRepositoryBase<Domain.Entities.EventActivity, Guid> _eventActivityRepository;
    private readonly IRepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid> _volunteerApplicationDetailRepository;

    private readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
    private readonly IRepositoryBase<Domain.Entities.AdoptPetApplication, Guid> _adoptPetApplicationRepository;
    private readonly IRepositoryBase<Domain.Entities.HistoryCat, Guid> _historyCatRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IMediaService _mediaService;

    public DeleteBranchCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IRepositoryBase<Domain.Entities.EventActivity, Guid> eventActivityRepository, IRepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid> volunteerApplicationDetailRepository, IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IRepositoryBase<AdoptPetApplication, Guid> adoptPetApplicationRepository, IRepositoryBase<Domain.Entities.HistoryCat, Guid> historyCatRepository, IEFUnitOfWork efUnitOfWork, IMediaService mediaService)
    {
        _branchRepository = branchRepository;
        _eventRepository = eventRepository;
        _eventActivityRepository = eventActivityRepository;
        _volunteerApplicationDetailRepository = volunteerApplicationDetailRepository;
        _catRepository = catRepository;
        _adoptPetApplicationRepository = adoptPetApplicationRepository;
        _historyCatRepository = historyCatRepository;
        _efUnitOfWork = efUnitOfWork;
        _mediaService = mediaService;
    }

    public async Task<Result> Handle(Command.DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        //Find Branch
        var branchFound = await _branchRepository.FindByIdAsync(request.Id);
        if (branchFound == null || branchFound.IsDeleted == true)
        {
            throw new BranchException.BranchNotFoundException(request.Id);
        }
        //Delete Image in Cloudinary
        await _mediaService.DeleteFileAsync(branchFound.PublicImageId);
        //Update Status is IsDeleted of Branch to true
        branchFound.UpdateBranch(branchFound.Name, branchFound.PhoneNumberOfBranch, branchFound.EmailOfBranch, branchFound.Description, branchFound.NumberHome, branchFound.StreetName, branchFound.Ward, branchFound.District, branchFound.Province, branchFound.PostalCode, branchFound.ImageUrl, branchFound.PublicImageId, branchFound.AccountId, (DateTime)branchFound.CreatedDate, DateTime.Now, true);
        _branchRepository.Update(branchFound);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        //Update Status is IsDeleted of each event of Branch to true
        foreach (var eventItem in branchFound.Events)
        {
            eventItem.IsDeleted = true;
            _eventRepository.Update(eventItem);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            //Update Status is IsDeleted of each eventAcitivty of each event of Branch to true
            foreach (var eventActivityItem in eventItem.Activities)
            {
                eventActivityItem.IsDeleted = true;
                _eventActivityRepository.Update(eventActivityItem);
                await _efUnitOfWork.SaveChangesAsync(cancellationToken);
                //Update Status is IsDeleted of each volunteerApplicationDetail of each eventAcitivty of each event of Branch to true
                foreach (var volunteerApplicationDetailItem in eventActivityItem.VolunteerApplicationDetails)
                {
                    volunteerApplicationDetailItem.IsDeleted = true;
                    _volunteerApplicationDetailRepository.Update(volunteerApplicationDetailItem);
                    await _efUnitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
        }
        //Update Status is IsDeleted of each cat of Branch to true
        foreach (var catItem in branchFound.Cats)
        {
            catItem.IsDeleted = true;
            _catRepository.Update(catItem);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            //Update Status is IsDeleted of each adoptApplication of each cat of Branch to true
            foreach (var applicationItem in catItem.AdoptPetApplications)
            {
                applicationItem.IsDeleted = true;
                _adoptPetApplicationRepository.Update(applicationItem);
                await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        //Return Result
        return Result.Success(new Success(MessagesList.BranchDeleteBranchSuccess.GetMessage().Code, MessagesList.BranchDeleteBranchSuccess.GetMessage().Message));
    }
}

