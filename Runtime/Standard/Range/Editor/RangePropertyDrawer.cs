using UnityEngine;
using UnityEditor;
using And.Editor;

namespace And.Math.Standard.Editor
{

    [CustomPropertyDrawer(typeof(Range))]
    public class RangePropertyDrawer : PropertyGridDrawer
    {
        protected override int GetRowCount(SerializedProperty property, GUIContent label)
        {
            int rowCount = 1;

            if(!IsFullyNested(property, label) && IsWrapping)
                rowCount++;            

            return rowCount;
        }

        protected override int GetColumnCount(SerializedProperty property, GUIContent label)
        {
            int columnCount = 3;

            if (IsFullyNested(property, label) || IsWrapping)
                columnCount--;

            return columnCount;
        }

        protected override void DrawGUI(Rect pos, SerializedProperty prop, GUIContent label, PropertyGrid grid)
        {
            int labelWidth = 38;
            int startRow = 0;
            if(!IsFullyNested(prop,label) && IsWrapping)
                startRow++;
            SerializedGridProperty min = new SerializedGridProperty(prop.FindPropertyRelative(nameof(Range.min)), labelWidth);
            SerializedGridProperty max = new SerializedGridProperty(prop.FindPropertyRelative(nameof(Range.max)), labelWidth);
            
            GridPosition gridPos;

            gridPos = new GridPosition(new Vector2Int(0, startRow), new Vector2Int(1,1));
            min.DrawFloat(grid, gridPos,showLabel: true);
            gridPos = new GridPosition(new Vector2Int(1, startRow));
            max.DrawFloat(grid, gridPos, showLabel: true);
        }
    }

}