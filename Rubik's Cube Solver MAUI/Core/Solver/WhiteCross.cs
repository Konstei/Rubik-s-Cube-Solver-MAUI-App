namespace Rubik_s_Cube_Solver_MAUI.Core.Solver;

internal static class WhiteCross
{
    public static void Solve(Cube cube)
    {
        for (int e = 0; e < 4; e++)
        {
            List<Cube.Color> face = cube.Edges.FirstOrDefault(innerList => innerList.Contains(Cube.Color.WHITE)) ?? throw new UniverseException("the name of the exception should be self explanatory really");

            switch (cube.Edges.IndexOf(face))
            {
                case 0:
                    // WHITE
                    switch (face.IndexOf(Cube.Color.WHITE))
                    {
                        case 4:
                            while (cube.Faces[5][6] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW);
                            cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE);
                            break;
                        case 1:
                            while (cube.Faces[5][3] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW);
                            cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE);
                            break;
                        case 3:
                            while (cube.Faces[5][4] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW);
                            cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED);
                            break;
                        case 2:
                            while (cube.Faces[5][1] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW);
                            cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN);
                            break;
                    }
                    break;

                case 1:
                    // ORANGE
                    switch (face.IndexOf(Cube.Color.WHITE))
                    {
                        case 5:
                            cube.RotateFace(Cube.Color.ORANGE);
                            while (cube.Edges[5][4] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.BLUE);
                            break;
                        case 4:
                            while (cube.Edges[5][4] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.BLUE);
                            break;
                        case 0:
                            while (cube.Edges[5][1] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.ORANGE);
                            while (cube.Edges[5][2] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN);
                            break;
                        case 2:
                            while (cube.Edges[5][2] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN);
                            break;
                    }
                    break;

                case 2:
                    // GREEN
                    switch (face.IndexOf(Cube.Color.WHITE))
                    {
                        case 5:
                            cube.RotateFace(Cube.Color.GREEN);
                            while (cube.Edges[5][1] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.ORANGE);
                            break;
                        case 1:
                            while (cube.Edges[5][1] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.ORANGE);
                            break;
                        case 0:
                            while (cube.Edges[5][2] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.GREEN);
                            while (cube.Edges[5][3] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED);
                            break;
                        case 3:
                            while (cube.Edges[5][3] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED);
                            break;
                    }
                    break;

                case 3:
                    // RED
                    switch (face.IndexOf(Cube.Color.WHITE))
                    {
                        case 5:
                            cube.RotateFace(Cube.Color.RED);
                            while (cube.Edges[5][2] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.GREEN);
                            break;
                        case 2:
                            while (cube.Edges[5][2] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.GREEN);
                            break;
                        case 0:
                            while (cube.Edges[5][3] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.RED);
                            while (cube.Edges[5][4] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE);
                            break;
                        case 4:
                            while (cube.Edges[5][4] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE);
                            break;
                    }
                    break;

                case 4:
                    // BLUE
                    switch (face.IndexOf(Cube.Color.WHITE))
                    {
                        case 5:
                            cube.RotateFace(Cube.Color.BLUE);
                            while (cube.Edges[5][3] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.RED);
                            break;
                        case 3:
                            while (cube.Edges[5][3] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.RED);
                            break;
                        case 0:
                            while (cube.Edges[5][4] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.BLUE);
                            while (cube.Edges[5][1] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE);
                            break;
                        case 1:
                            while (cube.Edges[5][1] == Cube.Color.WHITE) cube.RotateFace(Cube.Color.YELLOW, conf: true);
                            cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE);
                            break;
                    }
                    break;
            }

            cube.Configure();
        }

        while (
            new List<Cube.Color>() {
                cube.Edges[5][1],
                cube.Edges[5][2],
                cube.Edges[5][3],
                cube.Edges[5][4]
            }.Contains(Cube.Color.WHITE)
        )
        {
            switch (FindMatching(cube))
            {
                case Cube.Color.ORANGE: cube.RotateFace(Cube.Color.ORANGE); cube.RotateFace(Cube.Color.ORANGE); break;
                case Cube.Color.GREEN: cube.RotateFace(Cube.Color.GREEN); cube.RotateFace(Cube.Color.GREEN); break;
                case Cube.Color.RED: cube.RotateFace(Cube.Color.RED); cube.RotateFace(Cube.Color.RED); break;
                case Cube.Color.BLUE: cube.RotateFace(Cube.Color.BLUE); cube.RotateFace(Cube.Color.BLUE); break;
                default: cube.RotateFace(Cube.Color.YELLOW); break;
            }
            cube.Configure();
        }
    }

    private static Cube.Color FindMatching(Cube cube)
    {
        for (int f = 1; f <= 4; f++)
        {
            if (((int)cube.Edges[f][5]) == f && cube.Edges[5][f] == Cube.Color.WHITE) return (Cube.Color)(f);
        }
        return Cube.Color.NONE;
    }
}

[Serializable]
class UniverseException : Exception
{
    public UniverseException() { }

    public UniverseException(string message)
    : base(message)
    { }
}