using UnityEngine;
using UnityEditor;

public class PackageTool
{
	[MenuItem("Package/Update Package")]
	static void UpdatePackage()
	{
		const string VERSION = "0.0.1";
		AssetDatabase.ExportPackage(
			new string[] {"Assets/unity.webp"},
			string.Format("../webp-{0}.unitypackage", VERSION),
			ExportPackageOptions.Recurse
		);
	}
}
