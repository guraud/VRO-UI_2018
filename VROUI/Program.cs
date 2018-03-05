using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using VROUI.objectPool;
using DevExpress.XtraSplashScreen;
using DevExpress.UserSkins;
using DevExpress.Skins;

namespace VROUI
{
    static class Program
    {
        /// <summary>
        /// VRO UI 시작지점
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationKey.company = args[0];
            ApplicationKey.center = args[1];
            ApplicationKey.mst_id = args[2];
            ApplicationKey.plan_id = args[3];
            ApplicationKey.plan_st = args[4];
            ApplicationKey.userId = args[5];

            SplashScreenManager.ShowForm(typeof(FormSplash), true, false);

            ApplicationKey.plan_id2 = new string[3];
            for (int a = 0; a < 3; a++)
                ApplicationKey.plan_id2[a] = ApplicationKey.plan_id.Substring(0, 8)+"_0001"  + (a+1);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");

            Application.Run(new FormRibbon());
        }
    }
}