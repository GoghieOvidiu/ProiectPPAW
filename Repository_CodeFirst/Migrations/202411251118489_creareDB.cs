namespace Repository_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creareDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abonatiis",
                c => new
                    {
                        AbonatiiId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        AbonamentId = c.Int(nullable: false),
                        DataStart = c.DateTime(storeType: "date"),
                        DataEnd = c.DateTime(storeType: "date"),
                        NrUtilizari = c.Int(nullable: false),
                        DataUtilizarii = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.AbonatiiId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.TipAbonaments", t => t.AbonamentId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.AbonamentId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Nume = c.String(),
                        Email = c.String(),
                        Parola = c.String(),
                        DataStart = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.TipAbonaments",
                c => new
                    {
                        AbonamentId = c.Int(nullable: false, identity: true),
                        Tip = c.String(),
                        PretLuna = c.Int(nullable: false),
                        PretAn = c.Int(nullable: false),
                        NrUtilizari = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AbonamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Abonatiis", "AbonamentId", "dbo.TipAbonaments");
            DropForeignKey("dbo.Abonatiis", "ClientId", "dbo.Clients");
            DropIndex("dbo.Abonatiis", new[] { "AbonamentId" });
            DropIndex("dbo.Abonatiis", new[] { "ClientId" });
            DropTable("dbo.TipAbonaments");
            DropTable("dbo.Clients");
            DropTable("dbo.Abonatiis");
        }
    }
}
