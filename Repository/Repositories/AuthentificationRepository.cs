using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Domain.Authentication;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.UnitOfWork;
using Domain.Models;
using Repository.Data;

namespace Repository.Repositories
{
    public class AuthentificationRepository : IAuthentificationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork unitOfWork;

        public AuthentificationRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserModel?> GetUserInfos(string userId)
        {
            /*  var userDb = await _dbContext.AspNetUsers
               .Include(p => p.Roles)
               .Include(p => p.AgentSecteurs)
               .FirstOrDefaultAsync(u => u.Id == userId);


              var useraccess = await _dbContext.UserAccessEntites
                  .Where(e => e.AccesUserId == userId)
                  .Include(e => e.AccesEntite)
                  .ToListAsync();
              var userEntities = useraccess.Select(p => p.AccesEntite).ToList();
              var userSecteurs = userDb!.AgentSecteurs.Select(p => p.AgentSecteurSecteur).ToList();

              var user = new UserModel
              {
                  Entites = userEntities!,
                  NetUser = userDb,
                  Secteurs = userSecteurs!
              };*/
            var user = new UserModel
            {
                UserName = null,
                Email=null,


            };
            return user;
        }
    }
}
