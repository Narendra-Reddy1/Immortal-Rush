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
        private Transform m_currentPawn;
        private Transform m_previousPawn;
        [SerializeField] private float m_minDistance = 0.25f;
        [SerializeField] private uint m_gap = 10;
        [SerializeField] private UIEventsSO m_UIEventsHolder;
        #endregion

        #region Unity Built-In Methods

        private void Start()
        {
            //    m_followingPawns.Add(transform);
        }

        private void Update()
        {
            _ApplySnakeMovement();
        }


        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Pawn":
                    other.GetComponent<PawnBehaviour>().SetFollowTargetToPawn(other.gameObject, ref m_attachPosition);
                    m_followingPawns.Add(other.transform);
                    m_UIEventsHolder.OnTextUpdated?.Invoke(m_followingPawns.Count);
                    break;

                default:

                    break;
            }
        }

        #endregion

        #region Custom Methods


        private void _ApplySnakeMovement()
        {
            //  if (InputManager.instance.moveAxis.x == 0) return;
            //int count = m_followingPawns.Count;
            m_positionsHistory.Insert(0, transform.position);
            uint index = 0;
            foreach (Transform pawn in m_followingPawns)
            {
                Vector3 point = m_positionsHistory[(int)Mathf.Min(index * m_gap, m_positionsHistory.Count - 1)];
                point.y = pawn.position.y;
                //Vector3 moveDirection = point - pawn.position;
                pawn.position = Vector3.Lerp(pawn.position, point, m_snakeMoveSpeed * Time.deltaTime);
                pawn.LookAt(point);
                index++;
            }



            /*   for (int i = 1; i < count; i++)
               {
                   m_currentPawn = m_followingPawns[i];
                   m_previousPawn = m_followingPawns[i - 1];
                   float dist = Vector3.Distance(m_currentPawn.position, m_previousPawn.position);

                   Vector3 newPose = m_previousPawn.position;
                   newPose -= new Vector3(0f, 0f, 0.7f);
                   //  newPose.y = m_followingPawns[0].position.y;
                   float T = Time.smoothDeltaTime * dist / m_minDistance * m_snakeMoveSpeed;
                   if (T > 0.5f) T = 0.5f;

                   m_currentPawn.position = Vector3.Slerp(m_currentPawn.position, newPose, T);
                   m_currentPawn.rotation = Quaternion.Slerp(m_currentPawn.rotation, m_previousPawn.rotation, T);
               }
   */
        }

        #endregion
    }
}