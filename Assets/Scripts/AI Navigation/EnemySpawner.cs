using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyUnit;
    public float timeDelay;
    public float wait;
    // Start is called before the first frame update
    void Start()
    {
        wait = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (wait < Time.time)
        {
            GameObject currUnity = Instantiate(enemyUnit, transform.GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).rotation);
            currUnity.GetComponent<WaypointNav>().waypoints = transform.GetChild(0).gameObject;
            wait = Time.time + timeDelay;
        }
    }
}
