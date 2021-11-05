using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObstacle : MonoBehaviour {

	public GameObject bulletPrefab;
	public float bulletLifeTime = 3f;
	public float bulletSpawnInterval = 2f;
	public float bulletSpeed = 180f;

	void Start () {
		InvokeRepeating("SpawnBullet", bulletSpawnInterval, bulletSpawnInterval);
	}

	void SpawnBullet(){



		var bullet = (GameObject)Instantiate (
        bulletPrefab,
        new Vector3(transform.position.x, transform.position.y, bulletPrefab.transform.position.z),
        bulletPrefab.transform.rotation * transform.rotation);

    // Add velocity to the bullet
	// bullet.transform.position = new Vector3(0, 0, 0);
	// bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 4 * 10);
	var rgd = bullet.GetComponent<Rigidbody2D>();
	// Physics2D.IgnoreCollision(rgd.GetComponent<Collider2D>(),  GetComponent<Collider2D>());
	// rgd.velocity = bullet.transform.forward * 400f;
	// bullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(0,bulletSpeed,0));

	rgd.AddForce(transform.up * bulletSpeed);
    // bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.forward * Time.deltaTime * 6;

    // Destroy the bullet after 2 seconds
    Destroy(bullet, bulletLifeTime);


		// Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity);
	}
	

}
