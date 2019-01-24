using UnityEngine;

public class BlockManager_NewRow : MonoBehaviour {
    [SerializeField] BlockManager blockManager = null;
    public Block[] Blocks;
    [SerializeField] Block blockPrefab = null;
    [SerializeField] Transform blockParent = null;
    [SerializeField] IntReference boardColumns = null;

    void Awake() {
        Blocks = new Block[boardColumns.Value];

        for (int column = 0; column < boardColumns.Value; column++) {
            Blocks[column] = Instantiate(blockPrefab, blockParent);
            Blocks[column].name = string.Format("New Row Block [{0}]", column);
            Blocks[column].Column = column;
            Blocks[column].Row = -1;
        }
    }

    void Start() {
        CreateNewRowBlocks();
    }

    public void CreateNewRowBlocks() {
        for (int column = 0; column < boardColumns.Value; column++) {
            Blocks[column].State = BlockState.Idle;
            Blocks[column].Type = blockManager.GetRandomBlockType(column, 0);
        }
    }
}
