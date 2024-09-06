using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using TelerikUI.Models;

namespace TelerikUI.Components.Shared
{
    public partial class ComGeneratorsEllipses
    {

        [Parameter]
        public List<ModtblGenerator> Generators { get; set; }

        [Parameter]
        public string FirstChars { get; set; }

        TelerikGrid<ModtblGenerator> GridRef { get; set; }

        string strExcelFileName = "excelfile";

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

        private async Task OnGridRead(GridReadEventArgs args)
        {
            await Task.Delay(100); // simulate async operation
            DataSourceResult result = Generators.ToDataSourceResult(args.Request);

            args.Data = result.Data;
            args.Total = result.Total;
            args.AggregateResults = result.AggregateResults;

        }

        // *******************************  Grid Styling  *******************************

        // apply ellipsis to all columns
        private void OnRowRender(GridRowRenderEventArgs args)
        {
            args.Class = "custom-ellipsis";
        }

    }
}