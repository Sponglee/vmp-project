using UnityEngine;


[CreateAssetMenu(fileName = "ChoiceLibrary", menuName = "ScriptableObjects/ChoiceLibrary", order = 1)]
public class ChoiceLibrary : ScriptableObject
{
    public ChoiceData[] ChoicePool;


    public ChoiceData[] GetChoices()
    {
        ChoiceData[] tmpChoices = new ChoiceData[2];
        for (int i = 0; i < tmpChoices.Length; i++)
        {
            tmpChoices[i] = ChoicePool[Random.Range(0, ChoicePool.Length)];
        }

        return tmpChoices;
    }
}