using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Enums;

namespace ZooWorld.Features.Animals.Runtime.Interactions.Rules
{
    public class PreyPreyInteractionRule : IAnimalInteractionRule
    {
        private const float CollisionImpulse = 8f;

        public int Priority => 0;

        public bool CanResolve(AnimalActor first, AnimalActor second)
        {
            return first.Definition.Role == AnimalRole.Prey && second.Definition.Role == AnimalRole.Prey;
        }

        public void Resolve(AnimalActor first, AnimalActor second)
        {
            var direction = first.View.Body.position - second.View.Body.position;
            direction.y = 0f;
            first.View.Body.AddForce(direction.normalized * CollisionImpulse, ForceMode.Impulse);
        }
    }
}
