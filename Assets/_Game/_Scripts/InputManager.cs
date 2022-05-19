using UnityEngine;

namespace Naren_Dev
{
    public class InputManager : MonoBehaviour
    {

        #region Singleton
        public static InputManager instance { get; private set; }
        #endregion

        #region Variables

     //   [SerializeField] private VariableJoystick m_joystick;

        private ControlHub m_inputActions;
        public Vector2 moveAxis { get; private set; }

        #endregion

        #region Unity Built-In Methods

        private void Awake()
        {
            if (instance != this && instance != null) Destroy(gameObject);
            else if (instance == null) instance = this;

            m_inputActions = new ControlHub();
            m_inputActions.Enable();
        }

        //private void Update()
        //{
        //    GetInput();
        //}

        private void OnEnable()
        {
            m_inputActions.Player.Movement.performed += _ => GetInput();
            m_inputActions.Player.Movement.canceled += _ => GetInput();
        }

        private void OnDisable()
        {
            m_inputActions.Player.Movement.performed -= _ => GetInput();
            m_inputActions.Player.Movement.canceled -= _ => GetInput();
        }

        private void OnDestroy()
        {
            m_inputActions.Disable();
        }

        #endregion

        #region Custom Methods

        private void GetInput()
        {
            moveAxis = m_inputActions.Player.Movement.ReadValue<Vector2>();
       //    SovereignUtils.Log(moveAxis);
        }

        #endregion
    }
}