
namespace drawing_app;

public partial class DrawingForm : Form
    {
        public DrawingForm()
        {
            InitializeComponent();
            this.panel1.MouseClick += new MouseEventHandler(this.panel1_MouseClick);
            this.btnClear.MouseClick += new MouseEventHandler(this.btnClear_MouseClick);
        }

    private void btnClear_MouseClick(object? sender, MouseEventArgs e)
    {
        panel1.Invalidate(); 
    }

    private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
            Graphics g = panel1.CreateGraphics();
            SolidBrush redBrush = new SolidBrush(Color.Red);
            int dotSize = 10; // Size of the dot
            g.FillEllipse(redBrush, e.X, e.Y, dotSize, dotSize);
        }

    }