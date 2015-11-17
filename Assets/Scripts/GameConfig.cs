using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class GameConfig {

	private static Dictionary<string, string>s_dictionary;
	
	public static void ParseFromXML(string path)
	{
		s_dictionary = new Dictionary<string, string> ();
		
		TextAsset textAsset = Resources.Load (path) as TextAsset;
		XmlDocument document = new XmlDocument ();
		document.LoadXml (textAsset.text);

		foreach(XmlNode node in document.DocumentElement.ChildNodes) 
		{
			s_dictionary.Add(node.Name, node.InnerText);
		}
	}

	public static int GetIntValue(string id)
	{
		return int.Parse(s_dictionary [id]);
	}

	public static float GetFloatValue(string id)
	{
		return float.Parse(s_dictionary[id]);
	}

	public static bool GetBoolValue(string id)
	{
		return bool.Parse(s_dictionary[id]);
	}
}
