using UnityEngine;
using System;
using System.Collections;
using MiniJSON;

public class GameManager {

	private static GameManager s_instance;
	public static GameManager Instance
	{
		get
		{
			if(s_instance == null)
			{
				s_instance = new GameManager();
			}

			return s_instance;
		}
	}

	private const string SAVE_DATA_KEY = "GameSaveData";

	public GameManager()
	{
		Debug.Log ("Hello World");

		

	}

	public void Initialize()
	{
		//init game config
		//init strings
		Strings.ParseFromXML ("xml/en-us/strings");
		GameConfig.ParseFromXML ("xml/game_config");
		LoadData ();
		Debug.Log(GameConfig.GetIntValue("MAX_CHARACTER_SPEED"));
		Debug.Log (GameConfig.GetBoolValue ("IS_TRIAL"));
	}

	private void LoadData()
	{
		string serializedData = PlayerPrefs.GetString (SAVE_DATA_KEY);
		if (serializedData != string.Empty) 
		{
			Hashtable hashtable = (Hashtable)Json.Deserialize(SAVE_DATA_KEY);
		}
	}

	public void SaveData()
	{
		Hashtable hashtable = new Hashtable();

		PlayerPrefs.SetString (SAVE_DATA_KEY, Json.Serialize(hashtable));
		PlayerPrefs.Save();
	}
}
