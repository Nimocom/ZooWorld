using System.Collections.Generic;
using System.Linq;

namespace ZooWorld.Features.Animals.Runtime.Interactions
{
    public class AnimalInteractionResolver
    {
        private readonly IAnimalInteractionRule[] _rules;

        public AnimalInteractionResolver(IReadOnlyList<IAnimalInteractionRule> rules)
        {
            _rules = rules.OrderByDescending(rule => rule.Priority).ToArray();
        }

        public void Resolve(AnimalActor first, AnimalActor second)
        {
            if (!first.IsAlive || !second.IsAlive)
                return;

            foreach (var rule in _rules)
            {
                if (!rule.CanResolve(first, second))
                    continue;

                rule.Resolve(first, second);
                return;
            }
        }
    }
}
