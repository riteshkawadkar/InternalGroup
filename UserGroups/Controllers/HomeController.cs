using Microsoft.AspNetCore.Mvc;
using UserGroup.ViewModel;
using UserGroup.Model;
using UserGroup.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewEngines;
namespace UserGroups.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserForm()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetUsersWithGroupsByIds(List<int> ids)
        {
            var query = _context.Users
                .Where(u => ids.Contains(u.Id))
                .Select(u => new UserVM
                {
                    Id = u.Id,
                    Username = u.Username,
                    Groups = u.UserGroups.Select(ug => ug.Group.Name).ToList()
                });

            var usersWithGroups = await query.ToListAsync();
            return PartialView("_userTable", usersWithGroups);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetGroupsByIds(List<int> ids)
        //{
        //    var groups = await _context.Groups
        //         .Where(g => ids.Contains(g.Id))
        //         .Select(g => new GroupVM
        //         {
        //             Id = g.Id,
        //             Name = g.Name,
        //             TotalUsers = g.UserGroups.Where(ug => ug.GroupId == g.Id).Count(),
        //             UserIds = g.UserGroups.Where(ug=> ug.GroupId == g.Id).Select(ug=> ug.UserId).ToList()
        //         }).ToListAsync();

        //    return PartialView("_groupTable", groups);
        //}

        [HttpGet]
        public async Task<IActionResult> GetGroupsByIds(List<int> ids)
        {
            var groups = await _context.Groups
                .Where(g => ids.Contains(g.Id))
                .Select(g => new GroupVM
                {
                    Id = g.Id,
                    Name = g.Name,
                    TotalUsers = g.UserGroups.Where(ug => ug.GroupId == g.Id).Count(),
                    UserIds = g.UserGroups.Where(ug => ug.GroupId == g.Id).Select(ug => ug.UserId).ToList()
                })
                .ToListAsync();

            // Call the PartialView method
            PartialViewResult partialViewResult = PartialView("_groupTable", groups);

            // Get the view engine from the HttpContext
            var viewEngine = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

            // Create a string writer to capture the output
            var stringWriter = new StringWriter();

            // Create a new ActionContext
            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor);

            // Render the PartialViewResult to the string writer
            var viewResult = viewEngine.FindView(actionContext, partialViewResult.ViewName, false);
            var viewContext = new ViewContext(actionContext, viewResult.View, ViewData, TempData, stringWriter, new HtmlHelperOptions());
            await viewResult.View.RenderAsync(viewContext);

            // Convert the captured output to a string
            string partialViewHtml = stringWriter.ToString();

            // Concatenate all the UserIds into a single list and remove duplicates
            var allUserIds = new HashSet<int>(groups.SelectMany(g => g.UserIds)).ToList();
            

            var response = new GroupsResponse
            {
                PartialViewHtml = partialViewHtml,
                UserIds = allUserIds
            };

            return Json(response);
        }

       

        [HttpGet]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            
            var users = await _context.UserGroups
             .Where(ug => ug.GroupId == id)
             .Select(ug => ug.UserId)
             .ToListAsync();
            return Json(users);

          
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
           
            var groups = _context.UserGroups.Where(x=> x.UserId == id).Select(x=> x.GroupId).ToList();
            return Json(groups);
          
        }

       
        [HttpPost]
        public async Task<IActionResult> SaveInternalGroupData([FromBody] InternalUserGroupVM model)
        {
            if (ModelState.IsValid)
            {
                var internalUserGroup = new InternalUserGroup
                {
                    Name = model.Name,
                    Description = model.Description,
                    Type = model.Type
                };

                _context.InternalUserGroup.Add(internalUserGroup);
                await _context.SaveChangesAsync();
                if( model.UserIds.Count() > 0)
                {
                    List<InternalUserGroupUsers> internalUserGroupUsers = new List<InternalUserGroupUsers>();
                    foreach (var user in model.UserIds)
                    {
                        var internalUserGroupUser = new InternalUserGroupUsers
                        {
                            UserId = user,
                            InternalUserGroupId = internalUserGroup.Id
                        };

                        internalUserGroupUsers.Add(internalUserGroupUser);
                    }

                    _context.InternalUserGroupUsers.AddRange(internalUserGroupUsers);
                    await _context.SaveChangesAsync();
                }
                return Ok(internalUserGroup);
            }

            return BadRequest(ModelState);
        }


        #region API CALLS 

        [HttpGet]
        public async Task<List<UserVM>> GetUsersWithGroups(string searchTerm = null)
        {
            var query = _context.Users
                .Where(u => string.IsNullOrEmpty(searchTerm) || u.Username.Contains(searchTerm))
                .Select(u => new UserVM
                {
                    Id = u.Id,
                    Username = u.Username,
                    Groups = u.UserGroups.Select(ug => ug.Group.Name).ToList()
                });

            var usersWithGroups = await query.ToListAsync();
            return usersWithGroups;
        }

        [HttpGet]
        public async Task<List<GroupVM>> GetGroups(string searchTerm = null)
        {
            var groups = await _context.Groups.Where(u => string.IsNullOrEmpty(searchTerm) || u.Name.Contains(searchTerm))
                  .Select(g => new GroupVM
                  {
                      Id = g.Id,
                      Name = g.Name,
                  }).ToListAsync();
            return groups;
        }

        #endregion
    }
   
}

