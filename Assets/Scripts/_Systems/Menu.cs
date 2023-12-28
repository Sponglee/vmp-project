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
    }

    public override void RemovedFromEngine()
    {
        Remove<HealthBarController>();
        Remove<ExperienceBarController>();
        Remove<ChoiceController>();
        base.RemovedFromEngine();
    }
}