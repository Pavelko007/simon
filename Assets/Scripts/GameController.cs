using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject TilePrefab;

    public GameObject Grid;

    private List<Tile> tiles = new List<Tile>();

	// Use this for initialization
	void Awake ()
	{
	    InitTiles();
	}

    void Start()
    {
        int numSteps = 3;
        Queue<int> indxSequence = new Queue<int>();
        for (int i = 0; i < numSteps; i++)
        {
            indxSequence.Enqueue(Random.Range(0, tiles.Count));
        }

        StartCoroutine(PlaySequence(indxSequence));
    }

    IEnumerator PlaySequence(Queue<int> indxSequence)
    {
        while (indxSequence.Count > 0)
        {
            int curTileIndx = indxSequence.Dequeue();
            var tile = tiles[curTileIndx];

            HighlightTile(tile);
            var animator = tile.GetComponent<Animator>();
            var animationTime = animator.GetCurrentAnimatorStateInfo(0).length ;

            Debug.Log(animationTime);
            yield return new WaitForSeconds(animationTime);
        }
    }
    private void InitTiles()
    {
        int tileIndx = 0;
        foreach (var tile in Grid.GetComponentsInChildren<Tile>())
        {
            tiles.Add(tile);
            EventTrigger.Entry entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
            var indx = tileIndx;
            entry.callback.AddListener(x => { TileClicked(indx); });
            var eventTrigger = tile.gameObject.AddComponent<EventTrigger>();
            eventTrigger.triggers.Add(entry);
            tileIndx++;
        }
    }

    private void TileClicked(int tileIndx)
    {
        var tile = tiles[tileIndx];
    }

    private static void HighlightTile(Tile tile)
    {
        tile.GetComponent<Animator>().SetTrigger("highlight");
    }
}
