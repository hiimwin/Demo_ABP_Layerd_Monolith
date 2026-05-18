using abpSourceCode.Authors;
using abpSourceCode.Books;
using abpSourceCode.Categories;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace abpSourceCode.SeedData
{
    internal class BookStoreDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;

        private readonly IRepository<Category, Guid> _categoryRepository;

        public BookStoreDataSeederContributor(
            IRepository<Book, Guid> bookRepository,
            IRepository<Category, Guid> categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() <= 0)
            {
                var sciFiCategory =
                    await _categoryRepository.FirstOrDefaultAsync(
                        x => x.Code == "SCI_FI"
                    );

                var fantasyCategory =
                    await _categoryRepository.FirstOrDefaultAsync(
                        x => x.Code == "FANTASY"
                    );

                var romanceCategory =
                    await _categoryRepository.FirstOrDefaultAsync(
                        x => x.Code == "ROMANCE"
                    );

                if (sciFiCategory != null)
                {
                    await _bookRepository.InsertAsync(
                        new Book
                        {
                            Name = "1984",
                            Type = BookType.Dystopia,
                            PublishDate = new DateTime(1949, 6, 8),
                            Price = 19.84f,
                            CategoryId = sciFiCategory.Id,
                            Author = new Author
                            {
                                Name = "George Orwell",
                                DateOfBirth = new DateOnly(1903, 6, 25),
                                Nationality = "United Kingdom",
                                Biography = "George Orwell was an English novelist and journalist, known for his works critiquing totalitarianism and social injustice.",
                                AvartalUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/19cbbd163378427.63e4c7105b1fe.jpg"
                            }
                        },
                        autoSave: true
                    );

                    await _bookRepository.InsertAsync(
                        new Book
                        {
                            Name = "The Hitchhiker's Guide to the Galaxy",
                            Type = BookType.ScienceFiction,
                            PublishDate = new DateTime(1979, 10, 12),
                            Price = 42.0f,
                            CategoryId = sciFiCategory.Id,
                            Author = new Author
                            {
                                Name = "Douglas Adams",
                                DateOfBirth = new DateOnly(1952, 3, 11),
                                Nationality = "United Kingdom",
                                Biography = "Douglas Adams was an English author and humorist best known for The Hitchhiker's Guide to the Galaxy.",
                                AvartalUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/19cbbd163378427.63e4c7105b1fe.jpg"
                            }
                        },
                        autoSave: true
                    );
                }

                if (fantasyCategory != null)
                {
                    await _bookRepository.InsertAsync(
                        new Book
                        {
                            Name = "The Hobbit",
                            Type = BookType.Fantastic,
                            PublishDate = new DateTime(1937, 9, 21),
                            Price = 25.50f,
                            CategoryId = fantasyCategory.Id,
                            Author = new Author
                            {
                                Name = "J.R.R. Tolkien",
                                DateOfBirth = new DateOnly(1892, 1, 3),
                                Nationality = "United Kingdom",
                                Biography = "J.R.R. Tolkien was an English writer and philologist, author of The Hobbit and The Lord of the Rings.",
                                AvartalUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/19cbbd163378427.63e4c7105b1fe.jpg"
                            }
                        },
                        autoSave: true
                    );
                }

                if (romanceCategory != null)
                {
                    await _bookRepository.InsertAsync(
                        new Book
                        {
                            Name = "Pride and Prejudice",
                            Type = BookType.Adventure,
                            PublishDate = new DateTime(1813, 1, 28),
                            Price = 15.99f,
                            CategoryId = romanceCategory.Id,
                            Author = new Author
                            {
                                Name = "Jane Austen",
                                DateOfBirth = new DateOnly(1775, 12, 16),
                                Nationality = "United Kingdom",
                                Biography = "Jane Austen was an English novelist known for her romantic fiction and social commentary.",
                                AvartalUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/19cbbd163378427.63e4c7105b1fe.jpg"
                            }
                        },
                        autoSave: true
                    );
                }
            }
        }
    }
}