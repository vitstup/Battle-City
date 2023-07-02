using UnityEngine;
using Zenject;

public class LoseManager : MonoBehaviour
{
    [Inject] private LoseUI loseUI;

    public void Lose()
    {
        loseUI.ShowCanvas();

        Time.timeScale = 0;
    }
    
}