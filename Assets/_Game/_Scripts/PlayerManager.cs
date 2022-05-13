using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Naren_Dev
{
    /// <summary>
    /// This Script is responsible for managing player.
    /// It handles the player collisions, health, catching pawns, Score.
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {

        #region Variables

        [SerializeField] private Transform m_attachPosition;
        [SerializeField] private List<Transform> m_followingPawns;


        #endregion

        #region Unity Built-In Methods

        private void Start()
        {
            m_followingPawns.Add(transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Pawn":
                    other.GetComponent<PawnBehaviour>().SetFollowTargetToPawn(other.gameObject, ref m_attachPosition);
                    m_followingPawns.Add(other.transform);
                    break;

                default:

                    break;
            }
        }

        #endregion

        #region Custom Methods


        private void _ApplySnakeMovement()
        {

        }

        #endregion
    }
}