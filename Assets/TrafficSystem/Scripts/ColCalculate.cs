using System.Collections.Generic;
using UnityEngine;

public class ColCalculate : MonoBehaviour
{
	public GameObject spark;

	private Rigidbody rig;

	private GameObject sparkclone;

	private Vector3 pos;

	private List<GameObject> destroyparticle = new List<GameObject>();

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (base.gameObject.GetComponent<AIVehicle>().vehicleStatus == VehicleStatus.Player && destroyparticle.Count > 1)
		{
			for (int i = 0; i < destroyparticle.Count - 1; i++)
			{
				UnityEngine.Object.Destroy(destroyparticle[i]);
			}
			destroyparticle.RemoveRange(0, destroyparticle.Count - 1);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (base.gameObject.GetComponent<AIVehicle>().vehicleStatus == VehicleStatus.Player && rig.velocity.magnitude >= 3f && (collision.gameObject.layer == 0 || collision.gameObject.layer == 10 || collision.gameObject.layer == 13))
		{
			ContactPoint contactPoint = collision.contacts[0];
			UnityEngine.Debug.Log(collision.contacts[0]);
			pos = contactPoint.point;
			sparkclone = UnityEngine.Object.Instantiate(spark, pos, Quaternion.identity);
			destroyparticle.Add(sparkclone);
		}
	}
}
