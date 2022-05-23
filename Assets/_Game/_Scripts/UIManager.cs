using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Naren_Dev
{
    public class UIManager : MonoBehaviour
    {
        #region variables

        [SerializeField] private Image m_healthBarImg;
        [SerializeField] private TextMeshProUGUI m_followingPawnCountTxt;
        [SerializeField] private UIEventsSO m_UIEvents;
        [SerializeField] private Vector3 m_punchScale = new Vector3(0.2f, .2f, .0f);
        [SerializeField] private float m_punchScaleTimer;

        public int avgFrameRate;
        public TextMeshProUGUI fpsCounterTxt;
        private float _timer;
        private float _hudRefreshRate=1f;

        #endregion

        #region Unity Built-In Methods



        private void OnEnable()
        {
            m_UIEvents.OnTextUpdated.AddListener(UpdateFollowingPawnCountText);
            m_UIEvents.OnHealthUpdated.AddListener(UpdateHealthBar);
        }

        private void OnDisable()
        {
            m_UIEvents.OnTextUpdated.RemoveListener(UpdateFollowingPawnCountText);
            m_UIEvents.OnHealthUpdated.RemoveListener(UpdateHealthBar);
        }

        private void Update()
        {
            if (Time.unscaledTime > _timer)
            {
                float current = 0;
                current = (1 / Time.unscaledDeltaTime);
                avgFrameRate = (int)current;
                fpsCounterTxt.text = "FPS: " + avgFrameRate.ToString();

                _timer = Time.unscaledTime + _hudRefreshRate;
            }






        }


        #endregion

        #region Custom Methods

        private void UpdateHealthBar(object amount)
        {
            float value = (float)amount;
            m_healthBarImg.fillAmount += value;
            SovereignUtils.Log($"FillAmount: {m_healthBarImg.fillAmount}, delta: {value}");

        }

        private void UpdateFollowingPawnCountText(object value)
        {

            m_followingPawnCountTxt.transform.DOPunchScale(m_punchScale, m_punchScaleTimer);
            m_followingPawnCountTxt.SetText(value.ToString());
        }


        #endregion

    }
}