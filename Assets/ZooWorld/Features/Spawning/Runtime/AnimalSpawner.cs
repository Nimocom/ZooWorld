using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ZooWorld.Features.Animals.Runtime;
using ZooWorld.Shared.Runtime;

namespace ZooWorld.Features.Spawning.Runtime
{
    public class AnimalSpawner
    {
        private readonly AnimalCatalog _catalog;
        private readonly PlayArea _playArea;
        private readonly AnimalFactory _animalFactory;

        public AnimalSpawner(AnimalCatalog catalog, PlayArea playArea, AnimalFactory animalFactory)
        {
            _catalog = catalog;
            _playArea = playArea;
            _animalFactory = animalFactory;
        }

        public async UniTask Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var delay = UnityEngine.Random.Range(1f, 2f);
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cancellationToken);

                var definition = _catalog.GetRandom();
                var position = _playArea.GetRandomPosition();
                await _animalFactory.Create(definition, position);
            }
        }
    }
}
