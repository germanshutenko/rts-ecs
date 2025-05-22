using Scellecs.Morpeh;

namespace RtsEcs
{
    public class MovementFeature
    {
        public MovementFeature(World world)
        {
            var group = world.CreateSystemsGroup();

            group.AddSystem(new MovementSystem());
            group.AddSystem(new RoundMovingSystem());
            world.AddSystemsGroup(2, group);
        }
    }
}