using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image bar;

    public void SetBar(float fill)
    {
        bar.fillAmount = fill;
    }
}