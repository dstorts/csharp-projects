namespace ant_colony_optimization;

partial class DrawingForm
{
    private Panel panel1;
    private int dotSize = 10; // Size of the dot that reps a town
    private System.Windows.Forms.Button btnClear, btnRand, btnACO;
    private System.Windows.Forms.ListBox listbox1;

    private ant_colony_optimization.Graph graph;
    private ant_colony_optimization.AntColonyPathOptimization aco;
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.graph = new ant_colony_optimization.Graph(40);

        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 500);
        this.Text = "Drawing App";
        this.BackColor = Color.Beige;

        this.btnClear = new System.Windows.Forms.Button();
        this.btnClear.Location = new Point(520,15);
        this.btnClear.Size = new System.Drawing.Size(100,80);
        this.btnClear.Text = "Clear";
        this.Controls.Add(btnClear);

        this.btnRand = new System.Windows.Forms.Button();
        this.btnRand.Location = new Point(520,100);
        this.btnRand.Size = new System.Drawing.Size(100,80);
        this.btnRand.Text = "Generate Towns";
        this.Controls.Add(btnRand);

        this.btnACO = new System.Windows.Forms.Button();
        this.btnACO.Location = new Point(520,185);
        this.btnACO.Size = new System.Drawing.Size(100,80);
        this.btnACO.Text = "Run ACO";
        this.Controls.Add(btnACO);
        
        this.panel1 = new Panel();
        this.panel1.Location = new Point(10,10);
        this.panel1.Size = new Size(500, 450);
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.Controls.Add(panel1);

        this.listbox1 = new System.Windows.Forms.ListBox();
        this.listbox1.Location = new Point(520,270);
        this.listbox1.Size = new System.Drawing.Size(200,200);
        this.Controls.Add(listbox1);
    }

    #endregion
}
