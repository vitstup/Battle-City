using UnityEngine;
using Zenject;

public class HealthBarInstaller : MonoInstaller
{
    [SerializeField] private HealthBar healthBar;
    public override void InstallBindings()
    {
        Container.Bind<HealthBar>().FromComponentInHierarchy(healthBar).AsSingle().NonLazy();
    }
}