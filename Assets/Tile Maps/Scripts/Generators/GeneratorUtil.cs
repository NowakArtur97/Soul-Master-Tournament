using System.Linq;
using UnityEngine;

public static class GeneratorUtil
{
    public static bool IsBetweenCorners(int column, int maxColumns) => column > 0 && column < maxColumns - 1;

    public static bool IsFirstColumn(int column) => column == 0;

    public static bool IsLastColumn(int column, int maxColumns) => column == maxColumns - 1;

    public static bool IsFirstRow(int row) => row == 0;

    public static bool IsLastRow(int row, int maxRows) => row == maxRows - 1;

    public static bool IsFreePosition(Vector3Int reservedPosition, Vector2[] reservedPositions, Vector2 reservedPositionOffset)
        => !reservedPositions.Any(position =>
            position.x + reservedPositionOffset.x > reservedPosition.x && position.x - reservedPositionOffset.x < reservedPosition.x
               && position.y + reservedPositionOffset.y > reservedPosition.y && position.y - reservedPositionOffset.y < reservedPosition.y
    );

    public static bool IsOnWall(int column, int maxColumns, int row, int maxRows) =>
        IsFirstColumn(column) || IsLastColumn(column, maxColumns)
        || IsFirstRow(row) || IsLastRow(row, maxRows);
}
