using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Naren_Dev
{
    public class SimulatePawns : MonoBehaviour
    {
        public List<Transform> positions;
        public List<GameObject> activePawns;
        public Transform player;
        public PlayerController controller;
        public bool control;

        private void Awake()
        {
            control = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                SovereignUtils.Log("Simulate Pawns");
                controller.control = false;
                control = false;
                foreach (GameObject go in activePawns)
                {
                    go.transform.parent = null;
                }

                SetPlayerPose();
                SetPawnPoses();
            }
        }

        public void SetPlayerPose()
        {
            player.DOMove(positions[0].position, 3f).onComplete += () =>
            {
                player.GetComponentInChildren<Animator>().SetBool("END", true);
                SovereignUtils.Log("Done with Player pose setting locALPOSE: " + positions[0].position);
            };
        }
        public void SetPawnPoses()
        {
            int count = activePawns.Count;
            for (int i = 1; i < count; i++)
            {
                activePawns[i].transform.DOMove(positions[i].position, 3f).onComplete += () =>
                 {
                     SovereignUtils.Log("Done with Pawn POse setting.");
                 };
                activePawns[i].GetComponentInChildren<Animator>().SetBool("ENDING", true);
            }
        }
    }
}