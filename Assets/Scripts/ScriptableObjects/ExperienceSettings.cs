using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ExperienceSettings", menuName = "ScriptableObjects/ExperienceSettings", order = 1)]
public class ExperienceSettings : ScriptableObject
{
    public float startMaxExperience;
    public float experienceCurveMultiplier = 10f;
    public int maxLevels = 100;
    public AnimationCurve experienceCurve;

    public float GetMaxExperience(int aLevel)
    {
        return startMaxExperience +
               experienceCurveMultiplier * experienceCurve.Evaluate((float)aLevel / (float)maxLevels);
    }
}