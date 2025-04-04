using MercurialBackendDotnet.Model;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Utils;

public static class PasswordManipulation
{

	public static string HashPassword(string password)
	{
		// Implement your hashing logic here
		var passwordHasher = new PasswordHasher<object>();
    return passwordHasher.HashPassword(new object(), password);
	}

	public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
	{
		var passwordHasher = new PasswordHasher<object>();
    var result = passwordHasher.VerifyHashedPassword(new object(), hashedPassword, providedPassword);
    return result == PasswordVerificationResult.Success;
	}
}