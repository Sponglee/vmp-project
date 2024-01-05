using Anthill.Core;

public class Menu : AntScenario
{
    public Menu() : base("Menu")
    {
        // ...
    }

    public override void AddedToEngine()
    {
        base.AddedToEngine();

        Add<HealthBarController>();
        Add<ExperienceBarController>();
        Add<ChoiceController>();
        Add<LobbyController>();
    }

    public override void RemovedFromEngine()
    {
        base.RemovedFromEngine();

        Remove<HealthBarController>();
        Remove<ExperienceBarController>();
        Remove<ChoiceController>();
        Remove<LobbyController>();
    }
}