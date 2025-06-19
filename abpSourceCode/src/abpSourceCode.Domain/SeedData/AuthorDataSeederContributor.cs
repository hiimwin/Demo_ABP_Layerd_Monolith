using abpSourceCode.Authors;
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
    internal class AuthorDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Author, Guid> _authorRepository;

        public AuthorDataSeederContributor(IRepository<Author, Guid> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _authorRepository.GetCountAsync() <= 0)
            {
                await _authorRepository.InsertManyAsync(
                    new List<Author>
                    {
                        new Author{
                            Name = "Adele",
                            DateOfBirth = new DateOnly(1988, 5, 5),
                            Nationality = "United Kingdom",
                            Biography = "Adele is a Grammy-winning British singer and songwriter known for her deep, soulful voice.",
                            AvartalUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/19cbbd163378427.63e4c7105b1fe.jpg"
                        },
                        new Author{
                            Name = "Ed Sheeran",
                            DateOfBirth = new DateOnly(1991, 2, 17),
                            Nationality = "United Kingdom",
                            Biography = "Ed Sheeran is a world-renowned singer-songwriter known for hits like 'Shape of You' and 'Thinking Out Loud'.",
                            AvartalUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/19cbbd163378427.63e4c7105b1fe.jpg"
                        }
                    });
            }
        }
    }
}
