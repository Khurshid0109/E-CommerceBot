using AutoMapper;
using TelegramBot.Domain.Entities;
using TelegramBot.Service.DTOs.OrderItems;
using TelegramBot.Service.DTOs.Orders;
using TelegramBot.Service.DTOs.Products;
using TelegramBot.Service.DTOs.User;

namespace TelegramBot.Service.Mappers;
public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<ProductForCreateDto, Product>().ReverseMap();
        CreateMap<ProductForUpdateDto, Product>().ReverseMap();
        CreateMap<ProductForResultDto, Product>().ReverseMap();

        CreateMap<OrderForResultDto, Order>().ReverseMap();
        CreateMap<OrderForEntryDto, Order>().ReverseMap();

        CreateMap<OrderItemForCreatDto, OrderItem>().ReverseMap();
        CreateMap<OrderItemForResultDto, OrderItem>().ReverseMap();

        CreateMap<UserForEntryDto,User>().ReverseMap();
        CreateMap<UserForResultDto, User>().ReverseMap();

    }
}
