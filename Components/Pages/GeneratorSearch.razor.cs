using Microsoft.AspNetCore.Components.Web;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.Grid;
using Telerik.SvgIcons;
using TelerikUI.Models;
using static TelerikUI.Components.Pages.GridPractice;

namespace TelerikUI.Components.Pages
{
    public partial class GeneratorSearch
    {
        List<ModtblGenerator> lstGenerators;
        string strFirstChars;
        string ExcelFileName;
        bool ExportAllPages = true;
        bool blnFetching = false;
        bool blnError = false;

        TelerikTextBox TxtFirstChars { get; set; }
        TelerikGrid<ModtblGenerator> GridRef { get; set; }

        private async Task FetchGenerators()
        {
            blnFetching = true;
            blnError = false;
            lstGenerators = null;

            try
            {
                lstGenerators = await serOHRDatabase.GetMultipleGeneratorAsync(strFirstChars);
                //await GridRef.AutoFitAllColumnsAsync();
                StateHasChanged();
            }
            catch (Exception)
            {
                blnError = true;
            }
            finally
            {
                blnFetching = false;
            }
        }
        private async Task AutoFitAllColumns()
        {
            await GridRef.AutoFitAllColumnsAsync();
        }

        void SetUpperOnChange()
        {
            if (strFirstChars != null)
            {
                strFirstChars = strFirstChars.ToUpper();
                StateHasChanged();
            }
        }
        void SetUpperValueChanged(string strUserInput)
        {
            if (strUserInput != null)
            {
                strFirstChars = strUserInput.ToUpper();
                StateHasChanged();
            }
        }

        async Task DeleteFirstChars()
        {
            strFirstChars = string.Empty;
            lstGenerators = null;
            await TxtFirstChars.FocusAsync();
        }

        private async Task EnterTabFetchFirstChars(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" || e.Key == "Tab")
            {
                await FetchGenerators();
            }
        }

        void FileNameExcel()
        {
            ExcelFileName = $"GeneratorSearch_{strFirstChars}_{DateTime.Now.ToString("MMMM_dd_yyyy")}";
        }
        /********************************* Configurations Methods ********************************/

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(300);
                await TxtFirstChars.FocusAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}