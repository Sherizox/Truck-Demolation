using System;
using System.Collections.Generic;
using UnityEngine;

public class CarComponents : MonoBehaviour
{
	[Serializable]
	public class CameraViewSetting
	{
		public List<Transform> cameraViews;

		public float distance = 5f;

		public float height = 1f;

		public float Angle = 20f;
	}

	public Transform handleTrigger;

	public Transform door;

	public Transform sitPoint;

	public Transform driver;

	public AudioClip[] deathSoundClips;

	public CameraViewSetting cameraViewSetting;

	[HideInInspector]
	public bool driving = true;

	private void Update()
	{
		if (!driver)
		{
			return;
		}
		if (driving)
		{
			driver.position = sitPoint.position;
			driver.rotation = sitPoint.rotation;
			return;
		}
		driver.position = handleTrigger.position;
		driver.rotation = handleTrigger.rotation;
		Component[] componentsInChildren = driver.GetComponentsInChildren(typeof(Rigidbody));
		Component[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Rigidbody rigidbody = (Rigidbody)array[i];
			rigidbody.isKinematic = false;
		}
		Component[] componentsInChildren2 = driver.GetComponentsInChildren(typeof(Collider));
		Component[] array2 = componentsInChildren2;
		for (int j = 0; j < array2.Length; j++)
		{
			Collider collider = (Collider)array2[j];
			collider.enabled = true;
		}
		driver.GetComponent<AudioSource>().clip = deathSoundClips[UnityEngine.Random.Range(0, deathSoundClips.Length)];
		driver.GetComponent<AudioSource>().Play();
		UnityEngine.Object.Destroy(driver.gameObject, 10f);
		driver.parent = null;
		driver = null;
	}
}
