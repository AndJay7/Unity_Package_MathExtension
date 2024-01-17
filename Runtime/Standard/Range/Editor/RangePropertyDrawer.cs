using UnityEngine;
using UnityEditor;

namespace And.Math.Standard.Editor
{

    [CustomPropertyDrawer(typeof(Range))]
    public class RangePropertyDrawer : PropertyGridDrawer
    {
        protected override int ColumnCountWrap => ColumnCountStandard - 1;
        protected override int RowCountStandard => 1;
        protected override int ColumnCountStandard => 3;
        protected override bool IsWrappingSupported => true;
        protected override bool ForceIndent => false;

        protected override void DrawStandardProperties(Rect pos, SerializedProperty prop, GUIContent label, Grid grid)
        {
            int labelWidth = 28;
            SerializedGridProperty min = new SerializedGridProperty(prop.FindPropertyRelative(nameof(Range.min)), labelWidth);
            SerializedGridProperty max = new SerializedGridProperty(prop.FindPropertyRelative(nameof(Range.max)), labelWidth);
            GridPosition gridPos;

            gridPos = new GridPosition(0, 0);
            min.DrawFloat(grid, gridPos);
            gridPos = new GridPosition(1, 0);
            max.DrawFloat(grid, gridPos);
        }

        protected override void DrawIndentProperties(Rect pos, SerializedProperty property, GUIContent label, Grid grid)
        {
            DrawStandardProperties(pos, property, label, grid);
        }
    }

}