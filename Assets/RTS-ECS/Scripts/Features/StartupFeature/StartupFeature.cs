using Scellecs.Morpeh;
using UnityEngine;

namespace RtsEcs
{
    public class StartupFeature
    {
        public StartupFeature(World world)
        {
            var group = world.CreateSystemsGroup();

            var isCreated = group.AddSystem(new StartGameSystem());

            var entity = world.CreateEntity();
            var stash = world.GetStash<StartGameComponent>();
            stash.Add(entity);

            world.AddSystemsGroup(0, group);
        }
    }
}