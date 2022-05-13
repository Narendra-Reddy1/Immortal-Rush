using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// NOTE: All private member variables have prefix m_.
///       All Private methods will have prefix _.
/// </summary>
/// 
namespace Naren_Dev
{
    /// <summary>
    /// This Script is responsible for controlling player.
    /// It handles the Input, Movement,Animations.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {


        #region Variables

        private Vector2 m_moveAxis;

        [SerializeField] private float m_playerMovementSpeed;
        [SerializeField] private Rigidbody m_playerRb;


        #endregion

        #region Unity Built-In Methods

        private void Reset()
        {
            _Initialize();
        }

        private void Update()
        {
            _HandleInput();
            _ApplyMovement();
        }

        #endregion

        #region Custom Methods

        private void _Initialize()
        {
            TryGetComponent(out m_playerRb);
            m_playerMovementSpeed = 75f;
        }



        /// <summary>
        /// This method is responsible for retrieving the input from InputManager.
        /// </summary>
        private void _HandleInput()
        {
            m_moveAxis = InputManager.instance.moveAxis;
        }


        private void _ApplyMovement()
        {
            Vector3 moveDirection = new Vector3(m_playerMovementSpeed * m_moveAxis.x, 0f, m_playerMovementSpeed) * Time.smoothDeltaTime;
            m_playerRb.velocity = moveDirection;
           // SovereignUtils.Log(moveDirection + " " + m_moveAxis);
        }

        // TODO: Need to implement.
        private void _ApplyAnimations()
        {

        }


        #endregion



    }
}