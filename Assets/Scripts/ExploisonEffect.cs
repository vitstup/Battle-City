using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExploisonEffect : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!particleSystem.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    public void PlayEffect(float effectSize)
    {
        var mainModule = particleSystem.main;
        mainModule.startSize = new ParticleSystem.MinMaxCurve(effectSize * 0.9f, effectSize * 1.1f);
        particleSystem.Play();
    }
}