using ChatSite.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ChatSite.Models
{
	public class ManageUsersViewModel
	{
		public required IdentityUser[] Administrators { get; set; }
		public required IdentityUser[] Everyone { get; set; }
	}
}