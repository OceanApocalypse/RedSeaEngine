using System;
using System.Collections.Generic;
using System.Text;

namespace OceanApocalypseStudios.RedSea;

public enum LinuxDistribution : short
{
	NotApplicable,
	Debian,
	Ubuntu,
	Fedora,
	ArchLinux,
	Unknown = Int16.MaxValue
}
