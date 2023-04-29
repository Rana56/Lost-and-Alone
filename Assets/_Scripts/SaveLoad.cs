using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private GameObject player;     

    private const string SAVEGAME_FILE = "Assets/Saves/savegame.xml";

    public void Save()
	{
		XmlDocument xmlDocument = new XmlDocument();
		PlayerState state = new PlayerState(player.transform.position, player.transform.rotation);
		XmlSerializer serializer = new XmlSerializer(typeof(PlayerState));
		using (MemoryStream stream = new MemoryStream())
		{
			serializer.Serialize(stream, state);
			stream.Position = 0;
			xmlDocument.Load(stream);
			xmlDocument.Save(SAVEGAME_FILE);
            Debug.Log("save");
		}
	}

	public void Load()
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(SAVEGAME_FILE);
		string xmlString = xmlDocument.OuterXml;
		
		PlayerState state;
		using (StringReader read = new StringReader(xmlString))
		{
			XmlSerializer serializer = new XmlSerializer(typeof(PlayerState));
			using (XmlReader reader  = new XmlTextReader(read))
			{
				state = (PlayerState) serializer.Deserialize(reader);
			}
		}
		
        player.GetComponent<CharacterController>().enabled = false;
		player.transform.position = state.position;
		player.transform.rotation = state.rotation;
        player.GetComponent<CharacterController>().enabled = true;

        Debug.Log("load");
	}
}

[Serializable]
public struct PlayerState
{
	public Vector3 position;
	public Quaternion rotation;

	public PlayerState (Vector3 position, Quaternion rotation)
	{
		this.position = position;
		this.rotation = rotation;
	}
}
