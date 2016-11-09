namespace WebApplication5.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Long(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 65, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.RoleName, unique: true);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 65, storeType: "nvarchar"),
                        Firstname = c.String(unicode: false),
                        Lastname = c.String(unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        TokenString = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        TokenId = c.Long(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false, precision: 0),
                        User_UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TokenString)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Role_User",
                c => new
                    {
                        Users_UserId = c.Long(nullable: false),
                        Roles_RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Users_UserId, t.Roles_RoleId })
                .ForeignKey("dbo.Users", t => t.Users_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Roles_RoleId, cascadeDelete: true)
                .Index(t => t.Users_UserId)
                .Index(t => t.Roles_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Role_User", "Roles_RoleId", "dbo.Roles");
            DropForeignKey("dbo.Role_User", "Users_UserId", "dbo.Users");
            DropIndex("dbo.Role_User", new[] { "Roles_RoleId" });
            DropIndex("dbo.Role_User", new[] { "Users_UserId" });
            DropIndex("dbo.Tokens", new[] { "User_UserId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Roles", new[] { "RoleName" });
            DropTable("dbo.Role_User");
            DropTable("dbo.Tokens");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
