
namespace ant_colony_optimization;

public partial class DrawingForm : Form
{
    public DrawingForm()
    {
        InitializeComponent();
        this.panel1.MouseClick += new MouseEventHandler(this.panel1_MouseClick);
        this.btnClear.MouseClick += new MouseEventHandler(this.btnClear_MouseClick);
        this.btnRand.MouseClick += new MouseEventHandler(this.btnRand_MouseClick);
        this.btnACO.MouseClick += new MouseEventHandler(this.btnACO_MouseClick);
    }

    private void btnACO_MouseClick(object? sender, MouseEventArgs e)
    {
        aco = new AntColonyPathOptimization(graph);
        List<int> optimized_path = aco.GetOptimalPath();
        foreach (int town_index in optimized_path){
            listbox1.Items.Add(optimized_path[town_index]);
        }
        Pen pen = new Pen(Color.Red, 2);
        for(int ti = 0; ti < optimized_path.Count - 1; ti++){
            //draw lines between towns in order
            panel1.CreateGraphics().DrawLine(   pen, 
                                                graph.map[optimized_path[ti]].X + (dotSize /2), 
                                                graph.map[optimized_path[ti]].Y + (dotSize /2), 
                                                graph.map[optimized_path[ti + 1]].X + (dotSize /2), 
                                                graph.map[optimized_path[ti + 1]].Y + (dotSize /2)
                                            );
        }
    }

    private void btnRand_MouseClick(object? sender, MouseEventArgs e)
    {
        graph.RandTowns(new Size(500, 450), 10);
        foreach (var town in graph.map){
            draw_dot(town.X, town.Y);
        }
    }

    private void btnClear_MouseClick(object? sender, MouseEventArgs e)
    {
        graph.clear_graph();
        panel1.Invalidate(); 
    }

    private void panel1_MouseClick(object? sender, MouseEventArgs e)
    {
        graph.map.Add(new Town(e.X, e.Y));
        draw_dot(e.X, e.Y);
    }

    private void draw_dot(int mx, int my)
    {
        Graphics g = panel1.CreateGraphics();
        SolidBrush redBrush = new SolidBrush(Color.Red);
        g.FillEllipse(redBrush, mx, my, dotSize, dotSize);
    }
}