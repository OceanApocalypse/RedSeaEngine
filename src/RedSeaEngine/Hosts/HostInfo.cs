using System;


namespace OceanApocalypse.RedSea.Hosts;

/// <summary>
/// A set of information on a given host.
/// </summary>
/// <param name="machineName"><inheritdoc cref="MachineName"/></param>
/// <param name="osName"><inheritdoc cref="SystemName"/></param>
/// <param name="osVersion"><inheritdoc cref="SystemVersion"/></param>
/// <param name="osBuildNumber"><inheritdoc cref="SystemBuildNumber"/></param>
/// <param name="processorArchitecture"><inheritdoc cref="ProcessorArchitecture"/></param>
/// <param name="distroName"><inheritdoc cref="DistributionName"/></param>
/// <param name="distroFamily"><inheritdoc cref="DistributionFamily"/></param>
/// <seealso cref="HostFactory"/>
public readonly struct HostInfo(
	string? machineName,
	string? osName,
	float osVersion,
	string? osBuildNumber,
	string? processorArchitecture,
	string? distroName,
	string? distroFamily
)
{
	/// <inheritdoc cref="Environment.MachineName"/>
	public string? MachineName { get; } = machineName;

	/// <summary>
	/// The operating system's name.
	/// </summary>
	public string? SystemName { get; } = osName;

	/// <summary>
	/// The operating system's version. If Linux, it's the distribution's version instead.
	/// </summary>
	public float SystemVersion { get; } = osVersion;

	/// <summary>
	/// The operating system's build number.
	/// </summary>
	public string? SystemBuildNumber { get; } = osBuildNumber;

	/// <summary>
	/// The machine's processor architecture.
	/// </summary>
	public string? ProcessorArchitecture { get; } = processorArchitecture;

	/// <summary>
	/// True if the host runs on Linux.
	/// </summary>
	public bool IsLinux => SystemName is not null && SystemName.Equals("linux", StringComparison.OrdinalIgnoreCase);

	/// <summary>
	/// The Linux distribution's name.
	/// </summary>
	public string? DistributionName { get; } = distroName;

	/// <summary>
	/// The Linux distribution's family.
	/// </summary>
	public string? DistributionFamily { get; } = distroFamily;
}
