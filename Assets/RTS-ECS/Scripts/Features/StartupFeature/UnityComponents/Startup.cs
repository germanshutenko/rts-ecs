using Scellecs.Morpeh;
using UnityEngine;

namespace RtsEcs
{
    public class Startup : MonoBehaviour
    {
        private void Awake()
        {
            var world = World.Default;
            world.UpdateByUnity = true;

            var startupFeature = new StartupFeature(world);
            var unitsFeature = new UnitsFeature(world);
            var movementFeature = new MovementFeature(world);
            var healthFeature = new HealthFeature(world);
            var spawnFeature = new SpawnFeature(world);
        }
    }
}