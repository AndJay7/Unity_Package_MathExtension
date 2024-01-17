using UnityEngine;
using UnityEditor;

namespace And.Math.Standard.Editor
{

    [CustomPropertyDrawer(typeof(CurveEquation))]
    public class CurveEquationDrawer : PropertyGridDrawer
    {
        protected override int RowCountStandard => 2;
        protected override int ColumnCountStandard => 2;
        protected override bool IsWrappingSupported => true;
        protected override bool ForceIndent => false;

        protected override void DrawStandardProperties(Rect pos, SerializedProperty prop, GUIContent label, Grid grid)
        {
            int labelWidth = 60;
            SerializedGridProperty curve = new(prop.FindPropertyRelative(nameof(CurveEquation.curve)), labelWidth);
            SerializedGridProperty amplitude = new(prop.FindPropertyRelative(nameof(CurveEquation.amplitude)), labelWidth);
            SerializedGridProperty length = new(prop.FindPropertyRelative(nameof(CurveEquation.length)), labelWidth);

            GridPosition gridPos;

            gridPos = new GridPosition(0, 0, 2);
            curve.DrawCurve(grid, gridPos, showLabel: false);
            gridPos = new GridPosition(0, 1);
            amplitude.DrawFloat(grid, gridPos);
            gridPos = new GridPosition(1, 1);
            length.DrawFloat(grid, gridPos);
        }

        protected override void DrawIndentProperties(Rect pos, SerializedProperty property, GUIContent label, Grid grid)
        {
            DrawStandardProperties(pos, property, label, grid);
        }
    }

}