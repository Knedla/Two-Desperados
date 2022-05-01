using System.Collections.Generic;
using UnityEngine;

public class BasicNodeMatrixGenerator : IGenerator<Node[][]>
{
    Cell[][] cells;

    int firewallNodeMaxIndex;
    int spamNodeMaxIndex;
    int treasureNodeMaxIndex;
    //int dataNodeCount;
    //int nodeCount;

    List<int> indexes;

    public BasicNodeMatrixGenerator(Cell[][] cells)
    {
        this.cells = cells;
        
        SetData();

        indexes = new List<int>();
        for (int i = 0; i < Framework.PlayerPreferenceData.NodeCountIncludeStartNode; i++)
            indexes.Add(i);
    }

    void SetData()
    {
        firewallNodeMaxIndex = Framework.PlayerPreferenceData.FirewallNodeCount;
        spamNodeMaxIndex = firewallNodeMaxIndex + Framework.PlayerPreferenceData.SpamNodeCount;
        treasureNodeMaxIndex = spamNodeMaxIndex + Framework.PlayerPreferenceData.TreasureNodeCount;
        //dataNodeCount = dataNodeCount +Framework.PlayerPreferenceData.NodeCountIncludeStartNode - Framework.PlayerPreferenceData.FirewallNodeCount - Framework.PlayerPreferenceData.SpamNodeCount - Framework.PlayerPreferenceData.TreasureNodeCount;
        //nodeCount = Framework.PlayerPreferenceData.NodeCountIncludeStartNode;
    }

    public Node[][] Generate()
    {
        Node[][] matrix = new Node[cells.Length][];

        for (int i = 0; i < matrix.Length; i++)
        {
            Cell[] currentColumn = cells[i];
            
            Node[] column = new Node[currentColumn.Length];
            matrix[i] = column;

            for (int j = 0; j < column.Length; j++)
                column[j] = InstanciateBestTypeForCell(currentColumn[j]);
        }

        return matrix;
    }

    Node InstanciateBestTypeForCell(Cell cell)
    {
        // ako je na ivici daj prvo start ili firewall
        // firewall probaj da guras sto dalje od starta, prakticno da sto mozes ostavis za preko pola
        // za treasure probaj da bude pojednaka razlidaljina izmedju nodova
        // ako je vec neki nod istog tipa blizu vidi koliko mozes da imas razmaka izmedju sledeceg (koliko je jos ostalo tog tipa da se raspodeli u odnosu na ukupan count) pa odluci dal moras da ga stavis ovde ili mozes da izaberes neki drugi cell...
        // spam bi bilo dobro da se gura u sredinu

        return GetNode(GetRandomIndex(), cell);
    }

    int GetRandomIndex()
    {
        int index = Random.Range(0, indexes.Count);
        int value = indexes[index];
        indexes.RemoveAt(index);
        return value;
    }

    Node GetNode(int index, Cell cell)
    {
        Node node;
        if (index == 0)
            node = new StartNode();
        else if (index <= firewallNodeMaxIndex)
            node = new FirewallNode();
        else if (index <= spamNodeMaxIndex)
            node = new SpamNode();
        else if (index <= treasureNodeMaxIndex)
            node = new TreasureNode();
        else
            node = new DataNode();

        node.SetCell(cell);
        return node;
    }
}
