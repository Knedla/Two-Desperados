using System.Collections.Generic;
using UnityEngine;

public class NetworkData
{
    public int TreasureNodeCount { get; set; }
    public HashSet<NetworkConnection> NetworkConnections { get; private set; }

    public NetworkData()
    {
        TreasureNodeCount = Framework.PlayerPreferenceData.TreasureNodeCount;
        GenerateData();
    }

    void GenerateData()
    {
        int minSpawnPerColumn;
        int maxSpawnPerColumn;
        int maxWidth;

        SetMatrixData(out minSpawnPerColumn, out maxSpawnPerColumn, out maxWidth);

        //Cell[][] cellMatrix = new BasicCellMatrixGenerator(minSpawnPerColumn, maxSpawnPerColumn, maxWidth).Generate();
        //Node[][] nodeMatrix = new BasicNodeMatrixGenerator(cellMatrix).Generate();
        Node[][] nodeMatrix = new MergedCellAndNodeMatrixGenerator(minSpawnPerColumn, maxSpawnPerColumn, maxWidth).Generate();
        NetworkConnections = new BasicNetworkConnectionGenerator(nodeMatrix).Generate(); // imam ideju kako bi spojio i MergedCellAndNodeMatrixGenerator sa BasicNetworkConnectionGenerator da se sve izgenerise u jednom prolazu, ali to bi bilo sacuvaj boze za debagovati... zato sam i inicijalno poodvajao sve...
    }

    void SetMatrixData(out int minSpawnPerColumn, out int maxSpawnPerColumn, out int maxWidth)
    {
        int size = Mathf.CeilToInt(Mathf.Sqrt(Framework.PlayerPreferenceData.NodeCountIncludeStartNode));

        if (size <= 2)
        {
            minSpawnPerColumn = size;
            maxSpawnPerColumn = size;
            maxWidth = size;
            return;
        }

        minSpawnPerColumn = Random.Range(Mathf.CeilToInt(size / 2), size); // kad je 2, budu iste vrednosti, na 3 bude Random.Range(2, 2) - s obzirom da je max exclusive ne znam dal moze da pravi problem - mislim da ne, koliko se secam vrati min cak i ako je max manji
        maxSpawnPerColumn = size;
        maxWidth = Mathf.CeilToInt(Framework.PlayerPreferenceData.NodeCountIncludeStartNode / minSpawnPerColumn);
    }
}
