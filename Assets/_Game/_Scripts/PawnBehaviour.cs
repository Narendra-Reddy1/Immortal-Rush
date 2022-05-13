using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class PawnBehaviour : MonoBehaviour
    {
        public void SetFollowTargetToPawn(GameObject pawn, ref Transform m_attachPosition)
        {
            SovereignUtils.Log(pawn.name);
            pawn.SetActive(false);
            pawn.transform.SetParent(m_attachPosition.parent);
            pawn.transform.position = m_attachPosition.position;
            pawn.SetActive(true);
            m_attachPosition.position = new Vector3(m_attachPosition.position.x, m_attachPosition.position.y,
                m_attachPosition.position.z - m_attachPosition.transform.localScale.z);
        }
    }
}