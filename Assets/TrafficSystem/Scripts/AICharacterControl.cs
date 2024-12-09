using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class AICharacterControl : MonoBehaviour
{
	public Transform playerRoot;

	public AudioClip[] deathSoundClips;

	private Vector3 minBoundsPoint;

	private Vector3 maxBoundsPoint;

	private float boundsSize = float.NegativeInfinity;

	private bool findTarget;

	public NavMeshAgent agent
	{
		get;
		private set;
	}

	public ThirdPersonCharacter character
	{
		get;
		private set;
	}

	protected virtual void MuckAbout()
	{
		if (agent.desiredVelocity.magnitude < 0.1f)
		{
			agent.SetDestination(GetRandomTargetPoint());
		}
	}

	private Vector3 GetRandomTargetPoint()
	{
		if (boundsSize < 0f)
		{
			minBoundsPoint = Vector3.one * float.PositiveInfinity;
			maxBoundsPoint = -minBoundsPoint;
			NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
			Vector3[] vertices = navMeshTriangulation.vertices;
			Vector3[] array = vertices;
			for (int i = 0; i < array.Length; i++)
			{
				Vector3 vector = array[i];
				if (minBoundsPoint.x > vector.x)
				{
					minBoundsPoint = new Vector3(vector.x, minBoundsPoint.y, minBoundsPoint.z);
				}
				if (minBoundsPoint.y > vector.y)
				{
					minBoundsPoint = new Vector3(minBoundsPoint.x, vector.y, minBoundsPoint.z);
				}
				if (minBoundsPoint.z > vector.z)
				{
					minBoundsPoint = new Vector3(minBoundsPoint.x, minBoundsPoint.y, vector.z);
				}
				if (maxBoundsPoint.x < vector.x)
				{
					maxBoundsPoint = new Vector3(vector.x, maxBoundsPoint.y, maxBoundsPoint.z);
				}
				if (maxBoundsPoint.y < vector.y)
				{
					maxBoundsPoint = new Vector3(maxBoundsPoint.x, vector.y, maxBoundsPoint.z);
				}
				if (maxBoundsPoint.z < vector.z)
				{
					maxBoundsPoint = new Vector3(maxBoundsPoint.x, maxBoundsPoint.y, vector.z);
				}
			}
			boundsSize = Vector3.Distance(minBoundsPoint, maxBoundsPoint);
		}
		Vector3 sourcePosition = new Vector3(UnityEngine.Random.Range(minBoundsPoint.x, maxBoundsPoint.x), UnityEngine.Random.Range(minBoundsPoint.y, maxBoundsPoint.y), UnityEngine.Random.Range(minBoundsPoint.z, maxBoundsPoint.z));
		NavMesh.SamplePosition(sourcePosition, out NavMeshHit hit, boundsSize / 100f, 1);
		return hit.position;
	}

	private void Start()
	{
		agent = GetComponentInChildren<NavMeshAgent>();
		character = GetComponent<ThirdPersonCharacter>();
		agent.updateRotation = false;
		agent.updatePosition = true;
	}

	private void DisableRagdoll(bool active)
	{
		Component[] componentsInChildren = playerRoot.GetComponentsInChildren(typeof(Rigidbody));
		Component[] componentsInChildren2 = playerRoot.GetComponentsInChildren(typeof(Collider));
		Component[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Rigidbody rigidbody = (Rigidbody)array[i];
			rigidbody.isKinematic = !active;
		}
		Component[] array2 = componentsInChildren2;
		for (int j = 0; j < array2.Length; j++)
		{
			Collider collider = (Collider)array2[j];
			collider.enabled = active;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Vehicle") && collision.rigidbody.velocity.magnitude > 10f)
		{
			UnityEngine.Object.Destroy(GetComponent<AICharacterControl>());
			GetComponent<Rigidbody>().isKinematic = true;
			base.transform.GetComponent<Collider>().isTrigger = true;
			base.transform.GetComponent<Animator>().enabled = false;
			UnityEngine.Object.Destroy(agent);
			GetComponent<ThirdPersonCharacter>().enabled = false;
			DisableRagdoll(active: true);
			GetComponent<AudioSource>().clip = deathSoundClips[Random.Range(0, deathSoundClips.Length)];
			GetComponent<AudioSource>().Play();
			UnityEngine.Object.Destroy(base.gameObject, 10f);
		}
	}

	private void Update()
	{
		if (!agent.enabled)
		{
			return;
		}
		if (agent.enabled)
		{
			if (agent.pathPending)
			{
				return;
			}
			MuckAbout();
		}
		if (agent.remainingDistance > agent.stoppingDistance)
		{
			character.Move(agent.desiredVelocity / 2f, crouch: false, jump: false);
		}
		else
		{
			character.Move(Vector3.zero, crouch: false, jump: false);
		}
	}
}
