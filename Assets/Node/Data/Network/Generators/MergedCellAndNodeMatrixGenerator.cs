using System.Collections.Generic;
using UnityEngine;

public class MergedCellAndNodeMatrixGenerator : IGenerator<Node[][]>
{
    int minSpawnPerColumn;
    int maxSpawnPerColumn;
    int maxWidth;

    int firewallNodeMaxIndex;
    int spamNodeMaxIndex;
    int treasureNodeMaxIndex;

    List<int> indexes;
    int itemsToGenerate;

    public MergedCellAndNodeMatrixGenerator(int minSpawnPerColumn, int maxSpawnPerColumn, int maxWidth)
    {
        this.minSpawnPerColumn = minSpawnPerColumn;
        this.maxSpawnPerColumn = maxSpawnPerColumn;
        this.maxWidth = maxWidth;

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
    }

    public Node[][] Generate()
    {
        // na kraju moze da se desi da ne budu instancirane sve kolone
        // ako ima 16 nodova ukupno, to je prirodno 4 * 4 matrica
        // zarad raznovrsnosti, uzme se da matrica moze da se srecuje da bude 8 * 2 - horizontal oriented
        // zbog raznovrsnosti, broj nodova po koloni moze biti od 2 (strecovana matrica) do 4 (prirodna matrica)
        // ako svaka od prve 4 kolone instancira po 4 noda, potrosice se svih 16 nodova i zadnje 4 kolone ce ostati prazne - mozda nesto izmanipulisati, da centar bude 0 ppa da se ide od -x do +x zbog iscrtavanja
        Node[][] matrix = new Node[maxWidth][];

        itemsToGenerate = Framework.PlayerPreferenceData.NodeCountIncludeStartNode;

        for (int i = 0; i < maxWidth; i++)
        {
            Node[] column = GetMatrixColumn(i, minSpawnPerColumn, maxSpawnPerColumn);
            matrix[i] = column;

            if (itemsToGenerate <= 0)
                break;
        }

        return matrix;
    }

    Node[] GetMatrixColumn(int columnIndex, int minSpawnPerColumn, int maxSpawnPerColumn)
    {
        int spawnCount = Random.Range(minSpawnPerColumn, maxSpawnPerColumn + 1);
        Node[] column = new Node[spawnCount];

        int removeCount = maxSpawnPerColumn - spawnCount;

        int index = 0;
        for (int i = 0; i < maxSpawnPerColumn; i++)
        {
            if (removeCount > 0 && RandomHelper.RandomBool)
            {
                removeCount--;
                continue;
            }

            column[index] = InstanciateBestTypeForCell(new Cell(columnIndex, i));
            
            itemsToGenerate--;
            if (itemsToGenerate == 0)
                break;

            index++;
            if (index == spawnCount)
                break;
        }

        return column;
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
