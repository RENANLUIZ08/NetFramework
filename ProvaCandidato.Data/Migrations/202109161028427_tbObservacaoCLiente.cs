namespace ProvaCandidato.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbObservacaoCLiente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClienteObservacaos",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Observacao = c.String(),
                        IdCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Cliente", t => t.IdCliente, cascadeDelete: true)
                .Index(t => t.IdCliente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClienteObservacaos", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.ClienteObservacaos", new[] { "IdCliente" });
            DropTable("dbo.ClienteObservacaos");
        }
    }
}
