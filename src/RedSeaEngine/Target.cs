using System;
using System.Runtime.InteropServices;


namespace OceanApocalypse.RedSea;

public readonly struct Target(string? targetName, string? runnerName, string? machineName, OperatingSystem osName, float minOsVersion, float maxOsVersion, string? osBuildNumber, LinuxDistribution distroName, LinuxDistribution distroFamily, Architecture processorArchitecture, object? output)
{
	public Guid TargetGuid { get; } = Guid.NewGuid();
	public string TargetName => targetName ?? $"Target{TargetGuid:B}";
	public string? RunnerName => runnerName;
	public OperatingSystem OSName => osName;
	public float MinimumOSVersion => minOsVersion;
	public float MaximumOSVersion => maxOsVersion;
	public string? OSBuildNumber => osBuildNumber;
	public Architecture ProcessorArchitecture => processorArchitecture;
	public string? MachineName => machineName;
	public object? OutputValue => output;
	public LinuxDistribution DistributionName => distroName;
	public LinuxDistribution DistributionFamily => distroFamily;
}
