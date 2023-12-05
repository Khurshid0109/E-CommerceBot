using AutoMapper;
using TelegramBot.Domain.Entities;
using TelegramBot.Service.DTOs.Products;

namespace TelegramBot.Service.Mappers;
public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<ProductForEntryDto, Product>().ReverseMap();
        CreateMap<ProductForResultDto, Product>().ReverseMap();

    }
}
