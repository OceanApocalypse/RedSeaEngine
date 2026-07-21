using System;
using System.Collections.Generic;
using System.Text;

namespace OceanApocalypseStudios.RedSea;

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
