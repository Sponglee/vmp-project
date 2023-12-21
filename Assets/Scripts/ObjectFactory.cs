using UnityEngine;

public static class ObjectFactory 
{
    public static GameObject CreateObject(GameObject aObject, Vector3 aPosition)
    {
        GameObject tmpObject = GameObject.Instantiate(aObject, aPosition, Quaternion.identity);

        return tmpObject;
    }
}