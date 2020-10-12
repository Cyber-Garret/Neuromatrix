using System;

namespace BungieAPI.Entities.Vendors
{
	public class DestinyVendorComponent
	{
		public bool CanPurchase { get; set; }
		public DestinyProgression Progression { get; set; }
		public int VendorLocationIndex { get; set; }
		public int? SeasonalRank { get; set; }
		public uint VendorHash { get; set; }
		public DateTime NextRefreshDate { get; set; }
		public bool Enabled { get; set; }
	}
}