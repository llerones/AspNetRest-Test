using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoneyBox.Api.Services
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<MoneyBoxContext>
    {
        protected override void Seed(MoneyBoxContext context)
        {
            // Seed code here
        }
    }
}