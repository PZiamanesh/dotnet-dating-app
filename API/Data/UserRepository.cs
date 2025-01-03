using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DatingDbContextEF _context;

        public UserRepository(DatingDbContextEF context)
        {
            _context = context;
        }

        public async Task<PagedList<AppUser>> GetUsersAsync(UserParams userParams)
        {
            var query = _context.AppUsers
                .Include(u => u.Photos)
                .AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUserName);

            if (userParams.Gender != null)
            {
                query = query.Where(u => u.Gender == userParams.Gender);
            }

            return await PagedList<AppUser>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            return await _context.AppUsers
                .Include(u => u.Photos)
                .SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
