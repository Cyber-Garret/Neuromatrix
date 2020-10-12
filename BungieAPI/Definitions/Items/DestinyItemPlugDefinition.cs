namespace BungieAPI.Definitions.Items
{
	public class DestinyItemPlugDefinition
	{
		// insertionRules
		public string PlugCategoryIdentifier { get; set; }
		public uint PlugCategoryHash { get; set; }
		public bool OnActionRecreateSelf { get; set; }
		public uint InsertionMaterialRequirementHash { get; set; }
		public uint EnabledMaterialRequirementHash { get; set; }
		// enabledRules
		public string UiPlugLabel { get; set; }
		public PlugUiStyles PlugStyle { get; set; }
		public bool IsPseudoPlug { get; set; }
		public PlugAvailabilityMode PlugAvailability { get; set; }
		public string AlternateUiPlugLabel { get; set; }
		public int AlternatePlugStyle { get; set; }
		public bool IsDummyPlug { get; set; }
		// parentItemOverride
	}
}