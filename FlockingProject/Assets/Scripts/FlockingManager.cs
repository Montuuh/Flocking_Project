using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
	[Header("General Settings")]
	public GameObject fishPrefab;
	[Range(0.0f,50.0f)]
	public int numFish;
	public GameObject[] allFish;
	public Vector3 limits;
	[Header("Fish Settings")]
	[Range(0.0f,5.0f)]
	public float minSpeed;
	[Range(0.0f,5.0f)]
	public float maxSpeed;
	[Range(0.0f, 10.0f)]
	public float neighbourDistance;
	[Range(0.0f, 5.0f)]
	public float rotationSpeed;

	// Start is called before the first frame update
	void Start()
	{
		FlockGeneration();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void FlockGeneration()
	{
		
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; ++i)
        {
			Vector3 pos = /*this.transform.position +*/ RandomPosition(); // random position

			Vector3 randomize = RandomDirection(); // random vector direction

			allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.LookRotation(randomize));
            allFish[i].GetComponent<Flock>().myManager = this;
        }
    }

	Vector3 RandomPosition()
    {
		Vector3 pos;
		pos.x = Random.Range(0, limits.x);
		pos.y = Random.Range(0, limits.y);
		pos.z = Random.Range(0, limits.z);
		return pos;
	}

	Vector3 RandomDirection()
    {
		Vector3 dir;
		dir.x = Random.Range(-1.0f, 1.0f);
		dir.y = Random.Range(-1.0f, 1.0f);
		dir.z = Random.Range(-1.0f, 1.0f);
        dir.Normalize();
		return dir;
    }
}
