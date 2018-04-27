using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject TilePrefab;

    public GameObject Grid;

    private List<GameObject> tiles;

	// Use this for initialization
	void Start ()
	{
	    int tileIndx = 0;
	    foreach (var tile in Grid.GetComponentsInChildren<Tile>())
	    {
	        EventTrigger.Entry entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
	        var indx = tileIndx;
	        entry.callback.AddListener(x=>{ TileClicked(indx);});
	        var eventTrigger = tile.gameObject.AddComponent<EventTrigger>();
	        eventTrigger.triggers.Add(entry);
	        tileIndx++;
	    }
	    Grid.GetComponentsInChildren<EventTrigger>();
	}

    private void TileClicked(int tileIndx)
    {
        Debug.Log(string.Format("tile {0} was clicked", tileIndx));
    }

    
}
