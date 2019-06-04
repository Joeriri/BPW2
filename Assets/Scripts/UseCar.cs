using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCar : MonoBehaviour
{
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    private UnityStandardAssets.Vehicles.Car.CarUserControl carUserControl;
    private UnityStandardAssets.Vehicles.Car.CarController m_Car;
    [SerializeField] private Camera carCam;
    private Transform enterPoint;
    

    private bool inCar = false;
    public bool inTrigger = false;

    private void Awake()
    {
        carUserControl = GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>();
        m_Car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        enterPoint = GetComponentInChildren<CarEnterPoint>().transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!inCar && inTrigger)
            {
                inCar = true;
                carUserControl.enabled = true;
                player.gameObject.SetActive(false);
                carCam.gameObject.SetActive(true);
                player.transform.parent = transform;
                player.transform.position = transform.position;
                m_Car.Move(0, 0, 0, 0); // Disable handbreak

                Debug.Log("Gotten into car!");
            }
            else if (inCar)
            {
                inCar = false;
                carUserControl.enabled = false;
                player.transform.position = enterPoint.position;
                player.transform.parent = null;
                player.gameObject.SetActive(true);
                carCam.gameObject.SetActive(false);
                player.transform.LookAt(m_Car.transform);
                m_Car.Move(0, 0, 0, 1); // Enable handbreak

                Debug.Log("Gotten out of car!");
            }
        }
        
    }
}
