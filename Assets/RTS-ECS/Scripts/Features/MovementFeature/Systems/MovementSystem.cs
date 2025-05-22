using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace RtsEcs
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class MovementSystem : ISystem
    {
        public World World { get; set; }

        private Filter Filter;
        private Stash<UnitComponent> UnitComponents;
        private Stash<MovementComponent> MovementComponents;

        public void OnAwake()
        {
            Filter = World.Filter
                .With<UnitComponent>()
                .With<MovementComponent>()
                .Build();

            UnitComponents = World.GetStash<UnitComponent>();
            MovementComponents = World.GetStash<MovementComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in Filter)
            {
                ref var unitComponent = ref UnitComponents.Get(entity);
                ref var movementComponent = ref MovementComponents.Get(entity);

                var newPosition = unitComponent.Transform.position + movementComponent.MovementVector * deltaTime;
                unitComponent.Transform.position = newPosition;
                unitComponent.Transform.rotation = Quaternion.LookRotation(movementComponent.MovementVector);
            }
        }

        public void Dispose()
        {
            Filter = null;
        }
    }
}