using System;
using UnityEngine.Networking;


public static class ConfigLoad
{
    public static void SetupDownloadedItems(string url, Action<string[]> onItems, int ignoreRows = 1)
    {
        string text = ForceDownload(url);

        int rowIndex = 0;
        foreach (var myString in text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (rowIndex++ < ignoreRows)
                continue;

            var finalString = myString;
            if (finalString.EndsWith('\r'))
            {
                finalString = finalString.Substring(0, myString.Length - 1);
            }
            string[] lines = finalString.Split('\t');
            onItems?.Invoke(lines);
        }
    }

    private static string ForceDownload(string url)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);

        unityWebRequest.SendWebRequest();

        while (!unityWebRequest.downloadHandler.isDone)
        {

        }
        if (!string.IsNullOrEmpty(unityWebRequest.error))
        {
            return "";
        }

        return unityWebRequest.downloadHandler.text;
    }

}
