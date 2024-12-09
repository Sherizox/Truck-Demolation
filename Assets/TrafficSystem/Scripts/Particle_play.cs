using UnityEngine;

public class Particle_play : MonoBehaviour
{
	private ParticleSystem ps;

	private void Start()
	{
		ps = GetComponent<ParticleSystem>();
		ps.Play();
	}
}
