using System;
using System.Collections;
using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public List<SystemsGroup> SystemsGroups = new List<SystemsGroup>();
    public World World { get; set; }

    private void Awake()
    {
        World = World.Default;
        InitializeSystemGroups();
    }

    public void InitializeSystemGroups()
    {
        var systemsGroup = World.CreateSystemsGroup();

        systemsGroup.AddSystem(new GameplaySystem());
        systemsGroup.AddSystem(new HealthSystem());
        systemsGroup.AddSystem(new EnemySpawnSystem());
        systemsGroup.AddSystem(new OffScreenSystem());
        systemsGroup.AddSystem(new AttackSystem());
        systemsGroup.AddSystem(new DamageSystem());
        systemsGroup.AddSystem(new PickupSystem());
        systemsGroup.AddSystem(new OrbSpawnSystem());
        systemsGroup.AddSystem(new DeathSystem());

        systemsGroup.AddSystem(new MovementSystem());
        systemsGroup.AddSystem(new OffScreenMovementSystem());


        World.AddSystemsGroup(SystemsGroups.Count, systemsGroup);
    }

    public void DisableScenario(string aScenarioName)
    {
    }

    public void EnableScenario(string aScenarioName)
    {
    }
}