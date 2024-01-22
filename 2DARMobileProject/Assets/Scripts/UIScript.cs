using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Canvas desktopCanvas;
    public Canvas mobileCanvas;
    public Canvas pauseCanvas;
    public Canvas winCanvas;
    public Canvas loseCanvas;

    public bool desktopEnabled;
    public FloatingJoystick mobile_joystick;
    public FixedJoystick mobiletest_joystick;

    void Start()
    {
        Time.timeScale = 1;
        desktopCanvas = GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        winCanvas.enabled = false;
        loseCanvas.enabled = false;
        desktopEnabled = FindFirstObjectByType<PlayerMovement>().allowKeyControls;
        mobile_joystick = FindFirstObjectByType<PlayerMovement>().joystick;
        mobiletest_joystick = FindFirstObjectByType<PlayerMovement>().joystik;
    }
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (FindFirstObjectByType<PlayerMovement>().allowKeyControls)
        {
            desktopCanvas.enabled = true;
            mobileCanvas.enabled = false;
            mobile_joystick.GetComponent<Image>().enabled = false;
            mobiletest_joystick.GetComponent<Image>().enabled = false;
        }
        else
        {
            desktopCanvas.enabled = false;
            mobileCanvas.enabled = true;
            mobile_joystick.GetComponent<Image>().enabled = true;
            mobiletest_joystick.GetComponent<Image>().enabled = true;
        }
    }
    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        if(desktopEnabled)
        {
            desktopCanvas.enabled = false;
            FindFirstObjectByType<PlayerShoot>().shootEnabled = false;
        }
        else
        {
            mobile_joystick.GetComponent<Image>().enabled = false;
            mobileCanvas.enabled = false;
        }
        pauseCanvas.enabled = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (desktopEnabled)
        {
            desktopCanvas.enabled = true;
            FindFirstObjectByType<PlayerShoot>().shootEnabled = true;
        }
        else
        {
            mobile_joystick.GetComponent<Image>().enabled = true;
            mobileCanvas.enabled = true;
        }
        pauseCanvas.enabled = false;
    }

}
