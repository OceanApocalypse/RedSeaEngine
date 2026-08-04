using System;
using System.Collections.Generic;
using System.Text;

namespace OceanApocalypse.RedSea;

public enum LinuxDistribution : short
{
	NotApplicable,
	Debian,
	Ubuntu,
	Fedora,
	ArchLinux,
	Unknown = Int16.MaxValue
}
