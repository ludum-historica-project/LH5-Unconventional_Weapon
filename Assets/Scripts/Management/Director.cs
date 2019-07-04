using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Director
{
    private static Dictionary<string, Manager> managers;
    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {
        managers = new Dictionary<string, Manager>();
        SceneManager.LoadScene("Managers", LoadSceneMode.Additive);

    }

    public static T GetManager<T>() where T : Manager
    {
        string type = typeof(T).ToString();
        if (managers != null && managers.ContainsKey(type) && managers[type] != null)
        {
            return managers[type] as T;
        }
        Debug.LogWarning("Manager type " + type + " not found, returning null.");
        return null;
    }

    public static void SubscribeManager<T>(T manager) where T : Manager
    {
        managers[typeof(T).ToString()] = manager;
    }

}
