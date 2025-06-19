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
    internal class CategoryDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryDataSeederContributor(IRepository<Category, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _categoryRepository.GetCountAsync() <= 0)
            {
                await _categoryRepository.InsertManyAsync(
                    new List<Category>
                    {
                        new Category(){
                            Name = "Sci-Fi & Cyberpunk",
                            Description = "Explore futuristic worlds, advanced technology, and dystopian societies."
                        },
                        new Category(){
                            Name = "World History",
                            Description = "Uncover stories from ancient civilizations to modern revolutions."
                        },
                        new Category(){
                            Name = "Romantic Fiction",
                            Description = "Heartfelt stories of love, passion, and emotional journeys."
                        },
                        new Category(){
                            Name = "Personal Growth",
                            Description = "Transform your life with books on self-improvement and motivation."
                        },
                        new Category(){
                            Name = "Epic Fantasy",
                            Description = "Dive into magical realms, heroic quests, and mythical creatures."
                        }

                    },
                    autoSave: true
                );

            }
        }
    }
}
