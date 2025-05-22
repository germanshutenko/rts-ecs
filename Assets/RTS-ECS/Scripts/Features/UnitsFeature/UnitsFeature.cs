using Scellecs.Morpeh;

namespace RtsEcs
{
    public class UnitsFeature
    {
        public UnitsFeature(World world)
        {
            var group = world.CreateSystemsGroup();

            group.AddSystem(new CreateTankSystem());

            world.AddSystemsGroup(1, group);
        }
    }
}