using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    private UnityStandardAssets.Vehicles.Car.CarController car;

    private void Awake()
    {
        car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
    }

    public void StopDriving()
    {
        StartCoroutine(ApplyBrake());
    }

    IEnumerator ApplyBrake()
    {
        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            car.Move(0, 0, -1, 0);
            yield return null;
        }
    }
}
