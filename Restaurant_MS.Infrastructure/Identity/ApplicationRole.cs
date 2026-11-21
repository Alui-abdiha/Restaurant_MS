using Microsoft.AspNetCore.Identity;
using Restaurant_MS.Infrastructure.Identity;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant_MS.Infrastructure.Identity;

public class ApplicationRole : IdentityRole<int>
{
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}