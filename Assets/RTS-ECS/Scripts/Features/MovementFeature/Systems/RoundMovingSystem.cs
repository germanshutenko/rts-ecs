using Scellecs.Morpeh;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace RtsEcs
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class RoundMovingSystem : ISystem
    {
        public World World { get; set; }

        private Filter Filter;
        private Stash<MovementComponent> MovementComponents;
        private Stash<RoundMovingComponent> RoundMovingComponents;

        public void OnAwake()
        {
            Filter = World.Filter
                .With<RoundMovingComponent>()
                .Build();

            MovementComponents = World.GetStash<MovementComponent>();
            RoundMovingComponents = World.GetStash<RoundMovingComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in Filter)
            {
                ref var unitComponent = ref MovementComponents.Get(entity);

                var rotation = Quaternion.AngleAxis(-45f * deltaTime, Vector3.up);
                unitComponent.MovementVector = rotation * unitComponent.MovementVector;
            }
        }

        public void Dispose()
        {
            Filter = null;
        }
    }
}