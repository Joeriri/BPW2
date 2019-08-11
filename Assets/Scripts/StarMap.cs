using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarMap : MonoBehaviour
{
    [SerializeField] private RectTransform starsParent;
    [SerializeField] private RectTransform starsPivot;
    [SerializeField] private RectTransform playerTracker;
    [SerializeField] private RectTransform player;
    private Star[] stars;
    private PlayerStateMachine worldPlayer;
    private Camera cam;

    [SerializeField] private Sprite dimStar;
    [SerializeField] private Sprite brightStar;
    
    private int targetStarIndex = 0;
    public bool starsCompleted = false;
    [SerializeField] private float hitRadius = 20f;
    [SerializeField] private Vector3 starsOffset = new Vector3(0, 30, 0);

    private void Awake()
    {
        worldPlayer = FindObjectOfType<PlayerStateMachine>();
        cam = worldPlayer.GetComponentInChildren<Camera>();
        stars = starsParent.GetComponentsInChildren<Star>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        // DEBUG SOLVE PUZZLE
        if (Input.GetKeyDown(KeyCode.O))
        {
            starsCompleted = true;
            Debug.Log("Starmap: Cheat used.");
        }
    }

    public void UpdateStarMap()
    {
        

        //playerTracker.localPosition = new Vector3(worldPlayer.transform.position.x, worldPlayer.transform.position.z, 0);
        //var diffX = player.position.x - playerTracker.position.x;
        //var diffY = player.position.y - playerTracker.position.y;

        starsParent.localPosition = new Vector3(-worldPlayer.transform.position.x, -worldPlayer.transform.position.z, 0) + starsOffset;
        starsPivot.rotation = Quaternion.Euler(0, 0, worldPlayer.transform.eulerAngles.y);

        //Debug.Log("Screen.height: " + Screen.height);
        //Debug.Log("stars.position.y: " + starsParent.position.y);
        //Debug.Log("Cam.pixelHeight: " + cam.pixelHeight);

        // While starMap is not completed, check for collision with stars.
        if (!starsCompleted)
        {
            CheckStarHit();
        }
        else // When starMap is completed, things are cool.
        {
            // TODO: spawn the temple etc
        }
    }

    private void CheckStarHit()
    {
        foreach (Star s in stars)
        {
            // Collision
            if (Vector3.Distance(s.transform.position, player.position) < hitRadius)
            {
                Debug.Log("Star " + s.index + " was hit!");

                // Check if the star that was hit is the one we wanted to reach.
                if (s.index == targetStarIndex)
                {
                    // Make the next star the target.
                    targetStarIndex = s.index + 1;
                    Debug.Log("The next star is star " + targetStarIndex);

                    // Play sound
                    FindObjectOfType<AudioManager>().Play("StarHit");

                    // Do target star effects
                    TargetStarEffect();

                    // If this was the last star, complete the starMap.
                    if (s.index == stars.Length - 1)
                    {
                        starsCompleted = true;
                        Debug.Log("StarMap completed!");
                    }
                }
            }
        }
    }

    private void TargetStarEffect()
    {
        // Add the effect to the new target star and undo the effect for the old target star
        foreach (Star s in stars)
        {
            if (s.index == targetStarIndex) // The star that is now the target
            {
                s.transform.localScale = new Vector3(2, 2, 1);
                s.GetComponent<Image>().sprite = brightStar;
            }
            else if (s.index == targetStarIndex - 1) // The previous star that was the target
            {
                s.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
