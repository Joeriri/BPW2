using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // Refs
    private LevelUI levelUI;

    // Player
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;
    private CharacterController cc;
    private Camera cam;
    private Rigidbody rb;

    // Car
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
        // Refs
        levelUI = FindObjectOfType<LevelUI>();
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
        // Dit is voor wanneer de jeep wel rijdt.
        // if (Input.GetKeyDown(KeyCode.E) && car.CurrentSpeed < 15f)

        if (Input.GetKeyDown(KeyCode.E))
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
        //carUserControl.enabled = true;
        carCam.gameObject.SetActive(true);
        //car.Move(0, 0, 0, 0); // Disable handbreak
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
        //carUserControl.enabled = false;
        carCam.gameObject.SetActive(false);
        //car.Move(0, 0, 0, 1); // Enable handbreak
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
        StartCoroutine(LookUp());
    }

    void ExitTelescopeState()
    {
        state = PlayerStates.Walking;
        StartCoroutine(LookDown());
    }

    IEnumerator LookUp()
    {
        // Disable player and car
        fpc.enabled = false;
        car.enabled = false;
        // Fade in
        levelUI.StartFade(Color.clear, Color.black, 0.25f);
        yield return new WaitForSeconds(0.25f);
        // Enable StarMap
        starMap.gameObject.SetActive(true);
        starMap.UpdateStarMap();
        // Fade out
        levelUI.StartFade(Color.black, Color.clear, 0.5f);

        // Geheugensteun voor camera draai van de speler
        // playerCam.gameObject.transform.rotation = Quaternion.EulerAngles
    }

    IEnumerator LookDown()
    {
        // Fade in
        levelUI.StartFade(Color.clear, Color.black, 0.25f);
        yield return new WaitForSeconds(0.25f);
        // Disable Starmap, enable player and car
        fpc.enabled = true;
        car.enabled = true;
        starMap.gameObject.SetActive(false);
        // Fade out
        levelUI.StartFade(Color.black, Color.clear, 0.5f);

        // Check for stuff
        if (starMap.starsCompleted)
        {
            GameManager.Instance.ToTempleAnim();
        }
    }
}
