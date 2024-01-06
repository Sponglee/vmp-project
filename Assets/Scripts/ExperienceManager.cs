using Anthill.Inject;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [Inject] public Game Game { get; set; }

    private ExperienceSettings _experienceSettings;

    private void Awake()
    {
        AntInject.InjectMono<ExperienceManager>(this);
        _experienceSettings = GameSettings.GetReference<ExperienceSettings>();
    }

    public void InitializeExperience(ref ExperienceComponent aComponent)
    {
        aComponent.CurrentExperience = 0f;
        aComponent.CurrentLevel = PlayerPrefs.GetInt(string.Format("CurrentPlayer{{0}}", aComponent.Id), 0);
        aComponent.MaxExperience = _experienceSettings.GetMaxExperience(aComponent.CurrentLevel);
    }

    public void LevelUp(ref ExperienceComponent aComponent)
    {
        aComponent.CurrentLevel++;
        PlayerPrefs.SetInt(string.Format("CurrentPlayer{{0}}", aComponent.Id), aComponent.CurrentLevel);

        aComponent.MaxExperience = _experienceSettings.GetMaxExperience(aComponent.CurrentLevel);
    }
}