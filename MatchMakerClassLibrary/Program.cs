using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static void Main(string[] args)
{
	if (await MatchmakerAPI_Client.Authenticate("test@test.test", "Test1234")) {
		Console.WriteLine("auth success");
	}
}
