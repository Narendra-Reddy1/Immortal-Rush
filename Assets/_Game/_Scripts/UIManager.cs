using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Naren_Dev
{
    public class UIManager : MonoBehaviour
    {
        #region variables


        [SerializeField] private TextMeshProUGUI m_followingPawnCountTxt;
        [SerializeField] private UIEventsSO m_UIEventsHolder;

        #endregion

        #region Unity Built-In Methods



        private void OnEnable()
        {
            m_UIEventsHolder.OnTextUpdated.AddListener(UpdateFollowingPawnCountText);
        }

        private void OnDisable()
        {
            m_UIEventsHolder.OnTextUpdated.RemoveListener(UpdateFollowingPawnCountText);
        }

        #endregion

        #region Custom Methods

        private void UpdateFollowingPawnCountText(object value)
        {
            m_followingPawnCountTxt.SetText(value.ToString());
        }


        #endregion

    }
}