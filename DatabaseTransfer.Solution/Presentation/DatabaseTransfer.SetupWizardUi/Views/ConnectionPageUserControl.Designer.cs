namespace DatabaseTransfer.SetupWizardUi.Views
{
    partial class ConnectionPageUserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ActianConnectionTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TransferConnectionTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TransferDatabaseTypeImageComboBoxEdit = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.DxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActianConnectionTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferConnectionTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferDatabaseTypeImageComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.labelControl3);
            this.layoutControl1.Controls.Add(this.ActianConnectionTextEdit);
            this.layoutControl1.Controls.Add(this.TransferConnectionTextEdit);
            this.layoutControl1.Controls.Add(this.TransferDatabaseTypeImageComboBoxEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1564, 636, 1265, 528);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1200, 565);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(49, 75);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Padding = new System.Windows.Forms.Padding(10, 11, 12, 11);
            this.labelControl2.Size = new System.Drawing.Size(1102, 42);
            this.labelControl2.StyleController = this.layoutControl1;
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "Specify the database connection strings.";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(49, 473);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Padding = new System.Windows.Forms.Padding(10, 11, 12, 11);
            this.labelControl3.Size = new System.Drawing.Size(1102, 41);
            this.labelControl3.StyleController = this.layoutControl1;
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "Set the connection strings to continue.";
            // 
            // ActianConnectionTextEdit
            // 
            this.DxErrorProvider.SetIconAlignment(this.ActianConnectionTextEdit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.ActianConnectionTextEdit.Location = new System.Drawing.Point(57, 148);
            this.ActianConnectionTextEdit.Name = "ActianConnectionTextEdit";
            this.ActianConnectionTextEdit.Size = new System.Drawing.Size(1086, 22);
            this.ActianConnectionTextEdit.StyleController = this.layoutControl1;
            this.ActianConnectionTextEdit.TabIndex = 9;
            this.ActianConnectionTextEdit.Validating += new System.ComponentModel.CancelEventHandler(this.ActianConnectionTextEdit_Validating);
            // 
            // TransferConnectionTextEdit
            // 
            this.DxErrorProvider.SetIconAlignment(this.TransferConnectionTextEdit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.TransferConnectionTextEdit.Location = new System.Drawing.Point(57, 208);
            this.TransferConnectionTextEdit.Name = "TransferConnectionTextEdit";
            this.TransferConnectionTextEdit.Size = new System.Drawing.Size(1086, 22);
            this.TransferConnectionTextEdit.StyleController = this.layoutControl1;
            this.TransferConnectionTextEdit.TabIndex = 10;
            this.TransferConnectionTextEdit.Validating += new System.ComponentModel.CancelEventHandler(this.TransferConnectionTextEdit_Validating);
            // 
            // TransferDatabaseTypeImageComboBoxEdit
            // 
            this.TransferDatabaseTypeImageComboBoxEdit.Location = new System.Drawing.Point(57, 267);
            this.TransferDatabaseTypeImageComboBoxEdit.Name = "TransferDatabaseTypeImageComboBoxEdit";
            this.TransferDatabaseTypeImageComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TransferDatabaseTypeImageComboBoxEdit.Size = new System.Drawing.Size(496, 22);
            this.TransferDatabaseTypeImageComboBoxEdit.StyleController = this.layoutControl1;
            this.TransferDatabaseTypeImageComboBoxEdit.TabIndex = 19;
            this.TransferDatabaseTypeImageComboBoxEdit.SelectedIndexChanged += new System.EventHandler(this.TransferDatabaseTypeImageComboBoxEdit_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem8,
            this.emptySpaceItem2,
            this.layoutControlItem9});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(47, 47, 0, 49);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1200, 565);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.ActianConnectionTextEdit;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 119);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.OptionsToolTip.ToolTip = "The Actian/Pervasive database connection string.";
            this.layoutControlItem1.OptionsToolTip.ToolTipTitle = "Actian Connection String";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 9, 9);
            this.layoutControlItem1.Size = new System.Drawing.Size(1106, 60);
            this.layoutControlItem1.Text = "Actian Connection:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(143, 17);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.TransferConnectionTextEdit;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 179);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.OptionsToolTip.ToolTip = "The Postgre and/or Microsoft database connection string.";
            this.layoutControlItem3.OptionsToolTip.ToolTipTitle = "Transfer Connection String";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 9, 9);
            this.layoutControlItem3.Size = new System.Drawing.Size(1106, 60);
            this.layoutControlItem3.Text = "Transfer Connection:";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(143, 17);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 298);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1106, 173);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.labelControl3;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 471);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1106, 45);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.labelControl2;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 73);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(1106, 46);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(1106, 73);
            this.emptySpaceItem2.Text = "emptySpaceItem1";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.TransferDatabaseTypeImageComboBoxEdit;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 239);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.OptionsToolTip.ToolTip = "What type of database is the transfer connection string.";
            this.layoutControlItem9.OptionsToolTip.ToolTipTitle = "Transfer Database Type";
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 600, 9, 9);
            this.layoutControlItem9.Size = new System.Drawing.Size(1106, 59);
            this.layoutControlItem9.Text = "Transfer Database Type:";
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(143, 16);
            // 
            // DxErrorProvider
            // 
            this.DxErrorProvider.ContainerControl = this;
            // 
            // ConnectionPageUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "ConnectionPageUserControl";
            this.Size = new System.Drawing.Size(1200, 565);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ActianConnectionTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferConnectionTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferDatabaseTypeImageComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit ActianConnectionTextEdit;
        private DevExpress.XtraEditors.TextEdit TransferConnectionTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider DxErrorProvider;
        private DevExpress.XtraEditors.ImageComboBoxEdit TransferDatabaseTypeImageComboBoxEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}
