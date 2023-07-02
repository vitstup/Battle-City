using UnityEngine;

public class Hull : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform objectTransform;

    public void MoveForward()
    {
        objectTransform.transform.position += transform.up * movementSpeed * Time.deltaTime;
    }

    public void MoveBackward()
    {
        objectTransform.transform.position += transform.up * movementSpeed * Time.deltaTime * -1;
    }

    public void Rotate(float angle)
    {
        objectTransform.transform.Rotate(Vector3.forward, angle * rotationSpeed * Time.deltaTime * 60);
    }

}