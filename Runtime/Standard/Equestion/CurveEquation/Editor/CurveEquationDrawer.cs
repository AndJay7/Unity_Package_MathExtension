using UnityEngine;
using UnityEditor;
using Codice.Client.BaseCommands;

namespace And.Math.Standard.Editor
{

    [CustomPropertyDrawer(typeof(CurveEquation))]
    public class CurveEquationDrawer : GridDrawer
    {
        protected override int RowCount => 2;
        protected override int ColumnCount => 2;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            base.OnGUI(pos, prop, label);

            int labelWidth = 60;
            int indent = EditorGUI.indentLevel;
            SerializedGridProperty curve = new (prop.FindPropertyRelative(nameof(CurveEquation.curve)),labelWidth);
            SerializedGridProperty amplitude = new(prop.FindPropertyRelative(nameof(CurveEquation.amplitude)),labelWidth);
            SerializedGridProperty length = new(prop.FindPropertyRelative(nameof(CurveEquation.length)),labelWidth);


            GridPosition gridPos;

            gridPos = new GridPosition(0, 0, 2);
            curve.DrawCurve(_grid, gridPos,showLabel: false);
            gridPos = new GridPosition(0, 1);
            amplitude.DrawFloat(_grid, gridPos);
            gridPos = new GridPosition(1, 1);
            length.DrawFloat(_grid, gridPos);

            EditorGUI.indentLevel = indent;
        }
    }

}