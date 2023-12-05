using AutoMapper;
using TelegramBot.Service.Interfaces;
using TelegramBot.Data.IRepositories;
using TelegramBot.Service.DTOs.Products;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Domain.Entities;
using TelegramBot.Service.Exceptions;

namespace TelegramBot.Service.Services;
public class ProductService : IProductService
{

    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;

    public ProductService(IMapper mapper, IProductRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProductForResultDto> AddAsync(ProductForResultDto dto)
    {
        var mapped = _mapper.Map<Product>(dto);
        mapped.CreatedAt=DateTime.UtcNow;

        var result = await _repository.InsertAsync(mapped);

        return _mapper.Map<ProductForResultDto>(result);
    }

    public async Task<ProductForResultDto> ModifyAsync(long id, ProductForResultDto dto)
    {
        var product = await _repository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (product is null)
            throw new BotException(404, "Product is not found!");

        var mapped = _mapper.Map(dto, product);
        mapped.UpdatedAt=DateTime.UtcNow;

        await _repository.UpdateAsync(mapped);

        return _mapper.Map<ProductForEntryDto>(mapped);
    }

    public Task<bool> RemoveAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductForResultDto>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductForResultDto> RetrieveByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
