namespace ZooWorld.Features.Animals.Runtime.Interactions
{
    public interface IAnimalInteractionRule
    {
        int Priority { get; }
        bool CanResolve(AnimalActor first, AnimalActor second);
        void Resolve(AnimalActor first, AnimalActor second);
    }
}
