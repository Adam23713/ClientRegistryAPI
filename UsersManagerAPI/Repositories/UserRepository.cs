using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClientRegistryAPI.Data;
using ClientRegistryAPI.Models.Domain;

namespace ClientRegistryAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context = null!;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteUserAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<IEnumerable<User?>> GetAllUserAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUserNameAndEmail(string userName, string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Name == userName || u.Email == email);
        }

        public async Task<bool> IsUserNameOrEmailUsed(string userName, string email)
        {
            return await context.Users.AnyAsync(u => u.Name == userName || u.Email == email);
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            if(user == null)
            {
                return null;
            }

            var existingUser = await context.Users.FindAsync(id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                await context.SaveChangesAsync();
            }
            return existingUser;
        }
    }
}
