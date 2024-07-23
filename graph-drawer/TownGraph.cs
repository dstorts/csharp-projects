namespace my_graph;

public class Town
{
    public int X, Y;

    public Town(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

public class Graph
{
    public List<Town> map;
    public int margin;

    public Graph(int margin)
    {
        this.map = new List<Town>();
        this.margin = margin;
    }

    public void RandTowns(Size boundary, int num_nodes)
    {
        Random rng = new Random();
        for (int i = 0; i < num_nodes; i++)
        {
            int Rx = rng.Next(margin, boundary.Width) - (margin/2);
            int Ry = rng.Next(margin, boundary.Height) - (margin/2);
            map.Add(new Town(Rx, Ry));
        }
    }

    public void clear_graph(){
        map.Clear();
    }
}