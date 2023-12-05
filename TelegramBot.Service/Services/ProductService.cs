using AutoMapper;
using TelegramBot.Service.Helpers;
using TelegramBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Data.IRepositories;
using TelegramBot.Service.Interfaces;
using TelegramBot.Service.Exceptions;
using TelegramBot.Service.DTOs.Products;

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

    public async Task<ProductForResultDto> AddAsync(ProductForCreateDto dto)
    {
        var mapped = _mapper.Map<Product>(dto);
        mapped.CreatedAt=DateTime.UtcNow;
        mapped.Image= await MediaHelper.UploadFile(dto.Image);

        var result = await _repository.InsertAsync(mapped);

        return _mapper.Map<ProductForResultDto>(result);
    }

    public async Task<ProductForResultDto> ModifyAsync( ProductForUpdateDto dto)
    {
        var product = await _repository.SelectAll()
            .Where(p => p.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (product is null)
            throw new BotException(404, "Product is not found!");

        var mapped = _mapper.Map(dto, product);
        mapped.UpdatedAt=DateTime.UtcNow;
        mapped.Image = await MediaHelper.UploadFile(dto.Image);

        await _repository.UpdateAsync(mapped);

        return _mapper.Map<ProductForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var product = await _repository.SelectAll()
            .Where(p=> p.Id==id)
            .FirstOrDefaultAsync();

        if (product is null)
            throw new BotException(404, "Product is not found!");

        await _repository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<ProductForResultDto>> RetrieveAllAsync()
    {
        var products = await _repository.SelectAll()
              .ToListAsync();

        return _mapper.Map<IEnumerable<ProductForResultDto>>(products);
    }

    public async Task<ProductForResultDto> RetrieveByIdAsync(long id)
    {
        var product = await _repository.SelectAll()
            .Where(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product is null)
            throw new BotException(404, "Product is not found!");

        return _mapper.Map<ProductForResultDto>(product);
    }
}
