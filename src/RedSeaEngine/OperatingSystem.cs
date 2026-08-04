using System;
using System.Collections.Generic;
using System.Text;

namespace OceanApocalypse.RedSea;

public enum OperatingSystem : byte
{
	Windows,
	Linux,
	MacOS,
	FreeBSD,
	Android,
	IOS,
	TvOS,
	Unknown = Byte.MaxValue,
}
