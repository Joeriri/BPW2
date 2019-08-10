using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private Obelisk obelisk;

    private void Awake()
    {
        obelisk = GetComponentInParent<Obelisk>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            obelisk.Solve();
        }
    }
}