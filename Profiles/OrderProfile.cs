using AutoMapper;
using endavaPractica.Net.Models;
using endavaPractica.Net.Models.Dto;

namespace endavapractica.net.profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderPatchDto>().ReverseMap();
        }
    }
}
