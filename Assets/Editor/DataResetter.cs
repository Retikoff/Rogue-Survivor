#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

public class DataResetter : MonoBehaviour
{
    public ScriptableObject myData;

    #if UNITY_EDITOR
    private string jsonBackup;

    private void Awake()
    {
        if (myData != null)
        {
            jsonBackup = EditorJsonUtility.ToJson(myData);
        }
    }

    private void OnDestroy()
    {
        if (myData != null && !string.IsNullOrEmpty(jsonBackup))
        {
            EditorJsonUtility.FromJsonOverwrite(jsonBackup, myData);
            EditorUtility.SetDirty(myData);
        }        
    }
    #endif
}
