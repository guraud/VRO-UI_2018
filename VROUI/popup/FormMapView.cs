using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace VROUI.popup
{
    [ComVisible(true)]
    public partial class FormMapView : Form
    {
        Devart.Data.Oracle.OracleDataTable odt;
        //DataTable dt;
        double[,] vectors;
        //private Devart.Data.Oracle.OracleDataTable dt1;
        String searchOption = "0";

        public FormMapView(Devart.Data.Oracle.OracleDataTable dt, double[,] vectors)
        {
            InitializeComponent();

            setDataTable(dt);
            fillGrid();
            setVectors(vectors);

            string curDir = Directory.GetCurrentDirectory();

            this.webMap.Url = new Uri(String.Format("file:///{0}/Source/Main.html?width=" + (pnTop.Width)
                + "&height="+ (pnTop.Height), curDir));

            this.webMap.ObjectForScripting = this;
            
            //Visual settings to integrate the WebBrowser in our Winform.
            this.webMap.AllowWebBrowserDrop = false;
            this.webMap.WebBrowserShortcutsEnabled = false;

            this.webMap.ScriptErrorsSuppressed = true;

            //this.txtStartX1.Text = "14129105.461214";
            //this.txtStartX2.Text = "14136027.789587";
            //this.txtStartX3.Text = "14138427.689587";
            //this.txtStartX4.Text = "14132011.389587";

            //this.txtStartY1.Text = "4517042.1926406";
            //this.txtStartY2.Text = "4517572.4745242";
            //this.txtStartY3.Text = "4519965.4743242";
            //this.txtStartY4.Text = "4514375.6926406";

            //drawVectors();
        }

        public void ActivateControls()
        {
            //this.txtStartX1.Enabled = true;
            //this.txtStartX2.Enabled = true;
            //this.txtStartY2.Enabled = true;
            //this.txtStartX3.Enabled = true;
            //this.txtStartY3.Enabled = true;
            //this.sendToWebPageButton.Enabled = true;

            //this.webMap.Url = new Uri(Path.Combine(this.path, "SubsribeToNewsLetter.html"));
        }

        private void btAlertTest_Click(object sender, EventArgs e)
        {
            this.webMap.Document.InvokeScript("alert", new object[] { "Hello World" });
        }

        private void btSetParam_Click(object sender, EventArgs e)
        {
            //object[] args = new object[4];
            //args[0] = txtStartX1.Text;
            //args[1] = txtStartY1.Text;
            //args[2] = txtStartX2.Text;
            //args[3] = txtStartY2.Text;

            //this.webMap.Document.InvokeScript("searchRoute", args);

            //args[0] = txtStartX2.Text;
            //args[1] = txtStartY2.Text;
            //args[2] = txtStartX3.Text;
            //args[3] = txtStartY3.Text;
            //this.webMap.Document.InvokeScript("searchRoute", args);

            //args[0] = txtStartX3.Text;
            //args[1] = txtStartY3.Text;
            //args[2] = txtStartX4.Text;
            //args[3] = txtStartY4.Text;
            //this.webMap.Document.InvokeScript("searchRoute", args);

            //this.webMap.Document.InvokeScript("fillFields", args);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        internal void setDataTable(Devart.Data.Oracle.OracleDataTable dataTable)
        {
            odt = dataTable;
        }

        internal void fillGrid()
        {
            this.gridDispathingInfo.DataSource = odt;
        }

        internal void setVectors(double[,] paramVectors)
        {
            vectors = paramVectors;
        }

        public void drawVectors()
        {
            // 경로를 삭제하고,
            clearRoutes();

            if (vectors.Length <= 0)
                return;

            object[] args = new object[5];
            String startX = Convert.ToString(vectors[0, 0]);
            String startY = Convert.ToString(vectors[0, 1]);
            String endX, endY;

            args[0] = startX;
            args[1] = startY;

            for (int i=1; i<vectors.GetLength(0); i++)
            {
                endX = Convert.ToString(vectors[i, 0]);
                endY = Convert.ToString(vectors[i, 1]);

                args[2] = endX;
                args[3] = endY;
                args[4] = searchOption;

                //this.webMap.Document.InvokeScript("alert", new object[] { args });
                this.webMap.Document.InvokeScript("searchRoute", args);

                args[0] = endX;
                args[1] = endY;
            }

        }

        public void clearRoutes()
        {
            this.webMap.Document.InvokeScript("clearRoute");
        }

        private void FormMapView_SizeChanged(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();

            this.webMap.Url = new Uri(String.Format("file:///{0}/Source/Main.html?width=" + (pnTop.Width)
                + "&height=" + (pnTop.Height), curDir));

            drawVectors();
        }

        private void btnOptRoute_Click(object sender, EventArgs e)
        {
            searchOption = "0";
            drawVectors();
        }

        private void btnNopay_Click(object sender, EventArgs e)
        {
            searchOption = "1";
            drawVectors();
        }

        private void btnMintime_Click(object sender, EventArgs e)
        {
            searchOption = "2";
            drawVectors();
        }

        private void btnHighway_Click(object sender, EventArgs e)
        {
            searchOption = "4";
            drawVectors();
        }

        private void btnBeginner_Click(object sender, EventArgs e)
        {
            searchOption = "3";
            drawVectors();
        }

        private void btnMinlen_Click(object sender, EventArgs e)
        {
            searchOption = "10";
            drawVectors();
        }

        private void btnNormalway_Click(object sender, EventArgs e)
        {
            searchOption = "12";
            drawVectors();
        }
    }
}
