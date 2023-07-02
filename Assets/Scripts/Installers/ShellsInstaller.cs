using UnityEngine;
using Zenject;

public class ShellsInstaller : MonoInstaller
{
    [SerializeField] private ShellPools shellPools;
    public override void InstallBindings()
    {
        Container.Bind<ShellPools>().FromComponentInHierarchy(shellPools).AsSingle().NonLazy();
    }
}