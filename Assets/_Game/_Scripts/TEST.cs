using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public List<Transform> pawnlist;

    [SerializeField] private List<Vector3> m_positionsHistory;
    int index = 0;
    private void OnCollisionEnter(Collision other)
    {
        if (index >= pawnlist.Count) return;
        if (other.transform.CompareTag("Pawn"))
        {
            other.gameObject.SetActive(false);
            pawnlist[index].gameObject.SetActive(true);
            index++;
        }
    }


    private void Update()
    {
        _ApplySnakeMovement();
    }

    [SerializeField] private float m_snakeMoveSpeed;
    [SerializeField] private uint m_gap = 10;
    private void _ApplySnakeMovement()
    {
        //  if (InputManager.instance.moveAxis.x == 0) return;
        //int count = m_followingPawns.Count;

        //SovereignUtils.Log("LocalPose: " + m_attachPosition.localPosition + " Position: " + m_attachPosition.position);
        m_positionsHistory.Insert(0, transform.position);
        uint index = 0;
        foreach (Transform pawn in pawnlist)
        {

            Vector3 point = m_positionsHistory[(int)Mathf.Min(index * m_gap, m_positionsHistory.Count - 1)];
            point.y = pawn.position.y;
            //Vector3 moveDirection = point - pawn.position;
            pawn.position = Vector3.Lerp(pawn.position, point, m_snakeMoveSpeed * Time.deltaTime);
            pawn.rotation = Quaternion.Slerp(pawn.rotation, transform.rotation, Time.deltaTime);
            index++;
        }



    }

}
