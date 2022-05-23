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
        [SerializeField] private float m_maxSteerAngle;
        [SerializeField] private float m_steerSpeed;

        [SerializeField] private Rigidbody m_playerRb;
        [SerializeField] private VariableJoystick m_joyStick;
        [SerializeField] private PlayerVariables m_playerVariables;
        [SerializeField] private PlayerShooting m_playerShooting;

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
            TryGetComponent(out m_playerMovementSpeed);
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
            // Vector3 moveDirection = new Vector3(m_steerSpeed * m_moveAxis.x, 0f, m_playerMovementSpeed) * Time.smoothDeltaTime;
            //   transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.up * m_steerSpeed * m_moveAxis.x), 10 * Time.deltaTime);
            if (!m_playerVariables.isPlayerShooting && m_playerShooting.m_nearestEnemy == null)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.up * m_maxSteerAngle * m_joyStick.Horizontal), m_steerSpeed * Time.smoothDeltaTime);
                Vector3 moveDirection = new Vector3(m_maxSteerAngle * m_joyStick.Horizontal, 0f, m_playerMovementSpeed) * Time.smoothDeltaTime;
                m_playerRb.velocity = moveDirection;
            }
            else
                _GoForZombies();
            // SovereignUtils.Log(moveDirection + " " + m_moveAxis);
        }

        private void _GoForZombies()
        {
            Debug.DrawRay(transform.position, m_playerShooting.m_nearestEnemy.position - transform.position, Color.blue);
            transform.LookAt(m_playerShooting.m_nearestEnemy);
            Vector3 direction = (m_playerShooting.m_nearestEnemy.position - transform.position);
            Vector3 moveDirection = new Vector3(direction.x * m_maxSteerAngle * 2f  * m_joyStick.Horizontal, 0f, m_playerMovementSpeed / 100 * direction.z) * Time.deltaTime;
            m_playerRb.velocity = moveDirection;
            //m_playerRb.MovePosition(transform.position - m_playerShooting.m_nearestEnemy.position);
        }

        // TODO: Need to implement.
        private void _ApplyAnimations()
        {

        }


        #endregion



    }
}