using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace ProfilesManager.Persistence.Migrations
{
    [Migration(202106280001)]
    public class InitialTables_202106280001 : Migration
    {
        public override void Down()
        {            
            Delete.Table("Patients");
            Delete.Table("Receptionists");
            Delete.Table("Specializations");
            Delete.Table("Doctors");
        }
        public override void Up()
        {            
            Create.Table("Patients")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(60).NotNullable()
                .WithColumn("MiddleName").AsString(50).NotNullable()
                .WithColumn("DateOfBirth").AsDateTime().NotNullable()
                .WithColumn("AccountId").AsGuid().NotNullable();
            Create.Table("Receptionists")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(60).NotNullable()
                .WithColumn("MiddleName").AsString(50).NotNullable()
                .WithColumn("DateOfBirth").AsDateTime().NotNullable()
                .WithColumn("AccountId").AsGuid().NotNullable()
                .WithColumn("OfficeId").AsGuid().NotNullable()
                .WithColumn("Address").AsString(50).NotNullable();
            Create.Table("Specializations")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(50).NotNullable();
            Create.Table("Doctors")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(60).NotNullable()
                .WithColumn("MiddleName").AsString(50).NotNullable()
                .WithColumn("DateOfBirth").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("AccountId").AsGuid().NotNullable()
                .WithColumn("SpecializationId").AsGuid().NotNullable().ForeignKey("Specializations", "Id")
                .WithColumn("OfficeId").AsGuid().NotNullable()
                .WithColumn("CareerStart").AsDateTime().NotNullable()
                .WithColumn("Address").AsString(50).NotNullable();
        }
    }
}
