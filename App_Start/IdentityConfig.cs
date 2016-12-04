using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using DimondDating.Models;
using System.Net.Http;
using System.Net.Mail;


namespace DimondDating
{
    public static class Keys
    {
        public static string SMSAccountIdentification = "K4QWMN2WE46Y";
        public static string SMSAccountPassword = "king7613";
        public static string SMSAccountFrom = "+4126149963";
    }
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var soapSms = new DimondDating.ASPSMSX2.ASPSMSX2SoapClient("ASPSMSX2Soap");
            soapSms.SendSimpleTextSMS(
              Keys.SMSAccountIdentification,
              Keys.SMSAccountPassword,
              message.Destination,
              Keys.SMSAccountFrom,
              message.Body);
            soapSms.Close();
            return Task.FromResult(0);



        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
        //     Default EntityFramework IUser implementation
        public class IdentityUser<TKey, TLogin, TRole, TClaim> : IUser<TKey>
           where TLogin : IdentityUserLogin<TKey>
           where TRole : IdentityUserRole<TKey>
           where TClaim : IdentityUserClaim<TKey>
        {
            public IdentityUser()
            {
                Claims = new List<TClaim>();
                Roles = new List<TRole>();
                Logins = new List<TLogin>();
            }

            ///     User ID (Primary Key)
            public virtual TKey Id { get; set; }

            public virtual string Email { get; set; }
            public virtual bool EmailConfirmed { get; set; }

            public virtual string PasswordHash { get; set; }

            ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
            public virtual string SecurityStamp { get; set; }

            public virtual string PhoneNumber { get; set; }
            public virtual bool PhoneNumberConfirmed { get; set; }

            public virtual bool TwoFactorEnabled { get; set; }

            ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
            public virtual DateTime? LockoutEndDateUtc { get; set; }

            public virtual bool LockoutEnabled { get; set; }

            ///     Used to record failures for the purposes of lockout
            public virtual int AccessFailedCount { get; set; }

            ///     Navigation property for user roles
            public virtual ICollection<TRole> Roles { get; private set; }

            ///     Navigation property for user claims
            public virtual ICollection<TClaim> Claims { get; private set; }


            ///     Navigation property for user logins
            public virtual ICollection<TLogin> Logins { get; private set; }

            public virtual string UserName { get; set; }

        }
    }
}
