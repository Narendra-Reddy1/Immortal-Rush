using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Naren_Dev
{
    /// <summary>
    /// This Script is responsible for managing player.
    /// It handles the player collisions, health, catching pawns, Score.
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {

        #region Events

        //public static UnityEvent  

        #endregion

        #region Variables

        [SerializeField] private Transform m_attachPosition;
        [SerializeField] private List<Transform> m_followingPawns;
        [SerializeField] private float m_snakeMoveSpeed;
        [SerializeField] private List<Vector3> m_positionsHistory;
        [SerializeField] private float m_minDistance = 0.25f;
        [SerializeField] private uint m_gap = 10;
        [SerializeField] private UIEventsSO m_UIEventsHolder;


        private WaitForEndOfFrame m_waitForEndOfFrame = new WaitForEndOfFrame();
        //   [SerializeField] private PawnEventsSo m_pawnEventsSO;
        #endregion



        public static UnityAction<Transform> OnPawnCollidedWithObstacle;

        #region Unity Built-In Methods

        private void Awake()
        {
            _Init();
        }

        private void OnEnable()
        {
            OnPawnCollidedWithObstacle += WheneverPawnCollidedWithObstacle;

        }
        private void OnDisable()
        {

            OnPawnCollidedWithObstacle -= WheneverPawnCollidedWithObstacle;

        }

        private void Update()
        {
            _ApplySnakeMovement();
        }

        private void OnCollisionEnter(Collision other)
        {
            switch (other.transform.tag)
            {
                case "Pawn":
                    WhenPlayerAcquiredNewPawn(other.gameObject);
                    break;

                default:

                    break;
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        #endregion

        #region Custom Methods

        private void _Init()
        {
            foreach (Transform transform in m_followingPawns)
            {
                transform.gameObject.SetActive(false);
            }

        }

        private void _ApplySnakeMovement()
        {

            // SovereignUtils.Log("LocalPose: " + m_attachPosition.localPosition + " Position: " + m_attachPosition.position);
            m_positionsHistory.Insert(0, transform.position);
            uint index = 0;
            foreach (Transform pawn in m_followingPawns)
            {
                Vector3 point = m_positionsHistory[(int)Mathf.Min(index * m_gap, m_positionsHistory.Count - 1)];
                point.y = pawn.position.y;
                pawn.position = Vector3.Lerp(pawn.position, point, m_snakeMoveSpeed * Time.deltaTime);
                pawn.rotation = Quaternion.Slerp(pawn.rotation, transform.rotation, Time.deltaTime);
                index++;
            }
        }


        private int GetActiveFollowingPawnsCount()
        {
            int count = 0;
            foreach (Transform pawn in m_followingPawns)
            {
                if (pawn.gameObject.activeInHierarchy)
                {
                    count++;
                }
            }
            return count;
        }

        int index = 0;

        private void WhenPlayerAcquiredNewPawn(GameObject newPawn)
        {

            newPawn.SetActive(false);
            if (index >= m_followingPawns.Count) return;

            m_followingPawns[index].gameObject.SetActive(true);
            StartCoroutine(UpdateAtEndOfTheFrame());
            index++;
            SovereignUtils.Log(" Index: " + index);
        }

        private void WheneverPawnCollidedWithObstacle(Transform pawn)
        {
            if (m_followingPawns.Contains(pawn))
            {
                SovereignUtils.Log(pawn.name + " in list and removing it." + " Index: " + index);
                //  m_followingPawns.RemoveAt(m_followingPawns.Count - 1);
                m_followingPawns[m_followingPawns.Count - 1].gameObject.SetActive(false);
                index--;
                StartCoroutine(UpdateAtEndOfTheFrame());
            }

        }
        private IEnumerator UpdateAtEndOfTheFrame()
        {
            yield return m_waitForEndOfFrame;
            m_UIEventsHolder.OnTextUpdated?.Invoke(GetActiveFollowingPawnsCount());
        }

        #endregion
    }


}