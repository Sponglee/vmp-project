using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public float currentExperience;
    public float totalExperience;


    public float GetCurrentExperience()
    {
        return currentExperience;
    }


    public float GetMaxExperience()
    {
        return totalExperience;
    }
}