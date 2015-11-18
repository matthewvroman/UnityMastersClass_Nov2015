using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]
public class AttributesExample : MonoBehaviour {

	private enum SerializedEnum
	{
		Value1,
		Value2,
		Value3,
		Value4
	}

	[System.Serializable]
	private struct SerializedStruct
	{
		[SerializeField] private string structName;
		[SerializeField] private int structValue;
		[SerializeField] private SerializedEnum structEnum;
	}

	[SerializeField] private string m_string;
	
	[SerializeField] private string m_defaultString = "default";

	[SerializeField] private float m_normalFloat;

	[SerializeField] [Range(0.0f, 100.0f)] private float m_rangedFloat;

	[Tooltip("This slider can't go past 100!")] [SerializeField] [Range(0, 100)] private int m_rangedInt;

	[SerializeField] private SerializedEnum m_serializedEnum;

	[SerializeField] private SerializedStruct m_serializedStruct;

	[SerializeField] private SerializedStruct[] m_serializedStructArray;

	[HideInInspector] public int m_hiddenPublicInt;

	[System.NonSerialized] public float m_hiddenPublicFloat;

	[SerializeField] private Vector3 m_rotationSpeed;

	[ContextMenu("RenameGameObject")]
	private void RenameGameObject()
	{
		this.gameObject.name = m_defaultString;
	}

}
