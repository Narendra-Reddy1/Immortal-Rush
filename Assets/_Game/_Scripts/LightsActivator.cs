using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

namespace Naren_Dev
{
    public class LightsActivator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_lights;
        [SerializeField] private float m_timer;
        [SerializeField] private List<Light> spotLights;

        private void Awake()
        {
            
        }
        private void Start()
        {
            spotLights = new List<Light>();
            int count = m_lights.Count;
            for (int i = 0; i < count; i++)
            {
                spotLights.Add(m_lights[i].transform.GetChild(0).GetComponent<Light>());
                spotLights.Add(m_lights[i].transform.GetChild(1).GetComponent<Light>());
            }
        }



        private void TweenIntensity(Light light)
        {
            SovereignUtils.Log("Light: " + light.name);
            light.DOIntensity(150, m_timer);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _ActivateLights();
            }
        }


        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        _DeactivateLights();
        //    }
        //}

        private void _ActivateLights()
        {
            foreach (Light light in spotLights)
            {
                TweenIntensity(light);
            }

            //foreach (GameObject light in m_lights)
            //{
            //    light.SetActive(true);
            //}

        }

        private void _DeactivateLights()
        {
            foreach (GameObject light in m_lights)
            {
                light.SetActive(false);
            }
        }


    }
}