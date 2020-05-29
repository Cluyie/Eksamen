using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.User.Data.Context;
using UCLDreamTeam.User.Domain.Events;
using UCLDreamTeam.User.Domain.Interface;

namespace UCLDreamTeam.User.Data.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Mapper _mapper;
        private readonly UserDbContext _userDbContext;

        public UserRepository(Mapper mapper, UserDbContext userDbContext)
        {
            _mapper = mapper;
            _userDbContext = userDbContext;
        }

        public async Task<IdentityResult> CreateUserAsync(Domain.Models.User user)
        {
            try
            {
                if (!_userDbContext.Users.Any(u => EF.Functions.Like(u.UserName, user.UserName) || EF.Functions.Like(u.Email, user.Email)))
                {
                    user.NormalizedUserName = user.UserName.ToUpperInvariant();
                    await _userDbContext.Users.AddAsync(user);
                    await _userDbContext.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                      Description = "Identical user"
                    });
                }
            }
            catch (Exception e)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = e.Message
                });
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Domain.Models.User> GetUserAsync(Guid id)
        {
            return await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> UpdateUserAsync(Domain.Models.User inputUser, Domain.Models.User dbUser)
        {
            try
            {
                // Can only update an existing inputUser
                if (dbUser == null)
                {
                    return null;
                }

                // Update the inputUser
                if (!string.IsNullOrWhiteSpace(inputUser.PasswordHash) &&
                    inputUser.PasswordHash != dbUser.PasswordHash)
                {
                    //If the password is unchanged or empty, this does not update the password
                    dbUser.PasswordHash = inputUser.PasswordHash;
                }
                // Automapper is configured to only overwrite the fields that are not null
                _mapper.Map(inputUser, dbUser);

                _userDbContext.Users.Update(dbUser);
                await _userDbContext.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = e.Message
                });
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<IdentityResult> DeleteUserAsync(Domain.Models.User user)
        {
            try
            {
                _userDbContext.Users.Remove(user);
                await _userDbContext.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = e.Message
                });
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Domain.Models.User> GetFromUserNameAsync(string userName)
        {
            string normalizedUserName = userName.ToUpperInvariant();
            return await _userDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName || u.NormalizedUserName == normalizedUserName);
        }
    }
}
