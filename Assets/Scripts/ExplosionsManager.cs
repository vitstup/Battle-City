using UnityEngine;
using Zenject;

public class ExplosionsManager : MonoBehaviour
{
    [SerializeField] private ExploisonEffect effect;

    private MonoPool<ExploisonEffect> pool;

    [Inject] private DiContainer container;

    private void Start()
    {
        pool = new MonoPool<ExploisonEffect>(effect, 5, container);
    }

    public void ShowExplosion(Vector2 position, float effectSize)
    {
        var explosion = pool.GetElement();

        explosion.transform.position = position;

        explosion.PlayEffect(effectSize);
    }
}