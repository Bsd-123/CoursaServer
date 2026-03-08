using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CourseRatingRepository : IRepositoryDouble<CourseRating>
    {
        private readonly IContext _context;
        public CourseRatingRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<CourseRating> AddItem(CourseRating item)
        {
            _context.CourseRatings.Add(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id1 , int id2)
        {
            _context.CourseRatings.Remove(await GetById(id1,id2));
            await _context.SaveAsync();
        }

        public async Task<List<CourseRating>> GetAll()
        {
            return await _context.CourseRatings.ToListAsync();
        }

        public async Task<CourseRating> GetById(int id1, int id2)
        {
            return await _context.CourseRatings.FirstOrDefaultAsync(x => x.CourseId == id1 && x.UserId == id2);
        }
        //בטוח צריך???
        public async Task UpdateItem(int id1, int id2, CourseRating item)
        {
            var CourseRating = await GetById(id1,id2);
            CourseRating.Rating = item.Rating;
            CourseRating.Comment = item.Comment;
            await _context.SaveAsync();
        }
    }
}
