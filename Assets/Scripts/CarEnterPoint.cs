using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterPoint : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            GetComponentInParent<UseCar>().inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            GetComponentInParent<UseCar>().inTrigger = false;
        }
    }
}
