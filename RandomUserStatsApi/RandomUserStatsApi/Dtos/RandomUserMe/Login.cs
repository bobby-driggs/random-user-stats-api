using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomUserStatsApi.Dtos.RandomUserMe
{
    public class Login
	{
		public string username { get; set; }
		public string password { get; set; }
		public string salt { get; set; }
		public string md5 { get; set; }
		public string sha1 { get; set; }
		public string sha256 { get; set; }
	}
}
