using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float sensivity, timer, bombTimer, timerHighScore,star1Score,star2Score,star3Score;
    public int starsCount, starsMemory, tryCount, allStars;
    public InterAd interAd;

    public ParticleSystem explosion;

    public bool alive,win;

    public Text timerTXT,bombTimerTXT;
    public TextMeshProUGUI finishTimerTXT;
    public Canvas finishCanvas,dieCanvas,gameCanvas;
    public Sprite starFull, starEmpty;
    public Sprite[] timerState;

    public GameObject fracturedBall, newHighScoreUI,star1, star2, star3,timerImg;
    public CameraMovement cameraScript;

    public string prefsKeyTimer,prefsKeyStars;

    [SerializeField] private SounManager soundManager;
    private Rigidbody rb;
    private float timerspeed = 1f;

    private void Awake()
    {   
        timerHighScore = 1000;
        starsMemory = 0;
        if (PlayerPrefs.HasKey(prefsKeyTimer))
        {
            timerHighScore = PlayerPrefs.GetFloat(prefsKeyTimer);
        }
        if (PlayerPrefs.HasKey(prefsKeyStars))
        {
            starsMemory = PlayerPrefs.GetInt(prefsKeyStars);
        }
        if (PlayerPrefs.HasKey("allStars"))
        {
            allStars = PlayerPrefs.GetInt("allStars");
        }

        tryCount = PlayerPrefs.GetInt("TryCount");

    }

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        alive = true;
    }
    private void FixedUpdate()
    {
        AccelerationAcces();
    }

    private void Update()
    {

        if(timer <= star3Score) timerImg.GetComponent<SpriteRenderer>().sprite = timerState[0];
        if(timer > star3Score) if (timer <= star1Score) timerImg.GetComponent<SpriteRenderer>().sprite = timerState[1];
        if(timer <= star1Score) if (timer >= star2Score) timerImg.GetComponent<SpriteRenderer>().sprite = timerState[2];
        if (timer >= star1Score) timerImg.GetComponent<SpriteRenderer>().sprite = timerState[3];

        timer += 1 * Time.deltaTime;
        bombTimer -= timerspeed * Time.deltaTime;
        timerTXT.text = timer.ToString("0.000");
        bombTimerTXT.text = bombTimer.ToString("0.000");
        if (Input.GetKeyDown(KeyCode.Mouse3))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Keys Deleted");
        }
        if(bombTimer <= 0)
        {
            tryCount += 1;
            PlayerPrefs.SetInt("TryCount", tryCount);
            if (tryCount >= 1)
            {
                StartCoroutine(ShowAdDelayed());
            }

            Explode();
            timerspeed = 0;
            bombTimer = 1;
        }
        if (win == true) timerspeed = 0;
    }

    IEnumerator ShowAdDelayed()
    {
        yield return new WaitForSeconds(1f);
        interAd.ShowAd();
        tryCount = 0;
        PlayerPrefs.SetInt("TryCount", tryCount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Diezone")
        {
            tryCount += 1;
            PlayerPrefs.SetInt("TryCount", tryCount);
            if (tryCount >= 2)
            {
                StartCoroutine(ShowAdDelayed());
            }

            soundManager.Broken();
            Instantiate(fracturedBall, transform.position, Quaternion.identity);//Break a ball
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fall")
        {
            tryCount += 1;
            PlayerPrefs.SetInt("TryCount", tryCount);
            if (tryCount >= 1)
            {
                interAd.ShowAd();
                tryCount = 0;
                PlayerPrefs.SetInt("TryCount", tryCount);
            }

            cameraScript.player = null;
            soundManager.Fall();
            soundManager.Lose();
            Die();
        }
        
        if(other.tag == "Finish")
        {
            Finish();
        }
    }


    public void Finish()
    {
        win = true;
        cameraScript.player = null;
        soundManager.Victory();
        //Finish function
        gameCanvas.gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(true);

        finishTimerTXT.text = timer.ToString("0.00");

        if (timerHighScore > timer)
        {
            PlayerPrefs.SetFloat(prefsKeyTimer, timer);
            newHighScoreUI.SetActive(true);
        }
        if (timer <= star1Score) starsCount = 1;
        if (timer <= star2Score) starsCount = 2;
        if (timer <= star3Score) starsCount = 3;

        //Code that change an empty star sprite to full star sprite
        if (starsCount == 1)
        {           
            star1.GetComponent<SpriteRenderer>().sprite = starFull;
        }
        if (starsCount == 2)
        {
            star1.GetComponent<SpriteRenderer>().sprite = starFull;
            star2.GetComponent<SpriteRenderer>().sprite = starFull;
        }
        if (starsCount == 3)
        {
            star1.GetComponent<SpriteRenderer>().sprite = starFull;
            star2.GetComponent<SpriteRenderer>().sprite = starFull;
            star3.GetComponent<SpriteRenderer>().sprite = starFull;
        }

        if (starsCount > starsMemory) PlayerPrefs.SetInt(prefsKeyStars, starsCount);//Remember a acount of a stars
        int starsMinus = starsCount -= starsMemory;
        if (starsMinus > 0) allStars += starsMinus;
        PlayerPrefs.SetInt("allStars", allStars);

        sensivity = 0;
        VisualInactive();
    }

    public void Die()
    {
        soundManager.Lose();
        VisualInactive();
        //Die function



        cameraScript.player = null;//Camera stop to follow
        alive = false;

        gameCanvas.gameObject.SetActive(false);//Game UI disabled

        StartCoroutine(DieCourotine());
    }

    public void Explode()
    {
        soundManager.Explode();
        soundManager.Lose();
        VisualInactive();
        Instantiate(explosion, transform.position, Quaternion.identity);//Explosion

        cameraScript.player = null;//camera stop following
        gameCanvas.gameObject.SetActive(false);//Game ui disabled
        alive = false;
        StartCoroutine(DieCourotine());
    }

    private IEnumerator DieCourotine()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        dieCanvas.gameObject.SetActive(true);//You Loose menu enabled

    }

    void VisualInactive()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<SphereCollider>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        GameObject rope = GameObject.Find("Rope");
        Destroy(rope);
    }

    void AccelerationAcces()
    {
        if (alive)
        {
            Vector3 acceleration = Input.acceleration;

            rb.AddForce(new Vector3(acceleration.x * sensivity, -2, acceleration.y * sensivity * 1.2f), ForceMode.Acceleration);
        }
    }

}