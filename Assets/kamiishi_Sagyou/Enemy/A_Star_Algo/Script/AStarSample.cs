#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[ExecuteInEditMode]
public class AStarSample : MonoBehaviour
{

    private const int columnCount = 100;
    private const int rowCount = 100;
    private const float CELL_SIZE = 1.0f;

    [SerializeField]
    private AStarGrid _grid = null;

    private AStar _aStar;
    [SerializeField] private List<AStarGrid.Cell> _shortestWay = new List<AStarGrid.Cell>();

    public void OnEnable()
    {
        if (_grid == null)
        {
            _grid = new AStarGrid(columnCount, rowCount);
            _grid.StartCell = _grid.GetCell(0, 0);
            _grid.GoalCell = _grid.GetCell(columnCount - 1, rowCount - 1);
        }
        _aStar = new AStar(_grid);
    }


    private void OnDrawGizmos()
    {
        Tools.current = Tool.None;
        var preColor = Gizmos.color;
        var preMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * CELL_SIZE);

        // クリック判定
        if (Event.current != null && Event.current.type == EventType.MouseUp)
        {

            // レイを取得する
            var clickedPosition = Event.current.mousePosition;
            clickedPosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - clickedPosition.y;
            var clickRay = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(clickedPosition);

            // レイとy=0の平面との交点を求める
            var scale = Mathf.Abs(clickRay.origin.y / clickRay.direction.y);
            var intersection = clickRay.origin + clickRay.direction * scale;

            // 選択されたセルを編集するメニューを描画
            intersection /= CELL_SIZE;
            var selectedColumn = Mathf.FloorToInt(intersection.x);
            var selectedRow = Mathf.FloorToInt(intersection.z);
            if (selectedColumn >= 0 && selectedColumn < columnCount && selectedRow >= 0 && selectedRow < rowCount)
            {
                GenericMenu menu = new GenericMenu();

                // CellType変更メニュー
                var cell = _grid.GetCell(selectedColumn, selectedRow);
                menu.AddItem(new GUIContent("Set Type > Not Road"), cell.groundType == AStarGrid.GroundType.NotRoad, () => {
                    Undo.RecordObject(this, "Set Type > Not Road");
                    cell.groundType = AStarGrid.GroundType.NotRoad;
                    EditorUtility.SetDirty(this);
                });
                menu.AddItem(new GUIContent("Set Type > Road"), cell.groundType == AStarGrid.GroundType.Road, () => {
                    Undo.RecordObject(this, "Set Type > Road");
                    cell.groundType = AStarGrid.GroundType.Road;
                    EditorUtility.SetDirty(this);
                });
                menu.AddItem(new GUIContent("Set Type > Start"), _grid.StartCell == cell, () => {
                    Undo.RecordObject(this, "Set Type > Start");
                    _grid.StartCell = cell;
                    EditorUtility.SetDirty(this);
                });
                menu.AddItem(new GUIContent("Set Type > Goal"), _grid.GoalCell == cell, () => {
                    Undo.RecordObject(this, "Set Type > Goal");
                    _grid.GoalCell = cell;
                    EditorUtility.SetDirty(this);
                });
                menu.AddItem(new GUIContent("Show Shortest Way"), false, () => {
                    _shortestWay = _aStar.GetShortestWay(_grid.StartCell, _grid.GoalCell);
                });

                menu.ShowAsContext();
            }
        }

        // セルを描画
        System.Action<AStarGrid.Cell> drawCell = cell => {
            Gizmos.DrawWireCube(new Vector3(cell.coord.x + 0.5f, 0.0f, cell.coord.y + 0.5f), new Vector3(1.0f, 0.0f, 1.0f));
        };
        Gizmos.color = Color.green;
        foreach (var item in _grid.Cells.Where(cell => cell.groundType == AStarGrid.GroundType.NotRoad))
        {
            drawCell(item);
        }
        Gizmos.color = Color.yellow;
        foreach (var item in _grid.Cells.Where(cell => cell.groundType == AStarGrid.GroundType.Road))
        {
            drawCell(item);
        }
        Gizmos.color = Color.blue;
        if (_grid.StartCell != null)
        {
            drawCell(_grid.StartCell);
        }
        Gizmos.color = Color.red;
        if (_grid.GoalCell != null)
        {
            drawCell(_grid.GoalCell);
        }
        Gizmos.color = Color.red;
        foreach (var item in _shortestWay)
        {
            Gizmos.DrawSphere(new Vector3(item.coord.x + 0.5f, 0.0f, item.coord.y + 0.5f), 0.2f);
        }

        Gizmos.color = preColor;
        Gizmos.matrix = preMatrix;
    }

    void Update()
    {

        // ここで最短距離を計算する
        _shortestWay = _aStar.GetShortestWay(_grid.StartCell, _grid.GoalCell);

    }

    public AStar GetAstarGrid()
    {
        return _aStar;
    }

    // 最短経路取得
    public List<AStarGrid.Cell> GetShortestWay()
    {
        return _shortestWay;
    }

}
#endif