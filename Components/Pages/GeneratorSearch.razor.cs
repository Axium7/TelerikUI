using Microsoft.AspNetCore.Components.Web;
using Telerik.Blazor.Components;
using TelerikUI.Models;

namespace TelerikUI.Components.Pages
{
    public partial class GeneratorSearch
    {
        List<ModtblGenerator> lstGenerators;
        string strFirstChars;
        bool blnFetching = false;
        bool blnError = false;

        TelerikTextBox TxtFirstChars { get; set; }

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
        

        void SetUpperOnChange()
        {
            if (strFirstChars != null)
            {
                strFirstChars = strFirstChars.ToUpper();
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