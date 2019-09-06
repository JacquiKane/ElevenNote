using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {

        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            _userId = userId;
        }


        public bool CreateCategory(Category model)
        {
            var entity =
                new Category()
                {
                    CategoryOwner = model.CategoryOwner,
                    CategoryDescription = model.CategoryDescription,
                    CategoryType = model.CategoryType,
                    CategoryEventType = model.CategoryEventType
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateCategory(Category model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var detail =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == model.CategoryId);
                detail.CategoryId = model.CategoryId;
                detail.CategoryType = model.CategoryType;
                detail.CategoryEventType = model.CategoryEventType;
                detail.CategoryOwner = model.CategoryOwner;
                detail.CategoryDescription = model.CategoryDescription;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == categoryId);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }



        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Select(
                            e =>
                                new CategoryListItem
                                {
                                    CategoryDescription = e.CategoryDescription,
                                    CategoryEventType = e.CategoryEventType,
                                    CategoryType = e.CategoryType,
                                    CategoryId = e.CategoryId,
                                    CategoryOwner = e.CategoryOwner
                                }
                        );

                return query.ToArray();
            }
        }



        public IEnumerable<NoteListItem> GetNotesForCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Notes
                        .Where(e => e.CategoryId == categoryId)
                        .Select(e => 
                                new NoteListItem
                                {
                                    NoteId = e.NoteId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }


        // For details
        public Category GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var detail =
                    ctx
                        .Categories
                        .Single(e => e.CategoryId == id);
                return
                    new Category
                    {
                        CategoryId = detail.CategoryId,
                        CategoryType = detail.CategoryType,
                        CategoryEventType = detail.CategoryEventType,
                        CategoryOwner = detail.CategoryOwner,
                        CategoryDescription = detail.CategoryDescription
                    };
            }
        }
    }
}
