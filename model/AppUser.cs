using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestHarness.model
{
    public partial class AppUser
    {
       

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WindowsLoginName { get; set; }
        public bool? IsDisabled { get; set; }
        public string DefaultLanguage { get; set; }
        public string DefaultSkin { get; set; }
        public Guid? DepartmentId { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public Guid? LocationId { get; set; }


        public virtual IList<AppGroup> AppUserGroup { get; set; }
        public virtual IList<AppRole> AppUserRole { get; set; }
        
    }
}
