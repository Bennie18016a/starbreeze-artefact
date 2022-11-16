using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAI : MonoBehaviour
{
    public GameObject deadGuardPrefab;
    public GameObject deadCivPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit, 8f))
            {
                if(hit.transform.tag == "AI" && hit.transform.name == "Guard")
                {
                    Instantiate(deadGuardPrefab, hit.transform.position, Quaternion.identity);
                    Destroy(hit.transform.gameObject);
                } else if(hit.transform.tag == "AI" && hit.transform.name == "Civ")
                {
                    Instantiate(deadCivPrefab, hit.transform.position, Quaternion.identity);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }


    private Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
