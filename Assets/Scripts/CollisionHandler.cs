using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionFX;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>())
        {
            GetComponent<Movement>().enabled = false;
            collisionFX.Play();
            GetComponent<AudioSource>().Stop();
            collisionFX.GetComponent<AudioSource>().Play();
            Invoke("ReloadScene", 3f);
        }

        else if(collision.gameObject.GetComponent<LandingPad>())
        {
            Debug.Log("You Win");
        }
    }

    void ReloadScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
}
