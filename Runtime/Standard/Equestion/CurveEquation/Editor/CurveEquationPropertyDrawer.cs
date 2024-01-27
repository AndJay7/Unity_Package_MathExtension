using UnityEngine;
using UnityEditor;
using And.Editor;

namespace And.Math.Standard.Editor
{

    [CustomPropertyDrawer(typeof(CurveEquation))]
    public class CurveEquationPropertyDrawer : PropertyGridDrawer
    {
        protected override int GetRowCount(SerializedProperty property, GUIContent label)
        {
            return 2;
        }

        protected override int GetColumnCount(SerializedProperty property, GUIContent label)
        {
            return 2;
        }

        protected override void DrawGUI(Rect pos, SerializedProperty prop, GUIContent label, PropertyGrid grid)
        {
            SerializedGridProperty curve = new SerializedGridProperty(prop.FindPropertyRelative(nameof(CurveEquation.curve)), 0);
            SerializedGridProperty amplitude = new SerializedGridProperty(prop.FindPropertyRelative(nameof(CurveEquation.amplitude)), 65);
            SerializedGridProperty length = new SerializedGridProperty(prop.FindPropertyRelative(nameof(CurveEquation.length)), 55);

            GridPosition gridPos;

            gridPos = new GridPosition(new Vector2Int(0, 0), new Vector2Int(2, 1));
            curve.DrawProperty(grid, gridPos, showLabel: false);
            gridPos = new GridPosition(new Vector2Int(0, 1));
            amplitude.DrawProperty(grid, gridPos, showLabel: true);
            gridPos = new GridPosition(new Vector2Int(1, 1));
            length.DrawProperty(grid, gridPos, showLabel: true);
        }
    }

}