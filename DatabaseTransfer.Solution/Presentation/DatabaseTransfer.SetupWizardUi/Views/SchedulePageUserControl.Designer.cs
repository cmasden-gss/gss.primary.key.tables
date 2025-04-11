namespace DatabaseTransfer.SetupWizardUi.Views
{
    partial class SchedulePageUserControl
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
            this.TransferScheduleStartTimeTimeEdit = new DevExpress.XtraEditors.TimeEdit();
            this.NameTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TransferScheduleTypeComboBoxEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.TransferOffsetComboBoxEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.TransferSynchronousTypeImageComboBoxEdit = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.DxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.OkSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TransferScheduleStartTimeTimeEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferScheduleTypeComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferOffsetComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferSynchronousTypeImageComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.TransferScheduleStartTimeTimeEdit);
            this.layoutControl1.Controls.Add(this.NameTextEdit);
            this.layoutControl1.Controls.Add(this.TransferScheduleTypeComboBoxEdit);
            this.layoutControl1.Controls.Add(this.TransferOffsetComboBoxEdit);
            this.layoutControl1.Controls.Add(this.TransferSynchronousTypeImageComboBoxEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1564, 636, 1265, 528);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1200, 274);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // TransferScheduleStartTimeTimeEdit
            // 
            this.TransferScheduleStartTimeTimeEdit.EditValue = new System.DateTime(2020, 1, 27, 0, 0, 0, 0);
            this.TransferScheduleStartTimeTimeEdit.Location = new System.Drawing.Point(20, 170);
            this.TransferScheduleStartTimeTimeEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TransferScheduleStartTimeTimeEdit.Name = "TransferScheduleStartTimeTimeEdit";
            this.TransferScheduleStartTimeTimeEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.TransferScheduleStartTimeTimeEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TransferScheduleStartTimeTimeEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TransferScheduleStartTimeTimeEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TransferScheduleStartTimeTimeEdit.Size = new System.Drawing.Size(514, 24);
            this.TransferScheduleStartTimeTimeEdit.StyleController = this.layoutControl1;
            this.TransferScheduleStartTimeTimeEdit.TabIndex = 14;
            // 
            // NameTextEdit
            // 
            this.DxErrorProvider.SetIconAlignment(this.NameTextEdit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.NameTextEdit.Location = new System.Drawing.Point(20, 39);
            this.NameTextEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NameTextEdit.Name = "NameTextEdit";
            this.NameTextEdit.Size = new System.Drawing.Size(1160, 22);
            this.NameTextEdit.StyleController = this.layoutControl1;
            this.NameTextEdit.TabIndex = 9;
            this.NameTextEdit.Validating += new System.ComponentModel.CancelEventHandler(this.NameTextEdit_Validating);
            // 
            // TransferScheduleTypeComboBoxEdit
            // 
            this.TransferScheduleTypeComboBoxEdit.Location = new System.Drawing.Point(20, 110);
            this.TransferScheduleTypeComboBoxEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TransferScheduleTypeComboBoxEdit.Name = "TransferScheduleTypeComboBoxEdit";
            this.TransferScheduleTypeComboBoxEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.TransferScheduleTypeComboBoxEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TransferScheduleTypeComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TransferScheduleTypeComboBoxEdit.Size = new System.Drawing.Size(514, 22);
            this.TransferScheduleTypeComboBoxEdit.StyleController = this.layoutControl1;
            this.TransferScheduleTypeComboBoxEdit.TabIndex = 11;
            // 
            // TransferOffsetComboBoxEdit
            // 
            this.TransferOffsetComboBoxEdit.Enabled = false;
            this.DxErrorProvider.SetIconAlignment(this.TransferOffsetComboBoxEdit, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.TransferOffsetComboBoxEdit.Location = new System.Drawing.Point(554, 169);
            this.TransferOffsetComboBoxEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TransferOffsetComboBoxEdit.Name = "TransferOffsetComboBoxEdit";
            this.TransferOffsetComboBoxEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.TransferOffsetComboBoxEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TransferOffsetComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TransferOffsetComboBoxEdit.Properties.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "15",
            "30",
            "45",
            "60",
            "120",
            "240",
            "360",
            "720"});
            this.TransferOffsetComboBoxEdit.Size = new System.Drawing.Size(626, 22);
            this.TransferOffsetComboBoxEdit.StyleController = this.layoutControl1;
            this.TransferOffsetComboBoxEdit.TabIndex = 18;
            this.TransferOffsetComboBoxEdit.SelectedIndexChanged += new System.EventHandler(this.TransferOffsetComboBoxEdit_SelectedIndexChanged);
            this.TransferOffsetComboBoxEdit.Validating += new System.ComponentModel.CancelEventHandler(this.TransferOffsetComboBoxEdit_Validating);
            // 
            // TransferSynchronousTypeImageComboBoxEdit
            // 
            this.TransferSynchronousTypeImageComboBoxEdit.Location = new System.Drawing.Point(554, 109);
            this.TransferSynchronousTypeImageComboBoxEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TransferSynchronousTypeImageComboBoxEdit.Name = "TransferSynchronousTypeImageComboBoxEdit";
            this.TransferSynchronousTypeImageComboBoxEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.TransferSynchronousTypeImageComboBoxEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TransferSynchronousTypeImageComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TransferSynchronousTypeImageComboBoxEdit.Size = new System.Drawing.Size(626, 22);
            this.TransferSynchronousTypeImageComboBoxEdit.StyleController = this.layoutControl1;
            this.TransferSynchronousTypeImageComboBoxEdit.TabIndex = 19;
            this.TransferSynchronousTypeImageComboBoxEdit.SelectedIndexChanged += new System.EventHandler(this.TransferSynchronousTypeImageComboBoxEdit_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.simpleSeparator2,
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem9});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1200, 274);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.NameTextEdit;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.OptionsToolTip.ToolTip = "The name of the schedule.";
            this.layoutControlItem1.OptionsToolTip.ToolTipTitle = "Schedule Name";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 9, 9);
            this.layoutControlItem1.Size = new System.Drawing.Size(1180, 60);
            this.layoutControlItem1.Text = "Name:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(92, 17);
            // 
            // simpleSeparator2
            // 
            this.simpleSeparator2.AllowHotTrack = false;
            this.simpleSeparator2.Location = new System.Drawing.Point(0, 60);
            this.simpleSeparator2.Name = "simpleSeparator2";
            this.simpleSeparator2.Size = new System.Drawing.Size(1180, 11);
            this.simpleSeparator2.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.TransferScheduleTypeComboBoxEdit;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 71);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.OptionsToolTip.ToolTip = "How often the service should transfer the data.";
            this.layoutControlItem2.OptionsToolTip.ToolTipTitle = "Schedule Type";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 9, 9);
            this.layoutControlItem2.Size = new System.Drawing.Size(534, 60);
            this.layoutControlItem2.Text = "Schedule Type:";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(92, 17);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.Control = this.TransferScheduleStartTimeTimeEdit;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 131);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.OptionsToolTip.ToolTip = "When the service should transfer the data.";
            this.layoutControlItem6.OptionsToolTip.ToolTipTitle = "Schedule Type";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 9, 9);
            this.layoutControlItem6.Size = new System.Drawing.Size(534, 123);
            this.layoutControlItem6.Text = "Schedule Time:";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(92, 17);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.TransferOffsetComboBoxEdit;
            this.layoutControlItem7.Location = new System.Drawing.Point(534, 131);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.OptionsToolTip.ToolTip = "The offset is used for deleting records from the target tables and importing data" +
    " from the source tables.";
            this.layoutControlItem7.OptionsToolTip.ToolTipTitle = "Transfer Offset";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 9, 9);
            this.layoutControlItem7.Size = new System.Drawing.Size(646, 123);
            this.layoutControlItem7.Text = "Transfer Offset:";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(92, 16);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.TransferSynchronousTypeImageComboBoxEdit;
            this.layoutControlItem9.Location = new System.Drawing.Point(534, 71);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.OptionsToolTip.ToolTip = "If the transfer should be synchronous (merges data) or snapshot (bulk copy).";
            this.layoutControlItem9.OptionsToolTip.ToolTipTitle = "Transfer Synchronous Type";
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 9, 9);
            this.layoutControlItem9.Size = new System.Drawing.Size(646, 60);
            this.layoutControlItem9.Text = "Transfer Type:";
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(92, 16);
            // 
            // DxErrorProvider
            // 
            this.DxErrorProvider.ContainerControl = this;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.OkSimpleButton);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 222);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1200, 52);
            this.panelControl2.TabIndex = 10;
            // 
            // OkSimpleButton
            // 
            this.OkSimpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkSimpleButton.Location = new System.Drawing.Point(1094, 10);
            this.OkSimpleButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OkSimpleButton.Name = "OkSimpleButton";
            this.OkSimpleButton.Size = new System.Drawing.Size(93, 30);
            this.OkSimpleButton.TabIndex = 5;
            this.OkSimpleButton.Text = "Ok";
            this.OkSimpleButton.Click += new System.EventHandler(this.OkSimpleButton_Click);
            // 
            // SchedulePageUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "SchedulePageUserControl";
            this.Size = new System.Drawing.Size(1200, 274);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TransferScheduleStartTimeTimeEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferScheduleTypeComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferOffsetComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferSynchronousTypeImageComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit NameTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.ComboBoxEdit TransferScheduleTypeComboBoxEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TimeEdit TransferScheduleStartTimeTimeEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider DxErrorProvider;
        private DevExpress.XtraEditors.ComboBoxEdit TransferOffsetComboBoxEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.ImageComboBoxEdit TransferSynchronousTypeImageComboBoxEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton OkSimpleButton;
    }
}
