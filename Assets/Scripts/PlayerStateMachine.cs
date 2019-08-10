using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // Player
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;
    private CharacterController cc;
    private Camera cam;
    private Rigidbody rb;

    [Header("Car")]
    private UnityStandardAssets.Vehicles.Car.CarController car;
    private UnityStandardAssets.Vehicles.Car.CarUserControl carUserControl;
    [SerializeField] private Camera carCam;
    private CarExitPoint carExitPoint;
    [SerializeField] private float carAccesRange = 10f;

    // Sky
    [Header("Stargazing")]
    [SerializeField] private StarMap starMap;

    public enum PlayerStates
    {
        Walking,
        Driving,
        Stargazing
    }
    [Header("State")]
    public PlayerStates state;

    private void Awake()
    {
        // Player
        fpc = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        cam = GetComponentInChildren<Camera>();
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        // Car
        car = FindObjectOfType<UnityStandardAssets.Vehicles.Car.CarController>();
        carUserControl = car.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>();
        carExitPoint = car.GetComponentInChildren<CarExitPoint>();
    }

    private void Start()
    {
        state = PlayerStates.Walking;
    }

    void Update()
    {
        SwitchState();
    }

    void SwitchState()
    {
        switch (state)
        {
            case PlayerStates.Walking:
                WalkState();
                break;
            case PlayerStates.Driving:
                CarState();
                break;
            case PlayerStates.Stargazing:
                StargazeState();
                break;
        }
    }

    // De speler is niet aan de sterren aan het kijken
    void WalkState()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EnterTelescopeState();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Debug.DrawRay(cam.transform.position, cam.transform.forward * carAccesRange, Color.red);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, carAccesRange))
            {
                if (hit.collider.transform.root.gameObject == car.gameObject)
                {
                    EnterCarState();
                }
            }
        }
    }

    // De speler zit in de auto
    void CarState()
    {
        if (Input.GetKeyDown(KeyCode.E) && car.CurrentSpeed < 15f)
        {
            ExitCarState();
        }
    }

    void EnterCarState()
    {
        state = PlayerStates.Driving;
        // Child player
        transform.parent = car.transform;
        transform.position = car.transform.position;
        // Enable car
        carUserControl.enabled = true;
        carCam.gameObject.SetActive(true);
        car.Move(0, 0, 0, 0); // Disable handbreak
        // Disable player
        fpc.enabled = false;
        cc.enabled = false;
        cam.gameObject.SetActive(false);
    }

    void ExitCarState()
    {
        state = PlayerStates.Walking;
        // De-child player
        transform.position = carExitPoint.transform.position;
        transform.parent = null;
        transform.LookAt(car.transform);
        // Disable car
        carUserControl.enabled = false;
        carCam.gameObject.SetActive(false);
        car.Move(0, 0, 0, 1); // Enable handbreak
        // Enable player
        fpc.enabled = true;
        cc.enabled = true;
        cam.gameObject.SetActive(true);
    }

    // De speler is naar de sterren aan het kijken
    void StargazeState()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ExitTelescopeState();
        }
    }

    void EnterTelescopeState()
    {
        state = PlayerStates.Stargazing;
        fpc.enabled = false;
        car.enabled = false;
        starMap.gameObject.SetActive(true);
        starMap.UpdateStarMap();

        //StartCoroutine(LookUp());
    }

    void ExitTelescopeState()
    {
        state = PlayerStates.Walking;
        fpc.enabled = true;
        car.enabled = true;
        starMap.gameObject.SetActive(false);

        if (starMap.starsCompleted)
        {
            GameManager.Instance.ToTempleAnim();
        }
    }

    IEnumerator LookUp(float duration)
    {
        float t = 0;
        float step = 1f / duration;
        while (t < 1)
        {

        }

        //playerCam.gameObject.transform.rotation = Quaternion.EulerAngles

        yield return null;
    }
}
