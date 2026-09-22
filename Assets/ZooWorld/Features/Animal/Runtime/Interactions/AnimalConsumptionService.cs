using MessagePipe;
using ZooWorld.Features.Animals.Runtime.Messages;

namespace ZooWorld.Features.Animals.Runtime.Interactions
{
    public class AnimalConsumptionService
    {
        private readonly IPublisher<AnimalAteMessage> _animalAtePublisher;

        public AnimalConsumptionService(IPublisher<AnimalAteMessage> animalAtePublisher)
        {
            _animalAtePublisher = animalAtePublisher;
        }

        public void Eat(AnimalActor predator, AnimalActor victim)
        {
            victim.Die();
            _animalAtePublisher.Publish(new AnimalAteMessage(predator, victim));
        }
    }
}
