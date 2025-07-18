using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    
    //Variables
    
    //Camara
    [SerializeField] private GameObject camera;
    private Vector3 cameraPos = new (0.83f,2.36f,-3.3f);
    private Vector3 cameraRot = new (13.86f, 0, 0);
    
    private Vector3 cameraDeckPosition = new (0.83f,2f,-2.7f);
    private Vector3 cameraDeckRotation = new (4.37f,0,0);
    
    private Vector3 cameraBallPosition = new (0.3f,2,-2.4f);
    private Vector3 cameraBallRotation = new (3.9f,333.8f,358);
    
    private Vector3 cameraClockPosition = new (1.55f,2,-2.6f);
    private Vector3 cameraClockRotation = new (4.3f,3,0);
    
    private Vector3 cameraBellPosition = new (1.45f,2.35f,-2.4f);
    private Vector3 cameraBellRotation = new (4.35f,4.8f,0.3f);
    
    //Mazo
    [SerializeField] private GameObject deck;
    private Vector3 deckPos = new (0.83f,1.8f,-2.27f);
    
    //Bola de Cristal
    [SerializeField] private GameObject ball;
    private Vector3 ballPos = new (-0.177f, 2.035f, -2.015f);
    
    
    //Campana
    [SerializeField] private GameObject bell;
    private Vector3 bellPos = new (2.4f,2.8f,-1.25f);
    
    //Reloj
    [SerializeField] private GameObject clock;
    private Vector3 clockPos = new (2,2,-2);
    
    
    //Metodos
    public void ResetCamera()
    {
        camera.transform.DOMove(cameraPos, 1.5f);
        camera.transform.DORotate(cameraRot, 1.5f);
    }

    public void PoisitionCameraDeck()
    {
        camera.transform.DOMove(cameraDeckPosition, 1.5f);
        camera.transform.DORotate(cameraDeckRotation, 1.5f);
    }
    
    public void PoisitionCameraBall()
    {
        camera.transform.DOMove(cameraBallPosition, 1.5f);
        camera.transform.DORotate(cameraBallRotation, 1.5f);
    }
    
    public void PoisitionCameraClock()
    {
        camera.transform.DOMove(cameraClockPosition, 1.5f);
        camera.transform.DORotate(cameraClockRotation, 1.5f);
    }
    
    public void PoisitionCameraBell()
    {
        camera.transform.DOMove(cameraBellPosition, 1.5f);
        camera.transform.DORotate(cameraBellRotation, 1.5f);
    }
    
    public void PositionDeck()
    {
        deck.transform.DOMove(deckPos, 1.5f);
    }
    
    
    public void PositionBall()
    {
        ball.transform.DOMove(ballPos, 1.5f);
    }
    
    
    public void PositionClock()
    {
        clock.transform.DOMove(clockPos, 1.5f);
    }
    
    
    public void PositionBell()
    {
        bell.transform.DOMove(bellPos, 1.5f);
    }

    public void DisplayGameboard()
    {
        PositionDeck();
        PositionBell();
        PositionClock();
        PositionBall();
    }
}