using UnityEditor;

public class PackageTool
{
    [MenuItem("Package/Update Package")]
    private static void UpdatePackage()
    {
        const string VERSION = "0.3.9";
        AssetDatabase.ExportPackage(
            new[] {"Assets/unity.webp"},
            $"../webp-{VERSION}.unitypackage",
            ExportPackageOptions.Recurse
        );
    }
}
