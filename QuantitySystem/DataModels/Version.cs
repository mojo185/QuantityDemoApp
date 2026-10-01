using System;
using Newtonsoft.Json;

namespace QuantitySystem.DataModels
{
	public class Version : IComparable<Version>
	{
		[JsonProperty]
		public uint MajorVersion { get; set; }

		[JsonProperty]
		public uint MinorVersion { get; set; }

		public int CompareTo(Version other)
		{
			if (MajorVersion == other.MajorVersion)
			{
				return MinorVersion.CompareTo(other.MinorVersion);
			}
			else
			{
				return MajorVersion.CompareTo(other.MajorVersion);
			}
		}
	}
}
