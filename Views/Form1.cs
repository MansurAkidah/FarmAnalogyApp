using FarmAnalogyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FarmAnalogyApp.Models.FarmModel;

namespace FarmAnalogyApp.Views
{
    public partial class Form1 : Form
    {
        private FarmViewModel _viewModel;

        public Form1()
        {
            InitializeComponent();
            InitializeViewModel();
            SetupBindings();
            SetupDataGridViewBinding();
        }

        private void InitializeViewModel()
        {
            _viewModel = new FarmViewModel();
        }

        private void SetupBindings()
        {
            // Binding farm dimensions
            txtFarmLength.DataBindings.Add("Text", _viewModel, "Length", true, DataSourceUpdateMode.OnPropertyChanged);
            txtFarmWidth.DataBindings.Add("Text", _viewModel, "Width", true, DataSourceUpdateMode.OnPropertyChanged);

            // Binding max cows label
            lblMaxCows.DataBindings.Add("Text", _viewModel, "MaxCows", true, DataSourceUpdateMode.OnPropertyChanged);

            // Binding cows per paddock
            txtCowsPerPaddock.DataBindings.Add("Text", _viewModel, "CowsPerPaddock", true, DataSourceUpdateMode.OnPropertyChanged);

            // Binding length label
            Length.DataBindings.Add("Text", _viewModel, "Length", true, DataSourceUpdateMode.OnPropertyChanged);

            // Binding Width label
            Width.DataBindings.Add("Text", _viewModel, "Width", true, DataSourceUpdateMode.OnPropertyChanged);

            // Binding Length and Width label of the paddock
            paddockDimensions.DataBindings.Add("Text", _viewModel, "PaddockDimensions", true, DataSourceUpdateMode.OnPropertyChanged);

            // Binding Length and Width label of the paddock
            noOfPaddocks.DataBindings.Add("Text", _viewModel, "NumberofPaddocks", true, DataSourceUpdateMode.OnPropertyChanged);

            // Binding paddocks to datagridview
            dgvPaddocks.DataSource = _viewModel.Paddocks;
        }
        private void SetupDataGridViewBinding()
        {
            // Clear existing columns
            dgvPaddocks.Columns.Clear();

            // Add columns matching Paddock properties
            dgvPaddocks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Name",
                DataPropertyName = "Name"
            });

