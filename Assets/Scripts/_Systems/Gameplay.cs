using Anthill.Core;

public class Gameplay : AntScenario
{
    public Gameplay() : base("Gameplay")
    {
        // ...
    }

    public override void AddedToEngine()
    {
        base.AddedToEngine();
        // .. добавляйте здесь любые системы в рамках геймплея
    }

    public override void RemovedFromEngine()
    {
      
       
        base.RemovedFromEngine();
    }
}