using UnityEngine;

public class Node : MonoBehaviour
{
	public Transform previousNode;

	public Transform nextNode;

	public float widthDistance = 5f;

	public Color nodeColor = Color.green;

	[HideInInspector]
	public TrafficMode trafficMode = TrafficMode.Go;

	[HideInInspector]
	public string nodeState;

	[HideInInspector]
	public string mode = "OneWay";

	[HideInInspector]
	public string parentPath;

	[HideInInspector]
	public bool firistNode;

	[HideInInspector]
	public bool lastNode;

	[HideInInspector]
	public bool trafficNode;

	private void OnDrawGizmos()
	{
		if (trafficNode)
		{
			switch (trafficMode)
			{
			case TrafficMode.Go:
				Gizmos.color = Color.green;
				Gizmos.DrawWireSphere(base.transform.position, 2f);
				break;
			case TrafficMode.Wait:
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(base.transform.position, 2f);
				break;
			case TrafficMode.Stop:
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(base.transform.position, 2f);
				break;
			}
		}
		Gizmos.color = nodeColor;
		Vector3 a = base.transform.TransformDirection(Vector3.left);
		Gizmos.DrawRay(base.transform.position, a * widthDistance);
		Gizmos.DrawRay(base.transform.position, a * (0f - widthDistance));
		Gizmos.DrawSphere(base.transform.position, 1f);
		if ((bool)nextNode)
		{
			Vector3 forward = base.transform.position - nextNode.position;
			forward.y = 0f;
			base.transform.rotation = Quaternion.LookRotation(forward);
		}
	}

	private void Awake()
	{
		if (!previousNode)
		{
			UnityEngine.Debug.LogError("previousNode is missing on : " + parentPath + " Node " + base.name);
		}
		if ((bool)nextNode)
		{
			if ((bool)nextNode.GetComponent<WaysControl>())
			{
				nodeState = "NextPoint";
			}
			else
			{
				nodeState = "PreviousPoint";
			}
		}
		else
		{
			UnityEngine.Debug.LogError("NextNode is missing on : " + parentPath + " Node " + base.name);
		}
	}
}
