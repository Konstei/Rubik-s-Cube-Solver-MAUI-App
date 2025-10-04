using System.Diagnostics;

namespace Rubik_s_Cube_Solver_MAUI.Core.Solver;

internal static class SecondLayer
{
    public static void Solve(Cube cube)
    {
        while (!(
            cube.Edges[1][2] == Cube.Color.ORANGE && cube.Edges[2][1] == Cube.Color.GREEN &&
            cube.Edges[2][3] == Cube.Color.GREEN && cube.Edges[3][2] == Cube.Color.RED &&
            cube.Edges[3][4] == Cube.Color.RED && cube.Edges[4][3] == Cube.Color.BLUE &&
            cube.Edges[4][1] == Cube.Color.BLUE && cube.Edges[1][4] == Cube.Color.ORANGE
        ))
        {
            int edge = FindMatching(cube);
            int sign = edge < 0 ? -1 : edge > 0 ? 1 : 0;
            edge *= sign;

            switch (edge)
            {
                case 12:
                    FourMovesRight(cube, Cube.Color.ORANGE);
                    FourMovesLeft(cube, Cube.Color.GREEN);
                    break;
                case 23:
                    FourMovesRight(cube, Cube.Color.GREEN);
                    FourMovesLeft(cube, Cube.Color.RED);
                    break;
                case 34:
                    FourMovesRight(cube, Cube.Color.RED);
                    FourMovesLeft(cube, Cube.Color.BLUE);
                    break;
                case 41:
                    FourMovesRight(cube, Cube.Color.BLUE);
                    FourMovesLeft(cube, Cube.Color.ORANGE);
                    break;
            }
            if (edge > 10)
            {
                cube.Configure();
                continue;
            }

            switch (sign)
            {
                case 0:
                    cube.RotateFace(Cube.Color.YELLOW);
                    break;
                case -1:
                    FourMovesLeft(cube, cube.Edges[5][edge]);
                    FourMovesRight(cube, cube.Edges[edge][5]);
                    break;

                case 1:
                    FourMovesRight(cube, cube.Edges[5][edge]);
                    FourMovesLeft(cube, cube.Edges[edge][5]);
                    break;
            }
            cube.Configure();
        }
    }

    private static int FindMatching(Cube cube)
    {
        if (cube.Edges[4][5] == Cube.Color.ORANGE && cube.Edges[5][4] == Cube.Color.GREEN) return -4;
        if (cube.Edges[3][5] == Cube.Color.GREEN && cube.Edges[5][3] == Cube.Color.ORANGE) return 3;
        if (cube.Edges[1][5] == Cube.Color.GREEN && cube.Edges[5][1] == Cube.Color.RED) return -1;
        if (cube.Edges[4][5] == Cube.Color.RED && cube.Edges[5][4] == Cube.Color.GREEN) return 4;
        if (cube.Edges[2][5] == Cube.Color.RED && cube.Edges[5][2] == Cube.Color.BLUE) return -2;
        if (cube.Edges[1][5] == Cube.Color.BLUE && cube.Edges[5][1] == Cube.Color.RED) return 1;
        if (cube.Edges[3][5] == Cube.Color.BLUE && cube.Edges[5][3] == Cube.Color.ORANGE) return -3;
        if (cube.Edges[2][5] == Cube.Color.ORANGE && cube.Edges[5][2] == Cube.Color.BLUE) return 2;
        if (
            (cube.Edges[1][5] == Cube.Color.YELLOW || cube.Edges[5][1] == Cube.Color.YELLOW) &&
            (cube.Edges[2][5] == Cube.Color.YELLOW || cube.Edges[5][2] == Cube.Color.YELLOW) &&
            (cube.Edges[3][5] == Cube.Color.YELLOW || cube.Edges[5][3] == Cube.Color.YELLOW) &&
            (cube.Edges[4][5] == Cube.Color.YELLOW || cube.Edges[5][4] == Cube.Color.YELLOW)
        )
        {
            if (!(cube.Edges[1][2] == Cube.Color.ORANGE && cube.Edges[2][1] == Cube.Color.GREEN)) return 12;
            if (!(cube.Edges[2][3] == Cube.Color.GREEN && cube.Edges[3][2] == Cube.Color.RED)) return 23;
            if (!(cube.Edges[3][4] == Cube.Color.RED && cube.Edges[4][3] == Cube.Color.BLUE)) return 34;
            if (!(cube.Edges[4][1] == Cube.Color.BLUE && cube.Edges[1][4] == Cube.Color.ORANGE)) return 41;
        }
        return 0;
    }

    private static void FourMovesLeft(Cube cube, Cube.Color face)
    {
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW);
    }

    private static void FourMovesRight(Cube cube, Cube.Color face)
    {
        cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW);
    }
}
