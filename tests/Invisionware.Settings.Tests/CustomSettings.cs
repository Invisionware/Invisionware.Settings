namespace Invisionware.Settings.Tests
{
	public class CustomSettings
	{
		public string String1 { get; set; } = "String1";
		public int Int1 { get; set; } = 1;
		public CustomSettingsNested Nested { get; set; } = new CustomSettingsNested();
	}

	public class CustomSettingsNested
	{
		public string String2 { get; set; } = "Nested.String2";
	}
}