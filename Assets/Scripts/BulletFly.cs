using UnityEngine;
using System.Collections;

public class BulletFly : MonoBehaviour
{

	public int team;
	public GameObject currentDetonator;
	public GameObject owner;
	private const float BULLET_SPEED = 2.5f;

	void Start()
	{

	}

	void OnTriggerEnter(Collider collider)
	{
		var engine = collider.GetComponent<Engine>();
		if (engine != null && engine.team == team)
		{
			return;
		}

		var health = collider.gameObject.GetComponent<Health>();
		if (health != null)
		{
			health.onDamage(1);

			var expl = Instantiate(GameObject.Find("Explo"), collider.transform.position, Quaternion.identity) as GameObject;
			expl.GetComponent<Detonator>().Explode();

			DestroyObject(gameObject);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	void FixedUpdate()
	{

		float yaw = transform.rotation.eulerAngles.y;

		Vector3 pos = transform.position;
		pos.x -= Mathf.Cos(Mathf.Deg2Rad * yaw) * BULLET_SPEED;
		pos.z += Mathf.Sin(Mathf.Deg2Rad * yaw) * BULLET_SPEED;

		float size = collider.bounds.size.y;
		if (pos.x + size < -Engine.SCENE_SIZE || pos.x - size > Engine.SCENE_SIZE ||
			pos.z + size < -Engine.SCENE_SIZE || pos.z - size > Engine.SCENE_SIZE)
		{
			DestroyObject(gameObject);
			return;
		}

		transform.position = pos;

	}
}
