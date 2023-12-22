using UnityEngine;
namespace Anthill
{
    public static class ObjectCreator
    {
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

}
