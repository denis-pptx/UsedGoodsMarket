﻿using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Data.Initializers.Values;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Library.BLL.Services.Implementations;

namespace Item.BusinessLogic.Services.Implementations;

public class ItemService(
    IRepository<DataAccess.Models.Item> entityRepository, 
    IRepository<Status> _statusRepository,
    ICurrentUserService currentUserService,
    IMapper mapper) 
    : BaseService<DataAccess.Models.Item, ItemDto>(entityRepository, mapper), IItemService
{
    public async override Task<DataAccess.Models.Item> CreateAsync(ItemDto itemDto, CancellationToken token)
    {
        var item = _mapper.Map<ItemDto, DataAccess.Models.Item>(itemDto);
        item.CreatedAt = DateTime.UtcNow;

        var status = await _statusRepository.SingleOrDefaultAsync(x => x.NormalizedName == "under-review", token);
        item.StatusId = status!.Id;

        var currentUserId = currentUserService.UserId;

        if (currentUserId is null)
        {
            throw new UnauthorizedException();
        }

        item.UserId = Guid.Parse(currentUserId);

        var result = await _entityRepository.AddAsync(item, token);

        return result;
    }

    public async override Task<DataAccess.Models.Item> UpdateAsync(Guid id, ItemDto itemDto, CancellationToken token)
    {
        var item = await _entityRepository.GetByIdAsync(id, token);

        if (item is null)
        {
            throw new NotFoundException(GenericErrorMessages<DataAccess.Models.Item>.NotFound);
        }

        var currentUserId = currentUserService.UserId;

        if (currentUserId is null)
        {
            throw new UnauthorizedException();
        }
        else if (Guid.Parse(currentUserId) != item.UserId)
        {
            throw new ForbiddenException();
        }

        _mapper.Map(itemDto, item);

        var status = await _statusRepository.SingleOrDefaultAsync(
            x => x.NormalizedName == StatusValues.UnderReview.NormalizedName, token);

        item.StatusId = status!.Id;

        var result = await _entityRepository.UpdateAsync(item, token);

        return result;
    }
}