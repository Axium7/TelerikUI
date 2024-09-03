using Microsoft.AspNetCore.Components.Web;
using Telerik.Blazor.Components;
using Telerik.SvgIcons;
using TelerikUI.Models;

namespace TelerikUI.Components.Pages
{
    public partial class GeneratorSearch
    {
        private string strFirstChars;
        private List<ModtblGenerator> lstGenerators;
        private bool blnFetching = false;
        private bool blnError = false;
        private TelerikTextBox TxtFirstChars { get; set; }

        private async Task FetchGenerators()
        {
            blnFetching = true;
            blnError = false;
            lstGenerators = null;

            try
            {
                lstGenerators = await serOHRDb.GetMultipleGeneratorAsync(strFirstChars);
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