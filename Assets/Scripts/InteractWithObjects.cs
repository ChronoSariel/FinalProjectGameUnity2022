using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InteractWithObjects : MonoBehaviour
{
    public Rigidbody RB;
    private float invincibilityTime = 1f;
    private float invincibilityTimer;
    private ScoreManager scoreManager;
    private AudioSource miscAudio;
    public AudioClip collectCoin;
    public AudioClip playerHurt;
    public AudioClip collectPowerUp;

    
    
    void Start()
    {
        miscAudio = GetComponent<AudioSource>();
        scoreManager = FindObjectOfType<ScoreManager>(); 
        invincibilityTimer = invincibilityTime;
    }

    void Update()
    {
        invincibilityTimer += Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider other) //Handle Interactions
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            scoreManager.AddScore(1);
            Destroy(other.gameObject);
            miscAudio.PlayOneShot(collectCoin, 1.0f);
        }
         if (other.gameObject.CompareTag("Goal"))
        {
            miscAudio.PlayOneShot(collectCoin, 1.0f);
            SceneManager.LoadScene("TitleScreen");
        }
         if (other.gameObject.CompareTag("Hazard") && invincibilityTimer >= invincibilityTime)
        {
            if (scoreManager.score <= 0)
            {
                SceneManager.LoadScene("TitleScreen");
            }
            else
            {
                Destroy(other.gameObject);
                scoreManager.AddScore(-scoreManager.score);
                GetComponent<Rigidbody>().AddForce(Vector3.up * 8, ForceMode.Impulse);
                miscAudio.PlayOneShot(playerHurt, 1.0f);
                invincibilityTimer = 0f;
            }
        }
         if (other.gameObject.CompareTag("Bouncy"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 36, ForceMode.Impulse);
            miscAudio.PlayOneShot(collectPowerUp, 1.0f);
        }
        if (other.gameObject.CompareTag("InstaKill"))
        {
                SceneManager.LoadScene("TitleScreen");
        }
    }
}