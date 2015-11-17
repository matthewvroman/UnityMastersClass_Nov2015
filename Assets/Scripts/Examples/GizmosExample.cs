using UnityEngine;
using UnityEditor;
using System.Collections;

public class GizmosExample : MonoBehaviour {

	[System.Serializable]
	private struct Offset
	{
		[SerializeField] private string m_offsetName;
		public string OffsetName { get { return m_offsetName; } }

		[SerializeField] private Vector3 m_offsetPosition;
		public Vector3 OffsetPosition { get { return m_offsetPosition; } }

		[SerializeField] private Color m_offsetColor;
		public Color OffsetColor { get { return m_offsetColor; } }
	}

	[SerializeField] private string m_label;

	[SerializeField] private Color m_wireSphereColor = Color.red;
	[SerializeField] private float m_wireSphereRadius = 3.0f;

	[SerializeField] private Vector3 m_globalOffsetSize = Vector3.one;
	[SerializeField] private Offset[] m_offsets;


	private void OnDrawGizmos()
	{
		Handles.Label (this.transform.position, m_label);

		Gizmos.color = m_wireSphereColor;
		Gizmos.DrawWireSphere (this.transform.position, m_wireSphereRadius);
	}

	private void OnDrawGizmosSelected()
	{
		if (m_offsets != null) 
		{
			int numOffsets = m_offsets.Length;
			for (int i=0; i<numOffsets; i++) {
				Offset offset = m_offsets [i];

				Vector3 position = this.transform.position + offset.OffsetPosition;

				Handles.Label (position, offset.OffsetName);
			
				Gizmos.color = offset.OffsetColor;
				Gizmos.DrawWireCube (position, m_globalOffsetSize);
				Gizmos.DrawLine(position, this.transform.position);
			}
		}
	}
}
