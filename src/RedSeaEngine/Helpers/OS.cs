using System.Diagnostics.CodeAnalysis;


namespace OceanApocalypseStudios.RedSea.Helpers;

internal static class OS
{
	[SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "<Pending>")]
	public static bool IsWindows()
	{
#if NETFRAMEWORK
		return true;
#else
		return System.OperatingSystem.IsWindows();
#endif
	}

	[SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "<Pending>")]
	public static bool IsMacOS()
	{
#if NETFRAMEWORK
		return false;
#else
		return System.OperatingSystem.IsMacOS();
#endif
	}

	[SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "<Pending>")]
	public static bool IsLinux()
	{
#if NETFRAMEWORK
		return false;
#else
		return System.OperatingSystem.IsLinux();
#endif
	}

	[SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "<Pending>")]
	public static bool IsFreeBSD()
	{
#if NETFRAMEWORK
		return false;
#else
		return System.OperatingSystem.IsFreeBSD();
#endif
	}
}
