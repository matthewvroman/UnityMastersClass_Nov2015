using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour {

	[SerializeField] private string m_stringId;

	private Text m_text;

	private void Awake()
	{
		m_text = this.GetComponent<Text>();
	}

	private void OnEnable()
	{
		m_text.text = Strings.Get (m_stringId);
	}
}
