using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    private Camera introCam;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    private Camera playerCam;
    private LevelUI levelUI;
    private StartMenu startMenu;
    private Button[] startMenuButtons;
    [SerializeField] private Canvas titleScreen;

    [SerializeField] private float panDuration = 1f;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        introCam = GetComponentInChildren<Camera>();
        player = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        playerCam = player.GetComponentInChildren<Camera>();
        levelUI = FindObjectOfType<LevelUI>();
        startMenu = FindObjectOfType<StartMenu>();
        startMenuButtons = startMenu.GetComponentsInChildren<Button>();
    }

    public void StartTitleScreen()
    {
        // Deactiveer player
        player.enabled = false;
        playerCam.gameObject.SetActive(false);
        // Activeer intro camera
        introCam.gameObject.SetActive(true);
        // Laat title screen zien
        titleScreen.gameObject.SetActive(true);
        // Zet menu buttons aan
        foreach (Button b in startMenuButtons)
        {
            b.gameObject.SetActive(true);
        }
        // Deactiveer blackScreen (zodat je op de knoppen kan drukken)
        levelUI.BlackScreenActivate(false);
    }

    public void StartIntro()
    {
        // Zet menu buttons uit (technisch gezien hoeft dit niet omdat de blackScreen ervoor zit, maar voor de zekerheid)
        foreach (Button b in startMenuButtons)
        {
            b.gameObject.SetActive(false);
        }
        // Zet blackscreen weer aan
        levelUI.BlackScreenActivate(true);
        // Start intro coroutine
        StartCoroutine(IntroPan(panDuration));
    }

    private void EndIntro()
    {
        // Unfreeze player
        player.enabled = true;
        playerCam.gameObject.SetActive(true);
        // Deactivate intro camera
        introCam.gameObject.SetActive(false);
        // Hide title screen
        titleScreen.gameObject.SetActive(false);
    }

    IEnumerator IntroPan(float duration)
    {
        // pan the camera down
        for (float i = 0f; i < duration; i += Time.deltaTime)
        {
            var startPos = new Vector3(introCam.transform.position.x, 400, introCam.transform.position.z);
            var targetPos = new Vector3(introCam.transform.position.x, 20, introCam.transform.position.z);

            introCam.transform.position = Vector3.Lerp(startPos, targetPos, i / panDuration);
            yield return null;
        }
        // Fade to black
        levelUI.StartFadeToBlack(1f);
        yield return new WaitForSeconds(1f);
        // End intro sequence
        EndIntro();
        // Fade to clear
        levelUI.StartFadeToClear(1f);
    }
}
