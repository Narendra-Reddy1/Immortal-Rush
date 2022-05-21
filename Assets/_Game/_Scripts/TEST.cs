using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{

    [Header("Shoot")]
    [Space(4)]
    [SerializeField] private Transform m_muzzleEnd;
    [SerializeField] private float m_fireRate;
    private float m_fireRateDelay;
    private float m_enemyDetectionRange;


    private RaycastHit m_enemyHit;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _Shoot();
    }

    private void _Shoot()
    {
        ///Sphere cast for zombies when zombie activator is triggered. ===>Handled by GetNearbyEnemy method.
        ///Else stop searching for enemies.
        ///If zombies are found in searching, shoot at nearest zombies 
        ///


        if (Physics.Raycast(m_muzzleEnd.position, transform.forward, out m_enemyHit, 1000))
        {
            SovereignUtils.Log($"FireRate: {m_fireRate}, FireRateDelay {m_fireRateDelay}, Time.time: {Time.time}");
            if (Time.time < m_fireRateDelay || m_enemyHit.collider == null) return;

            Debug.DrawRay(m_muzzleEnd.position, m_enemyHit.transform.position, Color.red, 1f);
            m_fireRateDelay = m_fireRate + Time.time;
            SovereignUtils.Log("Shooting");
        }
    }
    private void OnDrawGizmos()
    {
        
    }

    //    public List<Transform> pawnlist;

    //    [SerializeField] private List<Vector3> m_positionsHistory;
    //    int index = 0;
    //    private void OnCollisionEnter(Collision other)
    //    {
    //        if (index >= pawnlist.Count) return;
    //        if (other.transform.CompareTag("Pawn"))
    //        {
    //            other.gameObject.SetActive(false);
    //            pawnlist[index].gameObject.SetActive(true);
    //            index++;
    //        }
    //    }


    //    private void Update()
    //    {
    //        _ApplySnakeMovement();
    //    }

    //    [SerializeField] private float m_snakeMoveSpeed;
    //    [SerializeField] private uint m_gap = 10;
    //    private void _ApplySnakeMovement()
    //    {
    //        //  if (InputManager.instance.moveAxis.x == 0) return;
    //        //int count = m_followingPawns.Count;

    //        //SovereignUtils.Log("LocalPose: " + m_attachPosition.localPosition + " Position: " + m_attachPosition.position);
    //        m_positionsHistory.Insert(0, transform.position);
    //        uint index = 0;
    //        foreach (Transform pawn in pawnlist)
    //        {

    //            Vector3 point = m_positionsHistory[(int)Mathf.Min(index * m_gap, m_positionsHistory.Count - 1)];
    //            point.y = pawn.position.y;
    //            //Vector3 moveDirection = point - pawn.position;
    //            pawn.position = Vector3.Lerp(pawn.position, point, m_snakeMoveSpeed * Time.deltaTime);
    //            pawn.rotation = Quaternion.Slerp(pawn.rotation, transform.rotation, Time.deltaTime);
    //            index++;
    //        }



    //    }

}
