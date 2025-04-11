using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace DatabaseTransfer.SetupWizardUi.Views
{
    partial class SchemaBrowserPageUserControl
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
            DevExpress.Utils.AppearanceObject appearanceObject1 = new DevExpress.Utils.AppearanceObject();
            this.DockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.TableNameDockPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.TableNameGridControl = new DevExpress.XtraGrid.GridControl();
            this.TableNameGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NameGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TableSchemaDockPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.TableSchemaGridControl = new DevExpress.XtraGrid.GridControl();
            this.TableSchemaGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NameGridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OrdinalGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DataTypeGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IsKeyGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaskDataTypeGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.StatusLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.BehaviorManager = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.TableSchemaGridViewDisabledCellEvents = new DevExpress.Utils.Behaviors.Common.DisabledCellEvents(this.components);
            this.TableSchemaPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.UseMaskBarCheckItem = new DevExpress.XtraBars.BarCheckItem();
            this.UseColumnBarCheckItem = new DevExpress.XtraBars.BarCheckItem();
            this.BarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.DetailsBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.TableNamePopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.DockManager)).BeginInit();
            this.TableNameDockPanel.SuspendLayout();
            this.dockPanel_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableNameGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableNameGridView)).BeginInit();
            this.TableSchemaDockPanel.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableSchemaGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableSchemaGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BehaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableSchemaPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableNamePopupMenu)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // DockManager
            // 
            this.DockManager.Form = this;
            this.DockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.TableNameDockPanel,
            this.TableSchemaDockPanel});
            this.DockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane"});
            // 
            // TableNameDockPanel
            // 
            this.TableNameDockPanel.AutoScroll = true;
            this.TableNameDockPanel.Controls.Add(this.dockPanel_Container);
            this.TableNameDockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.TableNameDockPanel.ID = new System.Guid("a045df26-1503-4d9a-99c1-a531310af22b");
            this.TableNameDockPanel.Location = new System.Drawing.Point(47, 0);
            this.TableNameDockPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TableNameDockPanel.Name = "TableNameDockPanel";
            this.TableNameDockPanel.Options.ShowCloseButton = false;
            this.TableNameDockPanel.Options.ShowMaximizeButton = false;
            this.TableNameDockPanel.OriginalSize = new System.Drawing.Size(482, 200);
            this.TableNameDockPanel.Size = new System.Drawing.Size(482, 751);
            this.TableNameDockPanel.Text = "Tables";
            // 
            // dockPanel_Container
            // 
            this.dockPanel_Container.Controls.Add(this.TableNameGridControl);
            this.dockPanel_Container.Location = new System.Drawing.Point(4, 32);
            this.dockPanel_Container.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dockPanel_Container.Name = "dockPanel_Container";
            this.dockPanel_Container.Size = new System.Drawing.Size(472, 715);
            this.dockPanel_Container.TabIndex = 0;
            // 
            // TableNameGridControl
            // 
            this.TableNameGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableNameGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TableNameGridControl.Location = new System.Drawing.Point(0, 0);
            this.TableNameGridControl.MainView = this.TableNameGridView;
            this.TableNameGridControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TableNameGridControl.Name = "TableNameGridControl";
            this.TableNameGridControl.Size = new System.Drawing.Size(472, 715);
            this.TableNameGridControl.TabIndex = 3;
            this.TableNameGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.TableNameGridView});
            // 
            // TableNameGridView
            // 
            this.TableNameGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.NameGridColumn});
            this.TableNameGridView.GridControl = this.TableNameGridControl;
            this.TableNameGridView.Name = "TableNameGridView";
            this.TableNameGridView.OptionsDetail.EnableMasterViewMode = false;
            this.TableNameGridView.OptionsFind.AlwaysVisible = true;
            this.TableNameGridView.OptionsFind.FindNullPrompt = "Select Tables...";
            this.TableNameGridView.OptionsSelection.CheckBoxSelectorField = "IsSelected";
            this.TableNameGridView.OptionsSelection.MultiSelect = true;
            this.TableNameGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.TableNameGridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.TableNameGridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.TableNameGridView.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False;
            this.TableNameGridView.OptionsView.ShowGroupPanel = false;
            this.TableNameGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.NameGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.TableNameGridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.TableNameGridView_PopupMenuShowing);
            this.TableNameGridView.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.TableNameGridView_SelectionChanged);
            this.TableNameGridView.Click += new System.EventHandler(this.TableNameGridView_Click);
            // 
            // NameGridColumn
            // 
            this.NameGridColumn.Caption = "Name";
            this.NameGridColumn.FieldName = "Name";
            this.NameGridColumn.MinWidth = 24;
            this.NameGridColumn.Name = "NameGridColumn";
            this.NameGridColumn.OptionsColumn.AllowEdit = false;
            this.NameGridColumn.Visible = true;
            this.NameGridColumn.VisibleIndex = 1;
            this.NameGridColumn.Width = 94;
            // 
            // TableSchemaDockPanel
            // 
            this.TableSchemaDockPanel.Controls.Add(this.dockPanel1_Container);
            this.TableSchemaDockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.TableSchemaDockPanel.FloatVertical = true;
            this.TableSchemaDockPanel.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.TableSchemaDockPanel.ID = new System.Guid("f4257c41-fa7e-4826-bb2f-94e7b4e536ea");
            this.TableSchemaDockPanel.Location = new System.Drawing.Point(529, 0);
            this.TableSchemaDockPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TableSchemaDockPanel.Name = "TableSchemaDockPanel";
            this.TableSchemaDockPanel.Options.AllowFloating = false;
            this.TableSchemaDockPanel.Options.FloatOnDblClick = false;
            this.TableSchemaDockPanel.Options.ShowAutoHideButton = false;
            this.TableSchemaDockPanel.Options.ShowCloseButton = false;
            this.TableSchemaDockPanel.Options.ShowMaximizeButton = false;
            this.TableSchemaDockPanel.OriginalSize = new System.Drawing.Size(624, 200);
            this.TableSchemaDockPanel.Size = new System.Drawing.Size(624, 751);
            this.TableSchemaDockPanel.Text = "[Table] Schema";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.layoutControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 32);
            this.dockPanel1_Container.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(616, 713);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panel1);
            this.layoutControl1.Controls.Add(this.TableSchemaGridControl);
            this.layoutControl1.Controls.Add(this.StatusLabelControl);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2545, 694, 1199, 758);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(616, 713);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // TableSchemaGridControl
            // 
            this.TableSchemaGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TableSchemaGridControl.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.TableSchemaGridControl.Location = new System.Drawing.Point(12, 12);
            this.TableSchemaGridControl.MainView = this.TableSchemaGridView;
            this.TableSchemaGridControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TableSchemaGridControl.Name = "TableSchemaGridControl";
            this.TableSchemaGridControl.Size = new System.Drawing.Size(592, 644);
            this.TableSchemaGridControl.TabIndex = 3;
            this.TableSchemaGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.TableSchemaGridView});
            this.TableSchemaGridControl.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.TableSchemaGridControl_ViewRegistered);
            // 
            // TableSchemaGridView
            // 
            this.TableSchemaGridView.AutoFillColumn = this.NameGridColumn1;
            this.BehaviorManager.SetBehaviors(this.TableSchemaGridView, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.Behaviors.Common.DisabledCellBehavior.Create(typeof(DevExpress.XtraGrid.Extensions.GridViewDisabledCellSource), "", appearanceObject1, this.TableSchemaGridViewDisabledCellEvents)))});
            this.TableSchemaGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.OrdinalGridColumn,
            this.NameGridColumn1,
            this.DataTypeGridColumn,
            this.IsKeyGridColumn,
            this.MaskDataTypeGridColumn});
            this.TableSchemaGridView.GridControl = this.TableSchemaGridControl;
            this.TableSchemaGridView.Name = "TableSchemaGridView";
            this.TableSchemaGridView.OptionsDetail.ShowDetailTabs = false;
            this.TableSchemaGridView.OptionsFind.AlwaysVisible = true;
            this.TableSchemaGridView.OptionsFind.FindNullPrompt = "Select Columns...";
            this.TableSchemaGridView.OptionsSelection.CheckBoxSelectorField = "IsSelected";
            this.TableSchemaGridView.OptionsSelection.MultiSelect = true;
            this.TableSchemaGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.TableSchemaGridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.TableSchemaGridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            this.TableSchemaGridView.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.True;
            this.TableSchemaGridView.OptionsView.ShowGroupPanel = false;
            this.TableSchemaGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.OrdinalGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.TableSchemaGridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.TableSchemaGridView_RowStyle);
            this.TableSchemaGridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.TableSchemaGridView_PopupMenuShowing);
            this.TableSchemaGridView.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.TableSchemaGridView_SelectionChanged);
            // 
            // NameGridColumn1
            // 
            this.NameGridColumn1.Caption = "Name";
            this.NameGridColumn1.FieldName = "Name";
            this.NameGridColumn1.MinWidth = 24;
            this.NameGridColumn1.Name = "NameGridColumn1";
            this.NameGridColumn1.OptionsColumn.AllowEdit = false;
            this.NameGridColumn1.Visible = true;
            this.NameGridColumn1.VisibleIndex = 1;
            this.NameGridColumn1.Width = 140;
            // 
            // OrdinalGridColumn
            // 
            this.OrdinalGridColumn.Caption = "Ordinal";
            this.OrdinalGridColumn.FieldName = "Ordinal";
            this.OrdinalGridColumn.MinWidth = 24;
            this.OrdinalGridColumn.Name = "OrdinalGridColumn";
            this.OrdinalGridColumn.Width = 94;
            // 
            // DataTypeGridColumn
            // 
            this.DataTypeGridColumn.Caption = "Data Type";
            this.DataTypeGridColumn.FieldName = "DataType";
            this.DataTypeGridColumn.MaxWidth = 140;
            this.DataTypeGridColumn.MinWidth = 24;
            this.DataTypeGridColumn.Name = "DataTypeGridColumn";
            this.DataTypeGridColumn.OptionsColumn.AllowEdit = false;
            this.DataTypeGridColumn.Visible = true;
            this.DataTypeGridColumn.VisibleIndex = 2;
            this.DataTypeGridColumn.Width = 140;
            // 
            // IsKeyGridColumn
            // 
            this.IsKeyGridColumn.AppearanceCell.Options.UseTextOptions = true;
            this.IsKeyGridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IsKeyGridColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.IsKeyGridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IsKeyGridColumn.Caption = "Key";
            this.IsKeyGridColumn.FieldName = "IsKey";
            this.IsKeyGridColumn.MaxWidth = 80;
            this.IsKeyGridColumn.MinWidth = 24;
            this.IsKeyGridColumn.Name = "IsKeyGridColumn";
            this.IsKeyGridColumn.OptionsColumn.AllowEdit = false;
            this.IsKeyGridColumn.Visible = true;
            this.IsKeyGridColumn.VisibleIndex = 3;
            this.IsKeyGridColumn.Width = 59;
            // 
            // MaskDataTypeGridColumn
            // 
            this.MaskDataTypeGridColumn.Caption = "Mask Data Type";
            this.MaskDataTypeGridColumn.FieldName = "MaskDataType";
            this.MaskDataTypeGridColumn.MaxWidth = 140;
            this.MaskDataTypeGridColumn.MinWidth = 24;
            this.MaskDataTypeGridColumn.Name = "MaskDataTypeGridColumn";
            this.MaskDataTypeGridColumn.OptionsColumn.AllowEdit = false;
            this.MaskDataTypeGridColumn.Visible = true;
            this.MaskDataTypeGridColumn.VisibleIndex = 4;
            this.MaskDataTypeGridColumn.Width = 94;
            // 
            // StatusLabelControl
            // 
            this.StatusLabelControl.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabelControl.Appearance.Options.UseFont = true;
            this.StatusLabelControl.Location = new System.Drawing.Point(591, 660);
            this.StatusLabelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.StatusLabelControl.Name = "StatusLabelControl";
            this.StatusLabelControl.Padding = new System.Windows.Forms.Padding(10, 11, 12, 11);
            this.StatusLabelControl.Size = new System.Drawing.Size(13, 41);
            this.StatusLabelControl.StyleController = this.layoutControl1;
            this.StatusLabelControl.TabIndex = 19;
            this.StatusLabelControl.Text = "Select your table schemas to continue.";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(616, 713);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.StatusLabelControl;
            this.layoutControlItem2.Location = new System.Drawing.Point(579, 648);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(17, 45);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.TableSchemaGridControl;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(596, 648);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // TableSchemaGridViewDisabledCellEvents
            // 
            this.TableSchemaGridViewDisabledCellEvents.ProcessingCell += new System.EventHandler<DevExpress.Utils.Behaviors.Common.ProcessCellEventArgs>(this.TableSchemaGridViewDisabledCellEvents_ProcessingCell);
            // 
            // TableSchemaPopupMenu
            // 
            this.TableSchemaPopupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.UseMaskBarCheckItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.UseColumnBarCheckItem, true)});
            this.TableSchemaPopupMenu.Manager = this.BarManager;
            this.TableSchemaPopupMenu.Name = "TableSchemaPopupMenu";
            // 
            // UseMaskBarCheckItem
            // 
            this.UseMaskBarCheckItem.Caption = "Use Mask";
            this.UseMaskBarCheckItem.Id = 8;
            this.UseMaskBarCheckItem.Name = "UseMaskBarCheckItem";
            this.UseMaskBarCheckItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.UseMaskBarCheckItem_ItemClicked);
            // 
            // UseColumnBarCheckItem
            // 
            this.UseColumnBarCheckItem.Caption = "Use Date Comparison Field";
            this.UseColumnBarCheckItem.Id = 9;
            this.UseColumnBarCheckItem.Name = "UseColumnBarCheckItem";
            this.UseColumnBarCheckItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.UseColumnBarCheckItem_ItemClicked);
            // 
            // BarManager
            // 
            this.BarManager.DockControls.Add(this.barDockControlTop);
            this.BarManager.DockControls.Add(this.barDockControlBottom);
            this.BarManager.DockControls.Add(this.barDockControlLeft);
            this.BarManager.DockControls.Add(this.barDockControlRight);
            this.BarManager.Form = this;
            this.BarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.UseMaskBarCheckItem,
            this.UseColumnBarCheckItem,
            this.DetailsBarButtonItem});
            this.BarManager.MaxItemId = 11;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(47, 0);
            this.barDockControlTop.Manager = this.BarManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1106, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(47, 751);
            this.barDockControlBottom.Manager = this.BarManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1106, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(47, 0);
            this.barDockControlLeft.Manager = this.BarManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 751);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1153, 0);
            this.barDockControlRight.Manager = this.BarManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 751);
            // 
            // DetailsBarButtonItem
            // 
            this.DetailsBarButtonItem.Caption = "Details";
            this.DetailsBarButtonItem.Id = 10;
            this.DetailsBarButtonItem.Name = "DetailsBarButtonItem";
            this.DetailsBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DetailsBarButtonItem_ItemClick);
            // 
            // TableNamePopupMenu
            // 
            this.TableNamePopupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.DetailsBarButtonItem)});
            this.TableNamePopupMenu.Manager = this.BarManager;
            this.TableNamePopupMenu.Name = "TableNamePopupMenu";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 660);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 41);
            this.panel1.TabIndex = 20;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(127, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(122, 12);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Use Date Comparision Field";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(165)))), ((int)(((byte)(118)))));
            this.pictureBox2.Location = new System.Drawing.Point(106, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(15, 15);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(38, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 12);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Use Mask";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(184)))), ((int)(((byte)(214)))));
            this.pictureBox1.Location = new System.Drawing.Point(17, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(15, 15);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem1.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem1.Control = this.panel1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 648);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(579, 45);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // SchemaBrowserPageUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableSchemaDockPanel);
            this.Controls.Add(this.TableNameDockPanel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SchemaBrowserPageUserControl";
            this.Padding = new System.Windows.Forms.Padding(47, 0, 47, 49);
            this.Size = new System.Drawing.Size(1200, 800);
            ((System.ComponentModel.ISupportInitialize)(this.DockManager)).EndInit();
            this.TableNameDockPanel.ResumeLayout(false);
            this.dockPanel_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableNameGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableNameGridView)).EndInit();
            this.TableSchemaDockPanel.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableSchemaGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableSchemaGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BehaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableSchemaPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableNamePopupMenu)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Docking.DockManager DockManager;
        private DevExpress.XtraBars.Docking.DockPanel TableNameDockPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel_Container;
        private DevExpress.XtraBars.Docking.DockPanel TableSchemaDockPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.GridControl TableSchemaGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView TableSchemaGridView;
        private DevExpress.XtraGrid.Columns.GridColumn NameGridColumn1;
        private DevExpress.XtraGrid.GridControl TableNameGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView TableNameGridView;
        private DevExpress.XtraGrid.Columns.GridColumn NameGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn DataTypeGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn IsKeyGridColumn;
        private DevExpress.Utils.Behaviors.BehaviorManager BehaviorManager;
        private DevExpress.Utils.Behaviors.Common.DisabledCellEvents TableSchemaGridViewDisabledCellEvents;
        private DevExpress.XtraGrid.Columns.GridColumn OrdinalGridColumn;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LabelControl StatusLabelControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager BarManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu TableSchemaPopupMenu;
        private DevExpress.XtraGrid.Columns.GridColumn MaskDataTypeGridColumn;
        private DevExpress.XtraBars.PopupMenu TableNamePopupMenu;
        private DevExpress.XtraBars.BarCheckItem UseMaskBarCheckItem;
        private DevExpress.XtraBars.BarCheckItem UseColumnBarCheckItem;
        private DevExpress.XtraBars.BarButtonItem DetailsBarButtonItem;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}