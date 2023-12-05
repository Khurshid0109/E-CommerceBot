using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Data.IRepositories;
using TelegramBot.Domain.Entities;
using TelegramBot.Service.DTOs.User;
using TelegramBot.Service.Exceptions;
using TelegramBot.Service.Interfaces;

namespace TelegramBot.Service.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserForResultDto> AddAsync(UserForEntryDto dto)
    {
        var user = await _repository.SelectAll()
             .Where(u => u.PhoneNumber == dto.PhoneNumber)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is not null)
            throw new BotException(409, "User already exists!");

        var mapped = _mapper.Map<User>(dto);
        mapped.CreatedAt = DateTime.UtcNow;

        var res = await _repository.InsertAsync(mapped);

        return _mapper.Map<UserForResultDto>(res);
    }

    public async Task<UserForResultDto> ModifyAsync(UserForEntryDto dto)
    {
        var user = await _repository.SelectAll()
              .Where(u => u.PhoneNumber == dto.PhoneNumber)
              .AsNoTracking()
              .FirstOrDefaultAsync();

        if (user is  null)
            throw new BotException(404, "User is not found!");

        var mapped = _mapper.Map<User>(dto);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(mapped);

        return _mapper.Map<UserForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await _repository.SelectAll()
             .Where(u => u.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is null)
            throw new BotException(404, "User is not found!");

        await _repository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync()
    {
        var users = await _repository.SelectAll()
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var user = await _repository.SelectAll()
               .Where(u => u.Id==id)
               .AsNoTracking()
               .FirstOrDefaultAsync();

        if (user is null)
            throw new BotException(404, "User is not found!");

        return _mapper.Map<UserForResultDto>(user);
    }
}
