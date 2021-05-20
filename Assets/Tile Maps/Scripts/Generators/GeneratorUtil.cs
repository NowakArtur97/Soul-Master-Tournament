using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GeneratorUtil
{
    public static bool IsBetweenCorners(int column, int maxColumns) => column > 0 && column < maxColumns - 1;

    public static bool IsFirstColumn(int column) => column == 0;

    public static bool IsLastColumn(int column, int maxColumns) => column == maxColumns - 1;

    public static bool IsFirstRow(int row) => row == 0;

    public static bool IsLastRow(int row, int maxRows) => row == maxRows - 1;

    public static bool IsPositionFree(Vector3Int position, List<Vector2> reservedPositions, float reservedPositionOffset) =>
        !reservedPositions.Any(reservedPosition =>
             IsCloseHorizontally(position, reservedPosition, reservedPositionOffset)
             || IsCloseVertically(position, reservedPositionOffset, reservedPosition));

    private static bool IsCloseVertically(Vector3Int position, float reservedPositionOffset, Vector2 reservedPosition) =>
        (reservedPosition.y + reservedPositionOffset >= position.y && reservedPosition.y - reservedPositionOffset <= position.y)
                     && reservedPosition.x == position.x;

    private static bool IsCloseHorizontally(Vector3Int position, Vector2 reservedPosition, float reservedPositionOffset) =>
        (reservedPosition.x + reservedPositionOffset >= position.x && reservedPosition.x - reservedPositionOffset <= position.x)
                     && reservedPosition.y == position.y;

    public static bool IsOnWall(int column, int maxColumns, int row, int maxRows) =>
        IsFirstColumn(column) || IsLastColumn(column, maxColumns)
        || IsFirstRow(row) || IsLastRow(row, maxRows);
}
