using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagment : MonoBehaviour
{
    [SerializeField] private int lvlToLoad;
    public InterAd interAd;
    private int adChance;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void NextLVL()
    {
        SceneManager.LoadScene(lvlToLoad);
        Time.timeScale = 1;
    }


}
