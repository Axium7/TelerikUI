using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components;
using Telerik.DataSource;
using TelerikUI.Models;
using Telerik.DataSource.Extensions;

namespace TelerikUI.Components.Shared
{
    public partial class ComGeneratorsEllipses
    {

        [Parameter]
        public List<ModtblGenerator> Generators { get; set; }

        [Parameter]
        public string FirstChars { get; set; }

        private ModtblGenerator currentGenerator = new ModtblGenerator();

        private TelerikGrid<ModtblGenerator> GridRef { get; set; }

        private DataSourceResult GridData;

        private bool blnShowWindowWasteClasses = false;

        private string strExcelFileName = "excelfile";

        //**************************** Methods *************************************

        private void OnRowDoubleClickHandler(GridRowClickEventArgs args)
        {
            currentGenerator = args.Item as ModtblGenerator;

            blnShowWindowWasteClasses = !blnShowWindowWasteClasses;
        }

        string CreateExcelFileName()
        {
            return $"GeneratorSearch_{FirstChars}_{DateTime.Now.ToString("MMMM_dd_yyyy")}";
        }

        private async Task AutoFitAllColumns()
        {
            if (GridRef != null)
            {
                await GridRef.AutoFitAllColumnsAsync();
            }
        }

        private async Task ClearGridFilter()
        {
            GridState<ModtblGenerator> desiredState = new GridState<ModtblGenerator>()
            {
                //clears the filter list in the new Grid state
                FilterDescriptors = new List<IFilterDescriptor>()

            };

            await GridRef.SetStateAsync(desiredState);
        }
        
        // **************************** Grid Events ****************************

        // apply ellipsis to all columns
        private void OnRowRender(GridRowRenderEventArgs args)
        {
            args.Class = "custom-ellipsis";
        }

        //Default Sort Order
        async Task OnStateInitHandler(GridStateEventArgs<ModtblGenerator> args)
        {
            var state = new GridState<ModtblGenerator>
            {
                SortDescriptors = new List<SortDescriptor>
            {
                new SortDescriptor{ Member = "GenName", SortDirection = ListSortDirection.Ascending }
            }
            };

            args.GridState = state;
        }

        protected void OnGridRead(GridReadEventArgs args)
        {
            GridData = Generators.ToDataSourceResult(args.Request);
        }
    }
}