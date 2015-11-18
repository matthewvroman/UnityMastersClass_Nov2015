using UnityEngine;
using System.Collections;

public class NavmeshExample : MonoBehaviour {

	[SerializeField] private float m_minDistance = 0.25f;
	[SerializeField] private GameObject m_player;
	[SerializeField] private float m_playerSpeed;

	private Vector3 m_desiredPosition;

	private NavMeshAgent m_navMeshAgent;

	private void Awake()
	{
		m_desiredPosition = m_player.transform.position;
	}

	private void Start()
	{
		m_navMeshAgent = m_player.AddComponent<NavMeshAgent> ();
	}

	private void Update()
	{
		if (Input.GetMouseButton (0)) 
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if(Physics.Raycast(mouseRay, out hitInfo))
			{
				Debug.Log(hitInfo.point);
				m_desiredPosition = hitInfo.point;
				m_navMeshAgent.SetDestination(m_desiredPosition);
				m_navMeshAgent.speed = m_playerSpeed;
				m_navMeshAgent.angularSpeed = 999.0f; //I want instant turning
				m_navMeshAgent.acceleration = m_playerSpeed;
				m_navMeshAgent.stoppingDistance = m_minDistance;
			}
		}


	}
}
