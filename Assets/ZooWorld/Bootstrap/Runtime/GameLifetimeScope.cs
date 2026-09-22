using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZooWorld.Bootstrap.Runtime;
using ZooWorld.Features.Animals.Runtime;
using ZooWorld.Features.Animals.Runtime.Interactions;
using ZooWorld.Features.Animals.Runtime.Interactions.Rules;
using ZooWorld.Features.DeathCounter.Runtime;
using ZooWorld.Features.Spawning.Runtime;
using ZooWorld.Features.TastyFeedback.Runtime;
using ZooWorld.Shared.Runtime;
using ZooWorld.Shared.Runtime.Updates;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private AnimalCatalog _animalCatalog;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _ground;
    [SerializeField] private RectTransform _uiRoot;
    [SerializeField] private TastyLabelView _tastyLabelPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterMessagePipe();
        builder.RegisterComponentInHierarchy<DeathCounterView>();
        builder.RegisterInstance(_animalCatalog);
        builder.RegisterInstance(_camera);
        builder.RegisterInstance(_uiRoot);
        builder.RegisterInstance(_tastyLabelPrefab);
        builder.RegisterInstance(new PlayArea(_camera, _ground.transform.position.y));
        builder.Register<AnimalConsumptionService>(Lifetime.Singleton);
        builder.Register<PreyPreyInteractionRule>(Lifetime.Singleton).As<IAnimalInteractionRule>();
        builder.Register<PredatorPreyInteractionRule>(Lifetime.Singleton).As<IAnimalInteractionRule>();
        builder.Register<PredatorPredatorInteractionRule>(Lifetime.Singleton).As<IAnimalInteractionRule>();
        builder.Register<AnimalInteractionResolver>(Lifetime.Singleton);
        builder.Register<AnimalViewPool>(Lifetime.Singleton);
        builder.Register<TastyLabelPool>(Lifetime.Singleton);
        builder.RegisterEntryPoint<FixedUpdateService>().AsSelf();
        builder.Register<AnimalFactory>(Lifetime.Singleton);
        builder.Register<AnimalSpawner>(Lifetime.Singleton);
        builder.RegisterEntryPoint<DeathCounterPresenter>();
        builder.RegisterEntryPoint<TastyFeedbackPresenter>();
        builder.RegisterEntryPoint<GameBootstrapper>();
    }
}
