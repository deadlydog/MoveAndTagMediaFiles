using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;

namespace MoveAndTagMediaFiles
{
	// Code extracted from: https://docs.microsoft.com/en-us/dotnet/api/system.security.principal.windowsidentity.runimpersonated?view=net-6.0
	public class Impersonation
	{
		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, out SafeAccessTokenHandle phToken);

		const int LOGON32_PROVIDER_DEFAULT = 0;
		const int LOGON32_LOGON_INTERACTIVE = 2;

		public static T InvokeMethodAsUser<T>(string username, SecureString password, Func<T> method)
		{
			var fullyQualifiedUserName = GetFullyQualifiedUsernameParts(username);
			var passwordString = new System.Net.NetworkCredential(string.Empty, password).Password;

			// Call LogonUser to obtain a handle to an access token.
			SafeAccessTokenHandle? safeAccessTokenHandle;
			bool authTokenAquired = LogonUser(fullyQualifiedUserName.Username, fullyQualifiedUserName.Domain, passwordString, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out safeAccessTokenHandle);

			if (!authTokenAquired)
			{
				int errorCode = Marshal.GetLastWin32Error();
				throw new InvalidCredentialsException(fullyQualifiedUserName.Domain, fullyQualifiedUserName.Username, errorCode);
			}

			// Note: if you want to run as unimpersonated, pass 'SafeAccessTokenHandle.InvalidHandle' instead of variable 'safeAccessTokenHandle'.
			return WindowsIdentity.RunImpersonated(safeAccessTokenHandle, () => method.Invoke());
		}

		public record FullyQualifiedUsername(string Domain, string Username);
		private static FullyQualifiedUsername GetFullyQualifiedUsernameParts(string fullyQualifiedUsername)
		{
			var parts = fullyQualifiedUsername.Split('\\');
			var domain = parts[0];
			var username = parts[1];
			return new FullyQualifiedUsername(domain, username);
		}
	}
}
