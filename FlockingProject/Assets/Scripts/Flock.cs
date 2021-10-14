using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockingManager myManager;

    public float speed;
    private Vector3 direction;

    private float timePassed;
    private float seconds;

    private bool turning;
    // Start is called before the first frame update
    void Start()
    {
        seconds = 1.5f;
        timePassed = seconds; // In order to make the Rules occur when the first frame
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        turning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed >= seconds)
        {
            if (Random.Range(0.0f, 100.0f) < 75.0f)
            {
                FlockingRules();
            }

            if (Random.Range(0.0f, 100.0f) < 10.0f)
            {
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            }

            timePassed = 0.0f;
        }
        timePassed += Time.deltaTime;


        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }

    void FlockingRules()
    {
        Debug.Log("Acabo de hacer las rules");
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;
        int num = 0;

        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    // Cohesion
                    cohesion += go.transform.position;

                    // Align
                    align += go.GetComponent<Flock>().direction;
                    num++;

                    // Separation
                    separation -= (transform.position - go.transform.position) / (distance * distance);
                }
            }
        }

        if (num > 0)
        {
            // Cohesion
            cohesion = (cohesion / num - transform.position).normalized * speed;

            // Align
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }
        
        // Final computation
        direction = (cohesion + align + separation).normalized * speed;
    }

    //bool isOutOfLimits()
    //{
    //    if (transform.position.x >= myManager.limits.x || transform.position.y >= myManager.limits.y || transform.position.z >= myManager.limits.z ||
    //        transform.position.x <= 0.0f || transform.position.y <= 0.0f || transform.position.z <= 0.0f)
    //        return true;

    //    if (transform.position.x < myManager.limits.x && transform.position.y < myManager.limits.y && transform.position.z < myManager.limits.z &&
    //        transform.position.x > 0.0f && transform.position.y > 0.0f && transform.position.z > 0.0f)
    //        return false;
    //    return true;
    //}

    //void ManageDirection(bool turnDir)
    //{
    //    if (turnDir)
    //    {
    //        direction = transform.position; 
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        if (timePassed >= seconds)
    //        {
    //            if (Random.Range(0.0f, 100.0f) < 75.0f)
    //            {
    //                FlockingRules();
    //            }

    //            if (Random.Range(0.0f, 100.0f) < 10.0f)
    //            {
    //                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    //            }

    //            timePassed = 0.0f;
    //        }
    //        timePassed += Time.deltaTime;
    //    }
    //}
}
