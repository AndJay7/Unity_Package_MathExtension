using UnityEngine;
using UnityEditor;

namespace And.Math.Standard.Editor
{

    [CustomPropertyDrawer(typeof(Range))]
    public class RangePropertyDrawer : GridDrawer
    {
        protected override int RowCount => 1;
        protected override int ColumnCount => 3; 

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            base.OnGUI(pos, prop, label);

            int labelWidth = 28;
            int indent = EditorGUI.indentLevel;
            SerializedGridProperty min = new SerializedGridProperty(prop.FindPropertyRelative(nameof(Range.min)), labelWidth);
            SerializedGridProperty max = new SerializedGridProperty(prop.FindPropertyRelative(nameof(Range.max)), labelWidth);
            GridPosition gridPos;

            gridPos = new GridPosition(0, 0);
            min.DrawFloat(_grid, gridPos);
            gridPos = new GridPosition(1, 0);
            max.DrawFloat(_grid, gridPos);

            EditorGUI.indentLevel = indent;
        }
    }

}