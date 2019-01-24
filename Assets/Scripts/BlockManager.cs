using UnityEngine;

public class BlockManager : MonoBehaviour {
    public Block[,] Blocks;
    [SerializeField] Block blockPrefab = null;
    [SerializeField] Transform blockParent = null;
    [SerializeField] IntReference columns = null;
    [SerializeField] IntReference rows = null;
    [SerializeField] IntReference typeCount = null;
    
    void Awake() {
        // Create an extra row for new blocks falling in from the top
        Blocks = new Block[columns.Value, rows.Value + 1];

        for(int row = 0; row < rows.Value + 1; row++) {
            for(int column = 0; column < columns.Value; column++) {
                Blocks[column, row] = Instantiate(blockPrefab, blockParent);
                Blocks[column, row].name = string.Format("Block [{0}, {1}]", column, row);
                Blocks[column, row].Column = column;
                Blocks[column, row].Row = row;
                Blocks[column, row].State = BlockState.Empty;
                Blocks[column, row].Type = -1;
            }
        }
    }

    public int GetRandomBlockType(int column, int row) {
		int type;
		do {
			type = Random.Range(0, typeCount.Value);
		} while((column != 0 && Blocks[column - 1, row].Type == type) || (row != 0 && Blocks[column, row - 1].Type == type));
		return type;
	}
}