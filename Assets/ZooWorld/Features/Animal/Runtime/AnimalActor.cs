using MessagePipe;
using R3;
using R3.Triggers;
using System;
using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Definitions;
using ZooWorld.Features.Animals.Runtime.Interactions;
using ZooWorld.Features.Animals.Runtime.Messages;
using ZooWorld.Features.Animals.Runtime.Movement.Contracts;
using ZooWorld.Shared.Runtime;
using ZooWorld.Shared.Runtime.Updates;

namespace ZooWorld.Features.Animals.Runtime
{
    public class AnimalActor : IFixedUpdatable
    {
        public AnimalDefinition Definition { get; }
        public AnimalActorView View { get; }

        private readonly IAnimalMovement _movement;
        private readonly AnimalInteractionResolver _interactionResolver;
        private readonly IPublisher<AnimalDiedMessage> _animalDiedPublisher;
        private readonly AnimalViewPool _viewPool;
        private readonly FixedUpdateService _fixedUpdateService;
        private readonly IDisposable _collisionSubscription;

        public bool IsAlive { get; private set; } = true;

        public AnimalActor(
            AnimalDefinition definition,
            AnimalActorView view,
            PlayArea playArea,
            AnimalInteractionResolver interactionResolver,
            IPublisher<AnimalDiedMessage> animalDiedPublisher,
            AnimalViewPool viewPool,
            FixedUpdateService fixedUpdateService)
        {
            Definition = definition;
            View = view;

            _interactionResolver = interactionResolver;
            _animalDiedPublisher = animalDiedPublisher;
            _viewPool = viewPool;
            _fixedUpdateService = fixedUpdateService;

            _movement = definition.Movement.CreateRuntime(view.Body, playArea);
            _collisionSubscription = view.Collider.OnCollisionEnterAsObservable().Subscribe(OnCollisionEnter);
        }

        public void FixedTick(float deltaTime)
        {
            _movement.FixedTick(deltaTime);
        }

        public void Collide(AnimalActor other)
        {
            _interactionResolver.Resolve(this, other);
        }

        public void Die()
        {
            IsAlive = false;

            _collisionSubscription.Dispose();
            _fixedUpdateService.Remove(this);
            _animalDiedPublisher.Publish(new AnimalDiedMessage(this));
            _viewPool.Release(Definition, View);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var otherActor = collision.gameObject.GetComponentInParent<AnimalActorView>()?.Actor;

            if (otherActor != null)
                Collide(otherActor);
        }
    }
}
