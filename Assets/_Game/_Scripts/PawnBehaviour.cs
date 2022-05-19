using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Naren_Dev
{
    public class PawnBehaviour : MonoBehaviour
    {
        #region Variables
        //   [SerializeField] private PawnEventsSo m_pawnEvetsSO;


        [SerializeField] private bool m_isDummy = false;
        [SerializeField] SkinnedMeshRenderer meshRenderer;
        [SerializeField] private Animator m_pawnAnimator;
        List<Material> m_tempMaterials;
        List<Material> m_originalMaterials;

        #endregion

        #region Unity Built-In Methods

        private void Awake()
        {
            _Init();
            CorruptMaterial();

        }


        //private void OnEnable()
        //{

        //   m_pawnEvetsSO.OnPawnCollidedWithObstacle += WheneverPawnCollidedWithObstacle;
        //}

        //private void OnDisable()
        //{
        //   m_pawnEvetsSO.OnPawnCollidedWithObstacle -= WheneverPawnCollidedWithObstacle;

        //}

      

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Obstacle"))
            {
                SovereignUtils.Log("Form OnTriggerEnter: Pawnehaviour");
                PlayerManager.OnPawnCollidedWithObstacle?.Invoke(transform);
                WheneverPawnCollidedWithObstacle(other.transform.position - transform.position);
            }

            //switch (other.transform.tag)
            //{
            //    case "Obstacle":
            //        SovereignUtils.Log("Form OnTriggerEnter: Pawnehaviour");
            //        PlayerManager.OnPawnCollidedWithObstacle?.Invoke(transform);
            //        WheneverPawnCollidedWithObstacle(other.transform.position - transform.position);
            //        break;

            //    default:
            //        break;
            //}
        }

        #endregion

        #region Custom Methods

        private void _Init()
        {
            m_tempMaterials = new List<Material>();
            if (meshRenderer == null) TryGetComponent(out meshRenderer);
            if (m_pawnAnimator == null) TryGetComponent(out m_pawnAnimator);
        }


        private void CorruptMaterial()
        {
            if (!m_isDummy) return;
            m_pawnAnimator.enabled = false;
            m_tempMaterials = meshRenderer.materials.ToList();
            m_originalMaterials = m_tempMaterials;
            SovereignUtils.Log(m_tempMaterials.Count);
            foreach (Material mat in m_tempMaterials)
            {
                mat.color = new Color(183f, 183, 183, 255);
            }
        }

        public void ResetMaterials()
        {
            int count = m_tempMaterials.Count;
            for (int i = 0; i < count; i++)
            {
                m_tempMaterials[i] = m_originalMaterials[i];
            }
        }

        public void SetFollowTargetToPawn(GameObject pawn, ref Transform m_attachPosition)
        {
            SovereignUtils.Log(pawn.name);
            pawn.SetActive(false);
            pawn.transform.SetParent(m_attachPosition.root);
            pawn.transform.position = m_attachPosition.position;
            pawn.SetActive(true);
            m_attachPosition.position = new Vector3(m_attachPosition.position.x, m_attachPosition.position.y,
                m_attachPosition.position.z - m_attachPosition.transform.localScale.z);
        }

        private void WheneverPawnCollidedWithObstacle(Vector3 direction)
        {
            SovereignUtils.Log(this.name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            gameObject.SetActive(false);
        }

        #endregion

    }
}


