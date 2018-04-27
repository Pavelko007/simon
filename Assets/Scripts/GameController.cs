using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject TilePrefab;

    public GameObject Grid;

    private List<Tile> tiles = new List<Tile>();

	// Use this for initialization
	void Start ()
	{
	    int tileIndx = 0;
	    foreach (var tile in Grid.GetComponentsInChildren<Tile>())
	    {
            tiles.Add(tile);
	        EventTrigger.Entry entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
	        var indx = tileIndx;
	        entry.callback.AddListener(x=>{ TileClicked(indx);});
	        var eventTrigger = tile.gameObject.AddComponent<EventTrigger>();
	        eventTrigger.triggers.Add(entry);
	        tileIndx++;
	    }
	}

    private void TileClicked(int tileIndx)
    {
        tiles[tileIndx].GetComponent<Animator>().SetTrigger("highlight");

        Debug.Log(string.Format("tile {0} was clicked", tileIndx));
    }

    
}
