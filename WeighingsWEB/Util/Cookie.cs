using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WeighingsWEB.Util
{
	public class Cookie
	{
		public static string GetCookieByName(HttpRequest httpRequest, string cookieName)
		{
			return httpRequest.Cookies[cookieName] == null ? null : httpRequest.Cookies[cookieName];
		}
	}
}
