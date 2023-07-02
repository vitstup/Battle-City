using Zenject;
using UnityEngine;

public class ExploisonsInstaller : MonoInstaller
{
    [SerializeField] private ExplosionsManager exploisonsManager;
    public override void InstallBindings()
    {
        Container.Bind<ExplosionsManager>().FromComponentInHierarchy(exploisonsManager).AsSingle().NonLazy();
    }
}