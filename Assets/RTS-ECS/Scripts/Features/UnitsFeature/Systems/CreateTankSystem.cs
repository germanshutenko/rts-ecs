using Scellecs.Morpeh;
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

        public World World { get; set; }

        private Filter Filter;
        private UnitProvider Prefab;

        public void OnAwake()
        {
            Filter = World.Filter
                .With<CreateTankComponent>()
                .Build();

            Prefab = Resources.Load<UnitProvider>("Tank");

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

                var unit = GameObject.Instantiate(Prefab);

                unit.transform.position = createTank.Position;
                unit.transform.rotation = createTank.Rotation;

                HealthComponents.Add(unit.Entity);
                ref var movementComponent = ref MovementComponents.Add(unit.Entity);
                RoundMovingComponents.Add(unit.Entity);
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