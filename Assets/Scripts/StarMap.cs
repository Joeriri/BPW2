using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMap : MonoBehaviour
{
    [SerializeField] private RectTransform starsParent;
    [SerializeField] private RectTransform playerTracker;
    [SerializeField] private RectTransform player;
    private Star[] stars;
    private PlayerStateMachine worldPlayer;
    private Camera cam;
    
    private int targetStarIndex = 0;
    public bool starsCompleted = false;
    [SerializeField] private float hitRadius = 20f;

    private void Awake()
    {
        worldPlayer = FindObjectOfType<PlayerStateMachine>();
        cam = worldPlayer.GetComponentInChildren<Camera>();
        stars = starsParent.GetComponentsInChildren<Star>();
    }

    private void Start()
    {
        
    }

    public void UpdateStarMap()
    {
        

        playerTracker.localPosition = new Vector3(worldPlayer.transform.position.x, worldPlayer.transform.position.z, 0);

        var diffX = Screen.width*0.5f + worldPlayer.transform.position.x - playerTracker.position.x;
        var diffY = Screen.height*0.5f + worldPlayer.transform.position.z - playerTracker.position.y;

        starsParent.localPosition = new Vector3(-worldPlayer.transform.position.x, -worldPlayer.transform.position.z, 0);
        //starsParent.rotation = Quaternion.Euler(0, 0, 180 + worldPlayer.transform.eulerAngles.y);

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
                s.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (s.index == targetStarIndex - 1) // The previous star that was the target
            {
                s.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            }
        }
    }
}
