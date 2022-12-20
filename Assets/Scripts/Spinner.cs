using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float xRotationAngle = 0f;
    [SerializeField] float yRotationAngle = 0f;
    [SerializeField] float zRotationAngle = 0f;

    void Update()
    {
        transform.Rotate(xRotationAngle / 10, yRotationAngle / 10 , zRotationAngle / 10);
    }
}
