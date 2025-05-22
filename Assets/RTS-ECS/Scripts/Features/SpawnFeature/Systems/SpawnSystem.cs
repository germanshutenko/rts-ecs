using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace RtsEcs
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class SpawnSystem : ISystem
    {
        public World World { get; set; }

        private Filter Filter;
        private Stash<SpawnComponent> SpawnComponents;
        private Stash<CreateTankComponent> CreateTankComponents;

        public void OnAwake()
        {
            Filter = World.Filter
                .With<SpawnComponent>()
                .Build();

            SpawnComponents = World.GetStash<SpawnComponent>();
            CreateTankComponents = World.GetStash<CreateTankComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in Filter)
            {
                ref var spawnComponent = ref SpawnComponents.Get(entity);

                for (var i = 0; i < spawnComponent.Count; i++)
                {
                    var forward = Random.insideUnitCircle.normalized;
                    var forward3D = new Vector3(forward.x, 0, forward.y);
                    var position = Random.insideUnitSphere * 20f;
                    position.y = 0f;

                    var newEntity = World.CreateEntity();

                    CreateTankComponents.Add(newEntity, new CreateTankComponent
                    {
                        Position = position,
                        Rotation = Quaternion.LookRotation(forward3D),
                        Health = 100f,
                    });
                }

                SpawnComponents.Remove(entity);
            }
        }

        public void Dispose()
        {
            Filter = null;
        }
    }
}