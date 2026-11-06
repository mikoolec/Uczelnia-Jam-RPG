using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ConnectWiresMinigame : MinigameBase
{
    public Color[] availableColors = new Color[5];

    public WireDrag[] leftWires;
    public WireReceiver[] rightReceivers;

    private int connectedWires = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        connectedWires = 0;
        infoText.text = "Connect all the wires!";

        SetupRandomWires();
    }

    private void SetupRandomWires()
    {
        List<Color> leftColors = new List<Color>(availableColors);
        ShuffleList(leftColors);

        List<Color> rightColors = new List<Color>(availableColors);
        ShuffleList(rightColors);

        for (int i = 0; i < leftWires.Length; i++)
        {
            leftWires[i].SetColor(leftColors[i]);
            leftWires[i].ResetWire();
            leftWires[i].enabled = true;
        }

        for (int i = 0; i < rightReceivers.Length; i++)
        {
            rightReceivers[i].SetColor(rightColors[i]);
        }
    }
    
    public void OnWireConnected()
    {
        connectedWires++;
        if (connectedWires >= leftWires.Length)
        {
            EndMinigame(true, "All wires connected!");
        }
    }

    public void OnWrongWireConnected()
    {
        EndMinigame(false, "Wrong wire connected!");
    }

    protected override void OnMinigameEnding()
    {
        for (int i = 0; i < leftWires.Length; i++)
        {
            leftWires[i].enabled = false;
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
