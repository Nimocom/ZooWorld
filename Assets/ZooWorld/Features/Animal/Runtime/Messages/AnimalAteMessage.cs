namespace ZooWorld.Features.Animals.Runtime.Messages
{
    public class AnimalAteMessage
    {
        public AnimalActor Predator { get; }
        public AnimalActor Victim { get; }

        public AnimalAteMessage(AnimalActor predator, AnimalActor victim)
        {
            Predator = predator;
            Victim = victim;
        }
    }
}
