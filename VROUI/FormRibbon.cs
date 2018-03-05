using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Helpers;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon;
using VROUI.Forms;
using VROUI.objectPool;
using VROUI.Services;
using DevExpress.XtraSplashScreen;
using Spring.Context;
using Spring.Context.Support;
//using Google.Maps.Geocoding;
//using Google.Maps.StaticMaps;
//using Google.Maps;
using VROUI.popup;

namespace VROUI
{
    public partial class FormRibbon : RibbonForm
    {
        private FormParameterSetup _formParameterSetup = null;
        private FormMakeDispatching _formMakeDispatching = null;
        private FormConfirmDispatching _formConfirmDispatching = null;

        public FormRibbon()
        {
            InitializeComponent();

            SkinHelper.InitSkinGallery(skinGallery, true);

            barOrder.Caption += ApplicationKey.plan_id.Substring(0, 4) + "월 " + ApplicationKey.plan_id.Substring(4, 2) + "월 " +
                ApplicationKey.plan_id.Substring(6, 2) + "일 신규 " + ApplicationKey.plan_st + "차수";

            BizService bizService = (BizService)ContextRegistry.GetContext().GetObject("BizService");
            barDB.Caption += bizService._dbService.Sid.ToString();

            //초기 화면 Loading
            int whichScreen = 0;
            InitializeForm(whichScreen);  //처음에는 파라메터 설정화면을 띄운다.

            SplashScreenManager.CloseForm();
        }

        //진행단계 Display (0:파라메터설정, 1:실행관리, 2: 가확정)
        private void InitializeForm(int whichScreen)
        {
            switch (whichScreen)
            {
                case 0:
                    {
                        FormFlag.formParameterSetup_flag = true;

                        if (tabbedMdiManager.Pages[_formParameterSetup] == null)
                        {
                            barResult.Caption = "배차결과: 파라메터 설정";
                            _formParameterSetup = new FormParameterSetup();
                            _formParameterSetup.MdiParent = this;
                            _formParameterSetup.Show();
                        }
                        break;
                    }
                case 1:
                    {
                        FormFlag.formMakeDispatching_flag = true;

                        if (tabbedMdiManager.Pages[_formMakeDispatching] == null)
                        {
                            barResult.Caption = "배차결과: 배차계획 실행관리";
                            _formMakeDispatching = new FormMakeDispatching();
                            _formMakeDispatching.MdiParent = this;
                            _formMakeDispatching.Show();
                        }
                        break;
                    }
                case 2:
                    {
                        FormFlag.formConfirmDispatching_flag = true;

                        if (tabbedMdiManager.Pages[_formConfirmDispatching] == null)
                        {
                            barResult.Caption = "배차결과: 배차지시 가확정";
                            _formConfirmDispatching = new FormConfirmDispatching();
                            _formConfirmDispatching.MdiParent = this;
                            _formConfirmDispatching.Show();
                        }
                        break;
                    }
            }
        }

        private void buttonParameterSetup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormFlag.formParameterSetup_flag = true;

            barResult.Caption = "배차결과: 파라메터 설정";
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

        private void buttonMakeDispatching_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormFlag.formMakeDispatching_flag = true;

            barResult.Caption = "배차결과: 배차계획 실행관리";
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

        private void buttonCarDispatching_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormFlag.formCarDispatching_flag = true;

            //if (tabbedMdiManager.Pages[_formDispatchingCar] == null)
            //{
            //    _formDispatchingCar = new FormCarDispatching();
            //    _formDispatchingCar.MdiParent = this;
            //    _formDispatchingCar.Show();
            //}
            //else
            //{
            //    _formDispatchingCar.BringToFront();
            //}
        }

        private void buttonItemConfirmDispatching_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormFlag.formConfirmDispatching_flag = true;

            //if (string.IsNullOrEmpty(ApplicationKey.current_plan_id))
            //{
            //    MessageBox.Show("배차 결과를 선택한 다음 가확정을 진행할 수 있습니다.", "배차 결과 미선택", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            barResult.Caption = "배차결과: 배차지시 가확정";
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

        private void buttonFinalizeDispatching_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    FormFlag.formFinalizeDispatching_flag = true;

        //    if (tabbedMdiManager.Pages[_formFinalizeDispatching] == null)
        //    {
        //        _formFinalizeDispatching = new FormFinalizeDispatching();
        //        _formFinalizeDispatching.MdiParent = this;
        //        _formFinalizeDispatching.Show();
        //    }
        //    else
        //    {
        //        _formFinalizeDispatching.BringToFront();
        //    }
        }
    }
}