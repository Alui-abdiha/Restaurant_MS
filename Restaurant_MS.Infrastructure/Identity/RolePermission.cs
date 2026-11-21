namespace Restaurant_MS.Infrastructure.Identity
{
    public class RolePermission
    {
        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public ApplicationRole Role { get; set; } = null!;

        public Permission Permission { get; set; } = null!;
    }
}