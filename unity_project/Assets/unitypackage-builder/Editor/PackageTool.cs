using UnityEngine;
using UnityEditor;

public class PackageTool
{
	[MenuItem("Package/Update Package")]
	static void UpdatePackage()
	{
		AssetDatabase.ExportPackage(new string[] {"Assets/SqlCipher4Unity3D"}, "../SqlCipher4Unity3D.unitypackage", ExportPackageOptions.Recurse);
	}
}
