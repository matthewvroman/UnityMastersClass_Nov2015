using UnityEngine;
using System.Collections;

public class MoveToClickExample : MonoBehaviour {
	
	[SerializeField] private float m_minDistance = 25.0f;
	[SerializeField] private GameObject m_player;
	[SerializeField] private float m_playerSpeed;
	
	private CharacterController m_characterController;
	
	private Vector3 m_desiredPosition;
	
	private void Awake()
	{
		m_desiredPosition = m_player.transform.position;
	}
	
	private void Start()
	{
		m_characterController = m_player.GetComponent<CharacterController> ();
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
			}
		}

		Vector3 yLessDesiredPosition = m_desiredPosition;
		yLessDesiredPosition.y = 0.0f;
		
		Vector3 yLessPlayerPosition = m_player.transform.position;
		yLessPlayerPosition.y = 0.0f;
		
		Vector3 movement = Physics.gravity * Time.deltaTime;
		
		if (Vector3.Distance (yLessDesiredPosition, yLessPlayerPosition) > m_minDistance) {
			Vector3 direction = m_desiredPosition - m_player.transform.position;
			direction.Normalize ();
			movement += direction * m_playerSpeed * Time.deltaTime;
		}
		
		m_characterController.Move (movement);
		
	}
}
