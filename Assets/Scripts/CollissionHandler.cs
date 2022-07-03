
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollissionHandler : MonoBehaviour
{
    [SerializeField] float waitForSecond = 1f;
    [SerializeField] AudioClip deathExplotion;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem deathPartical;
    [SerializeField] ParticleSystem successPartical;


    AudioSource audioSource;

    bool isAudioAlive = false;
    bool collisionDisable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Cheat();
    }
    
    void Cheat()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable; //toggle collision
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isAudioAlive || collisionDisable)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "LandingPad":
                StartCoroutine(LoadNextScene());
                break;
            case "Obstacle":
                StartCoroutine(CrashSequence());
                break;
        }
       
        void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);

        }

        IEnumerator LoadNextScene()
        {
            if (!isAudioAlive)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(success);
                isAudioAlive = true;
            }
            successPartical.Play();
            GetComponent<MovementHandler>().enabled = false;
            yield return new WaitForSecondsRealtime(waitForSecond);
            LoadNextLevel();
        }



        IEnumerator CrashSequence()
        {
            if (!isAudioAlive)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(deathExplotion);
                isAudioAlive = true;
            }
            deathPartical.Play();
            GetComponent<MovementHandler>().enabled = false;
            yield return new WaitForSecondsRealtime(waitForSecond);
            ReloadScene();
        }

    }

     void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
   

