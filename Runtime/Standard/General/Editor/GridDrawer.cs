using UnityEditor;
using UnityEngine;

public abstract class GridDrawer : PropertyDrawer
{
    protected abstract int RowCount { get; }
    protected abstract int ColumnCount { get; }

    protected Grid _grid;
    private const float COLUMN_MARGIN = 4f;
    private const float ROW_MARGIN = 2f;
    private const float MIN_NAME_WIDTH = 120f;
    private const float WRAP_WIDTH = 310f;


    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
    {
        float nameWidth = Mathf.Max(MIN_NAME_WIDTH,pos.width * 0.45f - 28);
        float gridHeight = pos.height;
        float gridWidth = pos.width - nameWidth;

        _grid = new Grid()
        {
            pivotX = pos.x + nameWidth,
            pivotY = pos.y,
            columnMarginSize = COLUMN_MARGIN,
            rowMarginSize = ROW_MARGIN,
            columnSize = (gridWidth - COLUMN_MARGIN * (ColumnCount - 1)) / ColumnCount,
            rowSize = (gridHeight - ROW_MARGIN * (RowCount - 1)) / RowCount
        };

        int indent = EditorGUI.indentLevel;

        EditorGUI.LabelField(new Rect(pos.x, pos.y, nameWidth, _grid.rowSize), property.displayName);

        EditorGUI.indentLevel = indent;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float rowSize = base.GetPropertyHeight(property, label);

        return Grid.GetSpanSize(rowSize,ROW_MARGIN,RowCount);
    }
}

public class SerializedGridProperty
{
    private SerializedProperty property;
    private float labelWidth;

    public SerializedGridProperty(SerializedProperty property, float labelWidth)
    {
        this.property = property;
        this.labelWidth = labelWidth;
    }

    public void DrawFloat(Grid grid, GridPosition gridPos, string label = null, bool showLabel = true)
    {
        Rect fieldRect = TryDrawLabel(grid,gridPos, label, showLabel);

        property.floatValue = EditorGUI.FloatField(fieldRect, property.floatValue);
    }

    public void DrawCurve(Grid grid, GridPosition gridPos, string label = null, bool showLabel = true)
    {
        Rect fieldRect = TryDrawLabel(grid, gridPos, label, showLabel);

        property.animationCurveValue = EditorGUI.CurveField(fieldRect, property.animationCurveValue);
    }

    private Rect TryDrawLabel(Grid grid, GridPosition gridPos, string label, bool showLabel)
    {
        Rect labelRect = new Rect()
        {
            x = grid.GetPosX(gridPos),
            y = grid.GetPosY(gridPos),
            width = labelWidth,
            height = grid.GetHeight(gridPos)
        };

        Rect fieldRect = new Rect()
        {
            x = grid.GetPosX(gridPos),
            y = grid.GetPosY(gridPos),
            width = grid.GetWidth(gridPos),
            height = grid.GetHeight(gridPos)
        };

        if (showLabel)
        {
            fieldRect.x += labelWidth;
            fieldRect.width -= labelWidth;
            DrawLabel(labelRect, label);
        }

        return fieldRect;
    }

    private void DrawLabel(Rect rect, string label)
    {
        if (label == null)
            label = property.displayName;

        EditorGUI.LabelField(rect, label);
    }
}

public struct GridPosition
{
    public int rowIndex;
    public int columnIndex;
    public int rowSpan;
    public int columnSpan;

    public GridPosition(int columnIndex = 0, int rowIndex = 0, int columnSpan = 1, int rowSpan = 1)
    {
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
        this.rowSpan = rowSpan;
        this.columnSpan = columnSpan;
    }
}

public struct Grid
{
    public float pivotX;
    public float pivotY;
    public float rowSize;
    public float columnSize;
    public float rowMarginSize;
    public float columnMarginSize;

    public float GetPosX(GridPosition gridPos)
    {
        float posX = pivotX + GetSpanSize(columnSize, columnMarginSize, gridPos.columnIndex);
        if (gridPos.columnIndex > 0)
            posX += columnMarginSize;
        return posX;
    }

    public float GetPosY(GridPosition gridPos)
    {
        float posY = pivotY + GetSpanSize(rowSize, rowMarginSize, gridPos.rowIndex);
        if (gridPos.rowIndex > 0)
            posY += rowMarginSize;
        return posY;
    }

    public float GetWidth(GridPosition gridPos)
    {
        return GetSpanSize(columnSize, columnMarginSize, gridPos.columnSpan);
    }

    public float GetHeight(GridPosition gridPos)
    {
        return GetSpanSize(rowSize, rowMarginSize, gridPos.rowSpan);
    }

    public static float GetSpanSize(float size, float marginSize, int count)
    {
        return size * count + marginSize * Mathf.Max(count - 1, 0);
    }
}
