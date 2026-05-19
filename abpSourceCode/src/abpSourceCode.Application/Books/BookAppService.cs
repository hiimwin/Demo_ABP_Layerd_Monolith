using abpSourceCode.Categories;
using abpSourceCode.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq.Dynamic.Core;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using static abpSourceCode.Permissions.abpSourceCodePermissions;

namespace abpSourceCode.Books
{
    //[Authorize(policy: "BookAppService")]
    [Authorize(abpSourceCodePermissions.Books.Default)]
    public class BookAppService : ApplicationService, IBookAppService
    {
        private readonly IRepository<Book, Guid> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public BookAppService(IRepository<Book, Guid> repository
            , IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _repository = repository;
        }
        [Authorize(abpSourceCodePermissions.Books.Create)]
        public async Task<BookDto> CreateAsync(CreateUpdateBookDto input)
        {
            var book = ObjectMapper.Map<CreateUpdateBookDto, Book>(input);
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _repository.InsertAsync(book);
                await uow.CompleteAsync();
            }
            return ObjectMapper.Map<Book, BookDto>(book);
        }
        [Authorize(abpSourceCodePermissions.Books.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _repository.DeleteAsync(id);
                await uow.CompleteAsync();
            }
        }
        //[Authorize(abpSourceCodePermissions.Books.Default)]
        //[Authorize(policy: ConstantPolicies.Admin)]
        public async Task<BookDto> GetAsync(Guid id)
        {
            var book = await _repository.GetAsync(id);
            return ObjectMapper.Map<Book, BookDto>(book);
        }
        public async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _repository.GetQueryableAsync();
            var query = queryable
                .OrderBy(x => x.Name)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var books = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<BookDto>(
                totalCount,
                ObjectMapper.Map<List<Book>, List<BookDto>>(books)
            );
        }
        [Authorize(abpSourceCodePermissions.Books.Edit)]
        public async Task<BookDto> UpdateAsync(Guid id, CreateUpdateBookDto input)
        {
            var book = await _repository.GetAsync(id);
            ObjectMapper.Map(input, book);
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await _repository.UpdateAsync(book);
                await uow.CompleteAsync();
            }

            return ObjectMapper.Map<Book, BookDto>(book);
        }

        [Authorize(abpSourceCodePermissions.Categories.Edit)]
        public async Task UpdateBooksAsync(Guid categoryId, List<Guid> bookIds)
        {
            var books = await _repository.GetListAsync(x => bookIds.Contains(x.Id));

            books.ForEach(book => book.CategoryId = categoryId);

            using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
            {
                await uow.CompleteAsync();
            }
        }
    }
}
