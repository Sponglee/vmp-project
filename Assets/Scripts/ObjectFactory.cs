using UnityEngine;

public static class ObjectFactory 
{
    public static GameObject CreateObject(GameObject aObject, Vector3 aPosition)
    {
        GameObject tmpObject = GameObject.Instantiate(aObject, aPosition, Quaternion.identity);

        return tmpObject;
    }
    
    
    public static GameObject CreateObject(GameObject aObject, Vector3 aPosition, Transform aContainer)
    {
        GameObject tmpObject = GameObject.Instantiate(aObject, aPosition, Quaternion.identity, aContainer);

        return tmpObject;
    }
}