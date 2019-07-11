using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    private Camera playerCam;
    private Intro introScript;
    private Camera introCam;

    [SerializeField] private GameStates startState;

    public enum GameStates
    {
        Title,          // Title screen
        Intro,          // Intro cinematic
        Desert,         // Desert gameplay
        Found,          // Puzzle solved cinematic
        Temple,         // Temple gameplay
        End             // End cinematic
    }
    [HideInInspector] public GameStates state;

    private void Awake()
    {
        player = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        playerCam = player.GetComponentInChildren<Camera>();
        introScript = FindObjectOfType<Intro>();
        introCam = introScript.GetComponentInChildren<Camera>();
    }

    void Start()
    {
        state = startState;
    }

    void Update()
    {
        SwitchState();
    }

    void SwitchState()
    {
        switch (state)
        {
            case GameStates.Title:
                // Title state code here!
                break;
            case GameStates.Intro:
                // Intro state code here!
                break;
            case GameStates.Desert:
                // Desert state code here!
                break;
            case GameStates.Found:
                // Found state code here!
                break;
            case GameStates.Temple:
                // Temple state code here!
                break;
            case GameStates.End:
                // End state code here!
                break;
        }
    }
}
