using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Transform target;
    private int wayPointIndex = 0;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[wayPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed; // very neat solution for returning to original speed
        //Unity calls update function (not at the same time, but one after another) so we can get offset by single frame.
        //So enemyMovement update could be called before turret update().
        //We can make turret update run before enemyMovement -- Script Execution Order
    }

    private void GetNextWayPoint()
    {
        if (wayPointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return; // that why we added return
        }

        wayPointIndex += 1;
        target = Waypoints.points[wayPointIndex];
    }

    private void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject); //Destroy operation takes some time
    }
}
