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
			""IsDisabled"": false,
			""AppUserGroup"":[
								{
									""Id"": ""80EC15CC-BD21-4835-8730-0A01FFD1E550"",
									""Name"": ""SuperUser"",
									""Description"": ""Power Users""
								},
								{
									""Id"": ""5BD2F2DE-9022-4B1E-9F22-3538AF6C27D2"",
									""Name"": ""DomainUser"",
									""Description"": ""Domain Users""
								},
								{
									""Id"": ""3DBFEB05-4EF8-4236-A386-843FCAFF1F36"",
									""Name"": ""Administrators"",
									""Description"": ""System Admins""
								}
							],
		""AppUserRole"":[
							{
								""Id"": ""5CDB8D70-D97F-435A-A03E-00C2E00C7120"",
								""Name"": ""r2"",
								""Description"": ""r2"",
								""IsBuiltIn"": false
							},
							{
								""Id"": ""6C76EBD0-C116-46D1-A635-6EC96A64BB8C"",
								""Name"": ""r1"",
								""Description"": ""r1"",
								""IsBuiltIn"": false
							},
							{
								""Id"": ""6339EBC3-4A6A-4CB6-BBCD-CE73F40780B9"",
								""Name"": ""r3"",
								""IsBuiltIn"": false
							}
						]
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
			""IsDisabled"": false,
			""AppUserGroup"":[
								{
									""Id"": ""80EC15CC-BD21-4835-8730-0A01FFD1E550"",
									""Name"": ""SuperUser"",
									""Description"": ""Power Users""
								},
								{
									""Id"": ""5BD2F2DE-9022-4B1E-9F22-3538AF6C27D2"",
									""Name"": ""DomainUser"",
									""Description"": ""Domain Users""
								},
								{
									""Id"": ""3DBFEB05-4EF8-4236-A386-843FCAFF1F36"",
									""Name"": ""Administrators"",
									""Description"": ""System Admins""
								},
								{
									""Id"": ""D7AE1884-2119-4344-9EAE-FB1F2DD10C4F"",
									""Name"": ""ReadOnly"",
									""Description"": ""Read Only Groups""
								}
							],
		""AppUserRole"":[
							{
								""Id"": ""5CDB8D70-D97F-435A-A03E-00C2E00C7120"",
								""Name"": ""r2"",
								""Description"": ""r2"",
								""IsBuiltIn"": false
							},
							{
								""Id"": ""6C76EBD0-C116-46D1-A635-6EC96A64BB8C"",
								""Name"": ""r1"",
								""Description"": ""r1"",
								""IsBuiltIn"": false
							},
							{
								""Id"": ""6339EBC3-4A6A-4CB6-BBCD-CE73F40780B9"",
								""Name"": ""r3"",
								""IsBuiltIn"": false
							}
						]
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
			""IsDisabled"": false,
			""AppUserGroup"":[
								{
									""Id"": ""80EC15CC-BD21-4835-8730-0A01FFD1E550"",
									""Name"": ""SuperUser"",
									""Description"": ""Power Users""
								}
							],
		""AppUserRole"":[
							
							{
								""Id"": ""6339EBC3-4A6A-4CB6-BBCD-CE73F40780B9"",
								""Name"": ""r3"",
								""IsBuiltIn"": false
							}
						]
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
			""IsDisabled"": false,
			""AppUserGroup"":[
								
								{
									""Id"": ""5BD2F2DE-9022-4B1E-9F22-3538AF6C27D2"",
									""Name"": ""DomainUser"",
									""Description"": ""Domain Users""
								}
							],
		""AppUserRole"":[
							{
								""Id"": ""5CDB8D70-D97F-435A-A03E-00C2E00C7120"",
								""Name"": ""r2"",
								""Description"": ""r2"",
								""IsBuiltIn"": false
							}
						]
		}
	]";


		public static List<AppUser> GetSampleAppUsers()
        {
			return AppUsers.jsonDeserializeFromString<List<AppUser>>();
        }
    }
}
