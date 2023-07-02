using UnityEngine;
using Zenject;

public class LoseInstaller : MonoInstaller
{
    [SerializeField] private LoseManager loseManager;
    [SerializeField] private LoseUI loseUI;

    public override void InstallBindings()
    {
        Container.Bind<LoseManager>().FromComponentInHierarchy(loseManager).AsSingle().NonLazy();
        Container.Bind<LoseUI>().FromComponentInHierarchy(loseUI).AsSingle().NonLazy();
    }
}