            dgvPaddocks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Length",
                HeaderText = "Length (m)",
                DataPropertyName = "Length"
            });
            

            

            // Bind the DataSource
            dgvPaddocks.DataSource = _viewModel.Paddocks;

            
            //dgvPaddocks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }


        private void btnGenerateFarm_Click(object sender, EventArgs e)
        {
            try
            {
                int length = int.Parse(txtFarmLength.Text);
                int width = int.Parse(txtFarmWidth.Text);

                if (length < 210 || width < 300)
                {
                    MessageBox.Show("Farm length must be >= 210 and width must be >= 300", "Invalid Dimensions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _viewModel.GenerateFarmCommand.Execute(null);
                this.Controls.Add(this.paddockDimensions);
                this.Controls.Add(this.noOfPaddocks);
                this.Controls.Add(this.label5);
                this.Controls.Add(this.label4);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.txtCowsPerPaddock);
                this.Controls.Add(this.btnGeneratePaddocks);
                

                
                this.Controls.Remove(this.Lengh);
                this.Controls.Remove(this.label1);
                this.Controls.Remove(this.btnGenerateFarm); 
                this.Controls.Remove(this.txtFarmLength);
                this.Controls.Remove(this.txtFarmWidth);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for farm dimensions", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGeneratePaddocks_Click(object sender, EventArgs e)
        {
            try
            {
                int cowsPerPaddock = 0;// int.Parse(txtCowsPerPaddock.Text);
                if (!int.TryParse(txtCowsPerPaddock.Text, out cowsPerPaddock) || cowsPerPaddock <= 0)
                {
                    MessageBox.Show("Please insert a valid number of cows", "Invalid Dimensions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_viewModel == null)
                {
                    MessageBox.Show("Please set up your farm first", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (cowsPerPaddock <= 0)
                //{
                //    MessageBox.Show("Please insert a valid number of cows", "Invalid Dimensions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                _viewModel.GeneratePaddocksCommand.Execute(null);
                this.Controls.Add(this.dgvPaddocks);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number of cows per paddock", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        



        // Design-time generated method stubs (you'll need to add these in the designer)
        private void InitializeComponent()
        {
            this.txtFarmLength = new System.Windows.Forms.TextBox();
            this.txtFarmWidth = new System.Windows.Forms.TextBox();
            this.lblMaxCows = new System.Windows.Forms.Label();
            this.txtCowsPerPaddock = new System.Windows.Forms.TextBox();
            this.btnGenerateFarm = new System.Windows.Forms.Button();
            this.btnGeneratePaddocks = new System.Windows.Forms.Button();
            this.dgvPaddocks = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Lengh = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Length = new System.Windows.Forms.Label();
            this.Width = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.noOfPaddocks = new System.Windows.Forms.Label();
            this.paddockDimensions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaddocks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFarmLength
            // 
            this.txtFarmLength.Location = new System.Drawing.Point(100, 50);
            this.txtFarmLength.Name = "txtFarmLength";
            this.txtFarmLength.Size = new System.Drawing.Size(150, 22);
            this.txtFarmLength.TabIndex = 0;
            // 
            // txtFarmWidth
            // 
            this.txtFarmWidth.Location = new System.Drawing.Point(100, 100);
            this.txtFarmWidth.Name = "txtFarmWidth";
            this.txtFarmWidth.Size = new System.Drawing.Size(150, 22);
            this.txtFarmWidth.TabIndex = 1;
            // 
            // lblMaxCows
            // 
            this.lblMaxCows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxCows.Location = new System.Drawing.Point(62, 252);
            this.lblMaxCows.Name = "lblMaxCows";
            this.lblMaxCows.Size = new System.Drawing.Size(127, 23);
            this.lblMaxCows.TabIndex = 22;
            this.lblMaxCows.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCowsPerPaddock
            // 
            this.txtCowsPerPaddock.Location = new System.Drawing.Point(110, 75);
            this.txtCowsPerPaddock.Name = "txtCowsPerPaddock";
            this.txtCowsPerPaddock.Size = new System.Drawing.Size(105, 22);
            this.txtCowsPerPaddock.TabIndex = 3;
            // 
            // btnGenerateFarm
            // 
            this.btnGenerateFarm.Location = new System.Drawing.Point(51, 137);
            this.btnGenerateFarm.Name = "btnGenerateFarm";
            this.btnGenerateFarm.Size = new System.Drawing.Size(150, 30);
            this.btnGenerateFarm.TabIndex = 4;
            this.btnGenerateFarm.Text = "Setup Farm";
            this.btnGenerateFarm.Click += new System.EventHandler(this.btnGenerateFarm_Click);
            // 
            // btnGeneratePaddocks
            // 
            this.btnGeneratePaddocks.Location = new System.Drawing.Point(51, 137);
            this.btnGeneratePaddocks.Name = "btnGeneratePaddocks";
            this.btnGeneratePaddocks.Size = new System.Drawing.Size(150, 30);
            this.btnGeneratePaddocks.TabIndex = 5;
            this.btnGeneratePaddocks.Text = "Generate Paddocks";
            this.btnGeneratePaddocks.Click += new System.EventHandler(this.btnGeneratePaddocks_Click);
            
            // 
            // dgvPaddocks
            // 
            this.dgvPaddocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaddocks.Location = new System.Drawing.Point(300, 50);
            this.dgvPaddocks.Name = "dgvPaddocks";
            this.dgvPaddocks.RowHeadersWidth = 51;
            this.dgvPaddocks.Size = new System.Drawing.Size(450, 400);
            this.dgvPaddocks.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(51, 204);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 97);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // Lengh
            // 
            this.Lengh.AutoSize = true;
            this.Lengh.Location = new System.Drawing.Point(13, 55);
            this.Lengh.Name = "Lengh";
            this.Lengh.Size = new System.Drawing.Size(81, 16);
            this.Lengh.TabIndex = 7;
            this.Lengh.Text = "Farm Length";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Farm Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Cows per paddock";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Max Cows";
            // 
            // Length
            // 
            this.Length.AutoSize = true;
            this.Length.Location = new System.Drawing.Point(97, 185);
            this.Length.Name = "Length";
            this.Length.Size = new System.Drawing.Size(47, 16);
            this.Length.TabIndex = 23;
            this.Length.Text = "Length";
            // 
            // Width
            // 
            this.Width.AutoSize = true;
            this.Width.Location = new System.Drawing.Point(208, 235);
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(41, 16);
            this.Width.TabIndex = 24;
            this.Width.Text = "Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 410);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 25;
            this.label4.Text = "Number of paddocks:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 434);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 16);
            this.label5.TabIndex = 26;
            this.label5.Text = "Paddock Dimensions:";
            // 
            // noOfPaddocks
            // 
            this.noOfPaddocks.AutoSize = true;
            this.noOfPaddocks.Location = new System.Drawing.Point(162, 410);
            this.noOfPaddocks.Name = "noOfPaddocks";
            this.noOfPaddocks.Size = new System.Drawing.Size(14, 16);
            this.noOfPaddocks.TabIndex = 27;
            this.noOfPaddocks.Text = "0";
            // 
            // paddockDimensions
            // 
            this.paddockDimensions.AutoSize = true;
            this.paddockDimensions.Location = new System.Drawing.Point(165, 434);
            this.paddockDimensions.Name = "paddockDimensions";
            this.paddockDimensions.Size = new System.Drawing.Size(14, 16);
            this.paddockDimensions.TabIndex = 28;
            this.paddockDimensions.Text = "0";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(782, 553);
            //this.Controls.Add(this.paddockDimensions);
            //this.Controls.Add(this.noOfPaddocks);
            //this.Controls.Add(this.label5);
            //this.Controls.Add(this.label4);
            this.Controls.Add(this.Width);
            this.Controls.Add(this.Length);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Lengh);
            this.Controls.Add(this.txtFarmLength);
            this.Controls.Add(this.txtFarmWidth);
            this.Controls.Add(this.lblMaxCows);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnGenerateFarm);
            //this.Controls.Add(this.label2);
            //this.Controls.Add(this.txtCowsPerPaddock);
            //this.Controls.Add(this.btnGeneratePaddocks);
            //this.Controls.Add(this.dgvPaddocks);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Farm Management System";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaddocks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Control declarations
        private TextBox txtFarmLength;
        private TextBox txtFarmWidth;
        private Label lblMaxCows;
        private TextBox txtCowsPerPaddock;
        private Button btnGenerateFarm;
        private Button btnGeneratePaddocks;
        private DataGridView dgvPaddocks;
    }
}
