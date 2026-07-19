using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using Microsoft.Win32;

using OceanApocalypseStudios.RedSea.Helpers;


namespace OceanApocalypseStudios.RedSea.Hosts;

/// <summary>
/// A factory for <see cref="HostInfo"/> instances.
/// </summary>
public static class HostFactory
{
	/// <summary>
	/// Returns a Windows XP host with an arbitrary build number.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetWindowsXP(string machineName, string? processorArchitecture) => new(machineName, "Windows", 5, "2600", processorArchitecture, null, null);

	/// <summary>
	/// Returns a Windows Vista host with an arbitrary build number.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetWindowsVista(string machineName, string? processorArchitecture) => new(machineName, "Windows", 6, "6003", processorArchitecture, null, null);

	/// <summary>
	/// Returns a Windows 7 host with an arbitrary build number.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetWindows7(string machineName, string? processorArchitecture) => new(machineName, "Windows", 7, "7601", processorArchitecture, null, null);

	/// <summary>
	/// Returns a Windows 8 host with an arbitrary build number.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetWindows8(string machineName, string? processorArchitecture) => new(machineName, "Windows", 8, "9200", processorArchitecture, null, null);

	/// <summary>
	/// Returns a Windows 8.1 host with an arbitrary build number.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetWindows81(string machineName, string? processorArchitecture) => new(machineName, "Windows", 8.1f, "9600", processorArchitecture, null, null);

	/// <summary>
	/// Returns a Windows 10 host with an arbitrary build number.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetWindows10(string machineName, string? processorArchitecture) => new(machineName, "Windows", 10, "19045", processorArchitecture, null, null);

	/// <summary>
	/// Returns a Windows 11 host with an arbitrary build number.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetWindows11(string machineName, string? processorArchitecture) => new(machineName, "Windows", 11, "28000", processorArchitecture, null, null);

	/// <summary>
	/// Returns an Ubuntu host.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="ubuntuVersion">The distribution version.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetUbuntu(string machineName, float ubuntuVersion, string? processorArchitecture) => new(machineName, "Linux", ubuntuVersion, null, processorArchitecture, "Ubuntu", "Debian");

	/// <summary>
	/// Returns a Debian host.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="debianVersion">The distribution version.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetDebian(string machineName, float debianVersion, string? processorArchitecture) => new(machineName, "Linux", debianVersion, null, processorArchitecture, "Debian", "Debian");

	/// <summary>
	/// Returns an Arch Linux host.
	/// </summary>
	/// <param name="machineName">The machine's name.</param>
	/// <param name="archLinuxVersion">The distribution version.</param>
	/// <param name="processorArchitecture">The processor architecture.</param>
	/// <returns>The host.</returns>
	public static HostInfo GetArchLinux(string machineName, float archLinuxVersion, string? processorArchitecture) => new(machineName, "Linux", archLinuxVersion, null, processorArchitecture, "ArchLinux", "ArchLinux");

#if NET5_0_OR_GREATER
	[System.Runtime.Versioning.SupportedOSPlatform("freebsd")]
	[System.Runtime.Versioning.SupportedOSPlatform("macos")]
#endif
	private static void InitializeFreeBSDOrMacOSVersionData(string program, string argument, ref float systemVersion)
	{
		try
		{
			using var proc = Process.Start(
				new ProcessStartInfo
				{
					FileName = program,
					Arguments = argument,
					RedirectStandardOutput = true,
					UseShellExecute = false
				}
			);

			string? fullVer = proc?.StandardOutput.ReadLine();

			if (Int32.TryParse(fullVer?.Split('.')[0], out int versionNum))
				systemVersion = versionNum;
		}
		catch { }
	}

#if NET5_0_OR_GREATER
	[System.Runtime.Versioning.SupportedOSPlatform("linux")]
#endif
	private static void InitializeLinuxData(ref string? distroName, ref string? distroFamily, ref float systemVersion)
	{
		if (!File.Exists("/etc/os-release"))
			return;

		string[] lines = File.ReadAllLines("/etc/os-release");

		string? idLine = lines.FirstOrDefault(l => l.StartsWith("ID="));

		if (idLine is null)
			return;

		distroName = idLine.Split('=')[1].Trim('"');

		string? versionLine = lines.FirstOrDefault(l => l.StartsWith("VERSION_ID="));

		if (versionLine is not null && Int32.TryParse(versionLine.Split('=')[1].Trim('"').Split('.')[0], out int versionNum))
			systemVersion = versionNum;

		if (distroName.Equals("fedora", StringComparison.OrdinalIgnoreCase))
			distroFamily = "fedora"; // fedora's ID_LIKE looks weird so we'll just say it's fedora

		string? likeLine = lines.FirstOrDefault(l => l.StartsWith("ID_LIKE="));

		if (likeLine is not null)
			distroFamily = likeLine.Split('=')[1].Trim('"');
	}

#if NET5_0_OR_GREATER
	[System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif
	private static void InitializeWindowsVersionData(ref float systemVersion, ref string? buildNumber)
	{
		using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
		buildNumber = key?.GetValue("CurrentBuildNumber") as string;

		if (key is not null && Int32.TryParse(buildNumber, out int buildNum))
		{
			systemVersion = buildNum switch
			{
				// Windows 11
				>= 22000 => 11,

				// Windows 10
				>= 10240 => 10,

				// Windows 8.1
				>= 9257 => 8.1f,

				// Windows 8
				>= 7652 => 8,

				// Windows 7
				>= 6427 => 7,

				// Windows Vista (since first Longhorn build)
				>= 3663 => 6,

				// Windows XP
				>= 2196 => 5,

				// Prior to XP
				_ => 4
			};
		}
	}

	/// <summary>
	/// Returns the current host as an <see cref="HostInfo"/> object.
	/// </summary>
	/// <returns>The current host's info.</returns>
	[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
	public static HostInfo GetCurrentHost()
	{
		string machineName = Environment.MachineName;
		string? systemName = null;
		string? distroName = null;
		string? distroFam = null;
		float systemVersion = -1.0f;
		string? buildNumber = null;
		string? processorArchitecture = RuntimeInformation.OSArchitecture switch
		{
			Architecture.X86 => "x86",
			Architecture.X64 => "x64",
			Architecture.Arm => "arm",
			Architecture.Arm64 => "arm64",
#if NET8_0_OR_GREATER
			Architecture.Wasm => "wasm",
			Architecture.S390x => "s390x",
			Architecture.LoongArch64 => "loongarch64",
			Architecture.Armv6 => "armv6",
			Architecture.Ppc64le => "ppc64le",
#endif
#if NET9_0_OR_GREATER
			Architecture.RiscV64 => "riscv64",
#endif
			_ => null
		};

		if (OS.IsWindows())
		{
			systemName = "Windows";
			InitializeWindowsVersionData(ref systemVersion, ref buildNumber);
		}

		else if (OS.IsMacOS())
		{
			systemName = "MacOS";
			InitializeFreeBSDOrMacOSVersionData("sw_vers", "-productVersion", ref systemVersion);			
		}

		else if (OS.IsLinux())
		{
			systemName = "Linux";
			InitializeLinuxData(ref distroName, ref distroFam, ref systemVersion);
		}

		else if (OS.IsFreeBSD())
		{
			systemName = "FreeBSD";
			InitializeFreeBSDOrMacOSVersionData("uname", "-r", ref systemVersion);
		}

		return new(machineName, systemName, systemVersion, buildNumber, processorArchitecture, distroName, distroFam);
	}
}