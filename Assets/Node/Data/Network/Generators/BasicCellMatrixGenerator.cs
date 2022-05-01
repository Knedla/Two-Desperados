using UnityEngine;

public class BasicCellMatrixGenerator : IGenerator<Cell[][]>
{
    int minSpawnPerColumn;
    int maxSpawnPerColumn;
    int maxWidth;

    public BasicCellMatrixGenerator(int minSpawnPerColumn, int maxSpawnPerColumn, int maxWidth)
    {
        this.minSpawnPerColumn = minSpawnPerColumn;
        this.maxSpawnPerColumn = maxSpawnPerColumn;
        this.maxWidth = maxWidth;
    }

    public Cell[][] Generate()
    {
        // na kraju moze da se desi da ne budu instancirane sve kolone
        // ako ima 16 nodova ukupno, to je prirodno 4 * 4 matrica
        // zarad raznovrsnosti, uzme se da matrica moze da se srecuje da bude 8 * 2 - horizontal oriented
        // zbog raznovrsnosti, broj nodova po koloni moze biti od 2 (strecovana matrica) do 4 (prirodna matrica)
        // ako svaka od prve 4 kolone instancira po 4 noda, potrosice se svih 16 nodova i zadnje 4 kolone ce ostati prazne - mozda nesto izmanipulisati, da centar bude 0 ppa da se ide od -x do +x zbog iscrtavanja
        Cell[][] matrix = new Cell[maxWidth][];

        int itemCount = Framework.PlayerPreferenceData.NodeCountIncludeStartNode;

        for (int i = 0; i < maxWidth; i++)
        {
            Cell[] column = GetMatrixColumn(i, minSpawnPerColumn, maxSpawnPerColumn);
            matrix[i] = column;
            itemCount -= column.Length;

            if (itemCount <= 0)
                break;
        }

        return matrix;
    }

    Cell[] GetMatrixColumn(int columnIndex, int minSpawnPerColumn, int maxSpawnPerColumn) // parent id da budem siguran da je povezan sa rootom?
    {
        int spawnCount = Random.Range(minSpawnPerColumn, maxSpawnPerColumn + 1);
        Cell[] column = new Cell[spawnCount];

        int removeCount = maxSpawnPerColumn - spawnCount;

        for (int i = 0; i < maxSpawnPerColumn; i++)
        {
            if (removeCount > 0 && RandomHelper.RandomBool)
            {
                removeCount--;
                continue;
            }

            column[i] = new Cell(columnIndex, i);
            spawnCount--;

            if (spawnCount == 0)
                break;
        }

        return column;
    }
}
