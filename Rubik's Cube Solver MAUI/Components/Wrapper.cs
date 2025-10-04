namespace Rubik_s_Cube_Solver_MAUI.Components;

public class Wrapper : AbsoluteLayout
{
    public double windowHeight, windowWidth;
    public bool gte136;

    protected override void OnParentSet()
    {
        base.OnParentSet();
    }

    protected override void OnChildAdded(Element child)
    {
        base.OnChildAdded(child);

        if (child is View view && !Children.Contains(view) && Children[1] is Grid content)
        {
            int index = Children.Count > 2 ? 1 : Children.Count;
            content.Children.Insert(index, view);
        }
    }
}
