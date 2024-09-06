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

        private ModtblGenerator currentGenerator = new ModtblGenerator();

        private TelerikGrid<ModtblGenerator> GridRef { get; set; }

        private DataSourceResult GridData;

        private bool blnShowWindowWasteClasses = false;

        private string strExcelFileName = "excelfile";

        //**************************** Custom Methods *************************************

        string CreateExcelFileName()
        {
            return $"GeneratorSearch_{FirstChars}_{DateTime.Now.ToString("MMMM_dd_yyyy")}";
        }

        private async Task ClearGridFilter()
        {
            GridState<ModtblGenerator> desiredState = new GridState<ModtblGenerator>()
            {
                //clears the filter list in the new Grid state
                FilterDescriptors = new List<IFilterDescriptor>(),

                //Sets the default sort order
                SortDescriptors = new List<SortDescriptor>
                {
                    new SortDescriptor{ Member = "GenName", SortDirection = ListSortDirection.Ascending }
                }

            };

            await GridRef.SetStateAsync(desiredState);
        }

        // **************************** Grid Events ****************************
        protected void ReadItems(GridReadEventArgs args)
        {
            GridData = Generators.ToDataSourceResult(args.Request);
            args.Data = GridData.Data;
            args.Total = GridData.Total;
        }

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

        private void ShowWindowWasteClasses(ModtblGenerator generator)
        {
            currentGenerator = generator;

            blnShowWindowWasteClasses = !blnShowWindowWasteClasses;
        }
        private void GoToPageGenSingle(ModtblGenerator generator)
        {
            //Navigate to the GenSingle Page
            NavigationManager.NavigateTo("/GenSingle",true);
        }
    }
}