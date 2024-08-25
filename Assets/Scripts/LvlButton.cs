using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlButton : MonoBehaviour
{
    public string prefsKeyStars,prefsKeyTimer;
    public int stars;
    public float timer;
    public GameObject star1, star2, star3;
    public Sprite emptyStar, fullStar;
    public Button lvlButton;
    public GameObject previosLvlButton;

    private void Awake()
    {

        timer = 1000;
        if (PlayerPrefs.HasKey(prefsKeyStars))
        {
            stars = PlayerPrefs.GetInt(prefsKeyStars);
        }
        if (PlayerPrefs.HasKey(prefsKeyTimer))
        {
            timer = PlayerPrefs.GetInt(prefsKeyTimer);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (stars == 1)
        {
            star1.GetComponent<SpriteRenderer>().sprite = fullStar;
        }
        if (stars == 2)
        {
            star1.GetComponent<SpriteRenderer>().sprite = fullStar;
            star2.GetComponent<SpriteRenderer>().sprite = fullStar;
        }
        if (stars == 3)
        {
            star1.GetComponent<SpriteRenderer>().sprite = fullStar;
            star2.GetComponent<SpriteRenderer>().sprite = fullStar;
            star3.GetComponent<SpriteRenderer>().sprite = fullStar;
        }

        if (previosLvlButton.GetComponent<LvlButton>().timer < 999) lvlButton.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
