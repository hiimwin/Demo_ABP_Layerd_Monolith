using abpSourceCode.Categories;
using System;
using System.Collections.Generic;
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
                        new Category
                        {
                            Name = "Sci-Fi & Cyberpunk",
                            Code = "SCI_FI",
                            Description = "Explore futuristic worlds, advanced technology, and dystopian societies.",
                            Slug = "sci-fi-cyberpunk",
                            ImageUrl = "https://images.unsplash.com/photo-1518709268805-4e9042af2176",
                            SeoTitle = "Best Sci-Fi & Cyberpunk Books",
                            SeoDescription = "Discover cyberpunk novels, futuristic adventures, AI worlds, and science fiction masterpieces.",
                            DisplayOrder = 1,
                            IsActive = true
                        },

                        new Category
                        {
                            Name = "World History",
                            Code = "WORLD_HISTORY",
                            Description = "Uncover stories from ancient civilizations to modern revolutions.",
                            Slug = "world-history",
                            ImageUrl = "https://images.unsplash.com/photo-1461360370896-922624d12aa1",
                            SeoTitle = "World History Books Collection",
                            SeoDescription = "Explore books covering global history, empires, wars, and historical figures.",
                            DisplayOrder = 2,
                            IsActive = true
                        },

                        new Category
                        {
                            Name = "Romantic Fiction",
                            Code = "ROMANCE",
                            Description = "Heartfelt stories of love, passion, and emotional journeys.",
                            Slug = "romantic-fiction",
                            ImageUrl = "https://images.unsplash.com/photo-1516979187457-637abb4f9353",
                            SeoTitle = "Top Romantic Fiction Books",
                            SeoDescription = "Browse emotional romance novels, modern love stories, and bestselling fiction.",
                            DisplayOrder = 3,
                            IsActive = true
                        },

                        new Category
                        {
                            Name = "Personal Growth",
                            Code = "SELF_HELP",
                            Description = "Transform your life with books on self-improvement and motivation.",
                            Slug = "personal-growth",
                            ImageUrl = "https://images.unsplash.com/photo-1495446815901-a7297e633e8d",
                            SeoTitle = "Personal Growth & Self Improvement Books",
                            SeoDescription = "Find motivation, productivity, mindset, and self-help books for personal success.",
                            DisplayOrder = 4,
                            IsActive = true
                        },

                        new Category
                        {
                            Name = "Epic Fantasy",
                            Code = "FANTASY",
                            Description = "Dive into magical realms, heroic quests, and mythical creatures.",
                            Slug = "epic-fantasy",
                            ImageUrl = "https://images.unsplash.com/photo-1512820790803-83ca734da794",
                            SeoTitle = "Epic Fantasy Books Collection",
                            SeoDescription = "Explore fantasy worlds filled with dragons, magic, heroes, and epic adventures.",
                            DisplayOrder = 5,
                            IsActive = true
                        }
                    },
                    autoSave: true
                );
            }
        }
    }
}