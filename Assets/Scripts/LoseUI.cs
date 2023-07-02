using UnityEngine;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private Canvas loseCanvas;

    public void ShowCanvas()
    {
        loseCanvas.gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        loseCanvas.gameObject.SetActive(false);
    }
}