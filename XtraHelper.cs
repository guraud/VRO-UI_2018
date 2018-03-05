using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;

namespace VROUI
{
    public class XtraHelper
    {
        public static void InitializeGrid(GridControl grid)
        {
            Guard.ArgumentNotNull(grid.Views[0], grid.Name);

            GridView gridView = (GridView)grid.Views[0];

            //gridView.OptionsView.EnableAppearanceEvenRow = true;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gridView.OptionsBehavior.Editable = false;

            gridView.OptionsSelection.MultiSelect = true;
        }

        public static void InitializeEditableGrid(GridControl grid)
        {
            Guard.ArgumentNotNull(grid.Views[0], grid.Name);

            GridView gridView = (GridView)grid.Views[0];

            //gridView.OptionsView.EnableAppearanceEvenRow = true;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.True;
            gridView.OptionsBehavior.Editable = true;

            gridView.OptionsSelection.MultiSelect = true;
        }

    }
}
