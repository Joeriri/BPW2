using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour
{
    // Objects
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    public Trigger target;
    private Shard[] shards;
    [SerializeField] private GameObject shardsParent;
    private TempleManager templeManager;
    
    // Player
    [SerializeField] private float maxDist = 15f;

    // SmoothDamping
    [SerializeField] [Range(0, 1)] private float shardSmoothing = 0.01f;
    private Vector3 dampVelocity;

    // Puzzle
    private bool frozen = false;
    private bool playerInPlace = false;

    private void Awake()
    {
        // Target moet via public in de inspector geset zijn om gizmo's te kunnen tekenen.
        // target = GetComponentInChildren<ObeliskTarget>(); // <- Gebruik deze als Target private is.
        shards = shardsParent.GetComponentsInChildren<Shard>();
        player = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        templeManager = FindObjectOfType<TempleManager>();
    }

    void Start()
    {
        foreach (Shard shard in shards)
        {
            // Pick random axis
            string[] dirs = new[] { "X", "Z" };

            // Dit is de regel die rekening houdt met ook de Y-as.
            // string[] dirs = new[] { "X", "Y" "Z" };

            int randomIndex = Random.Range(0, dirs.Length);
            string randomDir = dirs[randomIndex];
            shard.dir = randomDir;
            // Set zeroPos and maxPos
            shard.zeroPos = shard.transform.position;
            shard.maxPos = shard.transform.position + (shard.transform.position - shardsParent.transform.position) * 10;

            // Hieronder is de oude manier van het bepalen van de maxPos. Die is meer willekeurig.
            // shard.maxPos = shard.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-2, 5), Random.Range(-10, 10));

            // Set zeroRot and maxRotate
            shard.zeroRot = shard.transform.rotation.eulerAngles;
            shard.maxRotate = new Vector3(Random.Range(90f, 360f), Random.Range(90f, 360f), Random.Range(90f, 360f));
        }
    }

    void Update()
    {
        if (!frozen)
        {
            bool allShardsInPlace = true;

            // bereken dist en lrp
            Vector3 difference = player.transform.position - target.transform.position;
            var distX = Mathf.Abs(difference.x);
            var distY = Mathf.Abs(difference.y);
            var distZ = Mathf.Abs(difference.z);

            var lrpX = distX / maxDist;
            var lrpY = distY / maxDist;
            var lrpZ = distZ / maxDist;

            // Shards!
            foreach (Shard shard in shards)
            {
                Vector3 posDest = shard.transform.position;
                Vector3 rotDest = shard.transform.rotation.eulerAngles;

                // Als puzzel nog niet opgelost is
                if (!playerInPlace)
                {
                    // bereken destination
                    if (shard.dir == "X")
                    {
                        posDest = Vector3.Lerp(shard.zeroPos, shard.maxPos, lrpX);
                        rotDest = Vector3.Lerp(shard.zeroRot, shard.maxRotate, lrpX);
                    }
                    if (shard.dir == "Y")
                    {
                        posDest = Vector3.Lerp(shard.zeroPos, shard.maxPos, lrpY);
                        rotDest = Vector3.Lerp(shard.zeroRot, shard.maxRotate, lrpY);
                    }
                    if (shard.dir == "Z")
                    {
                        posDest = Vector3.Lerp(shard.zeroPos, shard.maxPos, lrpZ);
                        rotDest = Vector3.Lerp(shard.zeroRot, shard.maxRotate, lrpZ);
                    }
                }
                // Als puzzel opgelost is
                else
                {
                    // destination wordt zeropos
                    posDest = shard.zeroPos;
                    rotDest = shard.zeroRot;
                }

                // beweeg shards
                shard.transform.position = Vector3.SmoothDamp(shard.transform.position, posDest, ref dampVelocity, shardSmoothing);
                shard.transform.rotation = Quaternion.Euler(rotDest);

                // Zolang minstens één shard niet op zn plek is, maak allShardsInPlace false.
                if (Vector3.Distance(shard.transform.position, shard.zeroPos) > 0.005f)
                {
                    allShardsInPlace = false;
                }
            }

            // Zodra alle shards op hun plek zijn, eindig de puzzel en 'disable' dit script.
            if (allShardsInPlace && playerInPlace)
            {
                frozen = true;
                templeManager.ExitTemple();
                FindObjectOfType<AudioManager>().Play("ObeliskSolve");
                Debug.Log("Obelisk: All shards in place.");
            }

            // DEBUG SOLVE PUZZLE
            if (Input.GetKeyDown(KeyCode.P))
            {
                Solve();
                Debug.Log("Obelisk: Cheat used.");
            }
        }
    }

    // Los de puzzel op en freeze het blok. Aangeroepen vanuit SiderLocation script.
    public void Solve()
    {
        playerInPlace = true;
        Debug.Log("Obelisk: Player in place.");
    }

    private void OnDrawGizmos()
    {
        // Target
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireCube(target.transform.position, Vector3.one);
        // Max Distance
        Gizmos.color = new Color(255, 0, 255);
        Gizmos.DrawWireCube(target.transform.position, new Vector3(maxDist * 2, maxDist * 2, maxDist * 2));
        // Obelisk
        Gizmos.color = new Color(255, 255, 0);
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}