namespace Rubik_s_Cube_Solver_MAUI.Core.Solver;

internal static class FirstLayer
{
    public static void Solve(Cube cube)
    {
        while (!(
            cube.Corners[0].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.ORANGE, Cube.Color.GREEN }) &&
            cube.Corners[1].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.GREEN, Cube.Color.RED }) &&
            cube.Corners[2].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.RED, Cube.Color.BLUE }) &&
            cube.Corners[3].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.BLUE, Cube.Color.ORANGE })
        ))
        {
            int corner = FindMatching(cube);
            if (corner == -1)
            {
                cube.RotateFace(Cube.Color.YELLOW, conf: true);
                continue;
            }
            switch (corner)
            {
                case 4: case 8: FourMoves(cube, Cube.Color.ORANGE); cube.RotateFace(Cube.Color.YELLOW, conf: true); break;
                case 5: case 10: FourMoves(cube, Cube.Color.GREEN); cube.RotateFace(Cube.Color.YELLOW, conf: true); break;
                case 6: case 12: FourMoves(cube, Cube.Color.RED); cube.RotateFace(Cube.Color.YELLOW, conf: true); break;
                case 7: case 14: FourMoves(cube, Cube.Color.BLUE); cube.RotateFace(Cube.Color.YELLOW, conf: true); break;
            }
            if (corner > 7) continue;
            if (corner > 3) corner %= 4;
            switch (corner)
            {
                case 0:
                    while (!cube.Corners[0].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.ORANGE, Cube.Color.GREEN }))
                    {
                        FourMoves(cube, Cube.Color.ORANGE); FourMoves(cube, Cube.Color.ORANGE);
                        cube.Configure();
                    }
                    break;

                case 1:
                    while (!cube.Corners[1].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.GREEN, Cube.Color.RED }))
                    {
                        FourMoves(cube, Cube.Color.GREEN); FourMoves(cube, Cube.Color.GREEN);
                        cube.Configure();
                    }
                    break;

                case 2:
                    while (!cube.Corners[2].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.RED, Cube.Color.BLUE }))
                    {
                        FourMoves(cube, Cube.Color.RED); FourMoves(cube, Cube.Color.RED);
                        cube.Configure();
                    }
                    break;

                case 3:
                    while (!cube.Corners[3].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.BLUE, Cube.Color.ORANGE }))
                    {
                        FourMoves(cube, Cube.Color.BLUE); FourMoves(cube, Cube.Color.BLUE);
                        cube.Configure();
                    }
                    break;

                default:
                    cube.RotateFace(Cube.Color.YELLOW);
                    break;
            }
            cube.Configure();
        }
    }

    private static void FourMoves(Cube cube, Cube.Color color)
    {
        cube.RotateFace(color);
        cube.RotateFace(Cube.Color.YELLOW);
        cube.RotateFace(color); cube.RotateFace(color); cube.RotateFace(color);
        cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW); cube.RotateFace(Cube.Color.YELLOW);
    }

    private static int FindMatching(Cube cube)
    {
        if (cube.Corners[0].Contains(Cube.Color.WHITE) && cube.Corners[0].Contains(Cube.Color.ORANGE) && cube.Corners[0].Contains(Cube.Color.GREEN) && !cube.Corners[0].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.ORANGE, Cube.Color.GREEN })) return 0;
        if (cube.Corners[1].Contains(Cube.Color.WHITE) && cube.Corners[1].Contains(Cube.Color.GREEN) && cube.Corners[1].Contains(Cube.Color.RED) && !cube.Corners[1].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.GREEN, Cube.Color.RED })) return 1;
        if (cube.Corners[2].Contains(Cube.Color.WHITE) && cube.Corners[2].Contains(Cube.Color.RED) && cube.Corners[2].Contains(Cube.Color.BLUE) && !cube.Corners[2].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.RED, Cube.Color.BLUE })) return 2;
        if (cube.Corners[3].Contains(Cube.Color.WHITE) && cube.Corners[3].Contains(Cube.Color.BLUE) && cube.Corners[3].Contains(Cube.Color.ORANGE) && !cube.Corners[3].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.BLUE, Cube.Color.ORANGE })) return 3;
        if (cube.Corners[4].Contains(Cube.Color.WHITE) && cube.Corners[4].Contains(Cube.Color.ORANGE) && cube.Corners[4].Contains(Cube.Color.GREEN)) return 4;
        if (cube.Corners[5].Contains(Cube.Color.WHITE) && cube.Corners[5].Contains(Cube.Color.GREEN) && cube.Corners[5].Contains(Cube.Color.RED)) return 5;
        if (cube.Corners[6].Contains(Cube.Color.WHITE) && cube.Corners[6].Contains(Cube.Color.RED) && cube.Corners[6].Contains(Cube.Color.BLUE)) return 6;
        if (cube.Corners[7].Contains(Cube.Color.WHITE) && cube.Corners[7].Contains(Cube.Color.BLUE) && cube.Corners[7].Contains(Cube.Color.ORANGE)) return 7;
        if (cube.Corners[0].Contains(Cube.Color.WHITE) && cube.Corners[1].Contains(Cube.Color.WHITE) && cube.Corners[2].Contains(Cube.Color.WHITE) && cube.Corners[3].Contains(Cube.Color.WHITE))
        {
            if (!cube.Corners[0].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.ORANGE, Cube.Color.GREEN })) return 8;
            if (!cube.Corners[1].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.GREEN, Cube.Color.RED })) return 10;
            if (!cube.Corners[2].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.RED, Cube.Color.BLUE })) return 12;
            if (!cube.Corners[3].SequenceEqual(new List<Cube.Color>() { Cube.Color.WHITE, Cube.Color.BLUE, Cube.Color.ORANGE })) return 14;
        }
        return -1;
    }
}