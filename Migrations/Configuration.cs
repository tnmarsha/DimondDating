namespace DimondDating.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ContactManager.Models;
    using DimondDating.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
 

    internal sealed class Configuration : DbMigrationsConfiguration<DimondDating.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        bool AddUserAndRole(DimondDating.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };
            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }

        protected override void Seed(DimondDating.Models.ApplicationDbContext context)
        {

            {
                AddUserAndRole(context);
                context.Contacts.AddOrUpdate(p => p.Name,
                   new Contact
                   {
                       Name = "Debra Garcia",
                       Address = "1234 Main St",
                       City = "Redmond",
                       State = "WA",
                       Zip = "10999",
                       Email = "debra@example.com",
                   },
                    new Contact
                    {
                        Name = "Thorsten Weinrich",
                        Address = "5678 1st Ave W",
                        City = "Redmond",
                        State = "WA",
                        Zip = "10999",
                        Email = "thorsten@example.com",
                    },
                    new Contact
                    {
                        Name = "Yuhong Li",
                        Address = "9012 State st",
                        City = "Redmond",
                        State = "WA",
                        Zip = "10999",
                        Email = "yuhong@example.com",
                    },
                    new Contact
                    {
                        Name = "Jon Orton",
                        Address = "3456 Maple St",
                        City = "Redmond",
                        State = "WA",
                        Zip = "10999",
                        Email = "jon@example.com",
                    },
                    new Contact
                    {
                        Name = "Diliana Alexieva-Bosseva",
                        Address = "7890 2nd Ave E",
                        City = "Redmond",
                        State = "WA",
                        Zip = "10999",
                        Email = "diliana@example.com",
                    });

                AddUserAndRole(context);
                context.DimondDating.AddOrUpdate(p => p.familyname,
                   new QuickSearch
                   {
                       familyname = " Garcia",
                       address1 = "1234 Main St",
                       city = "Redmond",
                       state = "WA",
                       zip = "10999",
                       
                   },
                    new QuickSearch
                    {
                        familyname = " Weinrich",
                        address1 = "5678 1st Ave W",
                        city = "Redmond",
                        state = "WA",
                        zip = "10999",
                        
                    },
                    new QuickSearch
                    {
                        familyname = "Yuhong Li",
                        address1 = "9012 State st",
                        city = "Redmond",
                        state = "WA",
                        zip = "10999",
                        
                    },
                    new QuickSearch
                    {
                        familyname = " Orton",
                        address1 = "3456 Maple St",
                        city = "Redmond",
                        state = "WA",
                        zip = "10999",
                        
                    },
                    new QuickSearch
                    {
                        familyname = "Bosseva",
                        address1 = "7890 2nd Ave E",
                        city = "Redmond",
                        state = "WA",
                        zip = "10999",
                        
                    }
                    
                    );
            }
                //  This method will be called after migrating to the latest version.

                //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
                //  to avoid creating duplicate seed data. E.g.
                //
                //    context.People.AddOrUpdate(
                //      p => p.FullName,
                //      new Person { FullName = "Andrew Peters" },
                //      new Person { FullName = "Brice Lambson" },
                //      new Person { FullName = "Rowan Miller" }
                //    );
                //
            }
        }
    }

