using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundry
{
	public float xMin, xMax, zMin, zMax;
}

public class playerController : MonoBehaviour 
{
	private Rigidbody rb;
	private AudioSource audioSource;
    public SimpleTouchpad touchPad;
    public SimpleButton areaButton;
	public Boundry boundry;
	public float tilt;
	public float speed;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (areaButton.CanFire () && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate; 
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}
	}

	void FixedUpdate ()
	{
        Vector2 direction = touchPad.GetDirection ();
        Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
        rb.velocity = movement * speed;

        rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundry.xMin, boundry.xMax), 
			0.0f, 
			Mathf.Clamp (rb.position.z, boundry.zMin, boundry.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
