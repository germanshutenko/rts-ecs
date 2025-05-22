using Scellecs.Morpeh;

namespace RtsEcs
{
    public class SpawnFeature
    {
        public SpawnFeature(World world)
        {
            var group = world.CreateSystemsGroup();

            group.AddSystem(new SpawnSystem());

            world.AddSystemsGroup(3, group);
        }
    }
}