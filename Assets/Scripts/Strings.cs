using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Strings {

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

	public static string Get(string id)
	{
		return s_dictionary[id];
	}
}
