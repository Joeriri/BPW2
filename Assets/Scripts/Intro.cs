using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    private Camera introCam;

    [SerializeField] private float panDuration = 1f;

    private void Awake()
    {
        introCam = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartIntroPan()
    {
        StartCoroutine(IntroPan(panDuration));
    }

    IEnumerator IntroPan(float duration)
    {
        for (float i = 0f; i < duration; i += Time.deltaTime)
        {
            var startPos = new Vector3(introCam.transform.position.x, 400, introCam.transform.position.z);
            var targetPos = new Vector3(introCam.transform.position.x, 20, introCam.transform.position.z);

            introCam.transform.position = Vector3.Lerp(startPos, targetPos, i / panDuration);
            yield return null;
        }
        GameManager.Instance.GetComponent<GameStateMachine>().EnterDesertState();
    }
}
