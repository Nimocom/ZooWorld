using System;
using MessagePipe;
using VContainer.Unity;
using ZooWorld.Features.Animals.Runtime.Enums;
using ZooWorld.Features.Animals.Runtime.Messages;

namespace ZooWorld.Features.DeathCounter.Runtime
{
    public class DeathCounterPresenter : IStartable, IDisposable
    {
        private readonly DeathCounterView _view;
        private readonly ISubscriber<AnimalDiedMessage> _animalDiedSubscriber;

        private IDisposable _subscription;
        private int _deadPrey;
        private int _deadPredators;

        public DeathCounterPresenter(DeathCounterView view, ISubscriber<AnimalDiedMessage> animalDiedSubscriber)
        {
            _view = view;
            _animalDiedSubscriber = animalDiedSubscriber;
        }

        public void Start()
        {
            _view.Show(_deadPrey, _deadPredators);
            _subscription = _animalDiedSubscriber.Subscribe(OnAnimalDied);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnAnimalDied(AnimalDiedMessage message)
        {
            if (message.Animal.Definition.Role == AnimalRole.Prey)
                _deadPrey++;
            else
                _deadPredators++;

            _view.Show(_deadPrey, _deadPredators);
        }
    }
}
