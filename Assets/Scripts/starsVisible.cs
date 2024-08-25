using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class starsVisible : MonoBehaviour
{
    public TextMeshProUGUI allStarsTxt;
    private int allStars;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("allStars"))
            allStars = PlayerPrefs.GetInt("allStars");
    }

    // Start is called before the first frame update
    void Start()
    {
        allStarsTxt.text = allStars.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
