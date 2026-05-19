using abpSourceCode.Books;
using abpSourceCode.Permissions;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(abpSourceCodePermissions.Categories.Default)]
    public class CategoryAppService : ApplicationService, ICategoryAppService
    {
        private readonly IRepository<Category, Guid> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Book, Guid> _bookRepository;

        public CategoryAppService(IRepository<Category, Guid> repository, IUnitOfWorkManager unitOfWorkManager, IRepository<Book, Guid> bookRepository)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _bookRepository = bookRepository;
        }

        [Authorize(abpSourceCodePermissions.Categories.Create)]
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

        [Authorize(abpSourceCodePermissions.Categories.Delete)]
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

        [Authorize(abpSourceCodePermissions.Categories.Edit)]
        //public async Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        //{
        //    var category = await _repository.GetAsync(id);
        //    ObjectMapper.Map(input, category);
        //    using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
        //    {
        //        await _repository.UpdateAsync(category);
        //        await uow.CompleteAsync();
        //    }

        //    return ObjectMapper.Map<Category, CategoryDto>(category);
        //}

        public async Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                var category = await _repository.GetAsync(id, includeDetails: true);

                ObjectMapper.Map(input, category);

                await UpdateBooksAsync(category, input.books);

                await _repository.UpdateAsync(category);

                await uow.CompleteAsync();

                return ObjectMapper.Map<Category, CategoryDto>(category);
            }
        }

        private async Task UpdateBooksAsync(Category category, List<Guid> newBookIds)
        {
            newBookIds ??= new List<Guid>();

            var currentBookIds = category.Books.Select(b => b.Id).ToList();

            var toAddIds = newBookIds.Except(currentBookIds).ToList();
            var toRemoveIds = currentBookIds.Except(newBookIds).ToList();

            category.Books.RemoveAll(b => toRemoveIds.Contains(b.Id));

            if (toAddIds.Any())
            {
                var books = await _bookRepository.GetListAsync(x => toAddIds.Contains(x.Id));

                foreach (var book in books)
                {
                    category.Books.Add(book);
                }
            }
        }
    }
}
