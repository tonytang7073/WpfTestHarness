using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTestHarness.model;
using WpfTestHarness.helpers;

namespace WpfTestHarness
{
    public static class SampleDataHelper
    {
		public static string AppUsers = @"[
		{
			""Id"": ""4E243ED6-9170-4D9E-8A2F-061444D97390"",
			""UserName"": ""Iris_SysAdm"",
			""NormalizedUserName"": ""IRIS_SYSADM"",
			""EmailConfirmed"": false,
			""PhoneNumberConfirmed"": false,
			""TwoFactorEnabled"": false,
			""LockoutEnabled"": true,
			""AccessFailedCount"": 0,
			""FirstName"": ""System Admin"",
			""LastName"": ""Build In"",
			""WindowsLoginName"": ""justice\\tangz"",
			""IsDisabled"": false
		},
		{
			""Id"": ""8E7BDA9A-5B3A-4D68-B7B0-0FEA9A6ED890"",
			""UserName"": ""zhihua.tang@justice.wa.gov.au"",
			""NormalizedUserName"": ""ZHIHUA.TANG@JUSTICE.WA.GOV.AU"",
			""Email"": ""zhihua.tang@justice.wa.gov.au"",
			""NormalizedEmail"": ""ZHIHUA.TANG@JUSTICE.WA.GOV.AU"",
			""EmailConfirmed"": true,
			""PhoneNumberConfirmed"": false,
			""TwoFactorEnabled"": false,
			""LockoutEnabled"": true,
			""AccessFailedCount"": 0,
			""FirstName"": ""Tony"",
			""LastName"": ""Tang"",
			""IsDisabled"": false
		},
		{
			""Id"": ""372F584F-06B2-4B36-9C7F-352E9C655445"",
			""UserName"": ""t1@tt.com"",
			""NormalizedUserName"": ""T1@TT.COM"",
			""Email"": ""t1@tt.com"",
			""NormalizedEmail"": ""T1@TT.COM"",
			""EmailConfirmed"": false,
			""PhoneNumberConfirmed"": false,
			""TwoFactorEnabled"": false,
			""LockoutEnabled"": true,
			""AccessFailedCount"": 0,
			""FirstName"": ""Disabled"",
			""LastName"": ""Tang"",
			""IsDisabled"": false
		},
		{
			""Id"": ""4A93D193-C7A3-4DDC-9F70-5B85E0F8C63B"",
			""UserName"": ""tangt4"",
			""NormalizedUserName"": ""TANGT4"",
			""Email"": ""tangt4@tt.com"",
			""NormalizedEmail"": ""TANGT4@TT.COM"",
			""EmailConfirmed"": false,
			""PhoneNumberConfirmed"": false,
			""TwoFactorEnabled"": false,
			""LockoutEnabled"": true,
			""AccessFailedCount"": 0,
			""FirstName"": ""tony"",
			""LastName"": ""tang"",
			""IsDisabled"": false
		}
	]";


		public static List<AppUser> GetSampleAppUsers()
        {
			return AppUsers.jsonDeserializeFromString<List<AppUser>>();
        }
    }
}
