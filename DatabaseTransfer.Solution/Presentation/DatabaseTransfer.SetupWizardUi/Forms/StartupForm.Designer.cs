namespace DatabaseTransfer.SetupWizardUi.Forms
{
    partial class StartupForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupForm));
            this.DocumentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.MainWindowsUIView = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.WindowsUIView(this.components);
            this.pageGroup = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.PageGroup(this.components);
            this.closeFlyout = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.Flyout(this.components);
            this.ImageList = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DocumentManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainWindowsUIView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeFlyout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).BeginInit();
            this.SuspendLayout();
            // 
            // DocumentManager
            // 
            this.DocumentManager.ContainerControl = this;
            this.DocumentManager.View = this.MainWindowsUIView;
            this.DocumentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.MainWindowsUIView});
            // 
            // MainWindowsUIView
            // 
            this.MainWindowsUIView.AddTileWhenCreatingDocument = DevExpress.Utils.DefaultBoolean.False;
            this.MainWindowsUIView.AllowCaptionDragMove = DevExpress.Utils.DefaultBoolean.True;
            this.MainWindowsUIView.Appearance.Font = new System.Drawing.Font("Segoe UI", 20.75F);
            this.MainWindowsUIView.Appearance.Options.UseFont = true;
            this.MainWindowsUIView.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 16.75F);
            this.MainWindowsUIView.AppearanceCaption.Options.UseFont = true;
            this.MainWindowsUIView.Caption = "Database Transfer Wizard";
            this.MainWindowsUIView.ContentContainers.AddRange(new DevExpress.XtraBars.Docking2010.Views.WindowsUI.IContentContainer[] {
            this.pageGroup,
            this.closeFlyout});
            this.MainWindowsUIView.UseSplashScreen = DevExpress.Utils.DefaultBoolean.False;
            this.MainWindowsUIView.NavigationBarsShowing += new DevExpress.XtraBars.Docking2010.Views.WindowsUI.NavigationBarsCancelEventHandler(this.MainWindowsUIView_NavigationBarsShowing);
            this.MainWindowsUIView.QueryDocumentActions += new DevExpress.XtraBars.Docking2010.Views.WindowsUI.QueryDocumentActionsEventHandler(this.MainWindowsUIView_QueryDocumentActions);
            this.MainWindowsUIView.NavigatedTo += new DevExpress.XtraBars.Docking2010.Views.WindowsUI.NavigationEventHandler(this.MainWindowsUIView_NavigatedTo);
            // 
            // pageGroup
            // 
            this.pageGroup.ButtonInterval = 30;
            this.pageGroup.Name = "pageGroup";
            this.pageGroup.Properties.ShowPageHeaders = DevExpress.Utils.DefaultBoolean.False;
            // 
            // closeFlyout
            // 
            this.closeFlyout.FlyoutButtons = System.Windows.Forms.MessageBoxButtons.YesNo;
            this.closeFlyout.Name = "closeFlyout";
            // 
            // ImageList
            // 
            this.ImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.ImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.InsertGalleryImage("backward_32x32.png", "grayscaleimages/navigation/backward_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("grayscaleimages/navigation/backward_32x32.png"), 0);
            this.ImageList.Images.SetKeyName(0, "backward_32x32.png");
            this.ImageList.InsertGalleryImage("forward_32x32.png", "grayscaleimages/navigation/forward_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("grayscaleimages/navigation/forward_32x32.png"), 1);
            this.ImageList.Images.SetKeyName(1, "forward_32x32.png");
            this.ImageList.InsertGalleryImage("cancel_32x32.png", "grayscaleimages/actions/cancel_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("grayscaleimages/actions/cancel_32x32.png"), 2);
            this.ImageList.Images.SetKeyName(2, "cancel_32x32.png");
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 807);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("StartupForm.IconOptions.Image")));
            this.InactiveGlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(159)))), ((int)(((byte)(72)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "StartupForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wizard";
            ((System.ComponentModel.ISupportInitialize)(this.DocumentManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainWindowsUIView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeFlyout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.DocumentManager DocumentManager;
        private DevExpress.XtraBars.Docking2010.Views.WindowsUI.WindowsUIView MainWindowsUIView;
        private DevExpress.XtraBars.Docking2010.Views.WindowsUI.PageGroup pageGroup;
        private DevExpress.XtraBars.Docking2010.Views.WindowsUI.Flyout closeFlyout;
        private DevExpress.Utils.ImageCollection ImageList;
    }
}

