using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Tank player;

    public override void InstallBindings()
    {
        Container.Bind<Tank>().FromComponentInHierarchy(player).AsSingle().NonLazy();
    }
}