using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment
{
    public class SceneManagment : MonoBehaviour
    {
        [SerializeField] ParticleSystem particleSystem;
        [SerializeField] ParticleSystem fogParticleSystem;
        [SerializeField] Light light;
        
        [SerializeField] Color[] colors;
        [SerializeField] private string[] bossId;

        Dictionary<string, Color> colorDictionary = new Dictionary<string, Color>();
        private void Start()
        {
            for(int i = 0; i < colors.Length; i++)
            {
                colorDictionary.Add(bossId[i], colors[i]);
            }
        }

        public void ChangeToFoolScene()
        {
            if (colorDictionary.TryGetValue("Fool", out Color color))
            {
                //Transition to color with dotween
                Sequence s = DOTween.Sequence();
                s.Append(light.DOColor(color, 1f));
                var main = particleSystem.main;
                main.startColor = color;
                var fogMain = fogParticleSystem.main;
                fogMain.startColor = color;

                s.Play();
            }
        }
        
        public void ChangeToDeathScene()
        {
            if (colorDictionary.TryGetValue("Death", out Color color))
            {
                //Transition to color with dotween
                Sequence s = DOTween.Sequence();
                s.Append(light.DOColor(color, 1f));
                var main = particleSystem.main;
                main.startColor = color;
                var fogMain = fogParticleSystem.main;
                fogMain.startColor = color;

                s.Play();
            }
        }
        
        public void ChangeToDealerScene()
        {
            if (colorDictionary.TryGetValue("Dealer", out Color color))
            {
                //Transition to color with dotween
                Sequence s = DOTween.Sequence();
                s.Append(light.DOColor(color, 1f));
                var main = particleSystem.main;
                main.startColor = color;
                var fogMain = fogParticleSystem.main;
                fogMain.startColor = color;

                s.Play();
            }
        }
        
        public void ChangeToWheelOfFortuneScene()
        {
            if (colorDictionary.TryGetValue("WheelOfFortune", out Color color))
            {
                //Transition to color with dotween
                Sequence s = DOTween.Sequence();
                s.Append(light.DOColor(color, 1f));
                var main = particleSystem.main;
                main.startColor = color;
                var fogMain = fogParticleSystem.main;
                fogMain.startColor = color;

                s.Play();
            }
        }
        
        public void ChangeToHermitScene()
        {
            if (colorDictionary.TryGetValue("Hermit", out Color color))
            {
                //Transition to color with dotween
                Sequence s = DOTween.Sequence();
                s.Append(light.DOColor(color, 1f));
                var main = particleSystem.main;
                main.startColor = color;
                var fogMain = fogParticleSystem.main;
                fogMain.startColor = color;

                s.Play();
            }
        }
    }
}