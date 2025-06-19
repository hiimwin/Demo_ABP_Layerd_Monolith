using abpSourceCode.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace abpSourceCode.Categories
{
    public class CategoryAppService : ApplicationService, ICategoryAppService
    {
        private readonly IRepository<Category, Guid> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CategoryAppService(IRepository<Category, Guid> repository, IUnitOfWorkManager unitOfWorkManager)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            var category = ObjectMapper.Map<CreateUpdateCategoryDto, Category>(input);
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _repository.InsertAsync(category);
                await uow.CompleteAsync();
            }
            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _repository.DeleteAsync(id);
                await uow.CompleteAsync();
            }
        }

        public async Task<CategoryDto> GetAsync(Guid id)
        {
            // cách 1 
            //var queryable = await _repository.WithDetailsAsync(x => x.Books);
            //var category = queryable.FirstOrDefault(x => x.Id == id);

            // cách 2 
            var category = await _repository.GetAsync(id, includeDetails: false);
            await _repository.EnsureCollectionLoadedAsync(category, x => x.Books);

            // các 3 config chưa test
            //services.Configure<AbpEntityOptions>(options =>
            //{
            //    options.Entity<Student>(studentOptions =>
            //    {
            //        studentOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Groups);
            //    });
            //});

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public async Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _repository.WithDetailsAsync(x => x.Books);//GetQueryableAsync
            var query = queryable
                .OrderBy(x => x.Name)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var categories = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<CategoryDto>(
                totalCount,
                ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories)
            );
        }

        public async Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            var category = await _repository.GetAsync(id);
            ObjectMapper.Map(input, category);
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _repository.UpdateAsync(category);
                await uow.CompleteAsync();
            }

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }
    }
}
