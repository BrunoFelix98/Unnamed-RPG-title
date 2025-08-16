using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager managerInstance;

    public bool firstTime;

    //SoundsSettings
    public Slider masterVolumeSlider;
    public Slider BGMVolumeSlider;
    public Slider voicesVolumeSlider;
    public Slider interactionsVolumeSlider;
    public Slider combatVolumeSlider;

    private void Awake()
    {
        if (managerInstance == null)
        {
            managerInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplySettings()
    {

    }

    public void GoToGameScene()
    {
        if (firstTime) //Replace with playerPrefs in the future
        {

        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void QuitGame()
    {
        Application.Quit(); //Not good for mobile version
    }
}
