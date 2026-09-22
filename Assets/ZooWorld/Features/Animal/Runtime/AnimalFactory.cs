using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Definitions;
using ZooWorld.Features.Animals.Runtime.Interactions;
using ZooWorld.Features.Animals.Runtime.Messages;
using ZooWorld.Shared.Runtime;
using ZooWorld.Shared.Runtime.Updates;

namespace ZooWorld.Features.Animals.Runtime
{
    public class AnimalFactory
    {
        private readonly AnimalViewPool _viewPool;
        private readonly PlayArea _playArea;
        private readonly AnimalInteractionResolver _interactionResolver;
        private readonly IPublisher<AnimalDiedMessage> _animalDiedPublisher;
        private readonly FixedUpdateService _fixedUpdateService;

        public AnimalFactory(AnimalViewPool viewPool, PlayArea playArea, AnimalInteractionResolver interactionResolver, IPublisher<AnimalDiedMessage> animalDiedPublisher, FixedUpdateService fixedUpdateService)
        {
            _viewPool = viewPool;
            _playArea = playArea;

            _interactionResolver = interactionResolver;
            _animalDiedPublisher = animalDiedPublisher;
            _fixedUpdateService = fixedUpdateService;
        }

        public async UniTask<AnimalActor> Create(AnimalDefinition definition, Vector3 position)
        {
            var view = await _viewPool.Get(definition, position);
            var actor = new AnimalActor(definition, view, _playArea, _interactionResolver, _animalDiedPublisher, _viewPool, _fixedUpdateService);

            view.Bind(actor);

            _fixedUpdateService.Add(actor);

            return actor;
        }
    }
}
