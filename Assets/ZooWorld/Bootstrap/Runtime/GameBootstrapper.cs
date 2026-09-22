using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;
using ZooWorld.Features.Spawning.Runtime;

namespace ZooWorld.Bootstrap.Runtime
{
    public class GameBootstrapper : IStartable, IDisposable
    {
        private readonly AnimalSpawner _animalSpawner;
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public GameBootstrapper(AnimalSpawner animalSpawner)
        {
            _animalSpawner = animalSpawner;
        }

        public void Start()
        {
            _animalSpawner.Run(_cancellationTokenSource.Token).Forget();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}
