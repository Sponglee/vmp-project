using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFX
{
    public class LookAtTarget : MonoBehaviour
    {
        public Transform Target;

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(Target);
        }
    }
}