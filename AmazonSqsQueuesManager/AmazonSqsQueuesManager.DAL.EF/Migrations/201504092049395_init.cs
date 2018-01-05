namespace AmazonSqsQueuesManager.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraceRecords",
                c => new
                    {
                        TracingRecordId = c.Int(nullable: false, identity: true),
                        QueueName = c.String(),
                        ActionType = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TracingRecordId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TraceRecords");
        }
    }
}
