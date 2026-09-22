using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Enums;

namespace ZooWorld.Features.Animals.Runtime.Interactions.Rules
{
    public class PredatorPredatorInteractionRule : IAnimalInteractionRule
    {
        private readonly AnimalConsumptionService _consumptionService;

        public int Priority => 0;

        public PredatorPredatorInteractionRule(AnimalConsumptionService consumptionService)
        {
            _consumptionService = consumptionService;
        }

        public bool CanResolve(AnimalActor first, AnimalActor second)
        {
            return first.Definition.Role == AnimalRole.Predator && second.Definition.Role == AnimalRole.Predator;
        }

        public void Resolve(AnimalActor first, AnimalActor second)
        {
            if (first.Definition.Power > second.Definition.Power)
            {
                _consumptionService.Eat(first, second);
                return;
            }

            if (second.Definition.Power > first.Definition.Power)
            {
                _consumptionService.Eat(second, first);
                return;
            }

            if (Random.value < 0.5f)
                _consumptionService.Eat(first, second);
            else
                _consumptionService.Eat(second, first);
        }
    }
}
