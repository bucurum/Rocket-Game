using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] float speedValue = 5f;
    [SerializeField] float rotationValue = 5f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem particlesMainThrust;
    [SerializeField] ParticleSystem particlesLeftThrust;
    [SerializeField] ParticleSystem particlesRightThrust;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessForward();
        ProcessRotation();
    }

    void ProcessForward()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartTrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            return;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            StartRotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartTrusting()
    {
        if (!particlesMainThrust.isPlaying) 
        {
            //the main thrust particle sfx
            particlesMainThrust.Play();
        }
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * speedValue);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }
    private void StopThrusting()
    {
        audioSource.Pause();
        particlesMainThrust.Stop();
    }

    private void StartRotateLeft()
    {
        ApplyRotation(rotationValue);
        if (!particlesRightThrust.isPlaying)
        {
            particlesRightThrust.Play();
        }
    }
    private void StartRotateRight()
    {
        ApplyRotation(-rotationValue);
        if (!particlesLeftThrust.isPlaying)
        {
            particlesLeftThrust.Play();
        }
    }

    private void StopRotating()
    {
        particlesLeftThrust.Stop();
        particlesRightThrust.Stop();
    }


    void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateThisFrame);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
