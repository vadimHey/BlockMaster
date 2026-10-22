using UnityEngine;

public class BuilderController : MonoBehaviour
{
    [SerializeField] private GridModel gridModel;
    [SerializeField] private Grid2DView grid2DView;
    [SerializeField] private Scene3DView scene3DView;
    [SerializeField] private ActionHistory actionHistory;

    private const string DefaultSaveName = "construction";

    public void Save()
    {
        GridSaveData data = gridModel.ToSaveData();
        SaveSystem.Save(data, DefaultSaveName);
    }

    public void Load()
    {
        GridSaveData data = SaveSystem.Load(DefaultSaveName);
        if (data == null) return;

        gridModel.LoadFromSaveData(data);
        RefreshAll();
        actionHistory.Clear();
    }

    private void RefreshAll()
    {
        for (int x = 0; x < gridModel.Width; x++)
        {
            for (int z = 0; z < gridModel.Height; z++)
            {
                Color color = gridModel.GetTopColor(x, z);
                grid2DView.SetCellColor(x, z, color);
                scene3DView.RebuildColumn(x, z);
            }
        }
    }

    public void Undo()
    {
        GridAction action = actionHistory.Undo();
        if (action == null) return;

        for (int i = action.Changes.Count - 1; i >= 0; i--)
        {
            CellChange change = action.Changes[i];
            gridModel.RemoveTopBlock(change.X, change.Z);
            Color color = gridModel.GetTopColor(change.X, change.Z);
            grid2DView.SetCellColor(change.X, change.Z, change.OldColor);
            scene3DView.RebuildColumn(change.X, change.Z);
        }
    }

    public void Redo()
    {
        GridAction action = actionHistory.Redo();
        if (action == null) return;

        foreach (CellChange change in action.Changes)
        {
            BlockData b = change.AddedBlock;
            gridModel.AddBlock(change.X, change.Z, b.Bottom, b.Top, b.Color);
            grid2DView.SetCellColor(change.X, change.Z, b.Color);
            scene3DView.RebuildColumn(change.X, change.Z);
        }
    }

    public void ClearAll()
    {
        gridModel.Clear();
        grid2DView.ClearAll();
        scene3DView.ClearAll();
        actionHistory.Clear();
    }
}