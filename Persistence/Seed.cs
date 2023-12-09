using Domain;
using Microsoft.Extensions.Logging;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                // Branch
                if (!context.Branches.Any())
                {
                    var branchList = new List<Branch>
                    {
                        new Branch { BranchName = "Dier Ezzor" },
                        new Branch { BranchName = "AlTabqa" },
                        new Branch { BranchName = "Qoubani"  },
                        new Branch { BranchName = "Minbij" },
                        new Branch { BranchName = "Derek" },
                        new Branch { BranchName = "Qamashli" },
                    };
                    await context.Branches.AddRangeAsync(branchList);
                    await context.SaveChangesAsync();
                }

                // Job Positions
                if (!context.JobPositions.Any())
                {
                    var jobPositionList = new List<JobPosition>
                    {
                        new JobPosition { PositionName = "Assistant" },
                        new JobPosition { PositionName = "Officer" },
                        new JobPosition { PositionName = "Team Leader" },
                        new JobPosition { PositionName = "Manager"  },
                        new JobPosition { PositionName = "Coordinator" },
                        new JobPosition { PositionName = "Head of office" },
                    };
                    await context.JobPositions.AddRangeAsync(jobPositionList);
                    await context.SaveChangesAsync();
                }


                //// Users & Role
                //if (!userManager.Users.Any())
                //{

                //    // Roles
                //    var roles = new List<IdentityRole>
                //    {
                //        new IdentityRole { Name = "Telesales"},
                //        new IdentityRole { Name = "Sales"},
                //        new IdentityRole { Name = "Manager"},
                //    };

                //    context.Roles.AddRange(roles);
                //    await context.SaveChangesAsync();
                //    // User
                //    var user = new AppUser
                //    {
                //        DisplaytName = "manager",
                //        UserName = "manager",
                //        Email = "manager@manager.com"
                //    };

                //    await userManager.CreateAsync(user, "Pa$$w0rd");
                //    await context.SaveChangesAsync();
                //    // Add user to role.
                //    await userManager.AddToRoleAsync(user, "Manager");
                //    await context.SaveChangesAsync();
                //}
            }
            catch (SystemException ex)
            {
                var logger = loggerFactory.CreateLogger<Seed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
