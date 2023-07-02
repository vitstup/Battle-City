using UnityEngine;

public abstract class Booster : MonoBehaviour, IRevivable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entered(collision);
        Debug.Log("booster");
    }


    protected abstract void Entered(Collider2D collider);

    public void Revive()
    {
        gameObject.SetActive(true);
    }
}