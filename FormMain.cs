using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VROUI.Forms;
using System.Diagnostics;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraSplashScreen;
using System.Reflection;
using Spring.Context;
using VROUI.Services;
using Devart.Data.Oracle;
using Spring.Context.Support;
using VROUI.objectPool;



namespace VROUI
{
    public partial class FormMain : XtraForm
    {
        private FormParameterSetup _formParameterSetup = null;
        private FormMakeDispatching _formMakeDispatching = null;
        private FormConfirmDispatching _formConfirmDispatching = null;
        private FormFinalizeDispatching _formFinalizeDispatching = null;
        private FormCarDispatching _formDispatchingCar = null;

        public FormMain()
        {
            
            Bitmap SplashScreenImage = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("VROUI.Resources.splash.png"));

            //SplashScreenManager.ShowImage(SplashScreenImage, true, false);

            InitializeComponent();
        }

               
        private void FormMain_Load(object sender, EventArgs e)
        {

            SkinHelper.InitSkinPopupMenu(menuPaintStyle);
            BeginInvoke(new MethodInvoker(InitializeForm));

            #region 창 크기 초기화
            this.Width = 1500; this.Height = 950;

            #endregion
        }

        private void InitializeForm()
        {
            //초기 화면 Loading
            int whichScreen = 1;

            switch (whichScreen) 
            {
                case 1:
                    {
                        FormFlag.formParameterSetup_flag = true;

                        if (tabbedMdiManager.Pages[_formParameterSetup] == null)
                        {
                            _formParameterSetup = new FormParameterSetup();
                            _formParameterSetup.MdiParent = this;
                            _formParameterSetup.Show();
                        }
                        break;
                    }
                case 2:
                    {
                        FormFlag.formMakeDispatching_flag = true;

                        if (tabbedMdiManager.Pages[_formMakeDispatching] == null)
                        {
                            _formMakeDispatching = new FormMakeDispatching();
                            _formMakeDispatching.MdiParent = this;
                            _formMakeDispatching.Show();
                        }
                        break;
                    }
                case 3:
                    {
                        FormFlag.formConfirmDispatching_flag = true;

                        if (tabbedMdiManager.Pages[_formConfirmDispatching] == null)
                        {
                            _formConfirmDispatching = new FormConfirmDispatching();
                            _formConfirmDispatching.MdiParent = this;
                            _formConfirmDispatching.Show();
                        }
                        break;
                    }
            }

            SplashScreenManager.HideImage();
        }

        private void menuExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void menuParameterSetup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormFlag.formParameterSetup_flag = true;

            if (tabbedMdiManager.Pages[_formParameterSetup] == null)
            {
                _formParameterSetup = new FormParameterSetup();
                _formParameterSetup.MdiParent = this;
                _formParameterSetup.Show();
            }
            else
            {
                _formParameterSetup.BringToFront();
            }
        }

        private void menuMakeDispatching_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormFlag.formMakeDispatching_flag = true;

            if (tabbedMdiManager.Pages[_formMakeDispatching] == null)
            {
                _formMakeDispatching = new FormMakeDispatching();
                _formMakeDispatching.MdiParent = this;
                _formMakeDispatching.Show();
            }
            else
            {
                _formMakeDispatching.BringToFront();
            }
        }

        private void menuConfirmDispatching_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormFlag.formConfirmDispatching_flag = true;

            if (tabbedMdiManager.Pages[_formConfirmDispatching] == null)
            {
                _formConfirmDispatching = new FormConfirmDispatching();
                _formConfirmDispatching.MdiParent = this;
                _formConfirmDispatching.Show();
            }
            else
            {
                _formConfirmDispatching.BringToFront();
            }
        }

        private void menuFinalizeDispatching_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormFlag.formFinalizeDispatching_flag = true;

            if (tabbedMdiManager.Pages[_formFinalizeDispatching] == null)
            {
                _formFinalizeDispatching = new FormFinalizeDispatching();
                _formFinalizeDispatching.MdiParent = this;
                _formFinalizeDispatching.Show();
            }
            else
            {
                _formFinalizeDispatching.BringToFront();
            }
        }

        private void menuDispatchingCar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormFlag.formCarDispatching_flag = true;

            if (tabbedMdiManager.Pages[_formDispatchingCar] == null)
            {
                _formDispatchingCar = new FormCarDispatching();
                _formDispatchingCar.MdiParent = this;
                _formDispatchingCar.Show();
            }
            else
            {
                _formDispatchingCar.BringToFront();
            }
        }

        private void tabbedMdiManager_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            Debug.Print(tabbedMdiManager.Pages.Count.ToString());
        }

        private void buttonTest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IApplicationContext _applicationContext = ContextRegistry.GetContext();
            BizService bizService = (BizService)_applicationContext.GetObject("BizService");

            OracleDataTable dt = bizService.GetInBound();

            // gridControl1.DataSource = dt;  // 그리드에연결 
            // gridControl1.RefreshDataSource() ; // 화면 Refresh
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            Debug.WriteLine(string.Format("\tHeight: {0}, Width: {1}", this.Size.Height, this.Size.Width));
        }




    }
}