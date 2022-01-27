using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float thrustForce = 1;
    [SerializeField] float initialTorque = 1;
    [SerializeField] float fuel = 100;
    [SerializeField] float fuelBurnRate = 1;

    [SerializeField] Material w_Material;

    [SerializeField] Slider fuelSlider;
    [SerializeField] ParticleSystem[] thrusters;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rb.AddRelativeTorque(new Vector3(0, 0, initialTorque) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Fuel is " + fuel);
        ProcessInput();
        CheckFuel();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.down * thrustForce * Time.deltaTime);
            Debug.Log("W is being pressed");
            PlayRocketSFX();
            PlayThrusterParticleFX(thrusters[0]);
            fuel = fuel - fuelBurnRate * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.right * thrustForce * Time.deltaTime);
            Debug.Log("A is being pressed");
            PlayRocketSFX();
            PlayThrusterParticleFX(thrusters[1]);
            fuel = fuel - fuelBurnRate * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            Debug.Log("S is being pressed");
            PlayRocketSFX();
            PlayThrusterParticleFX(thrusters[2]);
            fuel = fuel - fuelBurnRate * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.left * thrustForce * Time.deltaTime);
            Debug.Log("D is being pressed");
            PlayRocketSFX();
            PlayThrusterParticleFX(thrusters[3]);
            fuel = fuel - fuelBurnRate * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            w_Material.color = Color.white;
        }

        else
        {
            audioSource.Stop();
            StopAllThrusterParticleFX();
        }
    }

    private void PlayRocketSFX()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    private void PlayThrusterParticleFX(ParticleSystem thruster)
    {
        StopAllThrusterParticleFX();
        if (!thruster.isPlaying)
        {
            Debug.Log("PlayingSFX");
            thruster.Play();
        }
    }

    private void StopAllThrusterParticleFX()
    {
        foreach (ParticleSystem thruster in thrusters)
        {
            if (thruster.isPlaying)
                thruster.Stop();
        }
    }

    private void CheckFuel()
    {
        fuelSlider.value = fuel;
        if (fuel <= 0)
        {
            GetComponent<Movement>().enabled = false;
            audioSource.enabled = false;
        }
    }
}

