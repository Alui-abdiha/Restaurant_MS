using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_MS.Infrastructure.Identity
{
    public static class PermissionCatalog
    {
        public const string ProductsView = "Products.View";
        public const string ProductsCreate = "Products.Create";
        public const string ProductsUpdate = "Products.Update";
        public const string ProductsDelete = "Products.Delete";

        public const string CategoriesView = "Categories.View";
        public const string CategoriesManage = "Categories.Manage";

        public const string TablesView = "Tables.View";
        public const string TablesManage = "Tables.Manage";

        public const string OrdersView = "Orders.View";
        public const string OrdersCreate = "Orders.Create";
        public const string OrdersUpdate = "Orders.Update";
        public const string OrdersCancel = "Orders.Cancel";

        public const string KitchenView = "Kitchen.View";
        public const string KitchenUpdateStatus = "Kitchen.UpdateStatus";

        public const string BillsView = "Bills.View";
        public const string BillsCreate = "Bills.Create";

        public const string PaymentsView = "Payments.View";
        public const string PaymentsCreate = "Payments.Create";
        public const string PaymentsRefund = "Payments.Refund";

        public const string InventoryView = "Inventory.View";
        public const string InventoryManage = "Inventory.Manage";

        public const string CustomersView = "Customers.View";
        public const string CustomersManage = "Customers.Manage";

        public const string ReportsView = "Reports.View";

        public const string UsersView = "Users.View";
        public const string UsersManage = "Users.Manage";

        public const string RolesManage = "Roles.Manage";
        public const string SettingsManage = "Settings.Manage";

        public static readonly IReadOnlyList<PermissionDefinition> All =
        [
            new(ProductsView, "View products", "Products"),
        new(ProductsCreate, "Create products", "Products"),
        new(ProductsUpdate, "Update products", "Products"),
        new(ProductsDelete, "Delete products", "Products"),

        new(CategoriesView, "View categories", "Categories"),
        new(CategoriesManage, "Manage categories", "Categories"),

        new(TablesView, "View dining tables", "Tables"),
        new(TablesManage, "Manage dining tables", "Tables"),

        new(OrdersView, "View orders", "Orders"),
        new(OrdersCreate, "Create orders", "Orders"),
        new(OrdersUpdate, "Update orders", "Orders"),
        new(OrdersCancel, "Cancel orders", "Orders"),

        new(KitchenView, "View kitchen tickets", "Kitchen"),
        new(KitchenUpdateStatus, "Update kitchen ticket status", "Kitchen"),

        new(BillsView, "View bills", "Billing"),
        new(BillsCreate, "Create bills", "Billing"),

        new(PaymentsView, "View payments", "Payments"),
        new(PaymentsCreate, "Record payments", "Payments"),
        new(PaymentsRefund, "Process payment refunds", "Payments"),

        new(InventoryView, "View inventory", "Inventory"),
        new(InventoryManage, "Manage inventory", "Inventory"),

        new(CustomersView, "View customers", "Customers"),
        new(CustomersManage, "Manage customers", "Customers"),

        new(ReportsView, "View reports", "Reports"),

        new(UsersView, "View users", "Users"),
        new(UsersManage, "Manage users", "Users"),

        new(RolesManage, "Manage roles", "Access Control"),
        new(SettingsManage, "Manage system settings", "Settings")
        ];
    }
    public record PermissionDefinition(
    string Code,
    string Name,
    string Module);
}




