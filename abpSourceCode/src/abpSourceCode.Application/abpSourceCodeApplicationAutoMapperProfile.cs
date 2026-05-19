using abpSourceCode.Authors;
using abpSourceCode.Books;
using abpSourceCode.Categories;
using abpSourceCode.OrderItems;
using abpSourceCode.Orders;
using abpSourceCode.Payments;
using AutoMapper;

namespace abpSourceCode;

public class abpSourceCodeApplicationAutoMapperProfile : Profile
{
    public abpSourceCodeApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();

        CreateMap<Author, AuthorDto>();
        CreateMap<CreateUpdateAuthorDto, Author>();

        CreateMap<Category, CategoryDto>();
        CreateMap<CreateUpdateCategoryDto, Category>().ForMember(dest => dest.Books, opt => opt.Ignore());

        CreateMap<Order, OrderDto>();
        CreateMap<CreateUpdateOrderDto, Order>();

        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<CreateOrderItemDto, OrderItem>();

        CreateMap<Payment, PaymentDto>();
        CreateMap<CreateUpdatePaymentDto, Payment>();
    }
}
