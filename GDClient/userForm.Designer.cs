using System.ComponentModel;

namespace GestiuneDonatii;

partial class userForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
        dataGridView1 = new DataGridView();
        button1 = new Button();
        button2 = new Button();
        numeCauza = new DataGridViewTextBoxColumn();
        sumaCauza = new DataGridViewTextBoxColumn();
        ((ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(new DataGridViewColumn[] { numeCauza, sumaCauza });
        dataGridView1.Location = new Point(15, 12);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 62;
        dataGridView1.RowTemplate.Height = 33;
        dataGridView1.Size = new Size(764, 378);
        dataGridView1.TabIndex = 0;
        // 
        // button1
        // 
        button1.Location = new Point(191, 404);
        button1.Name = "button1";
        button1.Size = new Size(211, 34);
        button1.TabIndex = 1;
        button1.Text = "Adauga donatie";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Location = new Point(466, 404);
        button2.Name = "button2";
        button2.Size = new Size(112, 34);
        button2.TabIndex = 2;
        button2.Text = "Logout";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // numeCauza
        // 
        numeCauza.HeaderText = "Cauza";
        numeCauza.MinimumWidth = 8;
        numeCauza.Name = "numeCauza";
        numeCauza.Width = 350;
        // 
        // sumaCauza
        // 
        sumaCauza.HeaderText = "Suma";
        sumaCauza.MinimumWidth = 8;
        sumaCauza.Name = "sumaCauza";
        sumaCauza.Width = 350;
        // 
        // userForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(dataGridView1);
        Name = "userForm";
        Text = "userForm";
        ((ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DataGridView dataGridView1;
    private Button button1;
    private Button button2;
    private DataGridViewTextBoxColumn numeCauza;
    private DataGridViewTextBoxColumn sumaCauza;
}