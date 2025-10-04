using Rubik_s_Cube_Solver_MAUI.Core;

namespace Rubik_s_Cube_Solver_MAUI.Core.Solver;

internal static class LastLayer
{
    public static bool Solve(Cube cube)
    {
        if (!YellowCross(cube)) return false;
        MatchEdges(cube);
        MatchCorners(cube);
        OrientCorners(cube);
        return true;
    }

    private static bool YellowCross(Cube cube)
    {
        while (!(
            cube.Edges[5][1] == Cube.Color.YELLOW &&
            cube.Edges[5][2] == Cube.Color.YELLOW &&
            cube.Edges[5][3] == Cube.Color.YELLOW &&
            cube.Edges[5][4] == Cube.Color.YELLOW
        ))
        {
            if (cube.Edges[5][1] == Cube.Color.YELLOW && cube.Edges[5][3] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.GREEN);
                FourMovesBottom(cube, Cube.Color.ORANGE);
                cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN, conf: true);
                if (cube.Edges[5][1] != Cube.Color.YELLOW && cube.Edges[5][2] != Cube.Color.YELLOW && cube.Edges[5][3] != Cube.Color.YELLOW && cube.Edges[5][4] != Cube.Color.YELLOW)
                {
                    return false;
                }
                continue;
            }
            if (cube.Edges[5][2] == Cube.Color.YELLOW && cube.Edges[5][4] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.ORANGE);
                FourMovesBottom(cube, Cube.Color.BLUE);
                cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE, conf: true);
                if (cube.Edges[5][1] != Cube.Color.YELLOW && cube.Edges[5][2] != Cube.Color.YELLOW && cube.Edges[5][3] != Cube.Color.YELLOW && cube.Edges[5][4] != Cube.Color.YELLOW)
                {
                    return false;
                }
                continue;
            }

            if (cube.Edges[5][1] == Cube.Color.YELLOW && cube.Edges[5][2] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.BLUE);
                FourMovesBottom(cube, Cube.Color.RED);
                cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE, conf: true);
                if ((cube.Edges[5][1] != Cube.Color.YELLOW || cube.Edges[5][3] != Cube.Color.YELLOW) && (cube.Edges[5][2] != Cube.Color.YELLOW || cube.Edges[5][4] != Cube.Color.YELLOW))
                {
                    return false;
                }
                continue;
            }
            if (cube.Edges[5][2] == Cube.Color.YELLOW && cube.Edges[5][3] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.ORANGE);
                FourMovesBottom(cube, Cube.Color.BLUE);
                cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE, conf: true);
                if ((cube.Edges[5][1] != Cube.Color.YELLOW || cube.Edges[5][3] != Cube.Color.YELLOW) && (cube.Edges[5][2] != Cube.Color.YELLOW || cube.Edges[5][4] != Cube.Color.YELLOW))
                {
                    return false;
                }
                continue;
            }
            if (cube.Edges[5][3] == Cube.Color.YELLOW && cube.Edges[5][4] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.GREEN);
                FourMovesBottom(cube, Cube.Color.ORANGE);
                cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN, conf: true);
                if ((cube.Edges[5][1] != Cube.Color.YELLOW || cube.Edges[5][3] != Cube.Color.YELLOW) && (cube.Edges[5][2] != Cube.Color.YELLOW || cube.Edges[5][4] != Cube.Color.YELLOW))
                {
                    return false;
                }
                continue;
            }
            if (cube.Edges[5][4] == Cube.Color.YELLOW && cube.Edges[5][1] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.RED);
                FourMovesBottom(cube, Cube.Color.GREEN);
                cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED, conf: true);
                if ((cube.Edges[5][1] != Cube.Color.YELLOW || cube.Edges[5][3] != Cube.Color.YELLOW) && (cube.Edges[5][2] != Cube.Color.YELLOW || cube.Edges[5][4] != Cube.Color.YELLOW))
                {
                    return false;
                }
                continue;
            }

            if (cube.Edges[1][5] == Cube.Color.YELLOW && cube.Edges[2][5] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.GREEN);
                FourMovesBottom(cube, Cube.Color.ORANGE);
                cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN, conf: true);
                if (
                    (cube.Edges[5][1] != Cube.Color.YELLOW || cube.Edges[5][2] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][2] != Cube.Color.YELLOW || cube.Edges[5][3] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][3] != Cube.Color.YELLOW || cube.Edges[5][4] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][4] != Cube.Color.YELLOW || cube.Edges[5][1] != Cube.Color.YELLOW)
                )
                {
                    return false;
                }
                continue;
            }
            if (cube.Edges[2][5] == Cube.Color.YELLOW && cube.Edges[3][5] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.RED);
                FourMovesBottom(cube, Cube.Color.GREEN);
                cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED, conf: true);
                if (
                    (cube.Edges[5][1] != Cube.Color.YELLOW || cube.Edges[5][2] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][2] != Cube.Color.YELLOW || cube.Edges[5][3] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][3] != Cube.Color.YELLOW || cube.Edges[5][4] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][4] != Cube.Color.YELLOW || cube.Edges[5][1] != Cube.Color.YELLOW)
                )
                {
                    return false;
                }
                continue;
            }
            if (cube.Edges[3][5] == Cube.Color.YELLOW && cube.Edges[4][5] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.BLUE);
                FourMovesBottom(cube, Cube.Color.RED);
                cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE, conf: true);
                if (
                    (cube.Edges[5][1] != Cube.Color.YELLOW || cube.Edges[5][2] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][2] != Cube.Color.YELLOW || cube.Edges[5][3] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][3] != Cube.Color.YELLOW || cube.Edges[5][4] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][4] != Cube.Color.YELLOW || cube.Edges[5][1] != Cube.Color.YELLOW)
                )
                {
                    return false;
                }
                continue;
            }
            if (cube.Edges[4][5] == Cube.Color.YELLOW && cube.Edges[1][5] == Cube.Color.YELLOW)
            {
                cube.RotateFace(Cube.Color.ORANGE);
                FourMovesBottom(cube, Cube.Color.BLUE);
                cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE, conf: true);
                if (
                    (cube.Edges[5][1] != Cube.Color.YELLOW || cube.Edges[5][2] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][2] != Cube.Color.YELLOW || cube.Edges[5][3] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][3] != Cube.Color.YELLOW || cube.Edges[5][4] != Cube.Color.YELLOW) &&
                    (cube.Edges[5][4] != Cube.Color.YELLOW || cube.Edges[5][1] != Cube.Color.YELLOW)
                )
                {
                    return false;
                }
                continue;
            }
        }
        return true;
    }

    private static void MatchEdges(Cube cube)
    {
        while (
            Convert.ToInt32(cube.Edges[1][5] == Cube.Color.ORANGE) +
            Convert.ToInt32(cube.Edges[2][5] == Cube.Color.GREEN) +
            Convert.ToInt32(cube.Edges[3][5] == Cube.Color.RED) +
            Convert.ToInt32(cube.Edges[4][5] == Cube.Color.BLUE) != 2 &&
            Convert.ToInt32(cube.Edges[1][5] == Cube.Color.ORANGE) +
            Convert.ToInt32(cube.Edges[2][5] == Cube.Color.GREEN) +
            Convert.ToInt32(cube.Edges[3][5] == Cube.Color.RED) +
            Convert.ToInt32(cube.Edges[4][5] == Cube.Color.BLUE) != 4
        ) cube.RotateFace(Cube.Color.YELLOW, true);

        if (
            Convert.ToInt32(cube.Edges[1][5] == Cube.Color.ORANGE) +
            Convert.ToInt32(cube.Edges[2][5] == Cube.Color.GREEN) +
            Convert.ToInt32(cube.Edges[3][5] == Cube.Color.RED) +
            Convert.ToInt32(cube.Edges[4][5] == Cube.Color.BLUE) == 4
        ) return;

        if (cube.Edges[1][5] == Cube.Color.ORANGE && cube.Edges[3][5] == Cube.Color.RED)
        {
            cube.RotateFace(Cube.Color.YELLOW);
            FourMovesVariation(cube, Cube.Color.BLUE);
            FourMovesVariation(cube, Cube.Color.GREEN);
        }
        else if (cube.Edges[2][5] == Cube.Color.GREEN && cube.Edges[4][5] == Cube.Color.BLUE)
        {
            cube.RotateFace(Cube.Color.YELLOW);
            FourMovesVariation(cube, Cube.Color.ORANGE);
            FourMovesVariation(cube, Cube.Color.RED);
        }

        else if (cube.Edges[1][5] == Cube.Color.ORANGE && cube.Edges[2][5] == Cube.Color.GREEN) FourMovesVariation(cube, Cube.Color.GREEN);
        else if (cube.Edges[2][5] == Cube.Color.GREEN && cube.Edges[3][5] == Cube.Color.RED) FourMovesVariation(cube, Cube.Color.RED);
        else if (cube.Edges[3][5] == Cube.Color.RED && cube.Edges[4][5] == Cube.Color.BLUE) FourMovesVariation(cube, Cube.Color.BLUE);
        else if (cube.Edges[4][5] == Cube.Color.BLUE && cube.Edges[1][5] == Cube.Color.ORANGE) FourMovesVariation(cube, Cube.Color.ORANGE);
    }

    private static void MatchCorners(Cube cube)
    {
        while (!(
            cube.Corners[4].Contains(Cube.Color.YELLOW) && cube.Corners[4].Contains(Cube.Color.ORANGE) && cube.Corners[4].Contains(Cube.Color.GREEN) &&
            cube.Corners[5].Contains(Cube.Color.YELLOW) && cube.Corners[5].Contains(Cube.Color.GREEN) && cube.Corners[5].Contains(Cube.Color.RED) &&
            cube.Corners[6].Contains(Cube.Color.YELLOW) && cube.Corners[6].Contains(Cube.Color.RED) && cube.Corners[6].Contains(Cube.Color.BLUE) &&
            cube.Corners[7].Contains(Cube.Color.YELLOW) && cube.Corners[7].Contains(Cube.Color.BLUE) && cube.Corners[7].Contains(Cube.Color.ORANGE)
        ))
        {
            int corner = FindMatching(cube);
            switch (corner)
            {
                case -1:
                case 1: PermutationMoves(cube, Cube.Color.RED, Cube.Color.ORANGE); break;
                case 2: PermutationMoves(cube, Cube.Color.BLUE, Cube.Color.GREEN); break;
                case 3: PermutationMoves(cube, Cube.Color.ORANGE, Cube.Color.RED); break;
                case 4: PermutationMoves(cube, Cube.Color.GREEN, Cube.Color.BLUE); break;
            }
            cube.Configure();
        }
    }

    private static void OrientCorners(Cube cube)
    {
        for (int f = 0; f < 4; f++)
        {
            while (cube.Corners[7][0] != Cube.Color.YELLOW)
            {
                FourMovesTop(cube, Cube.Color.ORANGE);
                FourMovesTop(cube, Cube.Color.ORANGE);
                cube.Configure();
            }
            cube.RotateFace(Cube.Color.YELLOW, conf: true);
        }
    }

    private static int FindMatching(Cube cube)
    {
        if (cube.Corners[4].Contains(Cube.Color.YELLOW) && cube.Corners[4].Contains(Cube.Color.ORANGE) && cube.Corners[4].Contains(Cube.Color.GREEN)) return 1;
        if (cube.Corners[5].Contains(Cube.Color.YELLOW) && cube.Corners[5].Contains(Cube.Color.GREEN) && cube.Corners[5].Contains(Cube.Color.RED)) return 2;
        if (cube.Corners[6].Contains(Cube.Color.YELLOW) && cube.Corners[6].Contains(Cube.Color.RED) && cube.Corners[6].Contains(Cube.Color.BLUE)) return 3;
        if (cube.Corners[7].Contains(Cube.Color.YELLOW) && cube.Corners[7].Contains(Cube.Color.BLUE) && cube.Corners[7].Contains(Cube.Color.ORANGE)) return 4;
        return -1;
    }

    private static void FourMovesBottom(Cube cube, Cube.Color face)
    {
        cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW);
    }

    private static void FourMovesTop(Cube cube, Cube.Color face)
    {
        cube.RotateFace(face);
        cube.RotateFace(Cube.Color.WHITE);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Cube.Color.WHITE); cube.RotateFace(Cube.Color.WHITE); cube.RotateFace(Cube.Color.WHITE);
    }

    private static void FourMovesVariation(Cube cube, Cube.Color face)
    {
        cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(face); cube.RotateFace(face); cube.RotateFace(face);
        cube.RotateFace(Cube.Color.YELLOW, true);
    }

    private static void PermutationMoves(Cube cube, Cube.Color left, Cube.Color right)
    {
        cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(right);
        cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(left); cube.RotateFace(left); cube.RotateFace(left);
        cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(right); cube.RotateFace(right); cube.RotateFace(right);
        cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(left);
    }
}
