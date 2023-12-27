using System.Collections;
using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public Installer[] Installers;

    public void DisableScenario(string aScenarioName)
    {
        for (int i = 0; i < Installers.Length; i++)
        {
            if (Installers[i].name == aScenarioName)
            {
                for (int j = 0; j < Installers[i].updateSystems.Length; j++)
                {
                    Installers[i].updateSystems[j].Enabled = false;
                }

                Installers[i].enabled = false;
            }
        }
    }

    public void EnableScenario(string aScenarioName)
    {
        for (int i = 0; i < Installers.Length; i++)
        {
            if (Installers[i].name == aScenarioName)
            {
                for (int j = 0; j < Installers[i].updateSystems.Length; j++)
                {
                    Installers[i].updateSystems[j].Enabled = true;
                }

                Installers[i].enabled = true;
            }
        }
    }
}