using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilesManager.Persistence.Migrations
{
    [Migration(202106280002)]
    public class InitialSeed_202106280002 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("Doctors");
            Delete.FromTable("Patients");
            Delete.FromTable("Receptionists");
            Delete.FromTable("Specializations");
        }
        public override void Up()
        {
            Insert.IntoTable("Doctors");
            Insert.IntoTable("Patients");
            Insert.IntoTable("Receptionists");
            Insert.IntoTable("Specializations");
        }
    }
}
