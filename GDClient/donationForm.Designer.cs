using System.ComponentModel;

namespace GestiuneDonatii;

partial class donationForm
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
        numeDonator = new DataGridViewTextBoxColumn();
        telefonDonator = new DataGridViewTextBoxColumn();
        adresaDonator = new DataGridViewTextBoxColumn();
        textBox1 = new TextBox();
        textBox2 = new TextBox();
        textBox3 = new TextBox();
        textBox4 = new TextBox();
        textBox5 = new TextBox();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        label4 = new Label();
        button1 = new Button();
        ((ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(new DataGridViewColumn[] { numeDonator, telefonDonator, adresaDonator });
        dataGridView1.Location = new Point(12, 12);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 62;
        dataGridView1.RowTemplate.Height = 33;
        dataGridView1.Size = new Size(776, 315);
        dataGridView1.TabIndex = 0;
        dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        // 
        // numeDonator
        // 
        numeDonator.HeaderText = "Nume";
        numeDonator.MinimumWidth = 8;
        numeDonator.Name = "numeDonator";
        numeDonator.Width = 245;
        // 
        // telefonDonator
        // 
        telefonDonator.HeaderText = "Telefon";
        telefonDonator.MinimumWidth = 8;
        telefonDonator.Name = "telefonDonator";
        telefonDonator.Width = 245;
        // 
        // adresaDonator
        // 
        adresaDonator.HeaderText = "Adresa";
        adresaDonator.MinimumWidth = 8;
        adresaDonator.Name = "adresaDonator";
        adresaDonator.Width = 245;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(18, 394);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(150, 31);
        textBox1.TabIndex = 1;
        // 
        // textBox2
        // 
        textBox2.Location = new Point(191, 394);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(150, 31);
        textBox2.TabIndex = 2;
        // 
        // textBox3
        // 
        textBox3.Location = new Point(359, 394);
        textBox3.Name = "textBox3";
        textBox3.Size = new Size(150, 31);
        textBox3.TabIndex = 3;
        // 
        // textBox4
        // 
        textBox4.Location = new Point(525, 394);
        textBox4.Name = "textBox4";
        textBox4.Size = new Size(150, 31);
        textBox4.TabIndex = 4;
        // 
        // textBox5
        // 
        textBox5.Location = new Point(305, 333);
        textBox5.Name = "textBox5";
        textBox5.Size = new Size(219, 31);
        textBox5.TabIndex = 5;
        textBox5.Text = "Cauta donator";
        textBox5.TextChanged += textBox5_TextChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(18, 366);
        label1.Name = "label1";
        label1.Size = new Size(60, 25);
        label1.TabIndex = 6;
        label1.Text = "Nume";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(191, 366);
        label2.Name = "label2";
        label2.Size = new Size(68, 25);
        label2.TabIndex = 7;
        label2.Text = "Telefon";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(359, 366);
        label3.Name = "label3";
        label3.Size = new Size(67, 25);
        label3.TabIndex = 8;
        label3.Text = "Adresa";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(525, 366);
        label4.Name = "label4";
        label4.Size = new Size(57, 25);
        label4.TabIndex = 9;
        label4.Text = "Suma";
        // 
        // button1
        // 
        button1.Location = new Point(700, 392);
        button1.Name = "button1";
        button1.Size = new Size(88, 34);
        button1.TabIndex = 10;
        button1.Text = "Adauga";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // donationForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(button1);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(textBox5);
        Controls.Add(textBox4);
        Controls.Add(textBox3);
        Controls.Add(textBox2);
        Controls.Add(textBox1);
        Controls.Add(dataGridView1);
        Name = "donationForm";
        Text = "donationForm";
        ((ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn numeDonator;
    private DataGridViewTextBoxColumn telefonDonator;
    private DataGridViewTextBoxColumn adresaDonator;
    private TextBox textBox1;
    private TextBox textBox2;
    private TextBox textBox3;
    private TextBox textBox4;
    private TextBox textBox5;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Button button1;
}