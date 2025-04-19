namespace UniLetters.Presentation;

partial class Form1
{
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(307, 144);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(177, 27);
        label1.TabIndex = 0;
        label1.Text = "Hello There!";
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(label1);
        Text = "Form1";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label label1;

    #endregion
}