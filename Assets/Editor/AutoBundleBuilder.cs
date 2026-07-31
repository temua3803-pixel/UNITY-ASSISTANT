using UnityEditor;
using System.IO;

public class AutoBundleBuilder
{
    public static void Build()
    {
        // Автоматически находим сцену и даем ей имя бандла
        string[] guids = AssetDatabase.FindAssets("t:Scene");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AssetImporter importer = AssetImporter.GetAtPath(path);
            if (importer != null)
            {
                importer.assetBundleName = Path.GetFileNameWithoutExtension(path).ToLower();
            }
        }

        // Создаем папку и собираем бандл для Android
        string outputDir = "Assets/StreamingAssets";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        BuildPipeline.BuildAssetBundles(outputDir, BuildAssetBundleOptions.None, BuildTarget.Android);
        AssetDatabase.Refresh();
    }
}
