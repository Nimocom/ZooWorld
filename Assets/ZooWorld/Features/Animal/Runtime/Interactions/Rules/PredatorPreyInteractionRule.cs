using ZooWorld.Features.Animals.Runtime.Enums;

namespace ZooWorld.Features.Animals.Runtime.Interactions.Rules
{
    public class PredatorPreyInteractionRule : IAnimalInteractionRule
    {
        private readonly AnimalConsumptionService _consumptionService;

        public int Priority => 0;

        public PredatorPreyInteractionRule(AnimalConsumptionService consumptionService)
        {
            _consumptionService = consumptionService;
        }

        public bool CanResolve(AnimalActor first, AnimalActor second)
        {
            return first.Definition.Role != second.Definition.Role;
        }

        public void Resolve(AnimalActor first, AnimalActor second)
        {
            var predator = first.Definition.Role == AnimalRole.Predator ? first : second;
            var victim = first.Definition.Role == AnimalRole.Prey ? first : second;
            _consumptionService.Eat(predator, victim);
        }
    }
}
