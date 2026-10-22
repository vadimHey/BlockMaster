using UnityEngine;
using System.Collections.Generic;

public class ActionHistory : MonoBehaviour
{
    private readonly Stack<GridAction> _undoStack = new();
    private readonly Stack<GridAction> _redoStack = new();

    public bool CanUndo => _undoStack.Count > 0;
    public bool CanRedo => _redoStack.Count > 0;

    public void Push(GridAction action)
    {
        if (action == null || action.Changes.Count == 0) return;
        _undoStack.Push(action);
        _redoStack.Clear();
    }

    public GridAction Undo()
    {
        if (!CanUndo) return null;
        GridAction action = _undoStack.Pop();
        _redoStack.Push(action);
        return action;
    }

    public GridAction Redo()
    {
        if (!CanRedo) return null;
        GridAction action = _redoStack.Pop();
        _undoStack.Push(action);
        return action;
    }

    public void Clear()
    {
        _undoStack.Clear();
        _redoStack.Clear();
    }
}