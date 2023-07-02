using UnityEngine;
using Zenject;

public class RestartInstaller : MonoInstaller
{
    [SerializeField] private RestartManager restartManager;

    public override void InstallBindings()
    {
        Container.Bind<RestartManager>().FromComponentInHierarchy(restartManager).AsSingle().NonLazy();
    }
}