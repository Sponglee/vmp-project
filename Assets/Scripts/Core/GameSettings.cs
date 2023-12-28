using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class GameSettings : MonoBehaviour
{
#if UNITY_EDITOR
    public static GameSettings instance => FindObjectOfType<GameSettings>();
#endif

    public ScriptableObject[] References;

    private static List<ScriptableObject> _references;

    private void Awake()
    {
        _references = References.ToList();
    }


    public static T GetReference<T>()
    {
        if (_references == null)
        {
            return default(T);
        }

        foreach (var r in _references.Where(r => r.GetType() == typeof(T)))
        {
            return (T)(object)r;
        }

        return default(T);
    }


#if UNITY_EDITOR
    public void UpdateReferences()
    {
        _references = References.ToList();
    }
#endif
}