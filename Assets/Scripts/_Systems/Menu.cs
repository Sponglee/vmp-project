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

        // .. добавляйте здесь любые системы в рамках меню
    }

    public override void RemovedFromEngine()
    {
        // Remove<HealthBarController>();
        //
        // Remove<DamageUIController>();

        base.RemovedFromEngine();
    }
}