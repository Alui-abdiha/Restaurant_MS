using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant_MS.Infrastructure.Persistence;


namespace Restaurant_MS.Infrastructure.Identity
{
    public static class IdentityDataSeeder
    {
        private static readonly Dictionary<string, string[]> RolePermissions =
            new()
            {
                ["Administrator"] = PermissionCatalog.All
                    .Select(p => p.Code)
                    .ToArray(),

                ["Manager"] =
                [
                    PermissionCatalog.ProductsView,
                PermissionCatalog.ProductsCreate,
                PermissionCatalog.ProductsUpdate,
                PermissionCatalog.CategoriesView,
                PermissionCatalog.CategoriesManage,
                PermissionCatalog.TablesView,
                PermissionCatalog.TablesManage,
                PermissionCatalog.OrdersView,
                PermissionCatalog.OrdersCreate,
                PermissionCatalog.OrdersUpdate,
                PermissionCatalog.OrdersCancel,
                PermissionCatalog.KitchenView,
                PermissionCatalog.KitchenUpdateStatus,
                PermissionCatalog.BillsView,
                PermissionCatalog.BillsCreate,
                PermissionCatalog.PaymentsView,
                PermissionCatalog.PaymentsCreate,
                PermissionCatalog.PaymentsRefund,
                PermissionCatalog.InventoryView,
                PermissionCatalog.InventoryManage,
                PermissionCatalog.CustomersView,
                PermissionCatalog.CustomersManage,
                PermissionCatalog.ReportsView,
                PermissionCatalog.UsersView
                ],

                ["Cashier"] =
                [
                    PermissionCatalog.ProductsView,
                PermissionCatalog.CategoriesView,
                PermissionCatalog.TablesView,
                PermissionCatalog.OrdersView,
                PermissionCatalog.OrdersCreate,
                PermissionCatalog.BillsView,
                PermissionCatalog.BillsCreate,
                PermissionCatalog.PaymentsView,
                PermissionCatalog.PaymentsCreate,
                PermissionCatalog.CustomersView
                ],

                ["Waiter"] =
                [
                    PermissionCatalog.ProductsView,
                PermissionCatalog.CategoriesView,
                PermissionCatalog.TablesView,
                PermissionCatalog.OrdersView,
                PermissionCatalog.OrdersCreate,
                PermissionCatalog.OrdersUpdate,
                PermissionCatalog.CustomersView
                ],

                ["KitchenStaff"] =
                [
                    PermissionCatalog.KitchenView,
                PermissionCatalog.KitchenUpdateStatus,
                PermissionCatalog.OrdersView
                ],

                ["InventoryOfficer"] =
                [
                    PermissionCatalog.ProductsView,
                PermissionCatalog.InventoryView,
                PermissionCatalog.InventoryManage,
                PermissionCatalog.ReportsView
                ]
            };

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var dbContext =
                serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 1. Create default roles
            foreach (var roleName in RolePermissions.Keys)
            {
                if (await roleManager.RoleExistsAsync(roleName))
                    continue;

                var role = new ApplicationRole
                {
                    Name = roleName,
                    
                };

                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Could not create role {roleName}: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            // 2. Create missing permissions
            foreach (var definition in PermissionCatalog.All)
            {
                var exists = await dbContext.Permissions
                    .AnyAsync(p => p.Code == definition.Code);

                if (exists)
                    continue;

                dbContext.Permissions.Add(new Permission
                {
                    Code = definition.Code,
                    Name = definition.Name,
                    Module = definition.Module
                });
            }

            await dbContext.SaveChangesAsync();

            // 3. Assign permissions to roles
            foreach (var roleEntry in RolePermissions)
            {
                var role = await roleManager.FindByNameAsync(roleEntry.Key);

                if (role is null)
                    throw new InvalidOperationException(
                        $"Role {roleEntry.Key} was not found.");

                foreach (var permissionCode in roleEntry.Value)
                {
                    var permission = await dbContext.Permissions
                        .SingleOrDefaultAsync(p => p.Code == permissionCode);

                    if (permission is null)
                        throw new InvalidOperationException(
                            $"Permission {permissionCode} was not found.");

                    var alreadyAssigned = await dbContext.RolePermissions
                        .AnyAsync(rp =>
                            rp.RoleId == role.Id &&
                            rp.PermissionId == permission.Id);

                    if (!alreadyAssigned)
                    {
                        dbContext.RolePermissions.Add(new RolePermission
                        {
                            RoleId = role.Id,
                            PermissionId = permission.Id
                        });
                    }
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
