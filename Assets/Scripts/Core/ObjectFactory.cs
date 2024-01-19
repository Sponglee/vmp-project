using UnityEngine;

public static class ObjectFactory
{
    public static GameObject CreateObject(GameObject aObject, Vector3 aPosition = default)
    {
        GameObject tmpObject = GameObject.Instantiate(aObject, aPosition, Quaternion.identity);

        return tmpObject;
    }


    public static GameObject CreateObject(GameObject aObject, Transform aContainer, Vector3 aPosition = default,
        Quaternion rotation = default)
    {
        GameObject tmpObject = GameObject.Instantiate(aObject, aPosition, rotation, aContainer);

        return tmpObject;
    }


    public static T Create<T>(string aPrefabPath, Transform aParent = null)
    {
        GameObject tmpObject = (GameObject)GameObject.Instantiate(Resources.Load(aPrefabPath));

        if (aParent != null)
        {
            tmpObject.transform.SetParent(aParent, false);
        }

        var result = tmpObject.GetComponent<T>();

        if (result == null)
        {
            result = tmpObject.GetComponentInChildren<T>();
        }

        return result;
    }
}