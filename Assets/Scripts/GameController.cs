using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject TilePrefab;

    public GameObject Grid;

    private List<Tile> tiles = new List<Tile>();

    private Queue<int> usersSequence = new Queue<int>();
    private Queue<int> generatedSequence;

    enum State
    {
        PlayingSequence,
        ReadingSequence,
        GameOver
    }

    private State state;


    // Use this for initialization

    void Awake ()
	{
	    InitTiles();
	}

    void Start()
    {
        StartSession();
    }

    private void StartSession()
    {
        int numSteps = 2;
        generatedSequence = new Queue<int>();
        for (int i = 0; i < numSteps; i++)
        {
            AddNextStep();
        }

        StartCoroutine(PlaySequence(generatedSequence));
    }

    private void AddNextStep()
    {
        generatedSequence.Enqueue(Random.Range(0, tiles.Count));
    }

    IEnumerator PlaySequence(Queue<int> generatedSequence)
    {
        var playingSequence = new Queue<int>(generatedSequence);
        state = State.PlayingSequence;
        while (playingSequence.Count > 0)
        {
            int curTileIndx = playingSequence.Dequeue();
            var tile = tiles[curTileIndx];

            HighlightTile(tile);
            var animator = tile.GetComponent<Animator>();
            var animationTime = animator.GetCurrentAnimatorStateInfo(0).length ;

            Debug.Log(animationTime);
            yield return new WaitForSeconds(animationTime);
        }
        usersSequence.Clear();
        state = State.ReadingSequence;
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
        switch (state)
        {
            case State.ReadingSequence:
                usersSequence.Enqueue(tileIndx);

                if (usersSequence.SequenceEqual(generatedSequence))
                {
                    Debug.Log("you get it, try more");

                    AddNextStep();
                    StartCoroutine(PlaySequence(generatedSequence));
                }
                else if (usersSequence.Count == generatedSequence.Count)
                {
                    state = State.GameOver;
                    Debug.Log("game over");
                }
                break;
        }
    }

    private static void HighlightTile(Tile tile)
    {
        tile.GetComponent<Animator>().SetTrigger("highlight");
    }
}
