using abpSourceCode.Authors;
using abpSourceCode.Books;
using abpSourceCode.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace abpSourceCode.SeedData
{
    internal class BookStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() <= 0)
            {
                await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "1984",
                        Type = BookType.Dystopia,
                        PublishDate = new DateTime(1949, 6, 8),
                        Price = 19.84f,
                        Category = new Category
                        {
                            Name = "Dystopian Fiction",
                            Description = "Novels depicting oppressive societies and totalitarian regimes."
                        },
                        Author = new Author
                        {
                            Name = "George Orwell",
                            DateOfBirth = new DateOnly(1903, 6, 25),
                            Nationality = "United Kingdom",
                            Biography = "George Orwell was an English novelist and journalist, known for his works critiquing social injustice and totalitarianism.",
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
                        Category = new Category
                        {
                            Name = "Science Fiction",
                            Description = "Books about futuristic science, space exploration, and imaginative concepts."
                        },
                        Author = new Author
                        {
                            Name = "Douglas Adams",
                            DateOfBirth = new DateOnly(1952, 3, 11),
                            Nationality = "United Kingdom",
                            Biography = "Douglas Adams was an English author and humorist, best known for writing The Hitchhiker's Guide to the Galaxy.",
                            AvartalUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/19cbbd163378427.63e4c7105b1fe.jpg"
                        }
                    },
                    autoSave: true
                );
            }
        }

    }
}
