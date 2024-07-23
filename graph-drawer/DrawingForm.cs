using my_graph;

namespace drawing_app;

public partial class DrawingForm : Form
{
    public DrawingForm()
    {
        InitializeComponent();
        this.panel1.MouseClick += new MouseEventHandler(this.panel1_MouseClick);
        this.btnClear.MouseClick += new MouseEventHandler(this.btnClear_MouseClick);
        this.btnRand.MouseClick += new MouseEventHandler(this.btnRand_MouseClick);
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

    private void panel1_MouseClick(object sender, MouseEventArgs e)
    {
        graph.map.Add(new Town(e.X, e.Y));
        draw_dot(e.X, e.Y);
    }

    private void draw_dot(int mx, int my)
    {
        Graphics g = panel1.CreateGraphics();
        SolidBrush redBrush = new SolidBrush(Color.Red);
        int dotSize = 10; // Size of the dot
        g.FillEllipse(redBrush, mx, my, dotSize, dotSize);
    }
}