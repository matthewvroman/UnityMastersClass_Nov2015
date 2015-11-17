using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class PreprocessorExample : MonoBehaviour {

	private Text m_text;

	private void Awake()
	{
		m_text = this.GetComponent<Text> ();
		m_text.text = string.Empty;

#if UNITY_5
		AppendText("UNITY_5");
#elif UNITY_4
		AppendText("UNITY_4");
#endif

#if UNITY_EDITOR
		AppendText("UNITY_EDITOR");
#endif

#if CUSTOM_PREPROCESSOR
		AppendText("CUSTOM_PREPROCESSOR");
#endif

#if UNITY_ANDROID
		AppendText("UNITY_ANDROID");
#elif UNITY_IPHONE
		AppendText("UNITY_IPHONE");
#elif UNITY_STANDALONE_WINDOWS
		AppendText("UNITY_STANDALONE_WINDOWS");
#elif UNITY_STANDALONE_OSX
		AppendText("UNITY_STANDALONE_OSX");
#endif
	}

	private void AppendText(string text)
	{
		m_text.text += text + "\n";
	}
}
