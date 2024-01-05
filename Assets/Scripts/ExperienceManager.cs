using Anthill.Inject;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [Inject] public Game Game { get; set; }

    public float currentExperience;
    public float currentMaxExperience;
    public int currentLevel = 0;

    private ExperienceSettings _experienceSettings;

    private void Awake()
    {
        AntInject.InjectMono<ExperienceManager>(this);
        _experienceSettings = GameSettings.GetReference<ExperienceSettings>();

        currentLevel = PlayerPrefs.GetInt("CurrentPlayer", 0);
        currentMaxExperience = _experienceSettings.GetMaxExperience(currentLevel);
    }

    public void LevelUp()
    {
        currentLevel++;
        PlayerPrefs.SetInt("CurrentPlayer", currentLevel);

        currentMaxExperience = _experienceSettings.GetMaxExperience(currentLevel);
    }

    public float GetCurrentExperience()
    {
        return currentExperience;
    }

    public float GetMaxExperience()
    {
        return currentMaxExperience;
    }
}