using Scellecs.Morpeh;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace RtsEcs
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CreateTankSystem : ISystem
    {
        private Stash<HealthComponent> HealthComponents;
        private Stash<MovementComponent> MovementComponents;
        private Stash<CreateTankComponent> CreateTankComponents;
        private Stash<RoundMovingComponent> RoundMovingComponents;
        private Stash<UnitComponent> UnitComponents;

        public World World { get; set; }

        private Filter Filter;
        private Tank Prefab;

        public void OnAwake()
        {
            Filter = World.Filter
                .With<CreateTankComponent>()
                .Build();

            Prefab = Resources.Load<Tank>("Tank");

            UnitComponents = World.GetStash<UnitComponent>();
            HealthComponents = World.GetStash<HealthComponent>();
            MovementComponents = World.GetStash<MovementComponent>();
            CreateTankComponents = World.GetStash<CreateTankComponent>();
            RoundMovingComponents = World.GetStash<RoundMovingComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in Filter)
            {
                ref var createTank = ref CreateTankComponents.Get(entity);

                var unit = GameObject.Instantiate(Prefab, createTank.Position, createTank.Rotation);

                UnitComponents.Add(entity, new UnitComponent()
                {
                    Gun = unit.Gun,
                    Transform = unit.transform,
                });
                HealthComponents.Add(entity);
                ref var movementComponent = ref MovementComponents.Add(entity);
                RoundMovingComponents.Add(entity);
                movementComponent.MovementVector = unit.transform.forward;

                CreateTankComponents.Remove(entity);
            }
        }

        public void Dispose()
        {
            Filter = null;
        }
    }
}