using System.Collections.Generic;

public class BasicNetworkConnectionGenerator : IGenerator<HashSet<NetworkConnection>>
{
    Node[][] matrix;

    public BasicNetworkConnectionGenerator(Node[][] matrix)
    {
        this.matrix = matrix;
    }

    public HashSet<NetworkConnection> Generate()
    {
        HashSet<NetworkConnection> networkConnections = new HashSet<NetworkConnection>();

        for (int i = 0; i < matrix.Length; i++)
        {
            Node[] currentColumn = matrix[i];

            if (currentColumn == null)
                break;

            for (int j = 0; j < currentColumn.Length; j++)
            {
                if (j < currentColumn.Length - 1)
                {
                    if (i < matrix.Length - 1)
                    {
                        Node[] nextColumn = matrix[i + 1]; // desna kolona

                        if (nextColumn != null)
                        {
                            Node currentCell = currentColumn[j]; // trenutno nod
                            Node rightCell = GetNode(nextColumn, j);
                            Node upperCell; // nod iznad ili po dijagonali na gore od trenutnog

                            bool canTakeDiagonal = nextColumn.Length > j + 1 && currentCell.Cell.Y == rightCell.Cell.Y;
                            if (canTakeDiagonal)
                                upperCell = RandomHelper.RandomBool ? currentColumn[j + 1] : GetNode(nextColumn, j + 1);
                            else
                                upperCell = currentColumn[j + 1];

                            if (rightCell != upperCell)
                                networkConnections.Add(new NetworkConnection(currentCell, upperCell));

                            networkConnections.Add(new NetworkConnection(currentCell, rightCell));
                        }
                        else
                            AddNodeAbove(networkConnections, currentColumn, j);  // moze samo gore
                    }
                    else
                        AddNodeAbove(networkConnections, currentColumn, j); // moze samo gore
                }
                else if (i < matrix.Length - 1) // moze samo desno
                {
                    Node[] nextColumn = matrix[i + 1]; // desna kolona

                    if (nextColumn == null) // ovo je skroz gore desno - i ako naleti na praznu kolonu ulevo, nema dalje svakako
                        break;

                    networkConnections.Add(new NetworkConnection(currentColumn[j], GetNode(nextColumn, j)));
                }
            }
        }

        return networkConnections;
    }

    void AddNodeAbove(HashSet<NetworkConnection> networkConnections, Node[] currentColumn, int j)
    {
        Node upperCell = currentColumn[j + 1];
        if (upperCell != null)
            networkConnections.Add(new NetworkConnection(currentColumn[j], upperCell)); // moze samo gore
    }

    Node GetNode(Node[] nextColumn, int j) // nod desno od trenutnog - ovde bi bilo valjano dodati da ako nod vec ima 2 veze, da ga preskoci, recimo...
    {
        Node rightCell = null;
        int rightCellIndex;

        if (j >= nextColumn.Length)
            rightCellIndex = nextColumn.Length - 1;
        else
            rightCellIndex = j;

        for (int x = rightCellIndex; x >= 0; x--)
        {
            rightCell = nextColumn[x];
            if (rightCell != null)
                break;
        }

        return rightCell;
    }
}
