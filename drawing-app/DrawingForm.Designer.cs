namespace drawing_app;

partial class DrawingForm
{
    private Panel panel1;
    private System.Windows.Forms.Button btnClear = new System.Windows.Forms.Button();
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
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Drawing App";
        this.BackColor = Color.Beige;

        this.btnClear.Location = new Point(520,15);
        this.btnClear.Size = new System.Drawing.Size(100,80);
        this.btnClear.Text = "Clear";
        this.Controls.Add(btnClear);
        
        this.panel1 = new Panel();
        this.panel1.Location = new Point(10,10);
        this.panel1.Size = new Size(500, 450);
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.Controls.Add(panel1);
    }

    #endregion
}
