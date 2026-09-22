namespace ZooWorld.Features.Animals.Runtime.Messages
{
    public class AnimalDiedMessage
    {
        public AnimalActor Animal { get; }

        public AnimalDiedMessage(AnimalActor animal)
        {
            Animal = animal;
        }
    }
}
