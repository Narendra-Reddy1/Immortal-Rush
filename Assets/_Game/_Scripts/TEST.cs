using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{



    public List<GameObject> lights;
    public Transform endTransform;
    public Transform playerTransform;
    int index = 3;
    private void Update()
    {
        float dist = Vector3.Distance(playerTransform.position, endTransform.position);

        if (dist % 14 == 0)
        {
            SovereignUtils.Log($"Distance: {dist}, Modulo 14: {dist % 14}");
            if (index >= lights.Count) return;
            lights[index].SetActive(true);
            index++;
        }
    }



    //public List<GameObject> bloodStreams;
    //RaycastHit hit;

    //private void Update()
    //{
    //    if (!Input.GetMouseButtonDown(0)) return;
    //    Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    SovereignUtils.Log($"{mousePos}");
    //    if (Physics.Raycast(mousePos, out hit, 1000f))
    //    {
    //        if (hit.collider.CompareTag("zombie"))
    //        {
    //            SovereignUtils.Log($"Hitted Zombie: ");

    //            hit.collider.GetComponent<Naren_Dev.HealthManager>().TakeDamage(2);
    //            GameObject go = Instantiate(bloodStreams[Random.Range(0, bloodStreams.Count)], hit.point, Quaternion.identity, hit.transform);
    //            go.GetComponent<ParticleSystem>().Play();
    //            if (hit.collider.GetComponent<Naren_Dev.HealthManager>().isDead)
    //                hit.collider.GetComponent<Naren_Dev.ZombieBehaviour>().OnHealthIsZero();

    //        }
    //    }

    //}

}
