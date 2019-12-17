using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;
using static CitizenFX.Core.Native.Function;

namespace RedMenuClient
{
    public class UiPromptsCollection : BaseScript
    {
        public static List<UiPrompt> prompts = new List<UiPrompt>();

        public UiPromptsCollection() { }

        [EventHandler("onResourceStop")]
        public void OnResourceStop(string name)
        {
            if (name == GetCurrentResourceName())
            {
                var allPrompts = prompts.ToList();
                foreach (var d in allPrompts)
                {
                    d.Dispose();
                }
            }
        }
    }

    public class UiPrompt
    {
        //private long text;
        private string textString;
        private Control[] controls;
        private int promptHandle;
        private string optionalThing;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="control"></param>
        /// <param name="text"></param>
        public UiPrompt(Control[] controls, string text, string optionalThing = null)
        {
            this.controls = controls;
            this.textString = text;
            this.promptHandle = 0;
            this.optionalThing = optionalThing;

            if (!UiPromptsCollection.prompts.Contains(this))
            {
                UiPromptsCollection.prompts.Add(this);
            }
        }

        /// <summary>
        /// Prepare the instructional button before it is displayed.
        /// </summary>
        public void Prepare()
        {
            if (this.IsPrepared())
            {
                return;
                //this.Dispose();
            }

            this.promptHandle = Call<int>((Hash)0x04F97DE45A519419); // UipromptRegisterBegin
            Call((Hash)0x5DD02A8318420DD7, this.promptHandle, Call<long>((Hash)0xFA925AC00EB830B9, 10, "LITERAL_STRING", textString)); // UipromptSetText
            if (!string.IsNullOrEmpty(this.optionalThing))
            {
                Call((Hash)0xDEC85C174751292B, this.promptHandle, optionalThing); // _UIPROMPT_SET_TAG
            }
            foreach (var c in this.controls)
            {
                Call((Hash)0xB5352B7494A08258, this.promptHandle, c); // UipromptSetControlAction
            }
            Call((Hash)0xCC6656799977741B, this.promptHandle, true); // UipromptSetStandardMode
            Call((Hash)0xF7AA2696A22AD8B9, this.promptHandle); // UipromptRegisterEnd
            //this.SetEnabled(false, false);

            if (!UiPromptsCollection.prompts.Contains(this))
            {
                UiPromptsCollection.prompts.Add(this);
            }
        }

        /// <summary>
        /// Check if it is ready to be displayed.
        /// </summary>
        /// <returns></returns>
        public bool IsPrepared()
        {
            if (Call<bool>((Hash)0x347469FBDD1589A9, this.promptHandle)) // UipromptIsValid
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Enables or disables the prompt on screen. You must prepare the prompt first using <see cref="UiPrompt.Prepare"/>.
        /// </summary>
        /// <param name="visible"></param>
        /// <param name="enabled"></param>
        public void SetEnabled(bool visible, bool enabled)
        {
            Call((Hash)0x71215ACCFDE075EE, this.promptHandle, visible); // UipromptSetVisible
            Call((Hash)0x8A0FB4D03A630D21, this.promptHandle, enabled); // UipromptSetEnabled
        }

        /// <summary>
        /// Disposes the prompt. Requires you to call <see cref="UiPrompt.Prepare"/> again before you can use it again.
        /// </summary>
        public void Dispose()
        {
            if (this.IsPrepared())
            {
                this.SetEnabled(false, false);
                Call((Hash)0x00EDE88D4D13CF59, this.promptHandle); // UipromptDelete
            }

            this.promptHandle = 0;

            if (UiPromptsCollection.prompts.Contains(this))
            {
                UiPromptsCollection.prompts.Remove(this);
            }
        }

        public string GetTextString() => textString;
        public Control[] GetControls() => controls;
    }
}
