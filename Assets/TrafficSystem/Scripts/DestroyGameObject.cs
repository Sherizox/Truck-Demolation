using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public float clearDistance = 150.0f;
    public GameObject myRoot;
    public Renderer myBody;
	public bool isAuto = false;
   
    void Update()
    {
		if (isAuto == false)
        {
			if (!AIContoller.Instance.player)
				return;
            
			if (Vector3.Distance (transform.position, AIContoller.Instance.player.transform.position) > clearDistance)
            {
                myRoot.SetActive(false);
            }
		}
    }
    
    private void OnDisable()
    {       
        if (AIContoller.Instance.currentVehicles > 0)
            AIContoller.Instance.currentVehicles--;
    }
}