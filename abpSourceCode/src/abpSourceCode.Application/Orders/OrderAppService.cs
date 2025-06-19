using abpSourceCode.Categories;
using abpSourceCode.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace abpSourceCode.Orders
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IRepository<OrderItem, long> _repositoryOrderItem;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public OrderAppService(IRepository<Order, Guid> repository, IUnitOfWorkManager unitOfWorkManager, IRepository<OrderItem, long> repositoryorder)
        {
            _repositoryOrderItem = repositoryorder;
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<OrderDto> CreateAsync(CreateUpdateOrderDto input)
        {
            var order = ObjectMapper.Map<CreateUpdateOrderDto, Order>(input);
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _repository.InsertAsync(order);
                await uow.CompleteAsync();
            }
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _repository.DeleteAsync(id);
                await uow.CompleteAsync();
            }
        }

        public async Task<OrderDto> GetAsync(Guid id)
        {
            // Cách 1 Chạy ổn nhưng không hay
            var order = await _repository.GetAsync(id, includeDetails: true);
            await _repository.EnsureCollectionLoadedAsync(order, x => x.OrderItems);

            foreach (var item in order.OrderItems)
            {
                await _repositoryOrderItem.EnsurePropertyLoadedAsync(item, x => x.Book);
            }

            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        public async Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _repository.WithDetailsAsync(x => x.OrderItems);

            var query = queryable
                .OrderBy(x => x.OrderDate)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var orders = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            // cách này chưa hay
            foreach (var order in orders)
            {
                foreach (var item in order.OrderItems)
                {
                    await _repositoryOrderItem.EnsurePropertyLoadedAsync(item, x => x.Book);
                }
            }

            return new PagedResultDto<OrderDto>(
                totalCount,
                ObjectMapper.Map<List<Order>, List<OrderDto>>(orders)
            );
        }

        public async Task<OrderDto> UpdateAsync(Guid id, CreateUpdateOrderDto input)
        {
            var order = await _repository.GetAsync(id);
            ObjectMapper.Map(input, order);
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _repository.UpdateAsync(order);
                await uow.CompleteAsync();
            }

            return ObjectMapper.Map<Order, OrderDto>(order);
        }
    }
}
