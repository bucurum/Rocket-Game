using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    float movementFactor;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        NewMethod();
    }

    private void NewMethod()
    {
        if(period != 0)
        {
            float cycles = Time.time / period; //Continually growing over time

            const float tau = Mathf.PI * 2; //constant value for 6.283
            float rawSinWave = Mathf.Sin(cycles * tau); //going -1 to 1

            movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1

            Vector3 offset = movementFactor * movementVector;
            transform.position = startingPosition + offset;
        }
        else
        {
            return;
        }
    }
}
