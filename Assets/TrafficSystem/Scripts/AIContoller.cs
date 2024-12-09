using UnityEngine;

public class AIContoller : MonoBehaviour
{
    public static AIContoller Instance;

    public int maxVehicles = 8;
    [Space]
    public GameObject[] vehiclesPrefabs;

    [HideInInspector] public int currentVehicles = 0;
    public Transform player;
    
    void Awake()
    {
        Instance = this;
        
    }

	private void Start()
	{
        //player = ReferenceManager.Instance.GameManager.CurrentBus.transform;
    }
}