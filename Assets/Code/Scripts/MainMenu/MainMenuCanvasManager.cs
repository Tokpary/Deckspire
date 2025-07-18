using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvasManager : MonoBehaviour
{
    
    //Variables

    [SerializeField] private Animator cameraAnimation;
    [SerializeField] private Animator fadeAnimator;
    
    //Metodos

    public void OnPlayPressed()
    {
        fadeAnimator.SetBool("FadeOut", true);
    }
    
    
    public void OnCreditsPressed()
    {
        cameraAnimation.SetBool("Credits", true);
    }
    
    
    public void OnBackPressed()
    {
        cameraAnimation.SetBool("Credits", false);
    }
    
    public void OnExitPressed()
    {
        Application.Quit();
    }
}