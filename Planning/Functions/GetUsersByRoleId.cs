using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Data;
using Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Planning.Functions
{
    public class GetUsersByRoleId
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public GetUsersByRoleId(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        [FunctionName("GetUsersByRoleId")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string roleId = req.Query["roleId"];

            if (string.IsNullOrEmpty(roleId))
            {
                return new BadRequestObjectResult("Please provide a roleId parameter.");
            }

            try
            {
                var users = _dbContext.AspNetUserRoles
                                    .Include(ur => ur.User)
                                    .Where(ur => ur.RoleId == roleId)
                                    .Select(ur => ur.User.UserName)
                                    .ToList();

                return new OkObjectResult(users);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error retrieving users by roleId.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
    
}
