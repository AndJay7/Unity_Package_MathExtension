using UnityEditor;
using UnityEngine;

public abstract class PropertyGridDrawer : PropertyDrawer
{
    protected abstract int RowCountStandard { get; }
    protected abstract int ColumnCountStandard { get; }
    protected abstract bool IsWrappingSupported { get; }
    protected abstract bool ForceIndent { get; }
    protected virtual int RowCountWrap => RowCountStandard;
    protected virtual int ColumnCountWrap => ColumnCountStandard;
    private bool IsIndent => ForceIndent || IsWrapping;
    private bool IsWrapping => IsWrappingSupported && INDENT_WIDTH >= Screen.width;
    private int RowCount => IsWrapping ? RowCountWrap : RowCountStandard;
    private int ColumnCount => IsWrapping ? ColumnCountWrap : ColumnCountStandard;


    //passing height is necessary, because List Drawer is changing rect after GetPropertyHeight()
    private float _targetHeight;
    private const float COLUMN_MARGIN = 4f;
    private const float ROW_MARGIN = 2f;
    private const float MIN_NAME_WIDTH = 120f;
    private const float INDENT_WIDTH = 331f;
    private const float INDENT_OFFSET_X = 16f;
    private const float INDENT_OFFSET_Y = 18f;

    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
    {
        int indent = EditorGUI.indentLevel;
        label = EditorGUI.BeginProperty(pos, label, property);

        EditorGUI.PrefixLabel(pos, label);

        Grid grid;
        if (IsIndent)
        {
            grid = GetGrid(pos, INDENT_OFFSET_X, INDENT_OFFSET_Y, _targetHeight);
            DrawIndentProperties(pos, property, label, grid);
        }
        else
        {
            grid = GetGrid(pos, GetPrefixWidth(pos), 0, _targetHeight);
            DrawStandardProperties(pos, property, label, grid);
        }

        EditorGUI.EndProperty();
        EditorGUI.indentLevel = indent;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float rowSize = base.GetPropertyHeight(property, label);
        int rowCount = RowCount + (IsIndent ? 1 : 0);
        _targetHeight = Grid.GetSpanSize(rowSize, ROW_MARGIN, rowCount);
        return _targetHeight;
    }

    protected virtual void DrawStandardProperties(Rect pos, SerializedProperty property, GUIContent label, Grid grid) { }

    protected virtual void DrawIndentProperties(Rect pos, SerializedProperty property, GUIContent label, Grid grid) { }

    private Grid GetGrid(Rect pos, float offsetX, float offsetY, float height)
    {
        float spacingY = (pos.height - height) / 2;
        float pivotX = pos.x + offsetX;
        float pivotY = pos.y + offsetY;
        float gridWidth = pos.width - offsetX;
        float gridHeight = height - offsetY;

        Grid grid = new Grid()
        {
            spacingY = spacingY,
            pivotX = pivotX,
            pivotY = pivotY,
            columnMarginSize = COLUMN_MARGIN,
            rowMarginSize = ROW_MARGIN,
            columnSize = (gridWidth - COLUMN_MARGIN * (ColumnCount - 1)) / ColumnCount,
            rowSize = (gridHeight - ROW_MARGIN * (RowCount - 1)) / RowCount
        };

        return grid;
    }

    private float GetPrefixWidth(Rect pos)
    {
        return Mathf.Max(MIN_NAME_WIDTH, pos.width * 0.45f - 28);
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
    public float spacingY;
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
        float posY = pivotY + spacingY + GetSpanSize(rowSize, rowMarginSize, gridPos.rowIndex);
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
