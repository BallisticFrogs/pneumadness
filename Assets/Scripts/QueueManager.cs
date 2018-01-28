using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

public class QueueManager : MonoBehaviour
{
    public static QueueManager INSTANCE;

    private Queue<PipeTile> pipeQueue = new Queue<PipeTile>();

    private List<KeyValuePair<PipeTile, int>> availableTubeWithProba = new List<KeyValuePair<PipeTile, int>>();

    public PipeTile tubeBendDr;
    public PipeTile tubeBendDl;
    public PipeTile tubeBendUl;
    public PipeTile tubeBendUr;
    public PipeTile tubeCross;
    public PipeTile tubeStraightV;
    public PipeTile tubeStraightH;
    public PipeTile tubeTD;
    public PipeTile tubeTL;
    public PipeTile tubeTR;
    public PipeTile tubeTU;

    // Use this for initialization
    void Awake()
    {
        INSTANCE = this;
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeBendDr, 9));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeBendDl, 9));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeBendUl, 9));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeBendUr, 9));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeStraightV, 22));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeStraightH, 22));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeTD, 4));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeTL, 4));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeTR, 4));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeTU, 4));
        availableTubeWithProba.Add(new KeyValuePair<PipeTile, int>(tubeCross, 4));

        for (int i = 0; i < 5; i++)
        {
            pipeQueue.Enqueue(GetRandomTubeWithProba());
        }

	}

    public PipeTile DequeuePipe()
    {
        pipeQueue.Enqueue(GetRandomTubeWithProba());
        return pipeQueue.Dequeue();
    }

    public PipeTile[] GetNextPipes()
    {
        return pipeQueue.ToArray();
    }

    public PipeTile GetNextPipe()
    {
        return pipeQueue.Peek();
    }

    private PipeTile GetRandomTubeWithProba()
    {
        PipeTile selectedTile = null;

        int diceRoll = Random.Range(0, 100);

        int cumulative = 0;
        for (int i = 0; i < availableTubeWithProba.Count; i++)
        {
            cumulative += availableTubeWithProba[i].Value;
            if (diceRoll < cumulative)
            {
                selectedTile =  availableTubeWithProba[i].Key;
                break;
            }
        }
        return selectedTile;
    }
}
